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
    public partial class RptVipMLblAtdForm : Form
    {
        public RptVipMLblAtdForm()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FillComboBox();
        }

        #region Init Method
        private void InitLabelType()
        {
            if (rbtnVIP_L001.Checked)
            {
                gbVIP_L001.Visible = true;
                gbVIP_L003.Visible = false;
                gbVIP_L004.Visible = false;

                FillPositionList(14);
            }

            if (rbtnVIP_L003.Checked)
            {
                gbVIP_L001.Visible = false;
                gbVIP_L003.Visible = true;
                gbVIP_L004.Visible = false;

                gbVIP_L003.Location = gbVIP_L001.Location;

                lblVIP_L003_1.Visible = true;
                lblVIP_L003_2.Visible = true;
                lblVIP_L003_3.Visible = true;
                lblVIP_L003_4.Visible = true;
                lblVIP_L003_5.Visible = true;

                FillPositionList(24);
            }

            if (rbtnVIP_L004.Checked)
            {
                gbVIP_L001.Visible = false;
                gbVIP_L003.Visible = false;
                gbVIP_L004.Visible = true;

                gbVIP_L004.Location = gbVIP_L001.Location;

                lblVIP_L004_1.Visible = true;
                lblVIP_L004_2.Visible = true;
                lblVIP_L004_3.Visible = true;
                lblVIP_L004_4.Visible = true;
                lblVIP_L004_5.Visible = true;

                FillPositionList(16);
            }
        }

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

        #region Data Binds

        private DataTable BindData()
        {
            string from = cmbFrom.Text.Remove(cmbFrom.Text.IndexOf("-")).Trim();
            string to = cmbTo.Text.Remove(cmbTo.Text.IndexOf("-")).Trim() + "z";

            string sql = @"
SELECT *, FirstName + ',' + LastName AS FullName
 FROM dbo.vwVIP_MemberList 
 WHERE VipNumber BETWEEN '" + from + @"' AND '" + to + @"'
 AND [Amount Purchased] = '" + this.txtPurchaseValue.Text.Trim() + @"'
 ORDER BY VipNumber";

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

        private void RptVipMLblAtdForm_Load(object sender, EventArgs e)
        {
            InitLabelType();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnVIP_L_CheckedChanged(object sender, EventArgs e)
        {
            InitLabelType();
        }

        private void btnPriview_Click(object sender, EventArgs e)
        {
            string[,] param = {
            {"PrintedOn",DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat())}
            };

            RT2008.Controls.Reporting.Viewer view = new RT2008.Controls.Reporting.Viewer();
            view.Datasource = BindData();

            #region ReportName
            if (rbtnLayout_1.Checked)
            {
                if (rbtnVIP_L001.Checked)
                {
                    view.ReportName = "RT2008.Member.Reports.VipMLblAtd_001ADCRdl.rdlc";
                }
                if (rbtnVIP_L003.Checked)
                {
                    view.ReportName = "RT2008.Member.Reports.VipMLblAtd_003ADCRdl.rdlc";
                }
                if (rbtnVIP_L004.Checked)
                {
                    view.ReportName = "RT2008.Member.Reports.VipMLblAtd_004ADCRdl.rdlc";
                }
            }
            else if(rbtnLayout_2.Checked)
            {
                if (rbtnVIP_L001.Checked)
                {
                    view.ReportName = "RT2008.Member.Reports.VipMLblAtd_001CDARdl.rdlc";
                }
                if (rbtnVIP_L003.Checked)
                {
                    view.ReportName = "RT2008.Member.Reports.VipMLblAtd_003CDARdl.rdlc";
                }
                if (rbtnVIP_L004.Checked)
                {
                    view.ReportName = "RT2008.Member.Reports.VipMLblAtd_004CDARdl.rdlc";
                }
            }
            #endregion

            view.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwVIP_MemberList";
            view.Parameters = param;
            view.Show();
        }
    }
}