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

namespace RT2008.Member.Reports
{
    public partial class RptVipMLblBirthdayYtdForm : Form
    {
        public RptVipMLblBirthdayYtdForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FillComboBox();
        }

        #region FillComboBox
        private void FillComboBox()
        {
            MemberCollection collection = RT2008.DAL.Member.LoadCollection(new string[] { "MemberNumber" }, true);
            if (collection.Count > 0)
            {
                foreach (RT2008.DAL.Member oMember in collection)
                {
                    string item = oMember.MemberNumber + " - " + oMember.FullName;
                    cmbFrom.Items.Add(item);
                    cmbTo.Items.Add(item);
                }
                cmbFrom.SelectedIndex = 0;

                cmbTo.SelectedIndex = collection.Count - 1;
            }
        }
        #endregion

        #region Init Method
        private void FillPositionList(int positionCount)
        {
            cboFromPosition.Items.Clear();
            cboToPosition.Items.Clear();

            for (int i = 1; i <= positionCount; i++)
            {
                cboFromPosition.Items.Add(i.ToString());
                cboToPosition.Items.Add(i.ToString());
            }

            cboFromPosition.SelectedIndex = 0;
            cboToPosition.SelectedIndex = positionCount - 1;
        }
        #endregion

        #region Data Binds

        private DataTable BindData()
        {
            string from = cmbFrom.Text.Remove(cmbFrom.Text.IndexOf("-")).Trim();
            string to = cmbTo.Text.Remove(cmbTo.Text.IndexOf("-")).Trim() + "z";

            string fromDay = dtpFromBirthday.Value.Day.ToString();
            string fromMonth = dtpFromBirthday.Value.Month.ToString();
            string toDay = dtpToBirthday.Value.Day.ToString();
            string toMonth = dtpToBirthday.Value.Month.ToString();

            string sql = @"
SELECT *, SUBSTRING(DateOfBirth, 6, LEN(DateOfBirth) - 4) AS Birthday, FirstName + ',' + LastName AS FullName
 FROM dbo.vwVIP_MemberList 
WHERE VipNumber BETWEEN '" + from + @"' AND '" + to + @"'
 AND DatePart(MM, DateOfBirth) >= " + fromMonth + @" AND  DatePart(MM, DateOfBirth) <= " + toMonth + @" 
 AND CONVERT(DateTime, [Date Commence], 103) >= CONVERT(DateTime, '" + this.dtpFromCommDate.Value.ToString("dd/MM/yyyy") + @"', 103)
 AND CONVERT(DateTime, [Date Commence], 103) < CONVERT(DateTime, '" + this.dtpToCommDate.Value.AddDays(1).ToString("dd/MM/yyyy") + @"', 103) 
 AND [Group]= '" + this.txtGroup.Text.Trim() + @"'
 AND [Amount Purchased] = '" + this.txtPurchaseValue.Text.Trim() + @"'
 ORDER BY Birthday";

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

        private void RptVipMLblBirthdayYtdForm_Load(object sender, EventArgs e)
        {
            FillPositionList(14);
        }

        private void btnPriview_Click(object sender, EventArgs e)
        {
            string[,] param = {
            {"PrintedOn",DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat())}
            };

            RT2008.Controls.Reporting.Viewer view = new RT2008.Controls.Reporting.Viewer();

            #region ReportName
            if (this.rbtnLayout_1.Checked)
            {
                view.ReportName = "RT2008.Member.Reports.VipMLblBirthdayYtdForm_001ADCRdl.rdlc";
            }
            else
            {
                view.ReportName = "RT2008.Member.Reports.VipMLblBirthdayYtdForm_001CDARdl.rdlc";
            }
            #endregion

            view.Datasource = BindData();
            view.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwVIP_MemberList";
            view.Parameters = param;

            view.Show();
        }
    }
}