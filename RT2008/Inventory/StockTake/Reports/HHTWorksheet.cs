#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using RT2008.DAL;

#endregion

namespace RT2008.Inventory.StockTake.Reports
{
    public partial class HHTWorksheet : Form
    {
        public HHTWorksheet()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BindComboList();
        }

        private void BindComboList()
        {
            StocktakeHeader_HHT.LoadCombo(ref cboTxNumber, new string[] { "TxNumber", "HHTId", "UploadedOn" }, "{0}\t{1}\t{2}", false, false, string.Empty, "YEAR(PostedOn) = 1900 AND Retired = 0", null);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (cboTxNumber.SelectedValue != null)
            {
                string txNumber = this.cboTxNumber.Text.Trim().Length > 12 ? this.cboTxNumber.Text.Trim().Substring(0, 12) : string.Empty;
                string[,] param = {
                { "FromTxNumber",txNumber},
                { "ToTxNumber",txNumber},
                { "PrintedOn",DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat())},
                { "DateFormat",RT2008.SystemInfo.Settings.GetDateFormat()},
                { "DateFormatWithTime",RT2008.SystemInfo.Settings.GetDateTimeFormat()},
                { "CompanyName", RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                { "DataType", rbtnAllData.Checked.ToString()},
                { "StockCode", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") }
                };

                RT2008.Controls.Reporting.Viewer view = new RT2008.Controls.Reporting.Viewer();

                view.Datasource = BindData();
                view.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptStkTkHHTList";
                view.ReportName = "RT2008.Inventory.StockTake.Reports.HHTWorksheetRdl.rdlc";
                view.Parameters = param;

                view.Show();
            }
            else
            {
                MessageBox.Show("No record found!", "ATTENTION");
            }
        }

        #region Bind date to report

        private DataTable BindData()
        {
            string sql = @"
                          SELECT TOP 100 PERCENT *
                          FROM vwRptStkTkHHTList
                          WHERE	HeaderId = '" + this.cboTxNumber.SelectedValue.ToString() + @"' AND YEAR(PostedOn) = 1900
                          ORDER BY TxNumber
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
    }
}