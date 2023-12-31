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
using RT2008.Controls;   
#endregion

namespace RT2008.Staff
{
    public partial class StaffGeneral : UserControl
    {
        public StaffGeneral()
        {
            InitializeComponent();
            InitialSmartTags();
            FillComo();
            cboSmartTag8.SelectedIndex = 0;
        }

        private void InitialSmartTags()
        {
            string[] orderBy = new string[] { "Priority" };
            SmartTag4StaffCollection smartTagList = SmartTag4Staff.LoadCollection(orderBy, true);

            SmartTag oTag = new SmartTag(this.gbGeneral);
            oTag.StaffSmartTagList = smartTagList;
            oTag.SetSmartTags();
        }

        #region Properties
        private Guid staffId = System.Guid.Empty;
        public Guid StaffId
        {
            get
            {
                return staffId;
            }
            set
            {
                staffId = value;
            }
        }
        #endregion

        #region FillComo
        private void FillComo()
        {
            FillGroup();
            FillDept();
            FillPosition();
            FillSex();
            FillAssistants();
        }
        #endregion

        #region FillGroup
        private void FillGroup()
        {
            cmbStaffGrade.Items.Clear();
            StaffGroup.LoadCombo(ref cmbStaffGrade, new string[] { "GradeCode", "GradeName" }, "{0} - {1}", false, false, String.Empty, String.Empty, null);
            cmbStaffGrade.SelectedIndex = 0;
        }
        #endregion

        #region FillDept
        private void FillDept()
        {
            cboDeptCode.Items.Clear();
            StaffDept.LoadCombo(ref cboDeptCode, new string[] { "DeptCode", "DeptName" }, "{0} - {1}", false, true, String.Empty, String.Empty, null);
            cboDeptCode.SelectedIndex = 0;  
        }
        #endregion

        #region FillPosition
        private void FillPosition()
        {
            cmbPosition.Items.Clear();
            StaffJobTitle.LoadCombo(ref cmbPosition, "JobTitleName", false, true, String.Empty, String.Empty);
            cmbPosition.SelectedIndex = 0;
        }
        #endregion

        #region FillSex
        private void FillSex()
        {
            cboSmartTag5.Items.Clear();
            cboSmartTag5.Items.Add("Male");
            cboSmartTag5.Items.Add("Female");
            cboSmartTag5.SelectedIndex = 0;
        }
        #endregion

        #region Fill Assistants

        private void FillAssistants()
        {
            RT2008.DAL.Staff.LoadCombo(ref cboSmartTag6, new string[] { "StaffNumber", "FullName" }, "{0} - {1}", false, true, String.Empty, "StaffId NOT IN ('" + this.StaffId.ToString() + "')", null);
            if (cboSmartTag6.Items.Count > 0) cboSmartTag6.SelectedIndex = 0;

            RT2008.DAL.Staff.LoadCombo(ref cboSmartTag7, new string[] { "StaffNumber", "FullName" }, "{0} - {1}", false, true, String.Empty, "StaffId NOT IN ('" + this.StaffId.ToString() + "')", null);
            if (cboSmartTag7.Items.Count > 0) cboSmartTag7.SelectedIndex = 0;  
        }

        #endregion

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(this.StaffId);
            changePassword.Closed += new EventHandler(changePassword_Closed);
            changePassword.ShowDialog(); 

        }

        void changePassword_Closed(object sender, EventArgs e)
        {
            ChangePassword changePassword = sender as ChangePassword;
            if (changePassword.IsCompleted)
            {
                txtPassword.Text = changePassword.Password;
            }
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
            if (txtFirstName.Text.Trim().Length > 0)
            {
                if (txtLastName.Text.Trim().Length > 0)
                {
                    txtName.Text = txtLastName.Text.Trim() + "," + txtFirstName.Text.Trim();
                }
                else
                {
                    txtName.Text = txtFirstName.Text.Trim();
                }
            }
            else
            {
                txtName.Text = txtLastName.Text.Trim();
            }
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (txtLastName.Text.Trim().Length > 0)
            {
                if (txtFirstName.Text.Trim().Length > 0)
                {
                    txtName.Text = txtLastName.Text.Trim() + "," + txtFirstName.Text.Trim();
                }
                else
                {
                    txtName.Text = txtLastName.Text.Trim();
                }
            }
            else
            {
                txtName.Text = txtFirstName.Text.Trim();
            }
        }
    }
}