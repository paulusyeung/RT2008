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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.SupplierContact.
    /// Date Created:   2020-08-09 02:14:17
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class SupplierContact
    {
        private Guid key = Guid.Empty;
        private Guid contactId = Guid.Empty;
        private Guid supplierId = Guid.Empty;
        private bool primaryRec;
        private string firstName = String.Empty;
        private string lastName = String.Empty;
        private string fullName = String.Empty;
        private Guid salutationId = Guid.Empty;
        private Guid jobTitleId = Guid.Empty;
        private string duty = String.Empty;
        private Guid phoneTag1 = Guid.Empty;
        private string phoneTag1Value = String.Empty;
        private Guid phoneTag2 = Guid.Empty;
        private string phoneTag2Value = String.Empty;
        private Guid phoneTag3 = Guid.Empty;
        private string phoneTag3Value = String.Empty;
        private Guid phoneTag4 = Guid.Empty;
        private string phoneTag4Value = String.Empty;
        private string notes = String.Empty;

        /// <summary>
        /// Initialize an new empty SupplierContact object.
        /// </summary>
        public SupplierContact()
        {
        }
		
        /// <summary>
        /// Initialize a new SupplierContact object with the given parameters.
        /// </summary>
        public SupplierContact(Guid contactId, Guid supplierId, bool primaryRec, string firstName, string lastName, string fullName, Guid salutationId, Guid jobTitleId, string duty, Guid phoneTag1, string phoneTag1Value, Guid phoneTag2, string phoneTag2Value, Guid phoneTag3, string phoneTag3Value, Guid phoneTag4, string phoneTag4Value, string notes)
        {
                this.contactId = contactId;
                this.supplierId = supplierId;
                this.primaryRec = primaryRec;
                this.firstName = firstName;
                this.lastName = lastName;
                this.fullName = fullName;
                this.salutationId = salutationId;
                this.jobTitleId = jobTitleId;
                this.duty = duty;
                this.phoneTag1 = phoneTag1;
                this.phoneTag1Value = phoneTag1Value;
                this.phoneTag2 = phoneTag2;
                this.phoneTag2Value = phoneTag2Value;
                this.phoneTag3 = phoneTag3;
                this.phoneTag3Value = phoneTag3Value;
                this.phoneTag4 = phoneTag4;
                this.phoneTag4Value = phoneTag4Value;
                this.notes = notes;
        }	
		
        /// <summary>
        /// Loads a SupplierContact object from the database using the given ContactId
        /// </summary>
        /// <param name="contactId">The primary key value</param>
        /// <returns>A SupplierContact object</returns>
        public static SupplierContact Load(Guid contactId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ContactId", contactId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spSupplierContact_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    SupplierContact result = new SupplierContact();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a SupplierContact object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A SupplierContact object</returns>
        public static SupplierContact LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spSupplierContact_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    SupplierContact result = new SupplierContact();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of SupplierContact objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the SupplierContact objects in the database.</returns>
        public static SupplierContactCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spSupplierContact_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SupplierContact objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the SupplierContact objects in the database ordered by the columns specified.</returns>
        public static SupplierContactCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spSupplierContact_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SupplierContact objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the SupplierContact objects in the database.</returns>
        public static SupplierContactCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spSupplierContact_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SupplierContact objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the SupplierContact objects in the database ordered by the columns specified.</returns>
        public static SupplierContactCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spSupplierContact_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SupplierContact objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the SupplierContact objects in the database.</returns>
        public static SupplierContactCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            SupplierContactCollection result = new SupplierContactCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    SupplierContact tmp = new SupplierContact();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a SupplierContact object from the database.
        /// </summary>
        /// <param name="contactId">The primary key value</param>
        public static void Delete(Guid contactId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ContactId", contactId) };
            SqlHelper.Default.ExecuteNonQuery("spSupplierContact_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) contactId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) supplierId = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) primaryRec = reader.GetBoolean(2);
                if (!reader.IsDBNull(3)) firstName = reader.GetString(3);
                if (!reader.IsDBNull(4)) lastName = reader.GetString(4);
                if (!reader.IsDBNull(5)) fullName = reader.GetString(5);
                if (!reader.IsDBNull(6)) salutationId = reader.GetGuid(6);
                if (!reader.IsDBNull(7)) jobTitleId = reader.GetGuid(7);
                if (!reader.IsDBNull(8)) duty = reader.GetString(8);
                if (!reader.IsDBNull(9)) phoneTag1 = reader.GetGuid(9);
                if (!reader.IsDBNull(10)) phoneTag1Value = reader.GetString(10);
                if (!reader.IsDBNull(11)) phoneTag2 = reader.GetGuid(11);
                if (!reader.IsDBNull(12)) phoneTag2Value = reader.GetString(12);
                if (!reader.IsDBNull(13)) phoneTag3 = reader.GetGuid(13);
                if (!reader.IsDBNull(14)) phoneTag3Value = reader.GetString(14);
                if (!reader.IsDBNull(15)) phoneTag4 = reader.GetGuid(15);
                if (!reader.IsDBNull(16)) phoneTag4Value = reader.GetString(16);
                if (!reader.IsDBNull(17)) notes = reader.GetString(17);
            }
        }
		
        public void Delete()
        {
            Delete(this.ContactId);
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
                if (key != ContactId)
                    this.Delete();
                Update();
            }
        }

        public Guid ContactId
        {
            get { return contactId; }
            set { contactId = value; }
        }

        public Guid SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        public bool PrimaryRec
        {
            get { return primaryRec; }
            set { primaryRec = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public Guid SalutationId
        {
            get { return salutationId; }
            set { salutationId = value; }
        }

        public Guid JobTitleId
        {
            get { return jobTitleId; }
            set { jobTitleId = value; }
        }

        public string Duty
        {
            get { return duty; }
            set { duty = value; }
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

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spSupplierContact_InsRec", "@ContactId", out returnedValue, parameterValues);
            
            contactId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spSupplierContact_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[18];
            prams[0] = GetSqlParameter("@ContactId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.ContactId);
            prams[1] = GetSqlParameter("@SupplierId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.SupplierId);
            prams[2] = GetSqlParameter("@PrimaryRec", ParameterDirection.Input, SqlDbType.Bit, 1, this.PrimaryRec);
            prams[3] = GetSqlParameter("@FirstName", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.FirstName);
            prams[4] = GetSqlParameter("@LastName", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.LastName);
            prams[5] = GetSqlParameter("@FullName", ParameterDirection.Input, SqlDbType.NVarChar, 128, this.FullName);
            prams[6] = GetSqlParameter("@SalutationId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.SalutationId);
            prams[7] = GetSqlParameter("@JobTitleId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.JobTitleId);
            prams[8] = GetSqlParameter("@Duty", ParameterDirection.Input, SqlDbType.NVarChar, 512, this.Duty);
            prams[9] = GetSqlParameter("@PhoneTag1", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.PhoneTag1);
            prams[10] = GetSqlParameter("@PhoneTag1Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.PhoneTag1Value);
            prams[11] = GetSqlParameter("@PhoneTag2", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.PhoneTag2);
            prams[12] = GetSqlParameter("@PhoneTag2Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.PhoneTag2Value);
            prams[13] = GetSqlParameter("@PhoneTag3", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.PhoneTag3);
            prams[14] = GetSqlParameter("@PhoneTag3Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.PhoneTag3Value);
            prams[15] = GetSqlParameter("@PhoneTag4", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.PhoneTag4);
            prams[16] = GetSqlParameter("@PhoneTag4Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.PhoneTag4Value);
            prams[17] = GetSqlParameter("@Notes", ParameterDirection.Input, SqlDbType.NVarChar, 512, this.Notes);
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
                GetSqlParameterWithoutDirection("@ContactId", SqlDbType.UniqueIdentifier, 16, this.ContactId),
                GetSqlParameterWithoutDirection("@SupplierId", SqlDbType.UniqueIdentifier, 16, this.SupplierId),
                GetSqlParameterWithoutDirection("@PrimaryRec", SqlDbType.Bit, 1, this.PrimaryRec),
                GetSqlParameterWithoutDirection("@FirstName", SqlDbType.NVarChar, 64, this.FirstName),
                GetSqlParameterWithoutDirection("@LastName", SqlDbType.NVarChar, 64, this.LastName),
                GetSqlParameterWithoutDirection("@FullName", SqlDbType.NVarChar, 128, this.FullName),
                GetSqlParameterWithoutDirection("@SalutationId", SqlDbType.UniqueIdentifier, 16, this.SalutationId),
                GetSqlParameterWithoutDirection("@JobTitleId", SqlDbType.UniqueIdentifier, 16, this.JobTitleId),
                GetSqlParameterWithoutDirection("@Duty", SqlDbType.NVarChar, 512, this.Duty),
                GetSqlParameterWithoutDirection("@PhoneTag1", SqlDbType.UniqueIdentifier, 16, this.PhoneTag1),
                GetSqlParameterWithoutDirection("@PhoneTag1Value", SqlDbType.NVarChar, 64, this.PhoneTag1Value),
                GetSqlParameterWithoutDirection("@PhoneTag2", SqlDbType.UniqueIdentifier, 16, this.PhoneTag2),
                GetSqlParameterWithoutDirection("@PhoneTag2Value", SqlDbType.NVarChar, 64, this.PhoneTag2Value),
                GetSqlParameterWithoutDirection("@PhoneTag3", SqlDbType.UniqueIdentifier, 16, this.PhoneTag3),
                GetSqlParameterWithoutDirection("@PhoneTag3Value", SqlDbType.NVarChar, 64, this.PhoneTag3Value),
                GetSqlParameterWithoutDirection("@PhoneTag4", SqlDbType.UniqueIdentifier, 16, this.PhoneTag4),
                GetSqlParameterWithoutDirection("@PhoneTag4Value", SqlDbType.NVarChar, 64, this.PhoneTag4Value),
                GetSqlParameterWithoutDirection("@Notes", SqlDbType.NVarChar, 512, this.Notes)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("contactId: " + contactId.ToString()).Append("\r\n");
            builder.Append("supplierId: " + supplierId.ToString()).Append("\r\n");
            builder.Append("primaryRec: " + primaryRec.ToString()).Append("\r\n");
            builder.Append("firstName: " + firstName.ToString()).Append("\r\n");
            builder.Append("lastName: " + lastName.ToString()).Append("\r\n");
            builder.Append("fullName: " + fullName.ToString()).Append("\r\n");
            builder.Append("salutationId: " + salutationId.ToString()).Append("\r\n");
            builder.Append("jobTitleId: " + jobTitleId.ToString()).Append("\r\n");
            builder.Append("duty: " + duty.ToString()).Append("\r\n");
            builder.Append("phoneTag1: " + phoneTag1.ToString()).Append("\r\n");
            builder.Append("phoneTag1Value: " + phoneTag1Value.ToString()).Append("\r\n");
            builder.Append("phoneTag2: " + phoneTag2.ToString()).Append("\r\n");
            builder.Append("phoneTag2Value: " + phoneTag2Value.ToString()).Append("\r\n");
            builder.Append("phoneTag3: " + phoneTag3.ToString()).Append("\r\n");
            builder.Append("phoneTag3Value: " + phoneTag3Value.ToString()).Append("\r\n");
            builder.Append("phoneTag4: " + phoneTag4.ToString()).Append("\r\n");
            builder.Append("phoneTag4Value: " + phoneTag4Value.ToString()).Append("\r\n");
            builder.Append("notes: " + notes.ToString()).Append("\r\n");
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
			
			SupplierContactCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = SupplierContact.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = SupplierContact.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (SupplierContact item in source)
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
                    sourceList.Add(new Common.ComboItem(code, item.ContactId));
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
		
		
		private static bool IgnorThis(SupplierContact target, string parentFilter)
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
				SupplierContact parentTemplate = SupplierContact.Load(target.SupplierId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(SupplierContact target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="SupplierContact">SupplierContact</see> objects.
    /// </summary>
    public class SupplierContactCollection : BindingList< SupplierContact>
    {
	}
}
