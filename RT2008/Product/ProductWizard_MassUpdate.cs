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
using System.Data.SqlClient;

#endregion

namespace RT2008.Product
{
    public partial class ProductWizard_MassUpdate : Form
    {
        private Guid _ProductId = System.Guid.Empty;
        private bool _AllowEmptyAppendix = false;

        #region Public Properties
        public Guid ProductId
        {
            get
            {
                return _ProductId;
            }
            set
            {
                _ProductId = value;
            }
        }
        #endregion

        public ProductWizard_MassUpdate()
        {
            InitializeComponent();
            FillComboList();
            LoadProductSupplement();

            SetSystemLabels();
        }

        private void ProductWizard_MassUpdate_Load(object sender, EventArgs e)
        {
            SetAttributes();
            txtStockCode.Focus();
        }

        #region Set System label
        private void SetSystemLabels()
        {
            lblStockCode.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") + ":";
            lblAppendix1_From.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") + ":";
            lblAppendix2_From.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") + ":";
            lblAppendix3_From.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") + ":";

            chkUpdateClass1.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS1") + ":";
            chkUpdateClass2.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS2") + ":";
            chkUpdateClass3.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS3") + ":";
            chkUpdateClass4.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS4") + ":";
            chkUpdateClass5.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS5") + ":";
            chkUpdateClass6.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS6") + ":";

            chkUpdateRemarks1.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK1") + ":";
            chkUpdateRemarks2.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK2") + ":";
            chkUpdateRemarks3.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK3") + ":";
            chkUpdateRemarks4.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK4") + ":";
            chkUpdateRemarks5.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK5") + ":";
            chkUpdateRemarks6.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK6") + ":";
        }
        #endregion

        private void SetAttributes()
        {
            #region 隱藏啲冇用嘅 Remarks1~6
            if (RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK1") == String.Empty)
            {
                chkUpdateRemarks1.Visible = false;
                txtRemarks1.Visible = false;
            }
            if (RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK2") == String.Empty)
            {
                chkUpdateRemarks2.Visible = false;
                txtRemarks2.Visible = false;
            }
            if (RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK3") == String.Empty)
            {
                chkUpdateRemarks3.Visible = false;
                txtRemarks3.Visible = false;
            }
            if (RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK4") == String.Empty)
            {
                chkUpdateRemarks4.Visible = false;
                txtRemarks4.Visible = false;
            }
            if (RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK5") == String.Empty)
            {
                chkUpdateRemarks5.Visible = false;
                txtRemarks5.Visible = false;
            }
            if (RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK6") == String.Empty)
            {
                chkUpdateRemarks6.Visible = false;
                txtRemarks6.Visible = false;
            }
            #endregion

            txtAppendix1.ReadOnly = true;
            txtAppendix1.BackColor = Color.LightYellow;
            txtAppendix2.ReadOnly = true;
            txtAppendix2.BackColor = Color.LightYellow;
            txtAppendix3.ReadOnly = true;
            txtAppendix3.BackColor = Color.LightYellow;
        }

        #region Fill Combo List
        private void FillComboList()
        {
            FillNatureList();
            FillAppendixes();
            FillClasses();
            FillVenderCurrency();
        }

        private void FillNatureList()
        {
            ProductNatureList natureList = new ProductNatureList();
            cboNature.Items.Clear();

            string[] orderBy = new string[] { "NatureCode" };
            ProductNatureCollection oNatureList = ProductNature.LoadCollection(orderBy, true);
            foreach (ProductNature nature in oNatureList)
            {
                natureList.Add(new ProductNatureRec(nature.NatureId, nature.NatureCode + " - " + nature.NatureName));
            }

            cboNature.DataSource = natureList;
            cboNature.DisplayMember = "ProductNatureCode";
            cboNature.ValueMember = "ProductNatureId";
            cboNature.SelectedIndex = 1;    //default N - Normal
        }

        private void FillVenderCurrency()
        {
            cboVendorCurrency.Items.Clear();

            Currency.LoadCombo(ref cboVendorCurrency, "CurrencyCode", false, true, String.Empty, String.Empty);
        }

        #region ComboBox Binding Class
        public class ProductNatureRec
        {
            Guid prodNatureId = System.Guid.Empty;
            string natureCode = string.Empty;

            public ProductNatureRec(Guid pNId, string sCode)
            {
                prodNatureId = pNId;
                natureCode = sCode;
            }

            public Guid ProductNatureId
            {
                get { return prodNatureId; }
                set { prodNatureId = value; }
            }

            public string ProductNatureCode
            {
                get { return natureCode; }
                set { natureCode = value; }
            }
        }

        public class ProductNatureList : BindingList<ProductNatureRec>
        {
        }
        #endregion

        #region Appendix
        private void FillAppendixes()
        {
            //FillAppendixe_From();
            //FillAppendixe_To();
            FillAppendix1();
            FillAppendix2();
            FillAppendix3();
        }

        private void FillAppendixe_From()
        {
            // Appendixe1
            cboAppendix1_From.Items.Clear();
            string[] orderBy = new string[] { "Appendix1Code" };
            ProductAppendix1Collection oA1ListFrom = ProductAppendix1.LoadCollection(orderBy, true);
            oA1ListFrom.Insert(0, new ProductAppendix1());
            cboAppendix1_From.DataSource = oA1ListFrom;
            cboAppendix1_From.DisplayMember = "Appendix1Code";
            cboAppendix1_From.ValueMember = "Appendix1Id";
            if (cboAppendix1_From.Items.Count > 0)
            {
                cboAppendix1_From.SelectedIndex = 1;
            }
            else
            {
                cboAppendix1_From.SelectedIndex = 0;
            }

            // Appendixe2
            cboAppendix2_From.Items.Clear();
            orderBy = new string[] { "Appendix2Code" };
            ProductAppendix2Collection oA2ListFrom = ProductAppendix2.LoadCollection(orderBy, true);
            oA2ListFrom.Insert(0, new ProductAppendix2());
            cboAppendix2_From.DataSource = oA2ListFrom;
            cboAppendix2_From.DisplayMember = "Appendix2Code";
            cboAppendix2_From.ValueMember = "Appendix2Id";
            if (cboAppendix2_From.Items.Count > 0)
            {
                cboAppendix2_From.SelectedIndex = 1;
            }
            else
            {
                cboAppendix2_From.SelectedIndex = 0;
            }

            // Appendixe3
            cboAppendix3_From.Items.Clear();
            orderBy = new string[] { "Appendix3Code" };
            ProductAppendix3Collection oA3ListFrom = ProductAppendix3.LoadCollection(orderBy, true);
            oA3ListFrom.Insert(0, new ProductAppendix3());
            cboAppendix3_From.DataSource = oA3ListFrom;
            cboAppendix3_From.DisplayMember = "Appendix3Code";
            cboAppendix3_From.ValueMember = "Appendix3Id";
            if (cboAppendix3_From.Items.Count > 0)
            {
                cboAppendix3_From.SelectedIndex = 1;
            }
            else
            {
                cboAppendix3_From.SelectedIndex = 0;
            }
        }

        private void FillAppendixe_To()
        {
            // Appendixe1
            cboAppendix1_To.Items.Clear();
            string[] orderBy = new string[] { "Appendix1Code" };
            ProductAppendix1Collection oA1ListTo = ProductAppendix1.LoadCollection(orderBy, true);
            oA1ListTo.Insert(0, new ProductAppendix1());
            cboAppendix1_To.DataSource = oA1ListTo;
            cboAppendix1_To.DisplayMember = "Appendix1Code";
            cboAppendix1_To.ValueMember = "Appendix1Id";
            cboAppendix1_To.SelectedIndex = cboAppendix1_To.Items.Count-1;

            // Appendixe2
            cboAppendix2_To.Items.Clear();
            orderBy = new string[] { "Appendix2Code" };
            ProductAppendix2Collection oA2ListTo = ProductAppendix2.LoadCollection(orderBy, true);
            oA2ListTo.Insert(0, new ProductAppendix2());
            cboAppendix2_To.DataSource = oA2ListTo;
            cboAppendix2_To.DisplayMember = "Appendix2Code";
            cboAppendix2_To.ValueMember = "Appendix2Id";
            cboAppendix2_To.SelectedIndex = cboAppendix2_To.Items.Count-1;

            // Appendixe3
            cboAppendix3_To.Items.Clear();
            orderBy = new string[] { "Appendix3Code" };
            ProductAppendix3Collection oA3ListTo = ProductAppendix3.LoadCollection(orderBy, true);
            oA3ListTo.Insert(0, new ProductAppendix3());
            cboAppendix3_To.DataSource = oA3ListTo;
            cboAppendix3_To.DisplayMember = "Appendix3Code";
            cboAppendix3_To.ValueMember = "Appendix3Id";
            cboAppendix3_To.SelectedIndex = cboAppendix3_To.Items.Count - 1;
        }

        private void FillAppendix1()
        {
            String sql = String.Format("SELECT DISTINCT [APPENDIX1] FROM [Product] WHERE [STKCODE] = '{0}' ORDER BY [APPENDIX1]", txtStockCode.Text.Trim());

            SqlHelper sh = new SqlHelper();
            SqlDataReader dr = sh.ExecuteReader(CommandType.Text, sql);

            cboAppendix1_From.Items.Clear();
            cboAppendix1_To.Items.Clear();

            cboAppendix1_From.Items.Add(String.Empty);
            cboAppendix1_To.Items.Add(String.Empty);
            while (dr.Read())
            {
                String code = (String)dr["APPENDIX1"];
                cboAppendix1_From.Items.Add(code);
                cboAppendix1_To.Items.Add(code);
            }

            if (cboAppendix1_From.Items.Count == 1)
            {
                cboAppendix1_From.SelectedIndex = 0;
                cboAppendix1_To.SelectedIndex = 0;
            }
            else
            {
                cboAppendix1_From.SelectedIndex = 1;
                cboAppendix1_To.SelectedIndex = cboAppendix1_To.Items.Count - 1;
            }
        }

        private void FillAppendix2()
        {
            String sql = String.Format("SELECT DISTINCT [APPENDIX2] FROM [Product] WHERE [STKCODE] = '{0}' ORDER BY [APPENDIX2]", txtStockCode.Text.Trim());

            SqlHelper sh = new SqlHelper();
            SqlDataReader dr = sh.ExecuteReader(CommandType.Text, sql);

            cboAppendix2_From.Items.Clear();
            cboAppendix2_To.Items.Clear();

            cboAppendix2_From.Items.Add(String.Empty);
            cboAppendix2_To.Items.Add(String.Empty);
            while (dr.Read())
            {
                String code = (String)dr["APPENDIX2"];
                cboAppendix2_From.Items.Add(code);
                cboAppendix2_To.Items.Add(code);
            }

            if (cboAppendix2_From.Items.Count == 1)
            {
                cboAppendix2_From.SelectedIndex = 0;
                cboAppendix2_To.SelectedIndex = 0;
            }
            else
            {
                cboAppendix2_From.SelectedIndex = 1;
                cboAppendix2_To.SelectedIndex = cboAppendix2_To.Items.Count - 1;
            }
        }

        private void FillAppendix3()
        {
            String sql = String.Format("SELECT DISTINCT [APPENDIX3] FROM [Product] WHERE [STKCODE] = '{0}' ORDER BY [APPENDIX3]", txtStockCode.Text.Trim());

            SqlHelper sh = new SqlHelper();
            SqlDataReader dr = sh.ExecuteReader(CommandType.Text, sql);

            cboAppendix3_From.Items.Clear();
            cboAppendix3_To.Items.Clear();

            cboAppendix3_From.Items.Add(String.Empty);
            cboAppendix3_To.Items.Add(String.Empty);
            while (dr.Read())
            {
                String code = (String)dr["APPENDIX3"];
                cboAppendix3_From.Items.Add(code);
                cboAppendix3_To.Items.Add(code);
            }

            if (cboAppendix3_From.Items.Count == 1)
            {
                cboAppendix3_From.SelectedIndex = 0;
                cboAppendix3_To.SelectedIndex = 0;
            }
            else
            {
                cboAppendix3_From.SelectedIndex = 1;
                cboAppendix3_To.SelectedIndex = cboAppendix3_To.Items.Count - 1;
            }
        }
        #endregion

        #region Class
        private void FillClasses()
        {
            FillClass1();
            FillClass2();
            FillClass3();
            FillClass4();
            FillClass5();
            FillClass6();
        }

        private void FillClass1()
        {
            cboClass1.Items.Clear();

            string[] orderBy = new string[] { "Class1Initial" };
            ProductClass1Collection oC1List = ProductClass1.LoadCollection(orderBy, true);
            cboClass1.DataSource = oC1List;
            cboClass1.DisplayMember = "Class1Initial";
            cboClass1.ValueMember = "Class1Id";
        }

        private void FillClass2()
        {
            cboClass2.Items.Clear();

            string[] orderBy = new string[] { "Class2Initial" };
            ProductClass2Collection oC2List = ProductClass2.LoadCollection(orderBy, true);
            cboClass2.DataSource = oC2List;
            cboClass2.DisplayMember = "Class2Initial";
            cboClass2.ValueMember = "Class2Id";
        }

        private void FillClass3()
        {
            cboClass3.Items.Clear();

            string[] orderBy = new string[] { "Class3Initial" };
            ProductClass3Collection oC3List = ProductClass3.LoadCollection(orderBy, true);
            cboClass3.DataSource = oC3List;
            cboClass3.DisplayMember = "Class3Initial";
            cboClass3.ValueMember = "Class3Id";
        }

        private void FillClass4()
        {
            cboClass4.Items.Clear();

            string[] orderBy = new string[] { "Class4Initial" };
            ProductClass4Collection oC4List = ProductClass4.LoadCollection(orderBy, true);
            cboClass4.DataSource = oC4List;
            cboClass4.DisplayMember = "Class4Initial";
            cboClass4.ValueMember = "Class4Id";
        }

        private void FillClass5()
        {
            cboClass5.Items.Clear();

            string[] orderBy = new string[] { "Class5Initial" };
            ProductClass5Collection oC5List = ProductClass5.LoadCollection(orderBy, true);
            cboClass5.DataSource = oC5List;
            cboClass5.DisplayMember = "Class5Initial";
            cboClass5.ValueMember = "Class5Id";
        }

        private void FillClass6()
        {
            cboClass6.Items.Clear();

            string[] orderBy = new string[] { "Class6Initial" };
            ProductClass6Collection oC6List = ProductClass6.LoadCollection(orderBy, true);
            cboClass6.DataSource = oC6List;
            cboClass6.DisplayMember = "Class6Initial";
            cboClass6.ValueMember = "Class6Id";
        }
        #endregion

        #endregion

        #region Show Records
        private void FindAndShow()
        {
            String stkcode = txtStockCode.Text.Trim();

            if (stkcode != String.Empty)
            {
                String sql = String.Format("STKCODE = '{0}'", stkcode);
                DAL.Product oProduct = DAL.Product.LoadWhere(sql);
                if (oProduct != null)
                {
                    _ProductId = oProduct.ProductId;
                    ShowRecords();
                }
                else
                {
                    MessageBox.Show(String.Format("No such product...{0}!", txtStockCode.Text.Trim()));
                    txtStockCode.Focus();
                }
            }
        }

        private void ShowRecords()
        {
            LoadGeneralInfo();
            FillAppendixes();
            LoadProductPrice();
            LoadProductRemarks();
            LoadProductSupplement();

            Verify();
        }

        private void LoadGeneralInfo()
        {
            RT2008.DAL.Product oItem = RT2008.DAL.Product.Load(_ProductId);

            if (oItem != null)
            {
                _ProductId = oItem.ProductId;
                if ((oItem.APPENDIX1 == String.Empty) || (oItem.APPENDIX2 == String.Empty) || (oItem.APPENDIX3 == String.Empty)) _AllowEmptyAppendix = true;

                txtStockCode.Text = oItem.STKCODE;
                txtAppendix1.Text = oItem.APPENDIX1;
                txtAppendix2.Text = oItem.APPENDIX2;
                txtAppendix3.Text = oItem.APPENDIX3;

                cboClass1.Text = oItem.CLASS1;
                cboClass2.Text = oItem.CLASS2;
                cboClass3.Text = oItem.CLASS3;
                cboClass4.Text = oItem.CLASS4;
                cboClass5.Text = oItem.CLASS5;
                cboClass6.Text = oItem.CLASS6;

                txtProductName.Text = oItem.ProductName;
                txtProductNameChs.Text = oItem.ProductName_Chs;
                txtProductNameCht.Text = oItem.ProductName_Cht;
                txtRemarks.Text = oItem.Remarks;

                txtWholesalesPrice.Text = oItem.WholesalePrice.ToString("n2");
                txtOriginalRetailPrice.Text = oItem.OriginalRetailPrice.ToString("n2");
                txtCurrentRetailPrice.Text = oItem.RetailPrice.ToString("n2");
                txtRetailDiscount.Text = oItem.NormalDiscount.ToString("n2");
                txtUnit.Text = oItem.UOM;
                cboNature.SelectedValue = oItem.NatureId;

                // Order Info
                txtVendorItemNum.Text = oItem.AlternateItem; // Vendor Item Number
                txtReorderLevel.Text = oItem.ReorderLevel.ToString("n2");
                txtReorderQuantity.Text = oItem.ReorderQty.ToString("n0");

                //tabDiscountInfo
                if (oItem.FixedPriceItem)
                    cboFixedPriceItem.SelectedIndex = 1;
                else
                    cboFixedPriceItem.SelectedIndex = 0;
            }
        }

        // Product Price
        private void LoadProductSupplement()
        {
            string sql = "ProductId = '" + _ProductId.ToString() + "'";
            ProductSupplement oProdSupp = ProductSupplement.LoadWhere(sql);
            if (oProdSupp != null)
            {
                cboVendorCurrency.Text = oProdSupp.VendorCurrencyCode;
                txtVendorPrice.Text = oProdSupp.VendorPrice.ToString("n2");
                txtProductMemo.Text = oProdSupp.ProductName_Memo;
                txtProductPole.Text = oProdSupp.ProductName_Pole;

                //tabDiscountInfo
                txtDiscountForFixPriceItem.Text = oProdSupp.VipDiscount_FixedItem.ToString("00.00");
                txtDiscountForDiscountItem.Text = oProdSupp.VipDiscount_DiscountItem.ToString("00.00");
                txtDiscountForNoDiscountItem.Text = oProdSupp.VipDiscount_NoDiscountItem.ToString("00.00");
                txtStaffDiscount.Text = oProdSupp.StaffDiscount.ToString("00.00");
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
            string sql = "ProductId = '" + _ProductId.ToString() + "' AND PriceTypeId = '" + GetPriceType(Common.Enums.ProductPriceType.BASPRC.ToString()) + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice != null)
            {
                txtCurrentRetailCurrency.Text = oPrice.CurrencyCode;
                txtCurrentRetailPrice.Text = oPrice.Price.ToString("n2");
            }
        }

        private void LoadProductOriginalPrice()
        {
            string sql = "ProductId = '" + _ProductId.ToString() + "' AND PriceTypeId = '" + GetPriceType(Common.Enums.ProductPriceType.ORIPRC.ToString()) + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice != null)
            {
                txtOriginalRetailCurrency.Text = oPrice.CurrencyCode;
                txtOriginalRetailPrice.Text = oPrice.Price.ToString("n2");
            }
        }

        private void LoadProductVendorPrice()
        {
            string sql = "ProductId = '" + _ProductId.ToString() + "' AND PriceTypeId = '" + GetPriceType(Common.Enums.ProductPriceType.VPRC.ToString()) + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice != null)
            {
                cboVendorCurrency.Text = oPrice.CurrencyCode;
                txtVendorPrice.Text = oPrice.Price.ToString("n2");
            }
        }

        private void LoadProductWholesalesPrice()
        {
            string sql = "ProductId = '" + _ProductId.ToString() + "' AND PriceTypeId = '" + GetPriceType(Common.Enums.ProductPriceType.WHLPRC.ToString()) + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice != null)
            {
                txtWholesalesCurrency.Text = oPrice.CurrencyCode;
                txtWholesalesPrice.Text = oPrice.Price.ToString("n2");
            }
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

        // Remarks
        private void LoadProductRemarks()
        {
            string sql = "ProductId = '" + _ProductId.ToString() + "'";
            ProductRemarks oRemarks = ProductRemarks.LoadWhere(sql);
            if (oRemarks != null)
            {
                txtBin_X.Text = oRemarks.BinX;
                txtBin_Y.Text = oRemarks.BinY;
                txtBin_Z.Text = oRemarks.BinZ;

                chkRetailItem.Checked = oRemarks.DownloadToShop;
                chkOffDisplayItem.Checked = oRemarks.OffDisplayItem;
                chkCounterItem.Checked = oRemarks.DownloadToCounter;

                txtRemarks1.Text = oRemarks.REMARK1;
                txtRemarks2.Text = oRemarks.REMARK2;
                txtRemarks3.Text = oRemarks.REMARK3;
                txtRemarks4.Text = oRemarks.REMARK4;
                txtRemarks5.Text = oRemarks.REMARK5;
                txtRemarks6.Text = oRemarks.REMARK6;

                txtMemo.Text = oRemarks.Notes;
                txtPhoto.Text = oRemarks.Photo;
            }
        }

        private void FindProduct()
        {
            ProdCare_FindProd objFindProd = new ProdCare_FindProd();
            objFindProd.Closed += new EventHandler(objFindProd_Closed);
            objFindProd.ShowDialog();
        }

        void objFindProd_Closed(object sender, EventArgs e)
        {
            ProdCare_FindProd objFindProd = sender as ProdCare_FindProd;
            if (objFindProd.IsCompleted)
            {
                _ProductId = objFindProd.ProductId;
                ShowRecords();
            }
        }
        #endregion

        #region Save Records

        private bool Verify()
        {
            return true;
        }

        //2012.12.24 paulus: 唔使 verify 啦，Product 係經 search 搵料，一定係冇問題的
        private bool Verify_Old()
        {
            bool result = false;
            if (txtStockCode.Text == string.Empty)
            {
                errorProvider.SetError(txtStockCode, "Can not be blank!");
            }
            else if(txtAppendix1.Text == string.Empty)
            {
                errorProvider.SetError(txtAppendix1, "Can not be blank!");
            }
            else if(txtAppendix2.Text == string.Empty)
            {
                errorProvider.SetError(txtAppendix2, "Can not be blank!");
            }
            else if(txtAppendix3.Text == string.Empty)
            {
                errorProvider.SetError(txtAppendix3, "Can not be blank!");
            }
            else
            {
                errorProvider.SetError(txtStockCode, string.Empty);
                txtStockCode.Text = Utility.VerifyQuotes(txtStockCode.Text);

                errorProvider.SetError(txtAppendix1, string.Empty);
                txtAppendix1.Text = Utility.VerifyQuotes(txtAppendix1.Text);

                errorProvider.SetError(txtAppendix2, string.Empty);
                txtAppendix2.Text = Utility.VerifyQuotes(txtAppendix2.Text);

                errorProvider.SetError(txtAppendix3, string.Empty);
                txtAppendix3.Text = Utility.VerifyQuotes(txtAppendix3.Text);

                result = true;
            }

            if (this.ProductId != System.Guid.Empty)
            {
                result = VerifyRanges();
            }

            return result;
        }

        #region Appendix Range
        private bool VerifyRanges()
        {
            if (!VerifyAppendix1(cboAppendix1_From.Text))
            {
                errorProvider.SetError(cboAppendix1_From, "Does not exist!");
                return false;
            }
            else if (!VerifyAppendix2(cboAppendix2_From.Text))
            {
                errorProvider.SetError(cboAppendix2_From, "Does not exist!");
                return false;
            }
            else if (!VerifyAppendix3(cboAppendix3_From.Text))
            {
                errorProvider.SetError(cboAppendix3_From, "Does not exist!");
                return false;
            }
            else if (!VerifyAppendix1(cboAppendix1_To.Text))
            {
                errorProvider.SetError(cboAppendix1_To, "Does not exist!");
                return false;
            }
            else if (!VerifyAppendix2(cboAppendix2_To.Text))
            {
                errorProvider.SetError(cboAppendix2_To, "Does not exist!");
                return false;
            }
            else if (!VerifyAppendix3(cboAppendix3_To.Text))
            {
                errorProvider.SetError(cboAppendix3_To, "Does not exist!");
                return false;
            }
            else
            {
                errorProvider.SetError(cboAppendix1_From, string.Empty);
                errorProvider.SetError(cboAppendix2_From, string.Empty);
                errorProvider.SetError(cboAppendix3_From, string.Empty);
                errorProvider.SetError(cboAppendix1_To, string.Empty);
                errorProvider.SetError(cboAppendix2_To, string.Empty);
                errorProvider.SetError(cboAppendix3_To, string.Empty);
                return true;
            }
        }

        private bool VerifyAppendix1(string appendix1)
        {
            bool result = false;

            string sql = "Appendix1Code = '" + appendix1 + "'";
            ProductAppendix1 oA1 = ProductAppendix1.LoadWhere(sql);
            if (oA1 != null)
            {
                result = true;
            }

            return result;
        }

        private bool VerifyAppendix2(string appendix2)
        {
            bool result = false;

            string sql = "Appendix2Code = '" + appendix2 + "'";
            ProductAppendix2 oA2 = ProductAppendix2.LoadWhere(sql);
            if (oA2 != null)
            {
                result = true;
            }

            return result;
        }

        private bool VerifyAppendix3(string appendix3)
        {
            bool result = false;

            string sql = "Appendix3Code = '" + appendix3 + "'";
            ProductAppendix3 oA3 = ProductAppendix3.LoadWhere(sql);
            if (oA3 != null)
            {
                result = true;
            }

            return result;
        } 
        #endregion

        private void SaveRecords()
        {
            if (string.IsNullOrEmpty(errorProvider.GetError(txtStockCode)) && string.IsNullOrEmpty(errorProvider.GetError(txtAppendix1))
                 && string.IsNullOrEmpty(errorProvider.GetError(txtAppendix2)) && string.IsNullOrEmpty(errorProvider.GetError(txtAppendix3))
                 && string.IsNullOrEmpty(errorProvider.GetError(cboAppendix1_From)) && string.IsNullOrEmpty(errorProvider.GetError(cboAppendix2_From))
                 && string.IsNullOrEmpty(errorProvider.GetError(cboAppendix3_From)) && string.IsNullOrEmpty(errorProvider.GetError(cboAppendix1_To)) 
                 && string.IsNullOrEmpty(errorProvider.GetError(cboAppendix2_To)) && string.IsNullOrEmpty(errorProvider.GetError(cboAppendix3_To)))
            {
                Save();
                MessageBox.Show("Success", "Save Result");
            }
        }

        #region Save Product Info
        private void Save()
        {
            System.Guid currentProductId = System.Guid.Empty;
            String sql = String.Format("STKCODE = '{0}'", txtStockCode.Text.Trim());
            DAL.ProductCollection oProducts = DAL.Product.LoadCollection(sql);
            foreach (DAL.Product oProduct in oProducts)
            {
                if (RT2008.Controls.Utility.StringBetween(oProduct.APPENDIX1, cboAppendix1_From.Text, cboAppendix1_To.Text) &&
                    RT2008.Controls.Utility.StringBetween(oProduct.APPENDIX2, cboAppendix2_From.Text, cboAppendix2_To.Text) &&
                    RT2008.Controls.Utility.StringBetween(oProduct.APPENDIX3, cboAppendix3_From.Text, cboAppendix3_To.Text))
                {
                    currentProductId = SaveGeneralInfo(oProduct.ProductId);
                }
            }
        }

        private Guid SaveGeneralInfo(Guid productId)
        {
            Guid result = System.Guid.Empty;

            RT2008.DAL.Product oItem = RT2008.DAL.Product.Load(productId);
            if (oItem != null)
            {
                string sql = "ProductId = '" + productId.ToString() + "'";
                ProductRemarks oRemarks = ProductRemarks.LoadWhere(sql);
                if (chkUpdateClass1.Checked) oItem.CLASS1 = cboClass1.Text;
                if (chkUpdateClass2.Checked) oItem.CLASS2 = cboClass2.Text;
                if (chkUpdateClass3.Checked) oItem.CLASS3 = cboClass3.Text;
                if (chkUpdateClass4.Checked) oItem.CLASS4 = cboClass4.Text;
                if (chkUpdateClass5.Checked) oItem.CLASS5 = cboClass5.Text;
                if (chkUpdateClass6.Checked) oItem.CLASS6 = cboClass6.Text;

                if (chkUpdateProductName.Checked) oItem.ProductName = txtProductName.Text;
                if (chkUpdateProductNameChs.Checked) oItem.ProductName_Chs = txtProductNameChs.Text;
                if (chkUpdateProductNameCht.Checked) oItem.ProductName_Cht = txtProductNameCht.Text;
                if (chkUpdateRemarks.Checked) oItem.Remarks = txtRemarks.Text;

                if (chkUpdateWholesales.Checked) oItem.WholesalePrice = Convert.ToDecimal(txtWholesalesPrice.Text == String.Empty ? "0" : txtWholesalesPrice.Text);
                if (chkUpdateOriginalRetail.Checked) oItem.OriginalRetailPrice = Convert.ToDecimal(txtOriginalRetailPrice.Text == String.Empty ? "0" : txtOriginalRetailPrice.Text);
                if (chkUpdateCurrentRetail.Checked) oItem.RetailPrice = Convert.ToDecimal(txtCurrentRetailPrice.Text == String.Empty ? "0" : txtCurrentRetailPrice.Text);
                if (chkUpdateRetailDiscount.Checked) oItem.NormalDiscount = Convert.ToDecimal((txtRetailDiscount.Text == string.Empty) ? "0" : txtRetailDiscount.Text);
                if (chkUpdateUnit.Checked) oItem.UOM = txtUnit.Text;
                if (chkUpdateNature.Checked) oItem.NatureId = new Guid(cboNature.SelectedValue.ToString());
                if (chkUpdateVendorItem.Checked) oItem.AlternateItem = txtVendorItemNum.Text; // Vendor Item Number
                if (chkUpdateReorderLevel.Checked) oItem.ReorderLevel = Convert.ToDecimal((txtReorderLevel.Text == string.Empty) ? "0" : txtReorderLevel.Text);
                if (chkUpdateReorderQuantiry.Checked) oItem.ReorderQty = Convert.ToDecimal((txtReorderQuantity.Text == string.Empty) ? "0" : txtReorderQuantity.Text);

                //tabDiscountInfo
                if (chkFixedPriceItem.Checked) oItem.FixedPriceItem = (cboFixedPriceItem.SelectedIndex == 1) ? true : false;

                // Download Packets
                oItem.DownloadToPOS = oRemarks.DownloadToShop;
                oItem.DownloadToCounter = oRemarks.DownloadToCounter;
                if (chkUpdateRetailItem.Checked) oItem.DownloadToPOS = chkRetailItem.Checked;
                if (chkUpdateCounterItem.Checked) oItem.DownloadToCounter = chkCounterItem.Checked;


                oItem.ModifiedBy = Common.Config.CurrentUserId;
                oItem.ModifiedOn = DateTime.Now;
                oItem.Save();

                // Appendix / Class
                SaveProductCode(oItem.ProductId,
                    new Guid(cboClass1.SelectedValue.ToString()), new Guid(cboClass2.SelectedValue.ToString()), new Guid(cboClass3.SelectedValue.ToString()),
                    new Guid(cboClass4.SelectedValue.ToString()), new Guid(cboClass5.SelectedValue.ToString()), new Guid(cboClass6.SelectedValue.ToString()));

                // Product Price
                SaveProductSupplement(oItem.ProductId);
                SaveProductPrice(oItem.ProductId);

                // Remarks
                SaveProductRemarks(oItem.ProductId);

                result = oItem.ProductId;
            }

            return result;
        }

        private void SaveProductCode(Guid productId, Guid c1Id, Guid c2Id, Guid c3Id, Guid c4Id, Guid c5Id, Guid c6Id)
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductCode oCode = ProductCode.LoadWhere(sql);
            if (oCode != null)
            {
                if (chkUpdateClass1.Checked) oCode.Class1Id = c1Id;
                if (chkUpdateClass2.Checked) oCode.Class2Id = c2Id;
                if (chkUpdateClass3.Checked) oCode.Class3Id = c3Id;
                if (chkUpdateClass4.Checked) oCode.Class4Id = c4Id;
                if (chkUpdateClass5.Checked) oCode.Class5Id = c5Id;
                if (chkUpdateClass6.Checked) oCode.Class6Id = c6Id;

                oCode.Save();
            }
        }

        private void SaveProductSupplement(Guid productId)
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductSupplement oProdSupp = ProductSupplement.LoadWhere(sql);
            if (oProdSupp != null)
            {
                if (chkUpdateVendorPrice.Checked) oProdSupp.VendorCurrencyCode = cboVendorCurrency.Text;
                if (chkUpdateVendorPrice.Checked) oProdSupp.VendorPrice = Convert.ToDecimal((txtVendorPrice.Text == string.Empty) ? "0" : txtVendorPrice.Text);
                if (chkUpdateProductMemo.Checked) oProdSupp.ProductName_Memo = txtProductMemo.Text;
                if (chkUpdateProductPole.Checked) oProdSupp.ProductName_Pole = txtProductPole.Text;

                //tabDiscountInfo
                if (chkDiscountForFixPriceItem.Checked) oProdSupp.VipDiscount_FixedItem = Convert.ToDecimal(txtDiscountForFixPriceItem.Text);
                if (chkDiscountForDiscountItem.Checked) oProdSupp.VipDiscount_DiscountItem = Convert.ToDecimal(txtDiscountForDiscountItem.Text);
                if (chkDiscountForNoDiscountItem.Checked) oProdSupp.VipDiscount_NoDiscountItem = Convert.ToDecimal(txtDiscountForNoDiscountItem.Text);
                if (chkStaffDiscount.Checked) oProdSupp.StaffDiscount = Convert.ToDecimal(txtStaffDiscount.Text);

                oProdSupp.Save();
            }
        }

        private void SaveProductPrice(Guid productId)
        {
            if (chkUpdateCurrentRetail.Checked) SaveProductPrice(productId, Common.Enums.ProductPriceType.BASPRC.ToString(), txtCurrentRetailCurrency.Text, txtCurrentRetailPrice.Text);
            if (chkUpdateOriginalRetail.Checked) SaveProductPrice(productId, Common.Enums.ProductPriceType.ORIPRC.ToString(), txtOriginalRetailCurrency.Text, txtOriginalRetailPrice.Text);
            if (chkUpdateVendorPrice.Checked) SaveProductPrice(productId, Common.Enums.ProductPriceType.VPRC.ToString(), cboVendorCurrency.Text, txtVendorPrice.Text);
            if (chkUpdateWholesales.Checked) SaveProductPrice(productId, Common.Enums.ProductPriceType.WHLPRC.ToString(), txtWholesalesCurrency.Text, txtWholesalesPrice.Text);
        }

        private void SaveProductPrice(Guid productId, string priceType, string currencyCode, string price)
        {
            string sql = "ProductId = '" + productId.ToString() + "' AND PriceTypeId = '" + GetPriceType(priceType).ToString() + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice != null)
            {
                oPrice.PriceTypeId = GetPriceType(priceType);
                oPrice.CurrencyCode = currencyCode;
                oPrice.Price = Convert.ToDecimal((price == string.Empty) ? "0" : price);
                oPrice.Save();
            }
        }

        private void SaveProductRemarks(Guid productId)
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductRemarks oRemarks = ProductRemarks.LoadWhere(sql);
            if (oRemarks != null)
            {
                if (chkUpdateBinX.Checked) oRemarks.BinX = txtBin_X.Text;
                if (chkUpdateBinY.Checked) oRemarks.BinY = txtBin_Y.Text;
                if (chkUpdateBinZ.Checked) oRemarks.BinZ = txtBin_Z.Text;

                if (chkUpdateRetailItem.Checked) oRemarks.DownloadToShop = chkRetailItem.Checked;
                if (chkUpdateOffDisplayItem.Checked) oRemarks.OffDisplayItem = chkOffDisplayItem.Checked;
                if (chkUpdateCounterItem.Checked) oRemarks.DownloadToCounter = chkCounterItem.Checked;

                if (chkUpdateRemarks1.Checked) oRemarks.REMARK1 = txtRemarks1.Text;
                if (chkUpdateRemarks2.Checked) oRemarks.REMARK2 = txtRemarks2.Text;
                if (chkUpdateRemarks3.Checked) oRemarks.REMARK3 = txtRemarks3.Text;
                if (chkUpdateRemarks4.Checked) oRemarks.REMARK4 = txtRemarks4.Text;
                if (chkUpdateRemarks5.Checked) oRemarks.REMARK5 = txtRemarks5.Text;
                if (chkUpdateRemarks6.Checked) oRemarks.REMARK6 = txtRemarks6.Text;

                if (chkUpdatePhoto.Checked) oRemarks.Photo = txtPhoto.Text;
                if (chkUpdateMemo.Checked) oRemarks.Notes = txtMemo.Text;

                oRemarks.Save();
            }
        }
        #endregion
        #endregion

        #region Selection
        private void SelectAll(bool checkedAll)
        {
            CheckingBoxes(checkedAll);
        }

        private void CheckingBoxes(bool isChecked)
        {
            for (int i = 0; i < this.tabMessUpdate.Controls.Count; i++)
            {
                TabPage tPage = (TabPage)this.tabMessUpdate.Controls[i];
                CheckingBoxesInTabCtrl(tPage, isChecked);
            }
        }

        private void CheckingBoxes(Control ctrl, bool isChecked)
        {
            if (ctrl.GetType() == typeof(CheckBox))
            {
                CheckBox chkUpdate = (CheckBox)ctrl;
                if (chkUpdate.Name.IndexOf("chkUpdate") >= 0)
                {
                    chkUpdate.Checked = isChecked;
                }
            }
        }

        private void CheckingBoxesInTabCtrl(TabPage tPage, bool isChecked)
        {
            for (int i = 0; i < tPage.Controls.Count; i++)
            {
                Control ctrl = tPage.Controls[i];
                CheckingBoxes(ctrl, isChecked);
                CheckingBoxesInGroupBox(ctrl, isChecked);
            }
        }

        private void CheckingBoxesInGroupBox(Control ctrl, bool isChecked)
        {
            if (ctrl.GetType() == typeof(GroupBox))
            {
                for (int i = 0; i < ctrl.Controls.Count; i++)
                {
                    CheckingBoxes(ctrl.Controls[i], isChecked);
                }
            }
        }
        #endregion

        #region Events

        #region Stock Code buttons events
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (_ProductId != System.Guid.Empty)
            {
                ShowRecords();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindProduct();
        }

        private void lnkNature_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProductNatureWizard wizNature = new ProductNatureWizard();
            wizNature.ShowDialog();
        }
        #endregion

        #region Main Button Events
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Verify())
            {
                SaveRecords();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SelectAll(true);
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            SelectAll(false);
        }
        #endregion

        #region Photo Events
        private void btnRefreshPhoto_Click(object sender, EventArgs e)
        {

        }

        private void btnLookupPhoto_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void txtStockCode_LostFocus(object sender, EventArgs e)
        {
            FindAndShow();
        }

        private void txtStockCode_EnterKeyDown(object objSender, KeyEventArgs objArgs)
        {
            if (objArgs.KeyCode == Keys.Enter)
            {
                FindAndShow();
            }
        }

        #endregion
    }
}