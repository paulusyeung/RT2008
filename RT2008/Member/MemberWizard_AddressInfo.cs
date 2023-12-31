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
    public partial class MemberWizard_AddressInfo : UserControl
    {
        public MemberWizard_AddressInfo()
        {
            InitializeComponent();
            InitialPhoneTag();
            FillComboList();
        }

        private void InitialPhoneTag()
        {
            RT2008.Controls.PhoneTag oTag = new RT2008.Controls.PhoneTag(this);
            oTag.SetPhoneTag();
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

        #region Fill Combo list
        private void FillComboList()
        {
            FillAddressList();
            FillCountryList();
            FillProvinceList(System.Guid.Empty);
            FillCityList(System.Guid.Empty);
        }

        private void FillAddressList()
        {
            cboAddressType.DataSource = null;
            cboAddressType.Items.Clear();

            string[] orderBy = new string[] { "AddressTypeName" };
            MemberAddressTypeCollection countryList = MemberAddressType.LoadCollection(orderBy, true);

            cboAddressType.DataSource = countryList;
            cboAddressType.DisplayMember = "AddressTypeName";
            cboAddressType.ValueMember = "AddressTypeId";
        }

        private void FillCountryList()
        {
            cboCountry.DataSource = null;
            cboCountry.Items.Clear();

            string[] orderBy = new string[] { "CountryName" };
            CountryCollection countryList = Country.LoadCollection(orderBy, true);
            countryList.Add(new Country());

            cboCountry.DataSource = countryList;
            cboCountry.DisplayMember = "CountryName";
            cboCountry.ValueMember = "CountryId";

            cboCountry.SelectedIndex = cboCountry.Items.Count - 1;
        }

        private void FillProvinceList(System.Guid CountryId)
        {
            cboProvince.DataSource = null;
            cboProvince.Items.Clear();

            string sql = " CountryId = '" + CountryId.ToString() + "'";
            string[] orderBy = new string[] { "ProvinceName" };
            ProvinceCollection provinceList = Province.LoadCollection(sql, orderBy, true);
            provinceList.Add(new Province());

            cboProvince.DataSource = provinceList;
            cboProvince.DisplayMember = "ProvinceName";
            cboProvince.ValueMember = "ProvinceId";

            cboProvince.SelectedIndex = cboProvince.Items.Count - 1;
        }

        private void FillCityList(System.Guid ProvinceId)
        {
            cboCity.DataSource = null;
            cboCity.Items.Clear();

            string sql = " ProvinceId = '" + ProvinceId.ToString() + "'";
            string[] orderBy = new string[] { "CityName" };
            CityCollection cityList = City.LoadCollection(sql, orderBy, true);
            cityList.Add(new City());

            cboCity.DataSource = cityList;
            cboCity.DisplayMember = "CityName";
            cboCity.ValueMember = "CityId";

            cboCity.SelectedIndex = cboCity.Items.Count - 1;
        }
        #endregion

        private void LoadAddress(Guid addressTypeId)
        {
            string sql = "MemberId = '" + this.MemberId.ToString() + "' AND AddressTypeId = '" + addressTypeId.ToString() + "'";
            MemberAddress oAddress = MemberAddress.LoadWhere(sql);
            if (oAddress != null)
            {
                txtAddress.Text = oAddress.Address;
                txtPostalCode.Text = oAddress.PostalCode;
                cboCountry.SelectedValue = oAddress.CountryId;
                cboProvince.SelectedValue = oAddress.ProvinceId;
                cboCity.SelectedValue = oAddress.CityId;
                txtDistrict.Text = oAddress.District;

                txtPhoneTag1.Text = oAddress.PhoneTag1Value;
                txtPhoneTag2.Text = oAddress.PhoneTag2Value;
                txtPhoneTag3.Text = oAddress.PhoneTag3Value;
                txtPhoneTag4.Text = oAddress.PhoneTag4Value;
                txtPhoneTag5.Text = LoadPaperNumberFromVIPData(addressTypeId);
            }
        }

        private string LoadPaperNumberFromVIPData(Guid addressTypeId)
        {
            string result = string.Empty;
            string key = "Address_Phone_Pager_" + addressTypeId.ToString("N");
            string sql = "MemberId = '" + this.MemberId.ToString() + "'";
            MemberVipData oVip = MemberVipData.LoadWhere(sql);
            if (oVip != null)
            {
                result = oVip.GetMetadata(key);
            }
            return result;
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
    }
}