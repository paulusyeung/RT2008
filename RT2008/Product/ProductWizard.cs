﻿#region Using

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
using System.IO;

#endregion

namespace RT2008.Product
{
    public partial class ProductWizard : Form
    {
        ProductWizard_General general;
        ProductWizard_Barcode barcode;
        ProductWizard_Quantity quantity;
        ProductWizard_Misc misc;
        ProductWizard_Order order;
        ProductWizard_Discount discount;

        public ProductWizard()
        {
            InitializeComponent();
            //this.Closing += new System.ComponentModel.CancelEventHandler(this.ProductWizard_Closing);
            SetToolBar();
            TabCtrl();
            SetCtrlEditable();
            FillAppendixes();
        }

        public ProductWizard(System.Guid productId)
        {
            InitializeComponent();
            this.ProductId = productId;
            SetToolBar();
            TabCtrl();
            SetCtrlEditable();
            LoadProductInfo();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetSystemLabels();
        }

        #region Set System label
        private void SetSystemLabels()
        {
            lblStkCode.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE");
            lblAppendix1.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1");
            lblAppendix2.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2");
            lblAppendix3.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3");
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

            if (ProductId == System.Guid.Empty)
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

        #region STKCODE / Appendix
        private void SetCtrlEditable()
        {
            txtStkCode.BackColor = (this.ProductId == System.Guid.Empty) ? Color.LightSkyBlue : Color.LightYellow;
            txtStkCode.Enabled = (this.ProductId == System.Guid.Empty);

            cboAppendix1.BackColor = (this.ProductId == System.Guid.Empty) ? Color.LightSkyBlue : Color.LightYellow;
            cboAppendix1.Enabled = (this.ProductId == System.Guid.Empty);

            cboAppendix2.BackColor = (this.ProductId == System.Guid.Empty) ? Color.LightSkyBlue : Color.LightYellow;
            cboAppendix2.Enabled = (this.ProductId == System.Guid.Empty);

            cboAppendix3.BackColor = (this.ProductId == System.Guid.Empty) ? Color.LightSkyBlue : Color.LightYellow;
            cboAppendix3.Enabled = (this.ProductId == System.Guid.Empty);
        }
        #endregion

        #region Tab
        private void TabCtrl()
        {
            // General
            general = new ProductWizard_General();
            general.Dock = DockStyle.Fill;
            general.ProductId = this.ProductId;
            tpGeneral.Controls.Add(general);

            // Barcode
            barcode = new ProductWizard_Barcode(this.ProductId);

            // Quantity
            quantity = new ProductWizard_Quantity(this.ProductId);

            // Misc
            misc = new ProductWizard_Misc(this.ProductId);

            // Order
            order = new ProductWizard_Order(this.ProductId);

            // Discount
            discount = new ProductWizard_Discount(this.ProductId);
        }
        #endregion

        #endregion

        #region Appendix
        private void FillAppendixes()
        {
            FillAppendixe1();
            FillAppendixe2();
            FillAppendixe3();
        }

        private void FillAppendixe1()
        {
            cboAppendix1.Items.Clear();

            string[] orderBy = new string[] { "Appendix1Code" };
            ProductAppendix1Collection oA1List = ProductAppendix1.LoadCollection(orderBy, true);
            oA1List.Add(new ProductAppendix1());
            cboAppendix1.DataSource = oA1List;
            cboAppendix1.DisplayMember = "Appendix1Code";
            cboAppendix1.ValueMember = "Appendix1Id";
        }

        private void FillAppendixe2()
        {
            cboAppendix2.Items.Clear();

            string[] orderBy = new string[] { "Appendix2Code" };
            ProductAppendix2Collection oA2List = ProductAppendix2.LoadCollection(orderBy, true);
            oA2List.Add(new ProductAppendix2());
            cboAppendix2.DataSource = oA2List;
            cboAppendix2.DisplayMember = "Appendix2Code";
            cboAppendix2.ValueMember = "Appendix2Id";
        }

        private void FillAppendixe3()
        {
            cboAppendix3.Items.Clear();

            string[] orderBy = new string[] { "Appendix3Code" };
            ProductAppendix3Collection oA3List = ProductAppendix3.LoadCollection(orderBy, true);
            oA3List.Add(new ProductAppendix3());
            cboAppendix3.DataSource = oA3List;
            cboAppendix3.DisplayMember = "Appendix3Code";
            cboAppendix3.ValueMember = "Appendix3Id";
        }
        #endregion

        #region Properties
        private Guid productId = System.Guid.Empty;
        public Guid ProductId
        {
            get
            {
                return productId;
            }
            set
            {
                productId = value;
            }
        }
        #endregion

        #region Actions Methods
        private bool VerifyAppendix1()
        {
            if (cboAppendix1.Text.Trim().Length > 0)
            {
                string sql = "Appendix1Code = '" + cboAppendix1.Text.Trim() + "'";
                ProductAppendix1 a1 = ProductAppendix1.LoadWhere(sql);
                if (a1 == null)
                {
                    errorProvider.SetError(cboAppendix1, "The code is invalid! Try to select a value from the list!");
                    return false;
                }
                else
                {
                    errorProvider.SetError(cboAppendix1, string.Empty);
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool VerifyAppendix2()
        {
            if (cboAppendix2.Text.Trim().Length > 0)
            {
                string sql = "Appendix2Code = '" + cboAppendix2.Text.Trim() + "'";
                ProductAppendix2 a2 = ProductAppendix2.LoadWhere(sql);
                if (a2 == null)
                {
                    errorProvider.SetError(cboAppendix2, "The code is invalid! Try to select a value from the list!");
                    return false;
                }
                else
                {
                    errorProvider.SetError(cboAppendix2, string.Empty);
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool VerifyAppendix3()
        {
            if (cboAppendix3.Text.Trim().Length > 0)
            {
                string sql = "Appendix3Code = '" + cboAppendix3.Text.Trim() + "'";
                ProductAppendix3 a3 = ProductAppendix3.LoadWhere(sql);
                if (a3 == null)
                {
                    errorProvider.SetError(cboAppendix3, "The code is invalid! Try to select a value from the list!");
                    return false;
                }
                else
                {
                    errorProvider.SetError(cboAppendix3, string.Empty);
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool VerifyAppendix()
        {
            bool result = true;

            result = result & VerifyAppendix1();
            result = result & VerifyAppendix2();
            result = result & VerifyAppendix3();

            return result;
        }

        private bool Verify()
        {
            if (txtStkCode.Text == string.Empty)
            {
                errorProvider.SetError(txtStkCode, "Can not be blank!");
                return false;
            }
            else if (!VerifyAppendix())
            {
                return false;
            }
            else
            {
                errorProvider.SetError(txtStkCode, string.Empty);
                return true;
            }
        }

        private bool VerifyDuplicated()
        {
            string sql = "STKCODE = '" + txtStkCode.Text + "' AND APPENDIX1 = '" + cboAppendix1.Text + "' AND APPENDIX2 = '" + cboAppendix2.Text + "' AND APPENDIX3 = '" + cboAppendix3.Text + "'";
            RT2008.DAL.Product oItem = RT2008.DAL.Product.LoadWhere(sql);
            if (oItem != null)
            {
                MessageBox.Show(string.Format(Resources.Common.DuplicatedCode, "Stock Code + Appendix1 + Appendix2 + Appendix3"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Save Product Info
        private bool Save()
        {
            bool result = false;

            if (Verify())
            {
                this.ProductId = SaveGeneralInfo();
                if (this.ProductId != System.Guid.Empty)
                {
                    SaveProductCurrentSummary(this.ProductId);

                    SaveQtyInfo();
                    SaveOrderInfo();
                    SaveDiscountInfo();

                    result = true;
                }
            }

            return result;
        }

        private Guid SaveGeneralInfo()
        {
            bool isNew = false;

            RT2008.DAL.Product oItem = RT2008.DAL.Product.Load(this.ProductId);
            if (oItem == null)
            {
                oItem = new RT2008.DAL.Product();

                oItem.STKCODE = txtStkCode.Text.Trim();
                oItem.APPENDIX1 = cboAppendix1.Text.Trim();
                oItem.APPENDIX2 = cboAppendix2.Text.Trim();
                oItem.APPENDIX3 = cboAppendix3.Text.Trim();

                oItem.Status = Convert.ToInt32(Common.Enums.Status.Active.ToString("d"));
                isNew = true;

                oItem.CreatedBy = Common.Config.CurrentUserId;
                oItem.CreatedOn = DateTime.Now;

                if (VerifyDuplicated())
                {
                    return System.Guid.Empty;
                }
            }

            oItem.CLASS1 = general.cboClass1.Text;
            oItem.CLASS2 = general.cboClass2.Text;
            oItem.CLASS3 = general.cboClass3.Text;
            oItem.CLASS4 = general.cboClass4.Text;
            oItem.CLASS5 = general.cboClass5.Text;
            oItem.CLASS6 = general.cboClass6.Text;

            oItem.ProductName = general.txtProductName.Text;
            oItem.ProductName_Chs = general.txtProductNameChs.Text;
            oItem.ProductName_Cht = general.txtProductNameCht.Text;
            oItem.Remarks = general.txtRemarks.Text;

            oItem.NormalDiscount = Convert.ToDecimal((general.txtRetailDiscount.Text == string.Empty) ? "0" : general.txtRetailDiscount.Text);
            oItem.UOM = general.txtUnit.Text;
            oItem.NatureId = new Guid(general.cboNature.SelectedValue.ToString());

            oItem.FixedPriceItem = discount.chkFixedPrice.Checked;

            // Price 
            oItem.RetailPrice = Convert.ToDecimal((general.txtCurrentRetailPrice.Text == string.Empty) ? "0" : general.txtCurrentRetailPrice.Text);
            oItem.WholesalePrice = Convert.ToDecimal((general.txtWholesalesPrice.Text == string.Empty) ? "0" : general.txtWholesalesPrice.Text);
            oItem.OriginalRetailPrice = Convert.ToDecimal((general.txtOriginalRetailPrice.Text == string.Empty) ? "0" : general.txtOriginalRetailPrice.Text);
            //oItem.Markup = Convert.ToDecimal((general.txtVendorPrice.Text == string.Empty) ? "0" : general.txtVendorPrice.Text);

            // Download Packets
            oItem.DownloadToPOS = general.chkRetailItem.Checked;
            oItem.DownloadToCounter = general.chkCounterItem.Checked;

            // If the item existed, change the status to Modified.
            if (!isNew)
            {
                oItem.Status = Convert.ToInt32(Common.Enums.Status.Modified.ToString("d"));
            }

            oItem.ModifiedBy = Common.Config.CurrentUserId;
            oItem.ModifiedOn = DateTime.Now;
            oItem.Save();

            if (isNew)
            {// log activity (New Record)
                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Create, oItem.ToString());
            }
            else
            { // log activity (Update)
                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Update, oItem.ToString());
            }     

            SaveProductBarcode(oItem);

            // Appendix / Class
            System.Guid a1Id = (cboAppendix1.SelectedValue != null) ? new Guid(cboAppendix1.SelectedValue.ToString()) : System.Guid.Empty;
            System.Guid a2Id = (cboAppendix2.SelectedValue != null) ? new Guid(cboAppendix2.SelectedValue.ToString()) : System.Guid.Empty;
            System.Guid a3Id = (cboAppendix3.SelectedValue != null) ? new Guid(cboAppendix3.SelectedValue.ToString()) : System.Guid.Empty;

            System.Guid c1Id = (general.cboClass1.SelectedValue != null) ? new Guid(general.cboClass1.SelectedValue.ToString()) : System.Guid.Empty;
            System.Guid c2Id = (general.cboClass2.SelectedValue != null) ? new Guid(general.cboClass2.SelectedValue.ToString()) : System.Guid.Empty;
            System.Guid c3Id = (general.cboClass3.SelectedValue != null) ? new Guid(general.cboClass3.SelectedValue.ToString()) : System.Guid.Empty;
            System.Guid c4Id = (general.cboClass4.SelectedValue != null) ? new Guid(general.cboClass4.SelectedValue.ToString()) : System.Guid.Empty;
            System.Guid c5Id = (general.cboClass5.SelectedValue != null) ? new Guid(general.cboClass5.SelectedValue.ToString()) : System.Guid.Empty;
            System.Guid c6Id = (general.cboClass6.SelectedValue != null) ? new Guid(general.cboClass6.SelectedValue.ToString()) : System.Guid.Empty;
            SaveProductCode(oItem.ProductId, a1Id, a2Id, a3Id, c1Id, c2Id, c3Id, c4Id, c5Id, c6Id);

            // Product Barcode
            barcode.AddBarcode();

            // Product Price
            SaveProductSupplement(oItem.ProductId);
            SaveProductPrice(oItem.ProductId);

            // Remarks
            SaveProductRemarks(oItem.ProductId);

            return oItem.ProductId;
        }

        private void SaveProductCurrentSummary(Guid productId)
        {
            string where = "ProductId = '" + productId.ToString() + "'";
            ProductCurrentSummary oCurrSummary = ProductCurrentSummary.LoadWhere(where);
            if (oCurrSummary == null)
            {
                oCurrSummary = new ProductCurrentSummary();
                oCurrSummary.ProductId = productId;
                oCurrSummary.CDQTY = 0;
                oCurrSummary.LastPurchasedOn = new DateTime(1900, 1, 1);
                oCurrSummary.LastSoldOn = new DateTime(1900, 1, 1);
                oCurrSummary.Save();
            }
        }

        private void SaveProductBarcode(RT2008.DAL.Product oItem)
        {
            string stkcode = oItem.STKCODE;

            if (oItem.STKCODE.Length > 10)
            {
                stkcode = oItem.STKCODE.Remove(10);
            }

            string barcode = stkcode + oItem.APPENDIX1 + oItem.APPENDIX2 + oItem.APPENDIX3;
            string sql = "ProductId = '" + oItem.ProductId.ToString() + "' AND Barcode = '" + barcode + "'";
            ProductBarcode oBarcode = ProductBarcode.LoadWhere(sql);
            if (oBarcode == null)
            {
                oBarcode = new ProductBarcode();
                oBarcode.ProductId = oItem.ProductId;
                oBarcode.Barcode = barcode;
                oBarcode.BarcodeType = "INTER";
                oBarcode.PrimaryBarcode = true;
                oBarcode.DownloadToPOS = general.chkRetailItem.Checked;
                oBarcode.DownloadToCounter = general.chkCounterItem.Checked;

                oBarcode.Save();
            }
        }

        private void SaveQtyInfo()
        {
            RT2008.DAL.Product oItem = RT2008.DAL.Product.Load(this.ProductId);
            if (oItem != null)
            {
                oItem.MaxOnLoanQty = Convert.ToDecimal((quantity.txtMaxOLNQty.Text == string.Empty) ? "0" : quantity.txtMaxOLNQty.Text);

                oItem.ModifiedBy = Common.Config.CurrentUserId;
                oItem.ModifiedOn = DateTime.Now;
                oItem.Save();
            }
        }

        private void SaveOrderInfo()
        {
            RT2008.DAL.Product oItem = RT2008.DAL.Product.Load(this.ProductId);
            if (oItem != null)
            {
                oItem.AlternateItem = order.txtVendorItemNum.Text; // Vendor Item Number
                oItem.ReorderLevel = Convert.ToDecimal((order.txtReorderLevel.Text == string.Empty) ? "0" : order.txtReorderLevel.Text);
                oItem.ReorderQty = Convert.ToDecimal((order.txtReorderQuantity.Text == string.Empty) ? "0" : order.txtReorderQuantity.Text);

                oItem.ModifiedBy = Common.Config.CurrentUserId;
                oItem.ModifiedOn = DateTime.Now;
                oItem.Save();
            }
        }

        private void SaveDiscountInfo()
        {
            SaveProductSupplement(this.ProductId);
        }

        private void SaveProductCode(Guid productId, Guid a1Id, Guid a2Id, Guid a3Id, Guid c1Id, Guid c2Id, Guid c3Id, Guid c4Id, Guid c5Id, Guid c6Id)
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductCode oCode = ProductCode.LoadWhere(sql);
            if (oCode == null)
            {
                oCode = new ProductCode();

                oCode.ProductId = productId;
                oCode.Appendix1Id = a1Id;
                oCode.Appendix2Id = a2Id;
                oCode.Appendix3Id = a3Id;
            }
            oCode.Class1Id = c1Id;
            oCode.Class2Id = c2Id;
            oCode.Class3Id = c3Id;
            oCode.Class4Id = c4Id;
            oCode.Class5Id = c5Id;
            oCode.Class6Id = c6Id;
            oCode.Save();
        }

        private void SaveProductSupplement(Guid productId)
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductSupplement oProdSupp = ProductSupplement.LoadWhere(sql);
            if (oProdSupp == null)
            {
                oProdSupp = new ProductSupplement();

                oProdSupp.ProductId = productId;
            }
            oProdSupp.VendorCurrencyCode = general.cboVendorCurrency.Text;
            oProdSupp.VendorPrice = Convert.ToDecimal((general.txtVendorPrice.Text == string.Empty) ? "0" : general.txtVendorPrice.Text);
            oProdSupp.ProductName_Memo = general.txtMemo.Text;
            oProdSupp.ProductName_Pole = general.txtPole.Text;

            oProdSupp.VipDiscount_FixedItem = Convert.ToDecimal((discount.txtDiscount1_FixPriceItem.Text == string.Empty) ? "0" : discount.txtDiscount1_FixPriceItem.Text);
            oProdSupp.VipDiscount_DiscountItem = Convert.ToDecimal((discount.txtDiscount2_DiscountItem.Text == string.Empty) ? "0" : discount.txtDiscount2_DiscountItem.Text);
            oProdSupp.VipDiscount_NoDiscountItem = Convert.ToDecimal((discount.txtDiscount3_NoDiscountItem.Text == string.Empty) ? "0" : discount.txtDiscount3_NoDiscountItem.Text);
            oProdSupp.StaffDiscount = Convert.ToDecimal((discount.txtStaff.Text == string.Empty) ? "0" : discount.txtStaff.Text);

            oProdSupp.Save();
        }

        private void SaveProductPrice(Guid productId)
        {
            SaveProductPrice(productId, Common.Enums.ProductPriceType.BASPRC.ToString(), general.txtCurrentRetailCurrency.Text, general.txtCurrentRetailPrice.Text);
            SaveProductPrice(productId, Common.Enums.ProductPriceType.ORIPRC.ToString(), general.txtOriginalRetailCurrency.Text, general.txtOriginalRetailPrice.Text);
            SaveProductPrice(productId, Common.Enums.ProductPriceType.VPRC.ToString(), general.cboVendorCurrency.Text, general.txtVendorPrice.Text);
            SaveProductPrice(productId, Common.Enums.ProductPriceType.WHLPRC.ToString(), general.txtWholesalesCurrency.Text, general.txtWholesalesPrice.Text);
        }

        private void SaveProductPrice(Guid productId, string priceType, string currencyCode, string price)
        {
            string sql = "ProductId = '" + productId.ToString() + "' AND PriceTypeId = '" + GetPriceType(priceType).ToString() + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice == null)
            {
                oPrice = new ProductPrice();

                oPrice.ProductId = productId;
            }
            oPrice.PriceTypeId = GetPriceType(priceType);
            oPrice.CurrencyCode = currencyCode;
            oPrice.Price = Convert.ToDecimal((price == string.Empty) ? "0" : price);
            oPrice.Save();
        }

        private Guid GetPriceType(string priceType)
        {
            string sql = "PriceType = '" + priceType + "'";
            ProductPriceType oType = ProductPriceType.LoadWhere(sql);
            if (oType == null)
            {
                oType = new ProductPriceType();

                oType.PriceType = priceType;
                oType.CurrencyCode = "HKD";
                oType.CoreSystemPrice = false;

                oType.Save();
            }
            return oType.PriceTypeId;
        }

        private void SaveProductRemarks(Guid productId)
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductRemarks oRemarks = ProductRemarks.LoadWhere(sql);
            if (oRemarks == null)
            {
                oRemarks = new ProductRemarks();

                oRemarks.ProductId = productId;
            }
            oRemarks.BinX = general.txtBin_X.Text;
            oRemarks.BinY = general.txtBin_Y.Text;
            oRemarks.BinZ = general.txtBin_Z.Text;

            oRemarks.DownloadToShop = general.chkRetailItem.Checked;
            oRemarks.OffDisplayItem = general.chkOffDisplayItem.Checked;
            oRemarks.DownloadToCounter = general.chkCounterItem.Checked;

            oRemarks.REMARK1 = general.txtRemarks1.Text;
            oRemarks.REMARK2 = general.txtRemarks2.Text;
            oRemarks.REMARK3 = general.txtRemarks3.Text;
            oRemarks.REMARK4 = general.txtRemarks4.Text;
            oRemarks.REMARK5 = general.txtRemarks5.Text;
            oRemarks.REMARK6 = general.txtRemarks6.Text;

            oRemarks.Notes = misc.txtMemo.Text;

            if (string.IsNullOrEmpty(oRemarks.Photo))
            {
                oRemarks.Photo = misc.txtPicFileName.Text;
            }
            else if (oRemarks.Photo != misc.txtPicFileName.Text)
            {
                oRemarks.Photo5 = oRemarks.Photo4;
                oRemarks.Photo4 = oRemarks.Photo3;
                oRemarks.Photo3 = oRemarks.Photo2;
                oRemarks.Photo2 = oRemarks.Photo;
                oRemarks.Photo = misc.txtPicFileName.Text;
            }

            oRemarks.Save();
        }
        #endregion

        #endregion

        #region Loading Product Info
        private void LoadProductInfo()
        {
            LoadGeneralInfo();
            LoadProductRemarks();
            LoadProductSupplement();
        }

        private void LoadGeneralInfo()
        {
            RT2008.DAL.Product oItem = RT2008.DAL.Product.Load(this.ProductId);
            if (oItem != null)
            {
                txtStkCode.Text = oItem.STKCODE;
                cboAppendix1.Text = oItem.APPENDIX1;
                cboAppendix2.Text = oItem.APPENDIX2;
                cboAppendix3.Text = oItem.APPENDIX3;

                general.cboClass1.Text = oItem.CLASS1;
                general.cboClass2.Text = oItem.CLASS2;
                general.cboClass3.Text = oItem.CLASS3;
                general.cboClass4.Text = oItem.CLASS4;
                general.cboClass5.Text = oItem.CLASS5;
                general.cboClass6.Text = oItem.CLASS6;

                general.txtProductName.Text = oItem.ProductName;
                general.txtProductNameChs.Text = oItem.ProductName_Chs;
                general.txtProductNameCht.Text = oItem.ProductName_Cht;
                general.txtRemarks.Text = oItem.Remarks;

                general.txtWholesalesPrice.Text = oItem.WholesalePrice.ToString("n2");
                general.txtOriginalRetailPrice.Text = oItem.OriginalRetailPrice.ToString("n2");
                general.txtCurrentRetailPrice.Text = oItem.RetailPrice.ToString("n2");
                general.txtRetailDiscount.Text = oItem.NormalDiscount.ToString("n2");
                general.txtUnit.Text = oItem.UOM;
                general.cboNature.SelectedValue = oItem.NatureId;

                discount.chkFixedPrice.Checked = oItem.FixedPriceItem;

                general.txtStatus_Counter.Text = "";
                general.txtStatus_Office.Text = "";
                general.txtCreatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oItem.CreatedOn, false);
                general.txtModifiedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oItem.ModifiedOn, false);
                general.txtModifiedBy.Text = GetStaffName(oItem.ModifiedBy);

                // Quantity Info
                quantity.txtMaxOLNQty.Text = oItem.MaxOnLoanQty.ToString("n0");

                // Order Info
                order.txtVendorItemNum.Text = oItem.AlternateItem; // Vendor Item Number
                order.txtReorderLevel.Text = oItem.ReorderLevel.ToString("n0");
                order.txtReorderQuantity.Text = oItem.ReorderQty.ToString("n0");

                // Product Price
                LoadProductPrice();

                general.txtCurrentRetailPrice.Text = oItem.RetailPrice.ToString("n2");
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

        // Product Price
        private void LoadProductSupplement()
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductSupplement oProdSupp = ProductSupplement.LoadWhere(sql);
            if (oProdSupp != null)
            {
                general.cboVendorCurrency.Text = oProdSupp.VendorCurrencyCode;
                general.txtVendorPrice.Text = oProdSupp.VendorPrice.ToString("n2");
                general.txtMemo.Text = oProdSupp.ProductName_Memo;
                general.txtPole.Text = oProdSupp.ProductName_Pole;

                discount.txtDiscount1_FixPriceItem.Text = oProdSupp.VipDiscount_FixedItem.ToString("n2");
                discount.txtDiscount2_DiscountItem.Text = oProdSupp.VipDiscount_DiscountItem.ToString("n2");
                discount.txtDiscount3_NoDiscountItem.Text = oProdSupp.VipDiscount_NoDiscountItem.ToString("n2");
                discount.txtStaff.Text = oProdSupp.StaffDiscount.ToString("n2");
            }
        }

        private void LoadProductPrice()
        {
            LoadProductBasicPrice();
            LoadProductOriginalPrice();
            LoadProductVendorPrice();
            LoadProductWholesalesPrice();
        }

        private void LoadProductBasicPrice()
        {
            string sql = "ProductId = '" + this.ProductId.ToString() + "' AND PriceTypeId = '" + GetPriceType(Common.Enums.ProductPriceType.BASPRC.ToString()) + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice != null)
            {
                general.txtCurrentRetailCurrency.Text = oPrice.CurrencyCode;
                general.txtCurrentRetailPrice.Text = oPrice.Price.ToString("n2");
            }
        }

        private void LoadProductOriginalPrice()
        {
            string sql = "ProductId = '" + this.ProductId.ToString() + "' AND PriceTypeId = '" + GetPriceType(Common.Enums.ProductPriceType.ORIPRC.ToString()) + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice != null)
            {
                general.txtOriginalRetailCurrency.Text = oPrice.CurrencyCode;
                general.txtOriginalRetailPrice.Text = oPrice.Price.ToString("n2");
            }
        }

        private void LoadProductVendorPrice()
        {
            string sql = "ProductId = '" + this.ProductId.ToString() + "' AND PriceTypeId = '" + GetPriceType(Common.Enums.ProductPriceType.VPRC.ToString()) + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice != null)
            {
                general.cboVendorCurrency.Text = oPrice.CurrencyCode;
                general.txtVendorPrice.Text = oPrice.Price.ToString("n2");
            }
        }

        private void LoadProductWholesalesPrice()
        {
            string sql = "ProductId = '" + this.ProductId.ToString() + "' AND PriceTypeId = '" + GetPriceType(Common.Enums.ProductPriceType.WHLPRC.ToString()) + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice != null)
            {
                general.txtWholesalesCurrency.Text = oPrice.CurrencyCode;
                general.txtWholesalesPrice.Text = oPrice.Price.ToString("n2");
            }
        }

        // Remarks
        private void LoadProductRemarks()
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductRemarks oRemarks = ProductRemarks.LoadWhere(sql);
            if (oRemarks != null)
            {
                general.txtBin_X.Text = oRemarks.BinX;
                general.txtBin_Y.Text = oRemarks.BinY;
                general.txtBin_Z.Text = oRemarks.BinZ;

                general.chkRetailItem.Checked = oRemarks.DownloadToShop;
                general.chkOffDisplayItem.Checked = oRemarks.OffDisplayItem;
                general.chkCounterItem.Checked = oRemarks.DownloadToCounter;

                general.txtRemarks1.Text = oRemarks.REMARK1;
                general.txtRemarks2.Text = oRemarks.REMARK2;
                general.txtRemarks3.Text = oRemarks.REMARK3;
                general.txtRemarks4.Text = oRemarks.REMARK4;
                general.txtRemarks5.Text = oRemarks.REMARK5;
                general.txtRemarks6.Text = oRemarks.REMARK6;

                misc.txtMemo.Text = oRemarks.Notes;

                // 2009.12.29 david: 如果为网络路径，则直接显示。
                string photoPath = Path.Combine(Path.Combine(Context.Config.GetDirectory("RTImages"), "Product"), oRemarks.Photo);
                try
                {
                    Uri uri = new Uri(oRemarks.Photo);
                    if (uri.IsUnc)
                    {
                        misc.imgProductPic.ImageName = uri.LocalPath;
                    }
                    else
                    {
                        misc.imgProductPic.ImageName = photoPath;
                    }
                }
                catch { }
                finally
                {
                    misc.imgProductPic.ImageName = photoPath;
                }

                misc.txtPicFileName.Text = oRemarks.Photo;
            }
        }
        #endregion

        #region Delete a product
        private void Delete()
        {
            RT2008.DAL.Product oItem = RT2008.DAL.Product.Load(this.ProductId);
            if (oItem != null)
            {
                string sql = "ProductId = '" + oItem.ProductId.ToString() + "'";

                DeleteRemarks(sql);
                DeleteWorkplace(sql);
                DeleteBarcode(sql);
                DeleteLineOfOperation(sql);
                DeleteSupplement(sql);
                DeletePrice(sql);
                DeleteProductCode(sql);

                oItem.Delete();
                // log activity
                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, oItem.ToString());
            }
            else
            {
                MessageBox.Show("Please sure the product info you want to delete is valid!");
            }
        }

        private void DeleteRemarks(string sql)
        {
            ProductRemarksCollection oRemarksList = ProductRemarks.LoadCollection(sql);
            foreach (ProductRemarks oRemarks in oRemarksList)
            {
                oRemarks.Delete();
               
            }
        }

        private void DeleteWorkplace(string sql)
        {
            ProductWorkplaceCollection oWorkplaceList = ProductWorkplace.LoadCollection(sql);
            foreach (ProductWorkplace oWorkplace in oWorkplaceList)
            {
                oWorkplace.Delete();
            }
        }

        private void DeleteBarcode(string sql)
        {
            ProductBarcodeCollection oBarcodeList = ProductBarcode.LoadCollection(sql);
            foreach (ProductBarcode oBarcode in oBarcodeList)
            {
                oBarcode.Delete();
            }
        }

        private void DeleteLineOfOperation(string sql)
        {
            ProductPrice_LineOfOperationCollection oPrice_LineOfOperationList = ProductPrice_LineOfOperation.LoadCollection(sql);
            foreach (ProductPrice_LineOfOperation oPrice_LineOfOperation in oPrice_LineOfOperationList)
            {
                oPrice_LineOfOperation.Delete();
            }
        }

        private void DeleteSupplement(string sql)
        {
            ProductSupplementCollection oSupplementList = ProductSupplement.LoadCollection(sql);
            foreach (ProductSupplement oSupplement in oSupplementList)
            {
                oSupplement.Delete();
            }
        }

        private void DeletePrice(string sql)
        {
            ProductPriceCollection oPriceList = ProductPrice.LoadCollection(sql);
            foreach (ProductPrice oPrice in oPriceList)
            {
                oPrice.Delete();
            }
        }

        private void DeleteProductCode(string sql)
        {
            ProductCodeCollection oCodeList = ProductCode.LoadCollection(sql);
            foreach (ProductCode oCode in oCodeList)
            {
                oCode.Delete();
            }
        }
        #endregion

        private void tabProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabProduct.SelectedIndex != 0 && this.ProductId == System.Guid.Empty)
            //{
            //MessageBox.Show("Please save new record before you go to other tab!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //tabProduct.SelectedIndex = 0;
            //}

            switch (tabProduct.SelectedIndex)
            {
                case 0: break;
                case 1:
                    // Barcode
                    barcode.Dock = DockStyle.Fill;
                    tpBarcode.Controls.Add(barcode);
                    break;
                case 2:
                    // Quantity
                    quantity.Dock = DockStyle.Fill;
                    quantity.ProductId = this.ProductId;
                    tpQty.Controls.Add(quantity);
                    break;
                case 3:
                    // Misc
                    misc.Dock = DockStyle.Fill;
                    misc.ProductId = this.ProductId;
                    tpMisc.Controls.Add(misc);
                    break;
                case 4:
                    // Order
                    order.Dock = DockStyle.Fill;
                    order.ProductId = this.ProductId;
                    tpOrder.Controls.Add(order);
                    break;
                case 5:
                    // Discount
                    discount.Dock = DockStyle.Fill;
                    discount.ProductId = this.ProductId;
                    tpDiscount.Controls.Add(discount);
                    break;
            }
        }

        //private void ProductWizard_Closing(object sender, CancelEventArgs e)
        //{
        //    if (tabProduct.SelectedIndex == 0 && this.ProductId == System.Guid.Empty)
        //    {
        //        MessageBox.Show("Save the changes?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveMessageHandler));
        //    }
        //}

        private void SaveMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save())
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<DefaultList>();
                    MessageBox.Show("Success!", "Save Result");

                    this.Close();
                    ProductWizard wizard = new ProductWizard(this.ProductId);
                    wizard.ShowDialog();
                }
            }
        }

        private void SaveNewMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save())
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<DefaultList>();
                    this.Close();
                    ProductWizard wizard = new ProductWizard();
                    wizard.ShowDialog();
                }
            }
        }

        private void SaveCloseMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save())
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<DefaultList>();
                    this.Close();
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

        private void ProductWizard_Load(object sender, EventArgs e)
        {
            this.txtStkCode.Focus();
        }

        private void cboAppendix3_LostFocus(object sender, EventArgs e)
        {
            general.txtProductName.Focus();
        }
    }
}