#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

using Microsoft.Reporting.WebForms;
#endregion

namespace RT2008.Web.Reports.Forms
{
    public partial class VIPProfile_Inline : UserControl
    {
        private DataTable _DataSource;
        private String DATASOURCE_XSD_NAME = "DataSource_vwVIPProfile_Ex";
        private String REPORT_RDLC_NAME = "RT2008.Web.Reports.Rdlc.VIPProfileRdl_Ex.rdlc";
        private String REPORT_FILENAME = "VIP Profile.PDF";

        public VIPProfile_Inline()
        {
            InitializeComponent();
            txtVip.Focus();
        }

        #region Bind Data to Report(Search)
        private DataTable BindData()
        {
            string sql = @"
SELECT TOP 100 PERCENT *
FROM vwVIPProfile_Ex";

            StringBuilder whereCaluse = new StringBuilder();

            if (txtVip.Text.Trim().Length > 0 && txtVip.Text.Trim() != "*")
            {
                whereCaluse.Append(" MemberNumber LIKE '" + txtVip.Text.Trim() + "%'");
            }
            if (txtNickName.Text.Trim().Length > 0 && txtNickName.Text.Trim() != "*")
            {
                whereCaluse.Append((whereCaluse.Length > 0 ? " AND " : "") + " FullName='" + txtNickName.Text.Trim() + "'");
            }
            if (txtFirstName.Text.Trim().Length > 0 && txtFirstName.Text.Trim() != "*")
            {
                whereCaluse.Append((whereCaluse.Length > 0 ? " AND " : "") + " FirstName='" + txtFirstName.Text.Trim() + "'");
            }
            if (txtLastName.Text.Trim().Length > 0 && txtLastName.Text.Trim() != "*")
            {
                whereCaluse.Append((whereCaluse.Length > 0 ? " AND " : "") + " LastName='" + txtLastName.Text.Trim() + "'");
            }
            if (txtPhone.Text.Trim().Length > 0 && txtPhone.Text.Trim() != "*")
            {
                whereCaluse.Append((whereCaluse.Length > 0 ? " AND " : "") + " ContectNumber LIKE '%" + txtPhone.Text.Trim() + "%'");
            }
            if (txtID.Text.Trim().Length > 0 && txtID.Text.Trim() != "*")
            {
                whereCaluse.Append((whereCaluse.Length > 0 ? " AND " : "") + " ID='" + txtID.Text.Trim() + "'");
            }

            sql += whereCaluse.Length > 0 ? string.Format(" Where {0}", whereCaluse.ToString()) : "";
            sql += " ORDER BY MemberNumber";

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimedOut"]);
            cmd.CommandType = CommandType.Text;

            using (DataSet dataset = RT2008.DAL.SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }
        }
        #endregion

        private void DoSearch()
        {
            if (txtVip.Text.Trim().Length > 0 || txtNickName.Text.Trim().Length > 0 || txtFirstName.Text.Trim().Length > 0
                || txtLastName.Text.Trim().Length > 0 || txtPhone.Text.Trim().Length > 0 || txtID.Text.Trim().Length > 0)
            {
                _DataSource = BindData();
                ShowReport();
            }
            else
            {
                MessageBox.Show("Select at least one Criteria.", "Message");
            }
            this.txtVip.Focus();
        }

        private void ShowReport()
        {
            if (_DataSource.Rows.Count > 0)
            {
                // 有 data 就顯示隻 report
                String userAgent = VWGContext.Current.HttpContext.Request.UserAgent.ToLower();
                if (userAgent.Contains("msie")) // || userAgent.Contains("chrome") || userAgent.Contains("safari"))
                {
                    ShowReportInHTML();
                }
                else
                {
                    ShowReportInPDF();
                }
            }
            else
            {
                MessageBox.Show("no record found.", "Message");
            }
        }

        private void ShowReportInHTML()
        {
            this.rptViewer.Datasource = _DataSource;

            Dictionary<string, DataTable> subSource = new Dictionary<string, DataTable>();
            subSource.Add(DATASOURCE_XSD_NAME, _DataSource);
            this.rptViewer.SubReportDataSourceList = subSource;

            this.rptViewer.ReportDatasourceName = DATASOURCE_XSD_NAME;
            this.rptViewer.ReportName = REPORT_RDLC_NAME;
            this.rptViewer.Parameters = GetSelParams();

            this.rptViewer.ZoomMode = ZoomMode.Percent;
            this.rptViewer.ZoomPercent = 150;

            this.rptViewer.PreviewReport();
        }

        private void ShowReportInPDF()
        {
            this.splitContainer1.Panel2.Controls.Clear();
            Gizmox.WebGUI.Forms.HtmlBox htmlBox = new HtmlBox();

            htmlBox.Dock = DockStyle.Fill;

            this.splitContainer1.Panel2.Controls.Add(htmlBox);

            Gizmox.WebGUI.Common.Gateways.GatewayReference gw = new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "LoadPDF", false);
            htmlBox.Url = gw.ToString();
        }

        protected override Gizmox.WebGUI.Common.Interfaces.IGatewayHandler ProcessGatewayRequest(System.Web.HttpContext objHttpContext, String strAction)
        {
            if ((strAction != null) && (strAction == "LoadPDF"))
            {
                // Variables
                Warning[] warnings;
                String[] streamIds;
                String mimeType = String.Empty, encoding = String.Empty, extension = String.Empty;

                // Setup the report viewer object and get the array of bytes
                ReportDataSource ds = new ReportDataSource(DATASOURCE_XSD_NAME, _DataSource);
                ReportViewer viewer = new ReportViewer();

                viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(RptSubreportProcessingEventHandler);

                viewer.ProcessingMode = ProcessingMode.Local;

                Dictionary<string, DataTable> subSource = new Dictionary<string, DataTable>();
                subSource.Add(DATASOURCE_XSD_NAME, _DataSource);

                //viewer.LocalReport.ReportPath = "RT2008.Web.Reports.Rdlc.StockQtyStatusRdl.rdlc";
                viewer.LocalReport.EnableExternalImages = true;
                viewer.LocalReport.EnableHyperlinks = true;
                viewer.LocalReport.ReportEmbeddedResource = REPORT_RDLC_NAME;
                viewer.LocalReport.DataSources.Add(ds);
                viewer.LocalReport.SetParameters(GetSelParams());

                byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                System.Web.HttpResponse response = objHttpContext.Response;
                response.Buffer = true;
                response.Clear();
                response.ContentType = mimeType;
                response.AddHeader("content-disposition", "inline; filename=" + REPORT_FILENAME + "." + extension);
                response.BinaryWrite(bytes);    // create the file
                response.Flush();               // send it to the client to download

                return null;
            }
            else
            {
                return this.ProcessGatewayRequest(objHttpContext, strAction);
            }
        }

        void RptSubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

            Dictionary<string, DataTable> subSource = new Dictionary<string, DataTable>();
            subSource.Add(DATASOURCE_XSD_NAME, _DataSource);

            if (subSource != null)
            {
                DataTable subDataSource;
                String subDataSourceName;
                foreach (KeyValuePair<string, DataTable> kvp in subSource)
                {
                    subDataSource = kvp.Value;
                    subDataSourceName = kvp.Key;

                    e.DataSources.Add(new ReportDataSource(subDataSourceName, subDataSource));
                }
            }
        }

        /// <summary>
        /// 準備 selection criteria
        /// </summary>
        /// <returns></returns>
        private List<ReportParameter> GetSelParams()
        {
            List<ReportParameter> rptParam = new List<ReportParameter>();

            rptParam.Add(new ReportParameter("STKCODE", Common.Utility.GetSystemLabelByKey("STKCODE")));

            return rptParam;
        }

        #region Events
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void txtVip_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            DoSearch();
        }

        private void txtNickName_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            DoSearch();
        }

        private void txtFirstName_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            DoSearch();
        }

        private void txtID_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            DoSearch();
        }

        private void txtLastName_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            DoSearch();
        }

        private void txtPhone_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            DoSearch();
        }
        #endregion
    }
}