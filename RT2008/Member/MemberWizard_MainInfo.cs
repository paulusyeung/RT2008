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

namespace RT2008.Member
{
    public partial class MemberWizard_MainInfo : UserControl
    {
        //2014.01.08 paulus: 根據 Opera 嘅 RT2000，Member Group = Workplace 嘅 Shop

        public MemberWizard_MainInfo()
        {
            InitializeComponent();
            InitialSmartTags();
            FillComboList();
        }

        private void InitialSmartTags()
        {
            string[] orderBy = new string[] { "Priority" };
            SmartTag4MemberCollection smartTagList = SmartTag4Member.LoadCollection(orderBy, true);

            SmartTag oTag = new SmartTag(this);
            oTag.MemberSmartTagList = smartTagList;
            oTag.SetSmartTags();
        }

        #region Properties
        private Guid memberId = System.Guid.Empty;
        public Guid MemberId
        {
            get
            {
                return memberId;
            }
            set
            {
                memberId = value;
            }
        }
        #endregion

        #region Fill Combo List
        private void FillComboList()
        {
            FillSalutationList();
            FillJobTitleList();
            FillMemberClass();
            FillMemberGroupList();
            FillWorkplace();
        }

        private void FillSalutationList()
        {
            cboSalutation.Items.Clear();

            Salutation.LoadCombo(ref cboSalutation, "SalutationName", true, true, String.Empty, String.Empty);
            cboSalutation.SelectedIndex = 0;
        }

        private void FillJobTitleList()
        {
            cboJobTitle.Items.Clear();

            JobTitle.LoadCombo(ref cboJobTitle, "JobTitleName", true, true, String.Empty, String.Empty);
            cboJobTitle.SelectedIndex = 0;
        }

        private void FillMemberClass()
        {
            cboPhoneBook.Items.Clear();

            MemberClass.LoadCombo(ref cboPhoneBook, "ClassName", true, true, String.Empty, String.Empty);
            cboPhoneBook.SelectedIndex = 0;
        }

        private void FillMemberGroupList()
        {
            cboGroup.Items.Clear();

            MemberGroup.LoadCombo(ref cboGroup, "GroupName", true, true, String.Empty, String.Empty);
            cboGroup.SelectedIndex = 0;
        }

        private void FillWorkplace()
        {
            cboWorkplace.Items.Clear();
            RT2008.Controls.Workplace.LoadComboBox_Shops(ref cboWorkplace);
            cboWorkplace.SelectedIndex = 0;
        }
        #endregion

        private void cboPhoneBook_LostFocus(object sender, EventArgs e)
        {
            cboSalutation.Focus();
        }
    }
}