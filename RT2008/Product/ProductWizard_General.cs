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

namespace RT2008.Product
{
    public partial class ProductWizard_General : UserControl
    {
        private Guid _ProductId = System.Guid.Empty;

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

        public ProductWizard_General()
        {
            InitializeComponent();
            SetSystemLabels();
            FillList();
            SetAttributes();
        }

        #region Set System label
        private void SetSystemLabels()
        {
            lblClass1.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS1");
            lblClass2.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS2");
            lblClass3.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS3");
            lblClass4.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS4");
            lblClass5.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS5");
            lblClass6.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS6");

            lblRemarks1.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK1");
            lblRemarks2.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK2");
            lblRemarks3.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK3");
            lblRemarks4.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK4");
            lblRemarks5.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK5");
            lblRemarks6.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("REMARK6");
        }
        #endregion

        #region Set Attributes
        private void SetAttributes()
        {
            chkRetailItem.Checked = true;
            this.chkCounterItem.LostFocus += new EventHandler(chkCounterItem_LostFocus);

            if (_ProductId == System.Guid.Empty)
            {
                txtModifiedOn.Enabled = false;
                txtModifiedBy.Enabled = false;
                txtCreatedOn.Enabled = false;
                txtStatus_Office.Enabled = false;
                txtStatus_Counter.Enabled = false;
            }
        }

        void chkCounterItem_LostFocus(object sender, EventArgs e)
        {
            this.cboVendorCurrency.Focus();
        }
        #endregion

        #region Binding List
        private void FillList()
        {
            FillClasses();
            FillNatureList();
            FillCurrencyList();
        }

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

            string[] orderBy = new string[] { "Class1Code" };
            ProductClass1Collection oC1List = ProductClass1.LoadCollection(orderBy, true);
            oC1List.Add(new ProductClass1());
            cboClass1.DataSource = oC1List;
            cboClass1.DisplayMember = "Class1Code";
            cboClass1.ValueMember = "Class1Id";
        }

        private void FillClass2()
        {
            cboClass2.Items.Clear();

            string[] orderBy = new string[] { "Class2Code" };
            ProductClass2Collection oC2List = ProductClass2.LoadCollection(orderBy, true);
            oC2List.Add(new ProductClass2());
            cboClass2.DataSource = oC2List;
            cboClass2.DisplayMember = "Class2Code";
            cboClass2.ValueMember = "Class2Id";
        }

        private void FillClass3()
        {
            cboClass3.Items.Clear();

            string[] orderBy = new string[] { "Class3Code" };
            ProductClass3Collection oC3List = ProductClass3.LoadCollection(orderBy, true);
            oC3List.Add(new ProductClass3());
            cboClass3.DataSource = oC3List;
            cboClass3.DisplayMember = "Class3Code";
            cboClass3.ValueMember = "Class3Id";
        }

        private void FillClass4()
        {
            cboClass4.Items.Clear();

            string[] orderBy = new string[] { "Class4Code" };
            ProductClass4Collection oC4List = ProductClass4.LoadCollection(orderBy, true);
            oC4List.Add(new ProductClass4());
            cboClass4.DataSource = oC4List;
            cboClass4.DisplayMember = "Class4Code";
            cboClass4.ValueMember = "Class4Id";
        }
            
        private void FillClass5()
        {
            cboClass5.Items.Clear();

            string[] orderBy = new string[] { "Class5Code" };
            ProductClass5Collection oC5List = ProductClass5.LoadCollection(orderBy, true);
            oC5List.Add(new ProductClass5());
            cboClass5.DataSource = oC5List;
            cboClass5.DisplayMember = "Class5Code";
            cboClass5.ValueMember = "Class5Id";
        }
        
        private void FillClass6()
        {
            cboClass6.Items.Clear();

            string[] orderBy = new string[] { "Class6Code" };
            ProductClass6Collection oC6List = ProductClass6.LoadCollection(orderBy, true);
            oC6List.Add(new ProductClass6());
            cboClass6.DataSource = oC6List;
            cboClass6.DisplayMember = "Class6Code";
            cboClass6.ValueMember = "Class6Id";
        }
        #endregion

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

        private void FillCurrencyList()
        {
            cboVendorCurrency.Items.Clear();

            //2013.12.17 paulus: ID# 566.1，有機會咩都唔選，所以改為 list 內有 String.Empty 
            //string[] orderBy = new string[] { "CurrencyCode" };
            //CurrencyCollection oCnyList = Currency.LoadCollection(orderBy, true);
            //cboVendorCurrency.DataSource = oCnyList;
            //cboVendorCurrency.DisplayMember = "CurrencyCode";
            //cboVendorCurrency.ValueMember = "CurrencyId";

            Currency.LoadCombo(ref cboVendorCurrency, "CurrencyCode", false, true, String.Empty, String.Empty);
        }

        #endregion

        private void chkMemoAsProductName_Click(object sender, EventArgs e)
        {
            if (chkMemoAsProductName.Checked)
            {
                txtMemo.Text = txtProductName.Text;
            }
        }

        private void chkPoleAsProductName_Click(object sender, EventArgs e)
        {
            if (chkPoleAsProductName.Checked)
            {
                txtPole.Text = txtProductName.Text;
            }
        }

        private void chkRemarksAsProductName_Click(object sender, EventArgs e)
        {
            if (chkRemarksAsProductName.Checked)
            {
                txtRemarks.Text = txtProductName.Text;
            }
        }

        private void lnkNature_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProductNatureWizard wizNature = new ProductNatureWizard();
            wizNature.ShowDialog();
            FillNatureList();
        }

        private void txtBin_Z_LostFocus(object sender, EventArgs e)
        {
            txtProductName.Focus();
        }
    }
}