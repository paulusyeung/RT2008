#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;

using RT2008.DAL;

#endregion

namespace RT2008.EmulatedPoS.Reports
{
    public partial class History : Form
    {
        public RT2008.DAL.Common.Enums.TxType SalesType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="History"/> class.
        /// </summary>
        /// <param name="salesType">Type of the sales.</param>
        public History(RT2008.DAL.Common.Enums.TxType salesType)
        {
            InitializeComponent();

            this.SalesType = salesType;
            dtpFromDate.Value = Convert.ToDateTime(RT2008.SystemInfo.CurrentInfo.Default.LastMonthEnd.Insert(4, "-") + "-01");
            dtpToDate.Value = Convert.ToDateTime(RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemDate.ToString("yyyy-MM-" + DateTime.Now.ToString("dd")));
            FillComboList();
        }

        /// <summary>
        /// Fills the combo list.
        /// </summary>
        private void FillComboList()
        {
            RT2008.DAL.EPOSSubLedgerHeader.LoadCombo(ref cboFromTrn, "TxNumber", false);
            RT2008.DAL.EPOSSubLedgerHeader.LoadCombo(ref cboToTrn, "TxNumber", false);
            cboToTrn.SelectedIndex = cboToTrn.Items.Count - 1;
        }

        #region Check Condition
        /// <summary>
        /// Checks the valid.
        /// </summary>
        /// <returns></returns>
        private bool CheckValid()
        {
            if (cboFromTrn.SelectedIndex > cboToTrn.SelectedIndex)
            {
                MessageBox.Show("Range Error: From Trn > To Trn ", "Message");
                return false;
            }
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                MessageBox.Show("Range Error: From Date > To Date ", "Message");
                return false;
            }
            return true;
        }
        #endregion

        #region Bind Data to Report
        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns></returns>
        private DataTable BindData()
        {
            string sql = "SELECT * FROM vwRptEPOSHistory " +
                         "WHERE TxNumber BETWEEN '" + cboFromTrn.Text + "' AND '" + cboToTrn.Text + "' " +
                         "AND TxDate BETWEEN '" + dtpFromDate.Value + "' AND '" + dtpToDate.Value + "' " +
                         "AND TxType = '" + this.SalesType.ToString() + "' " +
                         "ORDER BY TxNumber";
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (DataSet dataset = SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnPrint control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (CheckValid())
            {
                string title = "";
                if (this.SalesType == Common.Enums.TxType.CAS)
                {
                    title = "Sales Input History";
                }
                else
                {
                    title = "Sales Return History";
                }
                string[,] param = { 
                                  {"CompanyName", RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                                  {"WorksheetTitle",title},
                                  {"PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat())},
                                  {"FromTRN",cboFromTrn.Text.Trim()},
                                  {"ToTRN",cboToTrn.Text.Trim()},
                                  {"FromDate",dtpFromDate.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat())},
                                  {"ToDate",dtpToDate.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat())},
                                  {"CLASS1",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS1")},
                                  {"CLASS2",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS2")},
                                  {"CLASS3",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS3")},
                                  {"CLASS4",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS4")},
                                  {"CLASS5",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS5")},
                                  {"CLASS6",RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS6")},
                                  };
                RT2008.Controls.Reporting.Viewer oViewer = new RT2008.Controls.Reporting.Viewer();

                oViewer.Datasource = BindData();
                oViewer.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptEPOSHistory";
                oViewer.ReportName = "RT2008.EmulatedPoS.Reports.HistoryRdl.rdlc";
                oViewer.Parameters = param;
                oViewer.Show();
            }
        }
    }
}