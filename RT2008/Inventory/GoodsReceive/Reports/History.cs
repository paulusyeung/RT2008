#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using RT2008.DAL;
using Gizmox.WebGUI.Common.Interfaces;
using System.IO;
using FileHelpers.DataLink;
using FileHelpers.MasterDetail;
using System.Web;
using System.Data.SqlClient;

#endregion

namespace RT2008.Inventory.GoodsReceive.Reports
{
    // Carrie 29-09-2008 : Remove IGatewayControl
    //public partial class HistoryWizard : Form, IGatewayControl
    public partial class HistoryWizard : Form
    {
        private string _TxNumberFrom = "00";
        private string _TxNumberTo = "zz";

        public HistoryWizard()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            dtpTxDateFrom.Value = Convert.ToDateTime(RT2008.SystemInfo.CurrentInfo.Default.LastMonthEnd.Insert(4, "-") + "-01");
            dtpTxDateTo.Value = DateTime.Now;

            RT2008.Controls.InvtUtility.ShowCriteria(ref txtTxNumberFrom, ref txtTxNumberTo, "vwRptSubLedgerCAP", Common.Enums.TxType.CAP, dtpTxDateFrom.Value, dtpTxDateTo.Value);
        }

        #region IGatewayControl Members // Carrie 29-09-2008 : Buttom removed (Function is not available)
        //public IGatewayHandler GetGatewayHandler(IContext objContext, string strAction)
        //{
        //    switch (strAction)
        //    {
        //        case "xPdf":
        //            XtraReportToPdf();
        //            break;
        //        case "rdlExcel":
        //            RdlToExcel();
        //            break;
        //        case "rdlPDF":
        //            RdlToPdf();
        //            break;
        //    }

        //    return null;
        //}

        //private void XtraReportToPdf()
        //{
        //    DataTable dt = Reports.DataSource.History(_TxNumberFrom, _TxNumberTo, this.dtpTxDateFrom.Value, this.dtpTxDateTo.Value);

        //    string filename = _TxNumberFrom + ".pdf";

        //    RT2008.Inventory.GoodsReceive.Reports.HistoryRpt report = new RT2008.Inventory.GoodsReceive.Reports.HistoryRpt();
        //    report.DataSource = dt;
        //    report.TxNumberFrom = this._TxNumberFrom;
        //    report.TxNumberTo = this._TxNumberTo;
        //    report.TxDateFrom = this.dtpTxDateFrom.Value;
        //    report.TxDateTo = this.dtpTxDateTo.Value;
        //    HttpResponse objResponse = this.Context.HttpContext.Response;

        //    System.IO.MemoryStream memStream = new System.IO.MemoryStream();

        //    objResponse.Clear();
        //    objResponse.ClearHeaders();

        //    report.ExportToPdf(memStream);
        //    objResponse.ContentType = "application/pdf";
        //    objResponse.AddHeader("content-disposition", "attachment; filename=" + filename);
        //    objResponse.BinaryWrite(memStream.ToArray());
        //    objResponse.Flush();
        //    objResponse.End();
        //}

        //        private void RdlToExcel()
        //{
        //    RT2008.DAL.Staff curUser = RT2008.DAL.Staff.Load(Common.Config.CurrentUserId);
        //    string[,] param = {
        //        { "FromTxNumber", this._TxNumberFrom },
        //        { "ToTxNumber", this._TxNumberTo },
        //        { "FromTxDate", this.dtpTxDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
        //        { "ToTxDate", this.dtpTxDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
        //        { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
        //        { "PrintedBy", curUser.FullName },
        //        { "StockCode", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
        //        { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
        //        { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
        //        { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
        //        { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() }
        //        };

        //    RT2008.Controls.Reporting.RdlExport rdlExport = new RT2008.Controls.Reporting.RdlExport();

        //    rdlExport.Datasource = BindData();
        //    rdlExport.ReportName = "RT2008.Inventory.GoodsReceive.Reports.WorksheetRdl.rdlc";
        //    rdlExport.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptBatchCAP";
        //    rdlExport.Parameters = param;

        //    rdlExport.ToExcel();
        //}

        //private void RdlToPdf()
        //{
        //    RT2008.DAL.Staff curUser = RT2008.DAL.Staff.Load(Common.Config.CurrentUserId);
        //    string[,] param = {
        //        { "FromTxNumber", this._TxNumberFrom },
        //        { "ToTxNumber", this._TxNumberTo },
        //        { "FromTxDate", this.dtpTxDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
        //        { "ToTxDate", this.dtpTxDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
        //        { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
        //        { "PrintedBy", curUser.FullName },
        //        { "StockCode", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
        //        { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
        //        { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
        //        { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
        //        { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() }
        //        };

        //    RT2008.Controls.Reporting.RdlExport rdlExport = new RT2008.Controls.Reporting.RdlExport();

        //    rdlExport.Datasource = BindData();
        //    rdlExport.ReportName = "RT2008.Inventory.GoodsReceive.Reports.WorksheetRdl.rdlc";
        //    rdlExport.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptBatchCAP";
        //    rdlExport.Parameters = param;

        //    rdlExport.ToPdf();
        //}
        #endregion

        #region Validate Selections
        private bool IsSelValid()
        {
            bool result = true;

            if (this.txtTxNumberFrom.Text.Trim() != String.Empty)
            {
                _TxNumberFrom = this.txtTxNumberFrom.Text.Trim();
            }
            if (this.txtTxNumberTo.Text.Trim() != String.Empty)
            {
                _TxNumberTo = this.txtTxNumberTo.Text.Trim();
            }

            if (String.Compare(_TxNumberTo, _TxNumberFrom) < 0)                 // _TxNumberTo < _TxNumberFrom
            {
                result = false;
                MessageBox.Show("Range Error: Tx Number", "Message");
            }
            else if (dtpTxDateTo.Value < dtpTxDateFrom.Value)                   // dtpTxDateTo < dtpTxDateFrom
            {
                result = false;
                MessageBox.Show("Range Error: Tx Date", "Message");
            }

            return result;
        }
        #endregion

        #region Bind data to report
        private DataTable BindData()
        {
            string sql = @"SELECT TOP 100 PERCENT * FROM vwRptSubLedgerCAP
                            WHERE TxNumber >= '" + this._TxNumberFrom + @"' AND TxNumber <= '" + this._TxNumberTo 
                        + @"' AND TxDate >= '" + this.dtpTxDateFrom.Value.ToString("yyyy-MM-dd") 
                        + @"' AND TxDate < '" + this.dtpTxDateTo.Value.AddDays(1).ToString("yyyy-MM-dd") 
                        + @"' AND TxType = '" + Common.Enums.TxType.CAP.ToString() 
                        + @"' ORDER BY TxNumber, STKCODE, APPENDIX1, APPENDIX2, APPENDIX3 ";
                        //+ @"' ORDER BY TxNumber, TxDate, LineNumber ";
                        // 2014.01.18 PAULUS: 正常應該 sort by TxNumber + LineNumber，不過 Opera 要求 STKCODE + APPENDIX1 + APPENDIX2 + APPENDIX3，我加多個 TxNumber

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (DataSet dataset = SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Carrie 29-09-2008 : Buttom removed (Function is not available)
            //if (IsSelValid())
            //{
            //    Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "xPdf"));
            //}
        }

        private void cmdPDF_Click(object sender, EventArgs e)
        {
            // Carrie 29-09-2008 : Buttom removed (Function is not available)
            //if (IsSelValid())
            //{
            //    Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "rdlPDF"));
            //}
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            // Carrie 29-09-2008 : Buttom removed (Function is not available)
            //if (IsSelValid())
            //{
            //    Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "rdlExcel"));
            //}
        }

        private void cmdPreview_Click(object sender, EventArgs e)
        {
            if (IsSelValid())
            {
                RT2008.DAL.Staff curUser = RT2008.DAL.Staff.Load(Common.Config.CurrentUserId);
                string[,] param = {
                { "FromTxNumber", this._TxNumberFrom },
                { "ToTxNumber", this._TxNumberTo },
                { "FromTxDate", this.dtpTxDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "ToTxDate", this.dtpTxDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                { "PrintedBy", curUser.FullName },
                { "StockCode", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() },
                { "CompanyName", RT2008.SystemInfo.CurrentInfo.Default.CompanyName}
                };

                RT2008.Controls.Reporting.Viewer oViewer = new RT2008.Controls.Reporting.Viewer();

                oViewer.Datasource = BindData();
                oViewer.ReportName = "RT2008.Inventory.GoodsReceive.Reports.HistoryRdl.rdlc";
                oViewer.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptSubLedgerCAP";
                oViewer.Parameters = param;

                oViewer.Show();
            }
        }

        private void btnFindFromTxNumber_Click(object sender, EventArgs e)
        {
            RT2008.Controls.InvtTxSearcher findFromTxNumber = RT2008.Controls.InvtUtility.ShowTxSearcher("vwRptSubLedgerCAP", Common.Enums.TxType.CAP);
            findFromTxNumber.Closed += new EventHandler(findFromTxNumber_Closed);
            findFromTxNumber.ShowDialog();
        }

        void findFromTxNumber_Closed(object sender, EventArgs e)
        {
            RT2008.Controls.InvtTxSearcher findFromTxNumber = sender as RT2008.Controls.InvtTxSearcher;
            if (findFromTxNumber.SelectedTxNumber.Trim().Length > 0)
            {
                txtTxNumberFrom.Text = findFromTxNumber.SelectedTxNumber;
                dtpTxDateFrom.Value = findFromTxNumber.SelectedTxDate;
            }
        }

        private void btnFindToTxNumber_Click(object sender, EventArgs e)
        {
            RT2008.Controls.InvtTxSearcher findToTxNumber = RT2008.Controls.InvtUtility.ShowTxSearcher("vwRptSubLedgerCAP", Common.Enums.TxType.CAP);
            findToTxNumber.Closed += new EventHandler(findToTxNumber_Closed);
            findToTxNumber.ShowDialog();
        }

        void findToTxNumber_Closed(object sender, EventArgs e)
        {
            RT2008.Controls.InvtTxSearcher findToTxNumber = sender as RT2008.Controls.InvtTxSearcher;
            if (findToTxNumber.SelectedTxNumber.Trim().Length > 0)
            {
                txtTxNumberTo.Text = findToTxNumber.SelectedTxNumber;
                dtpTxDateTo.Value = findToTxNumber.SelectedTxDate;
            }
        }
    }
}