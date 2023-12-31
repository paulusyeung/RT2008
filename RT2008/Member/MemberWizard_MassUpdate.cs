#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using System.Reflection;
using RT2008.DAL;
using System.Data.SqlClient;

#endregion

namespace RT2008.Member
{
    public partial class MemberWizard_MassUpdate : Form
    {
        //2014.01.08 paulus: 根據 Opera 嘅 RT2000，Member Group = Workplace 嘅 Shop

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberWizard_MassUpdate"/> class.
        /// </summary>
        public MemberWizard_MassUpdate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:Gizmox.WebGUI.Forms.Form.Load"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.FillComboList();
            txtFromMemberNumber.Focus();

            cboFromShop.AutoCompleteMode = AutoCompleteMode.Append;
            cboToShop.AutoCompleteMode = AutoCompleteMode.Append;
            cboFromGrade.AutoCompleteMode = AutoCompleteMode.Append;
            cboToGrade.AutoCompleteMode = AutoCompleteMode.Append;
        }

        #region Fill ComboBox

        /// <summary>
        /// Fills the combo list.
        /// </summary>
        private void FillComboList()
        {
            FillGroup();
            FillGrade();
        }

        /// <summary>
        /// Fills the group.
        /// </summary>
        private void FillGroup()
        {
            cboFromShop.Items.Clear();
            cboToShop.Items.Clear();

            //MemberGroup.LoadCombo(ref cboFromGroup, new string[] { "GroupCode", "GroupName" }, "{0} - {1}", false, false, String.Empty, String.Empty);
            //MemberGroup.LoadCombo(ref cboToGroup, new string[] { "GroupCode", "GroupName" }, "{0} - {1}", false, false, String.Empty, String.Empty);
            RT2008.Controls.Workplace.LoadComboBox_Shops(ref cboFromShop);
            RT2008.Controls.Workplace.LoadComboBox_Shops(ref cboToShop);

            cboFromShop.SelectedIndex = 0;
            cboToShop.SelectedIndex = 0;
        }

        /// <summary>
        /// Fills the grade.
        /// </summary>
        private void FillGrade()
        {
            cboFromGrade.Items.Clear();
            cboToGrade.Items.Clear();

            string query = "SELECT DISTINCT SmartTagValue FROM MemberSmartTag WHERE TagId IN (SELECT TagId FROM SmartTag4Member WHERE TagCode = 'Grade' AND Priority = 1) ORDER BY SmartTagValue";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    cboFromGrade.Items.Add(reader.GetString(0));
                    cboToGrade.Items.Add(reader.GetString(0));
                }
            }
            if (cboFromGrade.Items.Count > 0)
            {
                cboFromGrade.SelectedIndex = 0;
                cboToGrade.SelectedIndex = 0;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Buttons the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button btnCtrl = sender as Button;
                if (btnCtrl != null)
                {
                    switch (btnCtrl.Name.ToLower())
                    {
                        case "btnlookupfrommembernumber":
                            this.ShowSearchForm(txtFromMemberNumber.Name);
                            break;
                        case "btnlookuptomembernumber":
                            this.ShowSearchForm(txtToMemberNumber.Name);
                            break;
                        case "btnupdate":
                            MessageBox.Show("Update Records?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(UpdateMessageHandler));
                            break;
                        case "btncancel":
                            this.Close();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks the box click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CheckBoxClick(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                CheckBox chkCtrl = sender as CheckBox;
                if (chkCtrl != null)
                {
                    switch (chkCtrl.Name.ToLower())
                    {
                        case "chknormaldiscount":
                            txtNormalDiscount.Enabled = chkNormalDiscount.Checked;
                            txtNormalDiscount.Select(0, txtNormalDiscount.Text.Length);
                            break;
                        case "chkpromotiondiscount":
                            txtPromotionDiscount.Enabled = chkPromotionDiscount.Checked;
                            txtPromotionDiscount.Select(0, txtPromotionDiscount.Text.Length);
                            break;
                        case "chkaddondiscount":
                            chkCheckAddOnDiscount.Enabled = chkAddOnDiscount.Checked;
                            chkCheckAddOnDiscount.Select();
                            break;
                        case "chkstaffquota":
                            txtStaffQuota.Enabled = chkStaffQuota.Checked;
                            txtStaffQuota.Select(0, txtStaffQuota.Text.Length);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Shows the search form.
        /// </summary>
        /// <param name="searchForm">the value be look up to TextBox control</param>
        private void ShowSearchForm(string lookupTo)
        {
            MemberWizard_Find findForm = new MemberWizard_Find();
            findForm.Closed += new EventHandler(findForm_Closed);
            findForm.LookUpTo = lookupTo;
            findForm.ShowDialog();
        }

        /// <summary>
        /// Handles the Closed event of the findForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void findForm_Closed(object sender, EventArgs e)
        {
            MemberWizard_Find findForm = sender as MemberWizard_Find;
            if (findForm != null)
            {
                Control[] txtCtrls = this.gbUpdateRange.Controls.Find(findForm.LookUpTo, false);
                if (txtCtrls.Length > 0)
                {
                    TextBox txtCtrl = txtCtrls[0] as TextBox;
                    if (txtCtrl != null)
                    {
                        txtCtrl.Text = findForm.SelectedMember;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the message handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UpdateMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                int succeedRec = UpdateVip();
                if (succeedRec > 0)
                {
                    MessageBox.Show("Updated " + succeedRec.ToString() + " records!", "Success!");
                }
                else
                {
                    MessageBox.Show("Updated 0 record!", "Failed!");
                }
            }
        }

        #endregion

        /// <summary>
        /// Determines whether the specified member id is updatable.
        /// </summary>
        /// <param name="memberId">The member id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified member id is updatable; otherwise, <c>false</c>.
        /// </returns>
        private bool IsUpdatable(Guid memberId)
        {
            bool result = true;
            List<Guid> shopRange = GetShopRange();

            RT2008.DAL.Member objMember = RT2008.DAL.Member.Load(memberId);
            if (objMember != null)
            {
                result = result & shopRange.Contains(objMember.WorkplaceId);
            }
            else
            {
                result = result & false;
            }

            String query = String.Format("MemberId = '{0}' AND SmartTagValue >= '{1}' AND SmartTagValue <= '{2}' AND TagId IN (SELECT TagId FROM SmartTag4Member WHERE TagCode = 'Grade' AND Priority = 1)",
                memberId.ToString(), cboFromGrade.Text, cboToGrade.Text);
            MemberSmartTagCollection objMemberTagList = MemberSmartTag.LoadCollection(query);
            result = result & (objMemberTagList.Count > 0);

            return result;
        }

        /// <summary>
        /// Full updates of this instance.
        /// </summary>
        /// <returns></returns>
        private int UpdateVip()
        {
            int result = 0;
            String query = String.Format("VipNumber >= '{0}' AND VipNumber <= '{1}'", txtFromMemberNumber.Text.Trim(), txtToMemberNumber.Text.Trim());

            MemberVipDataCollection objMemberVipList = MemberVipData.LoadCollection(query);
            foreach (MemberVipData objMemberVip in objMemberVipList)
            {
                if (objMemberVip != null)
                {
                    if (IsUpdatable(objMemberVip.MemberId))
                    {
                        // Normal Discount (Member)
                        RT2008.DAL.Member objMember = RT2008.DAL.Member.Load(objMemberVip.MemberId);
                        if (objMember != null)
                        {
                            if (chkNormalDiscount.Checked)
                            {
                                objMember.NormalDiscount = Common.Utility.IsNumeric(txtNormalDiscount.Text) ? Convert.ToDecimal(txtNormalDiscount.Text) : objMember.NormalDiscount;
                            }

                            objMember.ModifiedBy = Common.Config.CurrentUserId;
                            objMember.ModifiedOn = DateTime.Now;
                            objMember.Status = (int)Common.Enums.Status.Modified;
                            objMember.Save();
                        }

                        // Add-on Discount & Staff Quota (Member VIP Data)
                        if (chkAddOnDiscount.Checked)
                        {
                            objMemberVip.AddOnDiscount = chkCheckAddOnDiscount.Checked;
                        }

                        if (chkStaffQuota.Checked)
                        {
                            objMemberVip.StaffQuota = Common.Utility.IsNumeric(txtStaffQuota.Text) ? Convert.ToDecimal(txtStaffQuota.Text) : objMemberVip.StaffQuota;
                        }

                        objMemberVip.Save();

                        // Promotion Discount
                        query = "MemberVipId = '" + objMemberVip.MemberVipId.ToString() + "'";
                        MemberVipLineOfOperation objVipLoo = MemberVipLineOfOperation.LoadWhere(query);
                        if (objVipLoo != null)
                        {
                            if (chkPromotionDiscount.Checked)
                            {
                                objVipLoo.PromotionDiscount = Common.Utility.IsNumeric(txtPromotionDiscount.Text) ? Convert.ToDecimal(txtPromotionDiscount.Text) : objVipLoo.PromotionDiscount;
                            }

                            objVipLoo.Save();
                        }

                        result++;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the group range.
        /// </summary>
        /// <returns></returns>
        private List<Guid> GetShopRange()
        {
            List<Guid> shopRange = new List<Guid>();

            String query = String.Format("WorkplaceCode BETWEEN '{0}' AND '{1}",
                cboFromShop.Text.Substring(0, cboFromShop.Text.IndexOf('-')).Trim(), cboToShop.Text.Substring(0, cboToShop.Text.IndexOf('-')).Trim());
            DAL.WorkplaceCollection shopList = DAL.Workplace.LoadCollection(query, new string[] { "WorkplaceCode" }, true);
            for (int i = 0; i < shopList.Count; i++)
            {
                shopRange.Add(shopList[i].WorkplaceId);
            }

            return shopRange;
        }
    }
}