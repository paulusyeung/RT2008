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

namespace RT2008.Supplier
{
    public partial class SupplierWizard_Personal : UserControl
    {
        public SupplierWizard_Personal()
        {
            InitializeComponent();
            InitialSmartTags();
            FillComboList();
        }

        private void InitialSmartTags()
        {
            string[] orderBy = new string[] { "Priority" };
            SmartTag4SupplierCollection smartTagList = SmartTag4Supplier.LoadCollection(orderBy, true);

            SmartTag oTag = new SmartTag(this);
            oTag.SupplierSmartTagList = smartTagList;
            oTag.SetSmartTags();
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

        #region Fill Combo list
        private void FillComboList()
        {
            FillAddressList();
            FillCountryList();
            //2014.01.04 paulus: 屬於 cascade combobox 既然係吉嘅就無謂 fill，浪費時間
            //FillProvinceList(System.Guid.Empty);
            //FillCityList(System.Guid.Empty);
        }

        private void FillAddressList()
        {
            cboAddressType.DataSource = null;
            cboAddressType.Items.Clear();

            //string[] orderBy = new string[] { "AddressTypeName" };
            //SupplierAddressTypeCollection countryList = SupplierAddressType.LoadCollection(orderBy, true);

            //cboAddressType.DataSource = countryList;
            //cboAddressType.DisplayMember = "AddressTypeName";
            //cboAddressType.ValueMember = "AddressTypeId";
            SupplierAddressType.LoadCombo(ref cboAddressType, "AddressTypeName", false);
        }

        private void FillCountryList()
        {
            cboCountry.DataSource = null;
            cboCountry.Items.Clear();

            Country.LoadCombo(ref cboCountry, "CountryName", false, true, String.Empty, String.Empty);
            cboCountry.SelectedIndex = 0;

            cboProvince.DataSource = null;
            cboProvince.Items.Clear();
            cboCity.DataSource = null;
            cboCity.Items.Clear();
        }

        private void FillProvinceList(System.Guid CountryId)
        {
            cboProvince.DataSource = null;
            cboProvince.Items.Clear();

            string sql = " CountryId = '" + CountryId.ToString() + "'";
            Province.LoadCombo(ref cboProvince, "ProvinceName", false, true, String.Empty, sql);
            cboProvince.SelectedIndex = 0;

            cboCity.DataSource = null;
            cboCity.Items.Clear();
        }

        private void FillCityList(System.Guid ProvinceId)
        {
            cboCity.DataSource = null;
            cboCity.Items.Clear();

            string sql = " ProvinceId = '" + ProvinceId.ToString() + "'";
            City.LoadCombo(ref cboCity, "CityName", false, true, String.Empty, sql);
            cboCity.SelectedIndex = 0;
        }
        #endregion

        private void LoadAddress(Guid addressTypeId)
        {
            string sql = "SupplierId = '" + this.SupplierId.ToString() + "' AND AddressTypeId = '" + addressTypeId.ToString() + "'";
            SupplierAddress oAddress = SupplierAddress.LoadWhere(sql);
            if (oAddress != null)
            {
                txtAddress.Text = oAddress.Address;
                txtPostalCode.Text = oAddress.PostalCode;
                cboCountry.SelectedValue = oAddress.CountryId;
                cboProvince.SelectedValue = oAddress.ProvinceId;
                cboCity.SelectedValue = oAddress.CityId;
            }
        }

        private void cboCountry_SelectedIndexChangedQueued(object sender, EventArgs e)
        {
            if (cboCountry.SelectedValue != null)
            {
                if (Common.Utility.IsGUID(cboCountry.SelectedValue.ToString()))
                {
                    FillProvinceList(new System.Guid(cboCountry.SelectedValue.ToString()));
                }
            }
        }

        private void cboProvince_SelectedIndexChangedQueued(object sender, EventArgs e)
        {
            if (cboProvince.SelectedValue != null)
            {
                if (Common.Utility.IsGUID(cboProvince.SelectedValue.ToString()))
                {
                    FillCityList(new System.Guid(cboProvince.SelectedValue.ToString()));
                }
            }
        }

        private void cboAddressType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAddressType.SelectedValue != null)
            {
                if (Common.Utility.IsGUID(cboAddressType.SelectedValue.ToString()))
                {
                    LoadAddress(new Guid(cboAddressType.SelectedValue.ToString()));
                }
            }
        }
    }
}