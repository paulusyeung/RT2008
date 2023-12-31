#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using RT2008.DAL;

#endregion

namespace RT2008.Supplier
{
    public partial class SupplierWizard : Form
    {
        SupplierWizard_General general = null;
        SupplierWizard_Personal personal = null;
        SupplierWizard_Contact contact = null;
        SupplierWizard_Financial financial = null;

        public SupplierWizard()
        {
            InitializeComponent();
            SetToolBar();
            TabCtrl();
            SetCtrlEditable();

            tabContact.Visible = false;
        }

        public SupplierWizard(System.Guid supplierId)
        {
            InitializeComponent();
            this.SupplierId = supplierId;
            SetToolBar();
            TabCtrl();
            SetCtrlEditable();
            LoadSupplierInfo();

            tabContact.Visible = false;
        }

        #region Properties
        private Guid supplierId = System.Guid.Empty;
        public Guid SupplierId
        {
            get
            {
                return supplierId;
            }
            set
            {
                supplierId = value;
            }
        }
        #endregion

        #region ToolBar
        private void SetToolBar()
        {
            this.tbWizardAction.MenuHandle = false;
            this.tbWizardAction.DragHandle = false;
            this.tbWizardAction.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            // cmdSave
            ToolBarButton cmdSave = new ToolBarButton("Save", "Save");
            cmdSave.Tag = "Save";
            cmdSave.Image = new IconResourceHandle("16x16.16_L_save.gif");
            cmdSave.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);

            this.tbWizardAction.Buttons.Add(cmdSave);

            // cmdSaveNew
            ToolBarButton cmdSaveNew = new ToolBarButton("Save & New", "Save & New");
            cmdSaveNew.Tag = "Save & New";
            cmdSaveNew.Image = new IconResourceHandle("16x16.16_L_saveOpen.gif");
            cmdSaveNew.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);

            this.tbWizardAction.Buttons.Add(cmdSaveNew);

            // cmdSaveClose
            ToolBarButton cmdSaveClose = new ToolBarButton("Save & Close", "Save & Close");
            cmdSaveClose.Tag = "Save & Close";
            cmdSaveClose.Image = new IconResourceHandle("16x16.16_saveClose.gif");
            cmdSaveClose.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);

            this.tbWizardAction.Buttons.Add(cmdSaveClose);
            this.tbWizardAction.Buttons.Add(sep);

            // cmdDelete
            ToolBarButton cmdDelete = new ToolBarButton("Delete", "Delete");
            cmdDelete.Tag = "Delete";
            cmdDelete.Image = new IconResourceHandle("16x16.16_L_remove.gif");

            if (SupplierId == System.Guid.Empty)
            {
                cmdDelete.Enabled = false;
            }
            else
            {
                cmdDelete.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Delete);
            }

            this.tbWizardAction.Buttons.Add(cmdDelete);

            this.tbWizardAction.ButtonClick += new ToolBarButtonClickEventHandler(tbWizardAction_ButtonClick);
        }

        void tbWizardAction_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "save":
                        MessageBox.Show("Save Record?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveMessageHandler));
                        break;
                    case "save & new":
                        MessageBox.Show("Save Record?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveNewMessageHandler));
                        break;
                    case "save & close":
                        MessageBox.Show("Save Record And Close?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveCloseMessageHandler));
                        break;
                    case "delete":
                        MessageBox.Show("Delete Record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(DeleteConfirmationHandler));
                        break;
                }
            }
        }
        #endregion

        #region Controls

        #region Supplier Code
        private void SetCtrlEditable()
        {
            txtSupplierCode.BackColor = (this.SupplierId == System.Guid.Empty) ? Color.LightSkyBlue : Color.LightYellow;
            txtSupplierCode.ReadOnly = (this.SupplierId != System.Guid.Empty);
        }
        #endregion

        #region Tab
        private void TabCtrl()
        {
            // General
            general = new SupplierWizard_General();
            general.Dock = DockStyle.Fill;
            general.SupplierId = this.SupplierId;
            tabGeneral.Controls.Add(general);

            // Personal
            personal = new SupplierWizard_Personal();
            personal.Dock = DockStyle.Fill;
            personal.SupplierId = this.SupplierId;
            tabPersonal.Controls.Add(personal);

            // Contact
            //contact = new SupplierWizard_Contact();
            //contact.Dock = DockStyle.Fill;
            //contact.SupplierId = this.SupplierId;
            //tabContact.Controls.Add(contact);

            // Financial
            financial = new SupplierWizard_Financial();
            financial.Dock = DockStyle.Fill;
            financial.SupplierId = this.SupplierId;
            tabFinancial.Controls.Add(financial);
        }
        #endregion

        #endregion

        #region Save Methods

        private bool Verify()
        {
            if (txtSupplierCode.Text == string.Empty)
            {
                errorProvider.SetError(txtSupplierCode, "Can not be blank!");
                tabGeneral.Select();
                return false;
            }
            else if (!Common.Utility.IsNumeric(financial.txtCreditLimit.Text.Replace(",", "")))
            {
                errorProvider.SetError(financial.txtCreditLimit, Resources.Common.DigitalNeeded);
                tabFinancial.Select();
                return false;
            }
            else if (!Common.Utility.IsNumeric(financial.txtNormalDiscount.Text.Replace(",", "")))
            {
                errorProvider.SetError(financial.txtNormalDiscount, Resources.Common.DigitalNeeded);
                tabFinancial.Select();
                return false;
            }
            else if (!Common.Utility.IsNumeric(financial.txtWholesalesDiscount.Text.Replace(",", "")))
            {
                errorProvider.SetError(financial.txtWholesalesDiscount, Resources.Common.DigitalNeeded);
                tabFinancial.Select();
                return false;
            }
            else if (!Common.Utility.IsNumeric(financial.txtQuotaDiscount.Text.Replace(",", "")))
            {
                errorProvider.SetError(financial.txtQuotaDiscount, Resources.Common.DigitalNeeded);
                tabFinancial.Select();
                return false;
            }
            else if (!Common.Utility.IsNumeric(financial.txtYearEndBonus.Text.Replace(",", "")))
            {
                errorProvider.SetError(financial.txtYearEndBonus, Resources.Common.DigitalNeeded);
                tabFinancial.Select();
                return false;
            }
            else if (!Common.Utility.IsNumeric(financial.txtCashDiscount.Text.Replace(",", "")))
            {
                errorProvider.SetError(financial.txtCashDiscount, Resources.Common.DigitalNeeded);
                tabFinancial.Select();
                return false;
            }
            else if (!Common.Utility.IsNumeric(financial.txtOthersDiscount.Text.Replace(",", "")))
            {
                errorProvider.SetError(financial.txtOthersDiscount, Resources.Common.DigitalNeeded);
                tabFinancial.Select();
                return false;
            }
            else
            {
                errorProvider.SetError(txtSupplierCode, string.Empty);
                errorProvider.SetError(financial.txtCreditLimit, string.Empty);
                errorProvider.SetError(financial.txtNormalDiscount, string.Empty);
                errorProvider.SetError(financial.txtWholesalesDiscount, string.Empty);
                errorProvider.SetError(financial.txtQuotaDiscount, string.Empty);
                errorProvider.SetError(financial.txtYearEndBonus, string.Empty);
                errorProvider.SetError(financial.txtCashDiscount, string.Empty);
                errorProvider.SetError(financial.txtOthersDiscount, string.Empty);
                return true;
            }
        }

        private bool CheckSupplierExists()
        {
            RT2008.DAL.Supplier supplier = RT2008.DAL.Supplier.LoadWhere("SupplierCode = '" + txtSupplierCode.Text.Trim() + "'");
            if (supplier != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Save()
        {
            if (Verify())
            {
                bool canSave = false, isNew = false;

                RT2008.DAL.Supplier oSupplier = RT2008.DAL.Supplier.Load(this.SupplierId);
                if (oSupplier == null)
                {
                    oSupplier = new RT2008.DAL.Supplier();

                    oSupplier.SupplierCode = txtSupplierCode.Text;

                    isNew = true;

                    oSupplier.Status = (int)Common.Enums.Status.Active;         //2014.01.04 paulus: 一開始就係 Active
                    oSupplier.CreatedBy = Common.Config.CurrentUserId;
                    oSupplier.CreatedOn = DateTime.Now;

                    canSave = CheckSupplierExists();
                    if (canSave)
                    {
                        errorProvider.SetError(txtSupplierCode, "Duplicated Code!");
                    }
                    else
                    {
                        errorProvider.SetError(txtSupplierCode, string.Empty);
                    }
                }
                oSupplier.SupplierInitial = general.txtInitial.Text;
                oSupplier.SupplierName = general.txtName.Text;
                oSupplier.SupplierName_Chs = general.txtNameChs.Text;
                oSupplier.SupplierName_Cht = general.txtNameCht.Text;
                oSupplier.AlternateSupplier = general.txtAltSupplierNum.Text;
                oSupplier.MarketSectorId = new Guid(general.cboMarketSector.SelectedValue.ToString());
                oSupplier.TermsId = new Guid(financial.cboTerms.SelectedValue.ToString());
                oSupplier.CreditAmount = Convert.ToDecimal((financial.txtCreditLimit.Text.Length == 0) ? "0" : financial.txtCreditLimit.Text);
                oSupplier.Remarks = general.txtRemarks.Text;
                oSupplier.NormalDiscount = Convert.ToDecimal((financial.txtNormalDiscount.Text.Length == 0) ? "0" : financial.txtNormalDiscount.Text);
                oSupplier.WholesaleDiscount = Convert.ToDecimal((financial.txtWholesalesDiscount.Text.Length == 0) ? "0" : financial.txtWholesalesDiscount.Text);
                oSupplier.QuotaDiscount = Convert.ToDecimal((financial.txtQuotaDiscount.Text.Length == 0) ? "0" : financial.txtQuotaDiscount.Text);
                oSupplier.YearEndDiscount = Convert.ToDecimal((financial.txtYearEndBonus.Text.Length == 0) ? "0" : financial.txtYearEndBonus.Text);
                oSupplier.CashDiscount = Convert.ToDecimal((financial.txtCashDiscount.Text.Length == 0) ? "0" : financial.txtCashDiscount.Text);
                oSupplier.OtherDiscount = Convert.ToDecimal((financial.txtOthersDiscount.Text.Length == 0) ? "0" : financial.txtOthersDiscount.Text);
                oSupplier.CurrencyCode = financial.cboCurrency.Text;

                if (!isNew)
                {
                    oSupplier.Status = Convert.ToInt32(Common.Enums.Status.Modified.ToString("d"));
                }

                oSupplier.ModifiedBy = Common.Config.CurrentUserId;
                oSupplier.ModifiedOn = DateTime.Now;

                if (!canSave)
                {
                    oSupplier.Save();

                    this.SupplierId = oSupplier.SupplierId;

                    SaveAddress(oSupplier.SupplierId);
                    SaveSmartTagValue(oSupplier.SupplierId);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void SaveAddress(Guid supplierId)
        {
            string sql = "SupplierId = '" + supplierId.ToString() + "' AND AddressTypeId = '" + personal.cboAddressType.SelectedValue.ToString() + "'";
            SupplierAddress oAddress = SupplierAddress.LoadWhere(sql);
            if (oAddress == null)
            {
                oAddress = new SupplierAddress();
                oAddress.SupplierId = supplierId;
                oAddress.AddressTypeId = new Guid(personal.cboAddressType.SelectedValue.ToString());
            }
            oAddress.Address = personal.txtAddress.Text;
            oAddress.PostalCode = personal.txtPostalCode.Text;
            oAddress.CountryId = new Guid(personal.cboCountry.SelectedValue.ToString());
            oAddress.ProvinceId = new Guid(personal.cboProvince.SelectedValue.ToString());
            oAddress.CityId = new Guid(personal.cboCity.SelectedValue.ToString());

            oAddress.Save();
        }

        private void SaveSmartTagValue(Guid supplierId)
        {
            string sql = "SupplierId = '{0}'  AND TagId = '{1}'";

            // Smart Tag 1
            SupplierSmartTag oTag1 = SupplierSmartTag.LoadWhere(string.Format(sql, supplierId.ToString(), general.txtSmartTag1.Tag.ToString()));
            if (oTag1 == null)
            {
                oTag1 = new SupplierSmartTag();
                oTag1.SupplierId = supplierId;
                oTag1.TagId = (general.txtSmartTag1.Tag == null) ? System.Guid.Empty : new System.Guid(general.txtSmartTag1.Tag.ToString());
            }
            oTag1.SmartTagValue = general.txtSmartTag1.Text;
            oTag1.Save();

            // Smart Tag 2
            SupplierSmartTag oTag2 = SupplierSmartTag.LoadWhere(string.Format(sql, supplierId.ToString(), general.txtSmartTag2.Tag.ToString()));
            if (oTag2 == null)
            {
                oTag2 = new SupplierSmartTag();
                oTag2.SupplierId = supplierId;
                oTag2.TagId = (general.txtSmartTag2.Tag == null) ? System.Guid.Empty : new System.Guid(general.txtSmartTag2.Tag.ToString());
            }
            oTag2.SmartTagValue = general.txtSmartTag2.Text;
            oTag2.Save();

            // Smart Tag 3
            SupplierSmartTag oTag3 = SupplierSmartTag.LoadWhere(string.Format(sql, supplierId.ToString(), general.txtSmartTag3.Tag.ToString()));
            if (oTag3 == null)
            {
                oTag3 = new SupplierSmartTag();
                oTag3.SupplierId = supplierId;
                oTag3.TagId = (general.txtSmartTag3.Tag == null) ? System.Guid.Empty : new System.Guid(general.txtSmartTag3.Tag.ToString());
            }
            oTag3.SmartTagValue = general.txtSmartTag3.Text;
            oTag3.Save();

            // Smart Tag 4
            SupplierSmartTag oTag4 = SupplierSmartTag.LoadWhere(string.Format(sql, supplierId.ToString(), general.txtSmartTag4.Tag.ToString()));
            if (oTag4 == null)
            {
                oTag4 = new SupplierSmartTag();
                oTag4.SupplierId = supplierId;
                oTag4.TagId = (general.txtSmartTag4.Tag == null) ? System.Guid.Empty : new System.Guid(general.txtSmartTag4.Tag.ToString());
            }
            oTag4.SmartTagValue = general.txtSmartTag4.Text;
            oTag4.Save();

            // Smart Tag 5
            SupplierSmartTag oTag5 = SupplierSmartTag.LoadWhere(string.Format(sql, supplierId.ToString(), personal.txtSmartTag5.Tag.ToString()));
            if (oTag5 == null)
            {
                oTag5 = new SupplierSmartTag();
                oTag5.SupplierId = supplierId;
                oTag5.TagId = (personal.txtSmartTag5.Tag == null) ? System.Guid.Empty : new System.Guid(personal.txtSmartTag5.Tag.ToString());
            }
            oTag5.SmartTagValue = personal.txtSmartTag5.Text;
            oTag5.Save();

            // Smart Tag 6
            SupplierSmartTag oTag6 = SupplierSmartTag.LoadWhere(string.Format(sql, supplierId.ToString(), personal.txtSmartTag6.Tag.ToString()));
            if (oTag6 == null)
            {
                oTag6 = new SupplierSmartTag();
                oTag6.SupplierId = supplierId;
                oTag6.TagId = (personal.txtSmartTag6.Tag == null) ? System.Guid.Empty : new System.Guid(personal.txtSmartTag6.Tag.ToString());
            }
            oTag6.SmartTagValue = personal.txtSmartTag6.Text;
            oTag6.Save();
        }
        #endregion

        #region Load Methods
        private void LoadSupplierInfo()
        {
            LoadInfo();
        }

        private void LoadInfo()
        {
            RT2008.DAL.Supplier oSupplier = RT2008.DAL.Supplier.Load(this.SupplierId);
            if (oSupplier != null)
            {
                txtSupplierCode.Text = oSupplier.SupplierCode;
                general.txtInitial.Text = oSupplier.SupplierInitial;
                general.txtName.Text = oSupplier.SupplierName;
                general.txtNameChs.Text = oSupplier.SupplierName_Chs;
                general.txtNameCht.Text = oSupplier.SupplierName_Cht;
                general.txtAltSupplierNum.Text = oSupplier.AlternateSupplier;
                general.cboMarketSector.SelectedValue = oSupplier.MarketSectorId;
                financial.cboTerms.SelectedValue = oSupplier.TermsId;
                financial.txtCreditLimit.Text = oSupplier.CreditAmount.ToString("n2");
                general.txtRemarks.Text = oSupplier.Remarks;
                financial.txtNormalDiscount.Text = oSupplier.NormalDiscount.ToString("n2");
                financial.txtWholesalesDiscount.Text = oSupplier.WholesaleDiscount.ToString("n2");
                financial.txtQuotaDiscount.Text = oSupplier.QuotaDiscount.ToString("n2");
                financial.txtYearEndBonus.Text = oSupplier.YearEndDiscount.ToString("n2");
                financial.txtCashDiscount.Text = oSupplier.CashDiscount.ToString("n2");
                financial.txtOthersDiscount.Text = oSupplier.OtherDiscount.ToString("n2");
                financial.cboCurrency.Text = oSupplier.CurrencyCode;
                financial.txtBFAmount.Text = oSupplier.BFBAL.ToString("n2");
                financial.txtCDAmount.Text = oSupplier.CDBAL.ToString("n2");
                financial.txtLastPurchasedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oSupplier.DateLastPurchase, false);
                financial.txtLastPaidOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oSupplier.DateLastPay, false);
                financial.txtLastReturnedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oSupplier.DateLastReturn, false);

                general.txtLastUpdatedBy.Text = GetStaffName(oSupplier.ModifiedBy);
                general.txtLastUpdatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oSupplier.ModifiedOn, false);
                general.txtCreatedon.Text = RT2008.SystemInfo.Settings.DateTimeToString(oSupplier.CreatedOn, false);

                LoadAddress();
                LoadSmartTag();
            }
        }

        private string GetStaffName(Guid staffId)
        {
            RT2008.DAL.Staff oStaff = RT2008.DAL.Staff.Load(staffId);
            if (oStaff != null)
            {
                return oStaff.StaffNumber;
            }
            else
            {
                return string.Empty;
            }
        }

        private void LoadAddress()
        {
            string sql = "SupplierId = '" + this.SupplierId.ToString() + "'";
            SupplierAddressCollection oAddressList = SupplierAddress.LoadCollection(sql);
            if (oAddressList.Count > 0)
            {
                personal.cboAddressType.SelectedValue = oAddressList[0].AddressTypeId;
                personal.txtAddress.Text = oAddressList[0].Address;
                personal.txtPostalCode.Text = oAddressList[0].PostalCode;
                personal.cboCountry.SelectedValue = oAddressList[0].CountryId;
                personal.cboProvince.SelectedValue = oAddressList[0].ProvinceId;
                personal.cboCity.SelectedValue = oAddressList[0].CityId;
            }
        }

        private void LoadSmartTag()
        {
            string sql = "SupplierId = '" + this.SupplierId.ToString() + "'  AND TagId = '{0}'";

            SupplierSmartTag oTag1 = SupplierSmartTag.LoadWhere(string.Format(sql, general.txtSmartTag1.Tag.ToString()));
            if (oTag1 != null)
            {
                general.txtSmartTag1.Text = oTag1.SmartTagValue;
            }

            SupplierSmartTag oTag2 = SupplierSmartTag.LoadWhere(string.Format(sql, general.txtSmartTag2.Tag.ToString()));
            if (oTag2 != null)
            {
                general.txtSmartTag2.Text = oTag2.SmartTagValue;
            }

            SupplierSmartTag oTag3 = SupplierSmartTag.LoadWhere(string.Format(sql, general.txtSmartTag3.Tag.ToString()));
            if (oTag3 != null)
            {
                general.txtSmartTag3.Text = oTag3.SmartTagValue;
            }

            SupplierSmartTag oTag4 = SupplierSmartTag.LoadWhere(string.Format(sql, general.txtSmartTag4.Tag.ToString()));
            if (oTag4 != null)
            {
                general.txtSmartTag4.Text = oTag4.SmartTagValue;
            }

            SupplierSmartTag oTag5 = SupplierSmartTag.LoadWhere(string.Format(sql, personal.txtSmartTag5.Tag.ToString()));
            if (oTag5 != null)
            {
                personal.txtSmartTag5.Text = oTag5.SmartTagValue;
            }

            SupplierSmartTag oTag6 = SupplierSmartTag.LoadWhere(string.Format(sql, personal.txtSmartTag6.Tag.ToString()));
            if (oTag6 != null)
            {
                personal.txtSmartTag6.Text = oTag6.SmartTagValue;
            }
        }
        #endregion

        #region Delete
        private void Delete()
        {
            RT2008.DAL.Supplier oSupplier = RT2008.DAL.Supplier.Load(this.SupplierId);
            if (oSupplier != null)
            {
                switch ((int)oSupplier.Status)
                {
                    case (int)Common.Enums.Status.Active:       //2014.01.04 paulus: 如果 Supplier.Status = Active 設為 Deleted + Retired
                        oSupplier.Status = Convert.ToInt32(Common.Enums.Status.Deleted.ToString("d"));
                        oSupplier.Retired = true;
                        oSupplier.RetiredOn = DateTime.Now;
                        oSupplier.RetiredBy = Common.Config.CurrentUserId;
                        oSupplier.Save();
                        break;
                    case (int)Common.Enums.Status.Draft:        //2014.01.04 paulus: 如果 Supplier.Status = Draft 可以直接刪除
                        string sql = "SupplierId = '" + oSupplier.SupplierId.ToString() + "'";

                        DeleteSmartTags(sql);
                        DeleteContact(sql);
                        DeleteAddress(sql);

                        oSupplier.Delete();
                        break;
                }
            }
        }

        private void DeleteSmartTags(string sql)
        {
            SupplierSmartTagCollection oSmartTagList = SupplierSmartTag.LoadCollection(sql);
            foreach (SupplierSmartTag oSmartTag in oSmartTagList)
            {
                oSmartTag.Delete();
            }
        }

        private void DeleteContact(string sql)
        {
            SupplierContactCollection oContactList = SupplierContact.LoadCollection(sql);
            foreach (SupplierContact oContact in oContactList)
            {
                oContact.Delete();
            }
        }

        private void DeleteAddress(string sql)
        {
            SupplierAddressCollection oAddressList = SupplierAddress.LoadCollection(sql);
            foreach (SupplierAddress oAddress in oAddressList)
            {
                oAddress.Delete();
            }
        }
        #endregion

        private void SaveMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save())
                {
                    if (this.SupplierId != System.Guid.Empty)
                    {
                        RT2008.SystemInfo.Settings.RefreshMainList<DefaultList>();
                        MessageBox.Show("Success!", "Save Result");

                        this.Close();
                        SupplierWizard wizard = new SupplierWizard(this.SupplierId);
                        wizard.ShowDialog();
                    }
                }
            }
        }

        private void SaveNewMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save())
                {
                    if (this.SupplierId != System.Guid.Empty)
                    {
                        RT2008.SystemInfo.Settings.RefreshMainList<DefaultList>();
                        this.Close();
                        SupplierWizard wizard = new SupplierWizard();
                        wizard.ShowDialog();
                    }
                }
            }
        }

        private void SaveCloseMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save())
                {
                    if (this.SupplierId != System.Guid.Empty)
                    {
                        RT2008.SystemInfo.Settings.RefreshMainList<DefaultList>();
                        this.Close();
                    }
                }
            }
        }

        private void DeleteConfirmationHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                Delete();

                this.Close();
            }
        }
    }
}