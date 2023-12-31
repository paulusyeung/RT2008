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
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

#endregion

namespace RT2008.Inventory.Reports.StockStatus
{
    public partial class StockStatus_StockValue : Form
    {
        string currentYeaar = string.Empty;
        string currentMonth = string.Empty;
        String[] _StockCodeList = null;

        public StockStatus_StockValue()
        {
            InitializeComponent();

            SetAttributes();
            FillComboList();
        }

        private void SetAttributes()
        {
            currentYeaar = RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemYear;
            currentMonth = RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemMonth;

            this.txtYear.Text = currentYeaar;
            this.txtMonth.Text = currentMonth;
            this.txtMonth.Focus();

            cboFrom.DropDownStyle = ComboBoxStyle.DropDown;
            cboFrom.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboTo.DropDownStyle = ComboBoxStyle.DropDown;
            cboTo.AutoCompleteMode = AutoCompleteMode.Suggest;
            cboLocation.DropDownStyle = ComboBoxStyle.DropDown;
            cboLocation.AutoCompleteMode = AutoCompleteMode.Suggest;
        }

        #region Fill Combo List
        private void FillComboList()
        {
            SetStockCodeList();
            FillStockCodeFromList();
            FillStockCodeToList();
            FillLocationList();
        }

        private void SetStockCodeList()
        {
            string sql = " SELECT DISTINCT STKCODE FROM Product ORDER BY STKCODE";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = System.Data.CommandType.Text;

            using (DataSet ds = RT2008.DAL.SqlHelper.Default.ExecuteDataSet(cmd))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SortedList sList = new SortedList(ds.Tables[0].Rows.Count);
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        sList.Add(row["STKCODE"], null);
                    }
                    ArrayList aList = new ArrayList(sList.Keys);
                    _StockCodeList = (string[])aList.ToArray(typeof(string));
                }
            }
        }

        //2013.12.08 paulus: too slow, deprecated
        private DataTable StockCodeList()
        {
            string sql = " SELECT DISTINCT STKCODE FROM Product ORDER BY STKCODE";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = System.Data.CommandType.Text;

            using (DataSet dataset = RT2008.DAL.SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }
        }

        private void FillStockCodeFromList()
        {
            cboFrom.Items.Clear();

            //if (StockCodeList().Rows.Count > 0)
            //{
            //    cboFrom.DataSource = StockCodeList();
            //    cboFrom.DisplayMember = "STKCODE";
            //    cboFrom.SelectedIndex = 0;
            //}
            cboFrom.Items.AddRange(_StockCodeList);
            cboFrom.SelectedIndex = 0;
        }

        private void FillStockCodeToList()
        {
            cboTo.Items.Clear();

            //if (StockCodeList().Rows.Count > 0)
            //{
            //    cboTo.DataSource = StockCodeList();
            //    cboTo.DisplayMember = "STKCODE";
            //    cboTo.SelectedIndex = StockCodeList().Rows.Count - 1;
            //}
            cboTo.Items.AddRange(_StockCodeList);
            cboTo.SelectedIndex = _StockCodeList.Length - 1;
        }

        private void FillLocationList()
        {
            RT2008.DAL.WorkplaceCollection collection = RT2008.DAL.Workplace.LoadCollection(new string[] { "WorkplaceCode" }, true);

            foreach (RT2008.DAL.Workplace oWorkplace in collection)
            {
                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(oWorkplace.WorkplaceCode, oWorkplace.WorkplaceId.ToString());
                cboLocation.Items.Add(item);
            }

            cboLocation.SelectedIndex = 0;
        }

        #endregion

        #region Validate Section
        private bool IsSelValid()
        {
            bool result = true;
            int year = 0;
            int month = 0;

            if (int.TryParse(this.txtYear.Text.Trim(), out year) && int.TryParse(this.txtMonth.Text.Trim(), out month))
            {
                if (month > 13 || month < 1)
                {
                    result = false;
                    MessageBox.Show("Format Error: Month Error", "Message");
                }
                else if (year > 9999 || year < 0)
                {
                    result = false;
                    MessageBox.Show("Format Error: Year Error", "Message");
                }

                else if (string.Compare(this.cboFrom.Text.Trim(), this.cboTo.Text.Trim()) > 0)
                {
                    result = false;
                    MessageBox.Show("Range Error: STKCODE Error ", "Message");
                }
            }
            else
            {
                result = false;
                MessageBox.Show("Format Error:Year and Month Must be a number of", "Message");
            }
            return result;
        }
        #endregion

        #region Properties
        private int FromYear
        {
            get
            {
                return int.Parse(this.txtYear.Text.Trim());
            }
        }

        private int FromMonth
        {
            get
            {
                return int.Parse(this.txtMonth.Text.Trim());
            }
        }
        #endregion

        #region ReportDatasourceName
        private string ReportDataSourceName()
        {
            string reportDatasourceName = string.Empty;
            if (FromYear < int.Parse(currentYeaar))
            {
                reportDatasourceName = "vwRptStockStatus_StockValue_Period";
            }
            else
            {
                if (FromMonth >= int.Parse(currentMonth))
                {
                    reportDatasourceName = "vwRptStockStatus_StockValue";
                }
                else
                {
                    reportDatasourceName = "vwRptStockStatus_StockValue_Period";
                }
            }
            return reportDatasourceName;
        }
        #endregion

        #region Bind Data to Report
        private DataTable BindData()
        {
            string sql = @"
SELECT TOP(100) PERCENT * 
FROM " + ReportDataSourceName() + @"
WHERE STKCODE BETWEEN '" + this.cboFrom.Text.Trim() + "' AND '" + this.cboTo.Text.Trim() + @"'
AND WorkplaceCode = '" + cboLocation.Text.Trim() + @"'       
";
            if (chkZeroQty.Checked)
            {
                sql = sql + " AND CDQTY <> 0";
            }

            if (FromYear < int.Parse(currentYeaar) || (FromYear == int.Parse(currentYeaar) && FromMonth < int.Parse(currentMonth)))
            {
                sql = sql + " AND FiscalYear = " + FromYear + " AND Period = " + FromMonth;
            }

            sql = sql + " ORDER BY STKCODE";

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPriview_Click(object sender, EventArgs e)
        {
            if (IsSelValid())
            {

                string[,] param = {
                {"STKFrom",this.cboFrom.Text.Trim()},
                {"STKTo",this.cboTo.Text.Trim()},
                {"FromYear",this.txtYear.Text.Trim()},
                {"FromMonth",this.txtMonth.Text.Trim()},
                {"CurrentYear", currentYeaar},
                {"CurrentMonth",currentMonth},
                {"PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat())},
                {"STKLabel",RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE")},
                {"APPENDIX1",RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1")},
                {"APPENDIX2",RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2")},
                {"APPENDIX3",RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3")},
                {"CLASS1",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS1")},
                {"CLASS2",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS2")},
                {"CLASS3",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS3")},
                {"CLASS4",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS4")},
                {"CLASS5",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS5")},
                {"CLASS6",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS6")},
                {"Location",this.cboLocation.Text.Trim().ToString()},
                {"CompanyName", RT2008.SystemInfo.CurrentInfo.Default.CompanyName}
                };

                RT2008.Controls.Reporting.Viewer view = new RT2008.Controls.Reporting.Viewer();

                view.Datasource = BindData();
                view.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_" + ReportDataSourceName() +"";

                #region ReportName
                if (ReportDataSourceName() == "vwRptStockStatus_StockValue")
                {
                    if (this.cboStkRemark.Text.Trim() == "Class 1 - 6")
                    {
                        view.ReportName = "RT2008.Inventory.Reports.StockStatus.StockStatus_StockValueWithClassRdl.rdlc";
                    }
                    else if (this.cboStkRemark.Text.Trim() == "Description")
                    {
                        view.ReportName = "RT2008.Inventory.Reports.StockStatus.StockStatus_StockValueWithDescRdl.rdlc";
                    }
                }
                else
                {
                    if (this.cboStkRemark.Text.Trim() == "Class 1 - 6")
                    {
                        view.ReportName = "RT2008.Inventory.Reports.StockStatus.StockStatus_StockValuePeriodWithClassRdl.rdlc";
                    }
                    else if (this.cboStkRemark.Text.Trim() == "Description")
                    {
                        view.ReportName = "RT2008.Inventory.Reports.StockStatus.StockStatus_StockValuePeriodWithDescRdl.rdlc";
                    }
                }
                #endregion

                view.Parameters = param;

                view.Show();
            }
        }
    }
}