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

#endregion

namespace RT2008.Inventory.StockTake.Reports
{
    public partial class Worksheet : Form
    {
        public Worksheet()
        {
            InitializeComponent();

            FillComboList();

            dtpTxDateFrom.Value = Convert.ToDateTime(RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemDate.ToString("yyyy-MM-01"));
            dtpTxDateTo.Value = Convert.ToDateTime(RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemDate.ToString("yyyy-MM-" + DateTime.Now.ToString("dd")));
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

            StockTakeHeader.LoadCombo(ref cboFrom, "TxNumber", false, false, string.Empty, "YEAR(PostedOn) = 1900");
        }

        private void FillToList()
        {
            cboTo.Items.Clear();

            StockTakeHeader.LoadCombo(ref cboTo, "TxNumber", false, false, string.Empty, "YEAR(PostedOn) = 1900");

            cboTo.SelectedIndex = cboTo.Items.Count - 1;
        }

        #endregion

        #region Validate Selections
        private bool IsSelValid()
        {
            bool result = true;

            if (String.Compare(cboTo.Text.Trim(), cboFrom.Text.Trim()) < 0)        //cboTo < cboFrom
            {
                result = false;
                MessageBox.Show("Range Error: Tx Number", "Message");
            }
            else if (String.Compare(dtpTxDateTo.Value.ToString("yyyy-MM-dd"), dtpTxDateFrom.Value.ToString("yyyy-MM-dd")) < 0)                     //dtpTxDateTo<dtpTxDateFrom
            {
                result = false;
                MessageBox.Show("Range Error: Tx Date", "Message");
            }
            return result;
        }
        #endregion

        #region Bind date to report
        private DataTable BindData()
        {
            string sql = @"
                          SELECT TOP 100 PERCENT *
                          FROM vwRptStkTkList
                          WHERE	TxNumber >= '" + this.cboFrom.Text.Trim() + @"' AND TxNumber <= '" + this.cboTo.Text.Trim() + @"' 
                          AND CONVERT(VARCHAR(10), TxDate, 126) >= '" + this.dtpTxDateFrom.Value.ToString("yyyy-MM-dd") + @"' 
                          AND CONVERT(VARCHAR(10), TxDate, 126) <= '" + this.dtpTxDateTo.Value.ToString("yyyy-MM-dd") + @"'
                          AND YEAR(PostedOn) = 1900
                          ORDER BY TxNumber, STKCODE, APPENDIX1, APPENDIX2, APPENDIX3 
                          ";

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

        private void cmdPreview_Click(object sender, EventArgs e)
        {
            if (IsSelValid())
            {
                //RT2008.DAL.Staff curUser = RT2008.DAL.Staff.Load(Common.Config.CurrentUserId);
                string[,] param = {
                {"FromTxNumber",this.cboFrom.Text.Trim()},
                {"ToTxNumber",this.cboTo.Text.Trim()},
                {"FromTxDate",this.dtpTxDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat())},
                {"ToTxDate",this.dtpTxDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat())},
                {"PrintedOn",DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat())},
                //{"PrintedBy",curUser.FullName},
                {"DateFormat",RT2008.SystemInfo.Settings.GetDateFormat()},
                { "CompanyName", RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                { "StockCode", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") }
                };

                RT2008.Controls.Reporting.Viewer view = new RT2008.Controls.Reporting.Viewer();

                view.Datasource = BindData();
                view.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptStkTkList";
                view.ReportName = "RT2008.Inventory.StockTake.Reports.WorksheetRdl.rdlc";
                view.Parameters = param;

                view.Show();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}