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
using RT2008.DAL;
#endregion

namespace RT2008.Web.Reports.Forms
{
    public partial class DailyShopTransaction_Inline : UserControl
    {
        private DataTable _DataSource;
        private String DATASOURCE_XSD_NAME = "DataSource_vwDailyShopTransaction";
        private String REPORT_RDLC_NAME = "RT2008.Web.Reports.Rdlc.DailyShopTransactionRdl.rdlc";
        private String REPORT_FILENAME = "Daily Shop Transaction.PDF";

        public DailyShopTransaction_Inline()
        {
            InitializeComponent();
            txtShop.Focus();
        }

        private DataTable Binding(string whereCaluse)
        {
            string sql = @"
SELECT TOP 100 PERCENT *
FROM vwDailyShopTransaction
WHERE SHOP ='" + txtShop.Text.Trim() + @"'
AND YEAR(TxDate)='" + txtYear.Text.Trim() + @"'
AND MONTH(TxDate)='" + txtMonth.Text.Trim() + @"'
AND DAY(TxDate)='" + txtDate.Text.Trim() + @"'
";
            if (txtType.Text.Trim().Length > 0)
            {
                sql += " AND TxType='" + txtType.Text.Trim() + "'";
            }
            if (txtTxNumber.Text.Trim().Length > 0)
            {
                sql += " AND TxNumber='" + txtTxNumber.Text.Trim() + "'";
            }

            sql = sql + " ORDER BY TxType, TxNumber, STKCODE, APPENDIX1, APPENDIX2, APPENDIX3 ASC"; // 18/01/2010 David: Order by Type, TRN#, STK, A1, A2, A3 

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimedOut"]);
            cmd.CommandType = CommandType.Text;

            using (DataSet dataset = SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }
        }

        #region Bind Data to Report(Search)
        private DataTable BindData()
        {
            string sql = @"
SELECT TOP 100 PERCENT *
FROM vwDailyShopTransaction
WHERE SHOP ='" + txtShop.Text.Trim() + @"'
AND YEAR(TxDate)='" + txtYear.Text.Trim() + @"'
AND MONTH(TxDate)='" + txtMonth.Text.Trim() + @"'
AND DAY(TxDate)='" + txtDate.Text.Trim() + @"'
";
            if (txtType.Text.Trim().Length > 0)
            {
                sql += " AND TxType='" + txtType.Text.Trim() + "'";
            }
            if (txtTxNumber.Text.Trim().Length > 0)
            {
                sql += " AND TxNumber='" + txtTxNumber.Text.Trim() + "'";
            }

            sql = sql + " ORDER BY TxType, TxNumber, STKCODE, APPENDIX1, APPENDIX2, APPENDIX3 ASC"; // 18/01/2010 David: Order by Type, TRN#, STK, A1, A2, A3 

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimedOut"]);
            cmd.CommandType = CommandType.Text;

            using (DataSet dataset = SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }
        }
        #endregion

        private void DoSearch()
        {
            if (txtShop.Text.Length > 0 || txtDate.Text.Length > 0 || txtMonth.Text.Length > 0 || txtYear.Text.Length > 0)
            {
                _DataSource = BindData();
                ShowReport();
            }
            else
            {
                MessageBox.Show("Shop or Sales Date required!", "Message");
            }
            this.txtType.Focus();
        }

        private void ShowReport()
        {
            if (_DataSource.Rows.Count > 0)
            {
                // 有 data 就顯示隻 report
                String userAgent = VWGContext.Current.HttpContext.Request.UserAgent.ToLower();
                if (userAgent.Contains("msie") || userAgent.Contains("chrome") || userAgent.Contains("safari"))
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
            rptParam.Add(new ReportParameter("APPENDIX1", Common.Utility.GetSystemLabelByKey("APPENDIX1")));
            rptParam.Add(new ReportParameter("APPENDIX2", Common.Utility.GetSystemLabelByKey("APPENDIX2")));
            rptParam.Add(new ReportParameter("APPENDIX3", Common.Utility.GetSystemLabelByKey("APPENDIX3")));

            return rptParam;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void txtYear_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            DoSearch();
        }
    }
}