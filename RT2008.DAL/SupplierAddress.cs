﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection;
using System.Text;
using Gizmox.WebGUI.Forms;
using System.Xml;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace RT2008.DAL
{
    /// <summary>
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.SupplierAddress.
    /// Date Created:   2020-08-09 02:14:17
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class SupplierAddress
    {
        private Guid key = Guid.Empty;
        private Guid addressId = Guid.Empty;
        private Guid supplierId = Guid.Empty;
        private Guid addressTypeId = Guid.Empty;
        private string address = String.Empty;
        private string postalCode = String.Empty;
        private Guid countryId = Guid.Empty;
        private Guid provinceId = Guid.Empty;
        private Guid cityId = Guid.Empty;
        private Guid phoneTag1 = Guid.Empty;
        private string phoneTag1Value = String.Empty;
        private Guid phoneTag2 = Guid.Empty;
        private string phoneTag2Value = String.Empty;
        private Guid phoneTag3 = Guid.Empty;
        private string phoneTag3Value = String.Empty;
        private Guid phoneTag4 = Guid.Empty;
        private string phoneTag4Value = String.Empty;

        /// <summary>
        /// Initialize an new empty SupplierAddress object.
        /// </summary>
        public SupplierAddress()
        {
        }
		
        /// <summary>
        /// Initialize a new SupplierAddress object with the given parameters.
        /// </summary>
        public SupplierAddress(Guid addressId, Guid supplierId, Guid addressTypeId, string address, string postalCode, Guid countryId, Guid provinceId, Guid cityId, Guid phoneTag1, string phoneTag1Value, Guid phoneTag2, string phoneTag2Value, Guid phoneTag3, string phoneTag3Value, Guid phoneTag4, string phoneTag4Value)
        {
                this.addressId = addressId;
                this.supplierId = supplierId;
                this.addressTypeId = addressTypeId;
                this.address = address;
                this.postalCode = postalCode;
                this.countryId = countryId;
                this.provinceId = provinceId;
                this.cityId = cityId;
                this.phoneTag1 = phoneTag1;
                this.phoneTag1Value = phoneTag1Value;
                this.phoneTag2 = phoneTag2;
                this.phoneTag2Value = phoneTag2Value;
                this.phoneTag3 = phoneTag3;
                this.phoneTag3Value = phoneTag3Value;
                this.phoneTag4 = phoneTag4;
                this.phoneTag4Value = phoneTag4Value;
        }	
		
        /// <summary>
        /// Loads a SupplierAddress object from the database using the given AddressId
        /// </summary>
        /// <param name="addressId">The primary key value</param>
        /// <returns>A SupplierAddress object</returns>
        public static SupplierAddress Load(Guid addressId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@AddressId", addressId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spSupplierAddress_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    SupplierAddress result = new SupplierAddress();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a SupplierAddress object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A SupplierAddress object</returns>
        public static SupplierAddress LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spSupplierAddress_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    SupplierAddress result = new SupplierAddress();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of SupplierAddress objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the SupplierAddress objects in the database.</returns>
        public static SupplierAddressCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spSupplierAddress_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SupplierAddress objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the SupplierAddress objects in the database ordered by the columns specified.</returns>
        public static SupplierAddressCollection LoadCollection(string[] orderByColumns, bool ascending)
        {
            StringBuilder orderClause = new StringBuilder();
            for (int i = 0; i < orderByColumns.Length; i++)
            {
                orderClause.Append(orderByColumns[i]);

                if (i != orderByColumns.Length-1)
                    orderClause.Append(", ");
            }

            if (ascending)
                orderClause.Append(" ASC");
            else
                orderClause.Append(" DESC");

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@OrderBy", orderClause.ToString()) };
            return LoadCollection("spSupplierAddress_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SupplierAddress objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the SupplierAddress objects in the database.</returns>
        public static SupplierAddressCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spSupplierAddress_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SupplierAddress objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the SupplierAddress objects in the database ordered by the columns specified.</returns>
        public static SupplierAddressCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
        {
            StringBuilder orderClause = new StringBuilder();
            for (int i = 0; i < orderByColumns.Length; i++)
            {
                orderClause.Append(orderByColumns[i]);

                if (i != orderByColumns.Length-1)
                    orderClause.Append(", ");
            }

            if (ascending)
                orderClause.Append(" ASC");
            else
                orderClause.Append(" DESC");

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause), new SqlParameter("@OrderBy", orderClause.ToString()) };
            return LoadCollection("spSupplierAddress_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SupplierAddress objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the SupplierAddress objects in the database.</returns>
        public static SupplierAddressCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            SupplierAddressCollection result = new SupplierAddressCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    SupplierAddress tmp = new SupplierAddress();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a SupplierAddress object from the database.
        /// </summary>
        /// <param name="addressId">The primary key value</param>
        public static void Delete(Guid addressId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@AddressId", addressId) };
            SqlHelper.Default.ExecuteNonQuery("spSupplierAddress_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) addressId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) supplierId = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) addressTypeId = reader.GetGuid(2);
                if (!reader.IsDBNull(3)) address = reader.GetString(3);
                if (!reader.IsDBNull(4)) postalCode = reader.GetString(4);
                if (!reader.IsDBNull(5)) countryId = reader.GetGuid(5);
                if (!reader.IsDBNull(6)) provinceId = reader.GetGuid(6);
                if (!reader.IsDBNull(7)) cityId = reader.GetGuid(7);
                if (!reader.IsDBNull(8)) phoneTag1 = reader.GetGuid(8);
                if (!reader.IsDBNull(9)) phoneTag1Value = reader.GetString(9);
                if (!reader.IsDBNull(10)) phoneTag2 = reader.GetGuid(10);
                if (!reader.IsDBNull(11)) phoneTag2Value = reader.GetString(11);
                if (!reader.IsDBNull(12)) phoneTag3 = reader.GetGuid(12);
                if (!reader.IsDBNull(13)) phoneTag3Value = reader.GetString(13);
                if (!reader.IsDBNull(14)) phoneTag4 = reader.GetGuid(14);
                if (!reader.IsDBNull(15)) phoneTag4Value = reader.GetString(15);
            }
        }
		
        public void Delete()
        {
            Delete(this.AddressId);
        }

        public void Save()
        {
            //  We use the key field which will have its default value unless it is set by Load(). When we save we can know if
            //  we need to do an insert (key == null) an update (key == primaryKey) or a 
            //  delete+update (key != null && key != primaryKey)

            if (key == Guid.Empty)
                Insert();
            else
            {
                if (key != AddressId)
                    this.Delete();
                Update();
            }
        }

        public Guid AddressId
        {
            get { return addressId; }
            set { addressId = value; }
        }

        public Guid SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        public Guid AddressTypeId
        {
            get { return addressTypeId; }
            set { addressTypeId = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        public Guid CountryId
        {
            get { return countryId; }
            set { countryId = value; }
        }

        public Guid ProvinceId
        {
            get { return provinceId; }
            set { provinceId = value; }
        }

        public Guid CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }

        public Guid PhoneTag1
        {
            get { return phoneTag1; }
            set { phoneTag1 = value; }
        }

        public string PhoneTag1Value
        {
            get { return phoneTag1Value; }
            set { phoneTag1Value = value; }
        }

        public Guid PhoneTag2
        {
            get { return phoneTag2; }
            set { phoneTag2 = value; }
        }

        public string PhoneTag2Value
        {
            get { return phoneTag2Value; }
            set { phoneTag2Value = value; }
        }

        public Guid PhoneTag3
        {
            get { return phoneTag3; }
            set { phoneTag3 = value; }
        }

        public string PhoneTag3Value
        {
            get { return phoneTag3Value; }
            set { phoneTag3Value = value; }
        }

        public Guid PhoneTag4
        {
            get { return phoneTag4; }
            set { phoneTag4 = value; }
        }

        public string PhoneTag4Value
        {
            get { return phoneTag4Value; }
            set { phoneTag4Value = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spSupplierAddress_InsRec", "@AddressId", out returnedValue, parameterValues);
            
            addressId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spSupplierAddress_UpdRec", parameterValues);
        }
        
        /// <summary>
        /// Gets the SQL parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private SqlParameter GetSqlParameter(string name, ParameterDirection direction, SqlDbType dbType, int size, object value)
        {
            SqlParameter p = new SqlParameter(name, dbType, size);
            p.Value = value;
            p.Direction = direction;
            return p;
        }

        private SqlParameter[] GetInsertParameterValues()
        {
            SqlParameter[] prams = new SqlParameter[16];
            prams[0] = GetSqlParameter("@AddressId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.AddressId);
            prams[1] = GetSqlParameter("@SupplierId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.SupplierId);
            prams[2] = GetSqlParameter("@AddressTypeId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.AddressTypeId);
            prams[3] = GetSqlParameter("@Address", ParameterDirection.Input, SqlDbType.NVarChar, 512, this.Address);
            prams[4] = GetSqlParameter("@PostalCode", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.PostalCode);
            prams[5] = GetSqlParameter("@CountryId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.CountryId);
            prams[6] = GetSqlParameter("@ProvinceId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ProvinceId);
            prams[7] = GetSqlParameter("@CityId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.CityId);
            prams[8] = GetSqlParameter("@PhoneTag1", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.PhoneTag1);
            prams[9] = GetSqlParameter("@PhoneTag1Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.PhoneTag1Value);
            prams[10] = GetSqlParameter("@PhoneTag2", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.PhoneTag2);
            prams[11] = GetSqlParameter("@PhoneTag2Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.PhoneTag2Value);
            prams[12] = GetSqlParameter("@PhoneTag3", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.PhoneTag3);
            prams[13] = GetSqlParameter("@PhoneTag3Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.PhoneTag3Value);
            prams[14] = GetSqlParameter("@PhoneTag4", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.PhoneTag4);
            prams[15] = GetSqlParameter("@PhoneTag4Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.PhoneTag4Value);
            return prams;
        }
		
        /// <summary>
        /// Gets the SQL parameter without direction.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private SqlParameter GetSqlParameterWithoutDirection(string name, SqlDbType dbType, int size, object value)
        {
            SqlParameter p = new SqlParameter(name, dbType, size);
            p.Value = value;
            return p;
        }
        
        private SqlParameter[] GetUpdateParameterValues()
        {
            return new SqlParameter[] 
            {
                GetSqlParameterWithoutDirection("@AddressId", SqlDbType.UniqueIdentifier, 16, this.AddressId),
                GetSqlParameterWithoutDirection("@SupplierId", SqlDbType.UniqueIdentifier, 16, this.SupplierId),
                GetSqlParameterWithoutDirection("@AddressTypeId", SqlDbType.UniqueIdentifier, 16, this.AddressTypeId),
                GetSqlParameterWithoutDirection("@Address", SqlDbType.NVarChar, 512, this.Address),
                GetSqlParameterWithoutDirection("@PostalCode", SqlDbType.NVarChar, 10, this.PostalCode),
                GetSqlParameterWithoutDirection("@CountryId", SqlDbType.UniqueIdentifier, 16, this.CountryId),
                GetSqlParameterWithoutDirection("@ProvinceId", SqlDbType.UniqueIdentifier, 16, this.ProvinceId),
                GetSqlParameterWithoutDirection("@CityId", SqlDbType.UniqueIdentifier, 16, this.CityId),
                GetSqlParameterWithoutDirection("@PhoneTag1", SqlDbType.UniqueIdentifier, 16, this.PhoneTag1),
                GetSqlParameterWithoutDirection("@PhoneTag1Value", SqlDbType.NVarChar, 64, this.PhoneTag1Value),
                GetSqlParameterWithoutDirection("@PhoneTag2", SqlDbType.UniqueIdentifier, 16, this.PhoneTag2),
                GetSqlParameterWithoutDirection("@PhoneTag2Value", SqlDbType.NVarChar, 64, this.PhoneTag2Value),
                GetSqlParameterWithoutDirection("@PhoneTag3", SqlDbType.UniqueIdentifier, 16, this.PhoneTag3),
                GetSqlParameterWithoutDirection("@PhoneTag3Value", SqlDbType.NVarChar, 64, this.PhoneTag3Value),
                GetSqlParameterWithoutDirection("@PhoneTag4", SqlDbType.UniqueIdentifier, 16, this.PhoneTag4),
                GetSqlParameterWithoutDirection("@PhoneTag4Value", SqlDbType.NVarChar, 64, this.PhoneTag4Value)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("addressId: " + addressId.ToString()).Append("\r\n");
            builder.Append("supplierId: " + supplierId.ToString()).Append("\r\n");
            builder.Append("addressTypeId: " + addressTypeId.ToString()).Append("\r\n");
            builder.Append("address: " + address.ToString()).Append("\r\n");
            builder.Append("postalCode: " + postalCode.ToString()).Append("\r\n");
            builder.Append("countryId: " + countryId.ToString()).Append("\r\n");
            builder.Append("provinceId: " + provinceId.ToString()).Append("\r\n");
            builder.Append("cityId: " + cityId.ToString()).Append("\r\n");
            builder.Append("phoneTag1: " + phoneTag1.ToString()).Append("\r\n");
            builder.Append("phoneTag1Value: " + phoneTag1Value.ToString()).Append("\r\n");
            builder.Append("phoneTag2: " + phoneTag2.ToString()).Append("\r\n");
            builder.Append("phoneTag2Value: " + phoneTag2Value.ToString()).Append("\r\n");
            builder.Append("phoneTag3: " + phoneTag3.ToString()).Append("\r\n");
            builder.Append("phoneTag3Value: " + phoneTag3Value.ToString()).Append("\r\n");
            builder.Append("phoneTag4: " + phoneTag4.ToString()).Append("\r\n");
            builder.Append("phoneTag4Value: " + phoneTag4Value.ToString()).Append("\r\n");
            builder.Append("\r\n");
            return builder.ToString();
        }	
		
		#region Load ComboBox
        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. "FieldName"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
		public static void LoadCombo(ref ComboBox ddList, string TextField, bool SwitchLocale)
		{
			LoadCombo(ref ddList, TextField, SwitchLocale, false, string.Empty, string.Empty, new string[]{ TextField });
		}
		
        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. "FieldName"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="OrderBy">Sorting order, string array, e.g. {"FieldName1", "FiledName2"}</param>
		public static void LoadCombo(ref ComboBox ddList, string TextField, bool SwitchLocale, string[] OrderBy)
		{
			LoadCombo(ref ddList, TextField, SwitchLocale, false, string.Empty, string.Empty, OrderBy);
		}

        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. "FieldName"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="BlankLine">add blank label text to ComboBox or not</param>
        /// <param name="BlankLineText">the blank label text</param>
        /// <param name="WhereClause">Where Clause for SQL Statement. e.g. "FieldName = 'SomeCondition'"</param>
		public static void LoadCombo(ref ComboBox ddList, string TextField, bool SwitchLocale, bool BlankLine, string BlankLineText, string WhereClause)
		{
			LoadCombo(ref ddList, TextField, SwitchLocale, BlankLine, BlankLineText, string.Empty, WhereClause, new String[] { TextField });
		}

        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. "FieldName"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="BlankLine">add blank label text to ComboBox or not</param>
        /// <param name="BlankLineText">the blank label text</param>
        /// <param name="WhereClause">Where Clause for SQL Statement. e.g. "FieldName = 'SomeCondition'"</param>
        /// <param name="OrderBy">Sorting order, string array, e.g. {"FieldName1", "FiledName2"}</param>
		public static void LoadCombo(ref ComboBox ddList, string TextField, bool SwitchLocale, bool BlankLine, string BlankLineText, string WhereClause, string[] OrderBy)
		{
			LoadCombo(ref ddList, TextField, SwitchLocale, BlankLine, BlankLineText, string.Empty, WhereClause, OrderBy);
		}

        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. "FieldName"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="BlankLine">add blank label text to ComboBox or not</param>
        /// <param name="BlankLineText">the blank label text</param>
        /// <param name="ParentFilter">e.g. "ForeignFieldName = 'value'"</param>
        /// <param name="WhereClause">Where Clause for SQL Statement. e.g. "FieldName = 'SomeCondition'"</param>
        /// <param name="OrderBy">Sorting order, string array, e.g. {"FieldName1", "FiledName2"}</param>
		public static void LoadCombo(ref ComboBox ddList, string TextField, bool SwitchLocale, bool BlankLine, string BlankLineText, string ParentFilter, string WhereClause, string[] OrderBy)
		{
			string [] textField = {TextField};
			LoadCombo(ref ddList, textField, "{0}", SwitchLocale, BlankLine, BlankLineText, ParentFilter, WhereClause, OrderBy);
		}

        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. new string[]{"FieldName1", "FieldName2", ...}</param>
        /// <param name="TextFormatString">e.g. "{0} - {1}"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="BlankLine">add blank label text to ComboBox or not</param>
        /// <param name="BlankLineText">the blank label text</param>
        /// <param name="WhereClause">Where Clause for SQL Statement. e.g. "FieldName = 'SomeCondition'"</param>
        /// <param name="OrderBy">Sorting order, string array, e.g. {"FieldName1", "FiledName2"}</param>
		public static void LoadCombo(ref ComboBox ddList, string [] TextField, string TextFormatString, bool SwitchLocale, bool BlankLine, string BlankLineText, string WhereClause, string[] OrderBy)
		{
			LoadCombo(ref ddList, TextField, TextFormatString, SwitchLocale, BlankLine, BlankLineText, string.Empty, WhereClause, OrderBy);
		}
		
        /// <summary>
        /// Only support the ComboBox control from WinForm/Visual WebGUI
        /// </summary>
        /// <param name="ddList">the ComboBox control from WinForm/Visual WebGUI</param>
        /// <param name="TextField">e.g. new string[]{"FieldName1", "FieldName2", ...}</param>
        /// <param name="TextFormatString">e.g. "{0} - {1}"</param>
        /// <param name="SwitchLocale">Can be localized, if the FieldName has locale suffix, e.g. '_chs'</param>
        /// <param name="BlankLine">add blank label text to ComboBox or not</param>
        /// <param name="BlankLineText">the blank label text</param>
        /// <param name="ParentFilter">e.g. "ForeignFieldName = 'value'"</param>
        /// <param name="WhereClause">Where Clause for SQL Statement. e.g. "FieldName = 'SomeCondition'"</param>
        /// <param name="OrderBy">Sorting order, string array, e.g. {"FieldName1", "FiledName2"}</param>
		public static void LoadCombo(ref ComboBox ddList, string [] TextField, string TextFormatString, bool SwitchLocale, bool BlankLine, string BlankLineText, string ParentFilter, string WhereClause, string[] OrderBy)
		{
			if (SwitchLocale)
			{
				TextField = GetSwitchLocale(TextField);
			}
			ddList.Items.Clear();						
			
			SupplierAddressCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = SupplierAddress.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = SupplierAddress.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (SupplierAddress item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.SupplierId != Guid.Empty)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.AddressId));
				}
			}			

            ddList.DataSource = sourceList;
            ddList.DisplayMember = "Code";
            ddList.ValueMember = "Id";
			
			if (ddList.Items.Count > 0)
			{
			    ddList.SelectedIndex = 0;
            }
		}
		
		#endregion
		
		
		private static bool IgnorThis(SupplierAddress target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.SupplierId == Guid.Empty)
			{
				PropertyInfo pi = target.GetType().GetProperty(parsed[0]);
				string filterField = (string)pi.GetValue(target, null);
				if (filterField.ToLower() == parsed[1].ToLower())
				{
					result = false;
				}
			}
			else
			{
				SupplierAddress parentTemplate = SupplierAddress.Load(target.SupplierId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(SupplierAddress target, string [] textField, string textFormatString)
		{
			for (int i = 0; i < textField.Length; i++)
			{
				PropertyInfo pi = target.GetType().GetProperty(textField[i]);
				textFormatString = textFormatString.Replace("{" + i.ToString() +"}", pi != null ? pi.GetValue(target, null).ToString() : string.Empty);
			}
			return textFormatString;
		}
		
		private static string [] GetSwitchLocale(string [] source)
		{
			switch (Common.Config.CurrentLanguageId)
			{
				case 2:
					source[source.Length - 1] += "_Chs";
					break;
				case 3:
					source[source.Length - 1] += "_Cht";
					break;
			}
			return source;
		}
    }


    /// <summary>
    /// Represents a collection of <see cref="SupplierAddress">SupplierAddress</see> objects.
    /// </summary>
    public class SupplierAddressCollection : BindingList< SupplierAddress>
    {
	}
}
