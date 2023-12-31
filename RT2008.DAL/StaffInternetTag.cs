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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.StaffInternetTag.
    /// Date Created:   2020-08-09 02:14:16
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class StaffInternetTag
    {
        private Guid key = Guid.Empty;
        private Guid tagId = Guid.Empty;
        private Guid staffId = Guid.Empty;
        private Guid internetTag1 = Guid.Empty;
        private string internetTag1Value = String.Empty;
        private Guid internetTag2 = Guid.Empty;
        private string internetTag2Value = String.Empty;
        private Guid internetTag3 = Guid.Empty;
        private string internetTag3Value = String.Empty;
        private Guid internetTag4 = Guid.Empty;
        private string internetTag4Value = String.Empty;

        /// <summary>
        /// Initialize an new empty StaffInternetTag object.
        /// </summary>
        public StaffInternetTag()
        {
        }
		
        /// <summary>
        /// Initialize a new StaffInternetTag object with the given parameters.
        /// </summary>
        public StaffInternetTag(Guid tagId, Guid staffId, Guid internetTag1, string internetTag1Value, Guid internetTag2, string internetTag2Value, Guid internetTag3, string internetTag3Value, Guid internetTag4, string internetTag4Value)
        {
                this.tagId = tagId;
                this.staffId = staffId;
                this.internetTag1 = internetTag1;
                this.internetTag1Value = internetTag1Value;
                this.internetTag2 = internetTag2;
                this.internetTag2Value = internetTag2Value;
                this.internetTag3 = internetTag3;
                this.internetTag3Value = internetTag3Value;
                this.internetTag4 = internetTag4;
                this.internetTag4Value = internetTag4Value;
        }	
		
        /// <summary>
        /// Loads a StaffInternetTag object from the database using the given TagId
        /// </summary>
        /// <param name="tagId">The primary key value</param>
        /// <returns>A StaffInternetTag object</returns>
        public static StaffInternetTag Load(Guid tagId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@TagId", tagId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spStaffInternetTag_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    StaffInternetTag result = new StaffInternetTag();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a StaffInternetTag object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A StaffInternetTag object</returns>
        public static StaffInternetTag LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spStaffInternetTag_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    StaffInternetTag result = new StaffInternetTag();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of StaffInternetTag objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the StaffInternetTag objects in the database.</returns>
        public static StaffInternetTagCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spStaffInternetTag_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of StaffInternetTag objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the StaffInternetTag objects in the database ordered by the columns specified.</returns>
        public static StaffInternetTagCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spStaffInternetTag_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of StaffInternetTag objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the StaffInternetTag objects in the database.</returns>
        public static StaffInternetTagCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spStaffInternetTag_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of StaffInternetTag objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the StaffInternetTag objects in the database ordered by the columns specified.</returns>
        public static StaffInternetTagCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spStaffInternetTag_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of StaffInternetTag objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the StaffInternetTag objects in the database.</returns>
        public static StaffInternetTagCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            StaffInternetTagCollection result = new StaffInternetTagCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    StaffInternetTag tmp = new StaffInternetTag();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a StaffInternetTag object from the database.
        /// </summary>
        /// <param name="tagId">The primary key value</param>
        public static void Delete(Guid tagId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@TagId", tagId) };
            SqlHelper.Default.ExecuteNonQuery("spStaffInternetTag_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) tagId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) staffId = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) internetTag1 = reader.GetGuid(2);
                if (!reader.IsDBNull(3)) internetTag1Value = reader.GetString(3);
                if (!reader.IsDBNull(4)) internetTag2 = reader.GetGuid(4);
                if (!reader.IsDBNull(5)) internetTag2Value = reader.GetString(5);
                if (!reader.IsDBNull(6)) internetTag3 = reader.GetGuid(6);
                if (!reader.IsDBNull(7)) internetTag3Value = reader.GetString(7);
                if (!reader.IsDBNull(8)) internetTag4 = reader.GetGuid(8);
                if (!reader.IsDBNull(9)) internetTag4Value = reader.GetString(9);
            }
        }
		
        public void Delete()
        {
            Delete(this.TagId);
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
                if (key != TagId)
                    this.Delete();
                Update();
            }
        }

        public Guid TagId
        {
            get { return tagId; }
            set { tagId = value; }
        }

        public Guid StaffId
        {
            get { return staffId; }
            set { staffId = value; }
        }

        public Guid InternetTag1
        {
            get { return internetTag1; }
            set { internetTag1 = value; }
        }

        public string InternetTag1Value
        {
            get { return internetTag1Value; }
            set { internetTag1Value = value; }
        }

        public Guid InternetTag2
        {
            get { return internetTag2; }
            set { internetTag2 = value; }
        }

        public string InternetTag2Value
        {
            get { return internetTag2Value; }
            set { internetTag2Value = value; }
        }

        public Guid InternetTag3
        {
            get { return internetTag3; }
            set { internetTag3 = value; }
        }

        public string InternetTag3Value
        {
            get { return internetTag3Value; }
            set { internetTag3Value = value; }
        }

        public Guid InternetTag4
        {
            get { return internetTag4; }
            set { internetTag4 = value; }
        }

        public string InternetTag4Value
        {
            get { return internetTag4Value; }
            set { internetTag4Value = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spStaffInternetTag_InsRec", "@TagId", out returnedValue, parameterValues);
            
            tagId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spStaffInternetTag_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[10];
            prams[0] = GetSqlParameter("@TagId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.TagId);
            prams[1] = GetSqlParameter("@StaffId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.StaffId);
            prams[2] = GetSqlParameter("@InternetTag1", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.InternetTag1);
            prams[3] = GetSqlParameter("@InternetTag1Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.InternetTag1Value);
            prams[4] = GetSqlParameter("@InternetTag2", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.InternetTag2);
            prams[5] = GetSqlParameter("@InternetTag2Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.InternetTag2Value);
            prams[6] = GetSqlParameter("@InternetTag3", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.InternetTag3);
            prams[7] = GetSqlParameter("@InternetTag3Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.InternetTag3Value);
            prams[8] = GetSqlParameter("@InternetTag4", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.InternetTag4);
            prams[9] = GetSqlParameter("@InternetTag4Value", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.InternetTag4Value);
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
                GetSqlParameterWithoutDirection("@TagId", SqlDbType.UniqueIdentifier, 16, this.TagId),
                GetSqlParameterWithoutDirection("@StaffId", SqlDbType.UniqueIdentifier, 16, this.StaffId),
                GetSqlParameterWithoutDirection("@InternetTag1", SqlDbType.UniqueIdentifier, 16, this.InternetTag1),
                GetSqlParameterWithoutDirection("@InternetTag1Value", SqlDbType.NVarChar, 64, this.InternetTag1Value),
                GetSqlParameterWithoutDirection("@InternetTag2", SqlDbType.UniqueIdentifier, 16, this.InternetTag2),
                GetSqlParameterWithoutDirection("@InternetTag2Value", SqlDbType.NVarChar, 64, this.InternetTag2Value),
                GetSqlParameterWithoutDirection("@InternetTag3", SqlDbType.UniqueIdentifier, 16, this.InternetTag3),
                GetSqlParameterWithoutDirection("@InternetTag3Value", SqlDbType.NVarChar, 64, this.InternetTag3Value),
                GetSqlParameterWithoutDirection("@InternetTag4", SqlDbType.UniqueIdentifier, 16, this.InternetTag4),
                GetSqlParameterWithoutDirection("@InternetTag4Value", SqlDbType.NVarChar, 64, this.InternetTag4Value)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("tagId: " + tagId.ToString()).Append("\r\n");
            builder.Append("staffId: " + staffId.ToString()).Append("\r\n");
            builder.Append("internetTag1: " + internetTag1.ToString()).Append("\r\n");
            builder.Append("internetTag1Value: " + internetTag1Value.ToString()).Append("\r\n");
            builder.Append("internetTag2: " + internetTag2.ToString()).Append("\r\n");
            builder.Append("internetTag2Value: " + internetTag2Value.ToString()).Append("\r\n");
            builder.Append("internetTag3: " + internetTag3.ToString()).Append("\r\n");
            builder.Append("internetTag3Value: " + internetTag3Value.ToString()).Append("\r\n");
            builder.Append("internetTag4: " + internetTag4.ToString()).Append("\r\n");
            builder.Append("internetTag4Value: " + internetTag4Value.ToString()).Append("\r\n");
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
			
			StaffInternetTagCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = StaffInternetTag.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = StaffInternetTag.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (StaffInternetTag item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.StaffId != Guid.Empty)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.TagId));
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
		
		
		private static bool IgnorThis(StaffInternetTag target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.StaffId == Guid.Empty)
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
				StaffInternetTag parentTemplate = StaffInternetTag.Load(target.StaffId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(StaffInternetTag target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="StaffInternetTag">StaffInternetTag</see> objects.
    /// </summary>
    public class StaffInternetTagCollection : BindingList< StaffInternetTag>
    {
	}
}
