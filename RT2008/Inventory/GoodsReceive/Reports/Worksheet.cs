using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Interfaces;

using FileHelpers.DataLink;
using FileHelpers.MasterDetail;

using RT2008.DAL;

namespace RT2008.Inventory.GoodsReceive.Reports
{
    // Carrie 29-09-2008 : Remove IGatewayControl
    //public partial class WorksheetWizard : Form, IGatewayControl
    public partial class WorksheetWizard : Form
    {
        public WorksheetWizard()
        {
            InitializeComponent();
            FillComboList();

            dtpTxDateFrom.Value = Convert.ToDateTime(RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemDate.ToString("yyyy-MM-01"));
            dtpTxDateTo.Value = DateTime.Now;
        }

        #region Fill Combo List
        private void FillComboList()
        {
            FillFromList();
            FillToList();
        }

        private void FillFromList()
        {
            cboFrom.Items.Clear();

            string[] orderBy = { "TxNumber" };
            string sql = "TxType = '" + Common.Enums.TxType.CAP.ToString() + "'";

            InvtBatchCAP_HeaderCollection headerList = InvtBatchCAP_Header.LoadCollection(sql, orderBy, true);
            cboFrom.DataSource = headerList;
            cboFrom.DisplayMember = "TxNumber";
            cboFrom.ValueMember = "HeaderId";
        }

        private void FillToList()
        {
            cboTo.Items.Clear();

            string[] orderBy = { "TxNumber" };
            string sql = "TxType = '" + Common.Enums.TxType.CAP.ToString() + "'";

            InvtBatchCAP_HeaderCollection headerList = InvtBatchCAP_Header.LoadCollection(sql, orderBy, true);
            cboTo.DataSource = headerList;
            cboTo.DisplayMember = "TxNumber";
            cboTo.ValueMember = "HeaderId";

            cboTo.SelectedIndex = headerList.Count - 1;
        }
        #endregion

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
        //    DataTable dt = Reports.DataSource.Worksheet(this.cboFrom.Text, this.cboTo.Text, this.dtpTxDateFrom.Value, this.dtpTxDateTo.Value);

        //    string filename = cboFrom.Text.Trim() + ".pdf";

        //    RT2008.Inventory.GoodsReceive.Reports.WorksheetRpt report = new RT2008.Inventory.GoodsReceive.Reports.WorksheetRpt();
        //    report.DataSource = dt;
        //    report.TxNumberFrom = this.cboFrom.Text.Trim();
        //    report.TxNumberTo = this.cboTo.Text.Trim();
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

        //private void RdlToExcel()
        //{
        //    RT2008.DAL.Staff curUser = RT2008.DAL.Staff.Load(Common.Config.CurrentUserId);
        //    string[,] param = {
        //        { "FromTxNumber", this.cboFrom.Text.Trim() },
        //        { "ToTxNumber", this.cboTo.Text.Trim() },
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
        //        { "FromTxNumber", this.cboFrom.Text.Trim() },
        //        { "ToTxNumber", this.cboTo.Text.Trim() },
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

            if (String.Compare(cboTo.Text.Trim(), cboFrom.Text.Trim()) < 0)     // cboTo < cboFrom
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
            string sql = @" 
SELECT TOP 100 PERCENT * FROM vwRptBatchCAP
WHERE	TxNumber >= '" + this.cboFrom.Text.Trim() + @"' AND TxNumber <= '" + this.cboTo.Text.Trim() + @"' AND
        TxDate >= CAST('" + this.dtpTxDateFrom.Value.ToString("yyyy-MM-dd 00:00:00") + @"' AS Datetime) AND 
        TxDate <= CAST('" + this.dtpTxDateTo.Value.ToString("yyyy-MM-dd 23:59:59") + @"' AS Datetime) AND
        TxType = '" + Common.Enums.TxType.CAP.ToString() + @"' 
ORDER BY TxNumber, STKCODE, APPENDIX1, APPENDIX2, APPENDIX3 ";    //2013.07.09 paulus: 舊 sorted by TxNumber, TxDate, LineNumber

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

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            // Carrie 29-09-2008 : Buttom removed (Function is not available)
            //if (IsSelValid())
            //{
            //    Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "rdlExcel"));
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

        private void cmdPreview_Click(object sender, EventArgs e)
        {
            if (IsSelValid())
            {
                RT2008.DAL.Staff curUser = RT2008.DAL.Staff.Load(Common.Config.CurrentUserId);
                string[,] param = {
                { "FromTxNumber", this.cboFrom.Text.Trim() },
                { "ToTxNumber", this.cboTo.Text.Trim() },
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
                oViewer.ReportName = "RT2008.Inventory.GoodsReceive.Reports.WorksheetRdl.rdlc";
                oViewer.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptBatchCAP";
                oViewer.Parameters = param;

                oViewer.Show();
            }
        }
    }
}