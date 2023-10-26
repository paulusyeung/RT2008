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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.SmartTag4Workplace.
    /// Date Created:   2020-08-09 02:14:16
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class SmartTag4Workplace
    {
        private Guid key = Guid.Empty;
        private Guid tagId = Guid.Empty;
        private string tagCode = String.Empty;
        private string tagName = String.Empty;
        private string tagName_Chs = String.Empty;
        private string tagName_Cht = String.Empty;
        private int priority = 0;

        /// <summary>
        /// Initialize an new empty SmartTag4Workplace object.
        /// </summary>
        public SmartTag4Workplace()
        {
        }
		
        /// <summary>
        /// Initialize a new SmartTag4Workplace object with the given parameters.
        /// </summary>
        public SmartTag4Workplace(Guid tagId, string tagCode, string tagName, string tagName_Chs, string tagName_Cht, int priority)
        {
                this.tagId = tagId;
                this.tagCode = tagCode;
                this.tagName = tagName;
                this.tagName_Chs = tagName_Chs;
                this.tagName_Cht = tagName_Cht;
                this.priority = priority;
        }	
		
        /// <summary>
        /// Loads a SmartTag4Workplace object from the database using the given TagId
        /// </summary>
        /// <param name="tagId">The primary key value</param>
        /// <returns>A SmartTag4Workplace object</returns>
        public static SmartTag4Workplace Load(Guid tagId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@TagId", tagId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spSmartTag4Workplace_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    SmartTag4Workplace result = new SmartTag4Workplace();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a SmartTag4Workplace object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A SmartTag4Workplace object</returns>
        public static SmartTag4Workplace LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spSmartTag4Workplace_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    SmartTag4Workplace result = new SmartTag4Workplace();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of SmartTag4Workplace objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the SmartTag4Workplace objects in the database.</returns>
        public static SmartTag4WorkplaceCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spSmartTag4Workplace_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SmartTag4Workplace objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the SmartTag4Workplace objects in the database ordered by the columns specified.</returns>
        public static SmartTag4WorkplaceCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spSmartTag4Workplace_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SmartTag4Workplace objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the SmartTag4Workplace objects in the database.</returns>
        public static SmartTag4WorkplaceCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spSmartTag4Workplace_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SmartTag4Workplace objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the SmartTag4Workplace objects in the database ordered by the columns specified.</returns>
        public static SmartTag4WorkplaceCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spSmartTag4Workplace_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of SmartTag4Workplace objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the SmartTag4Workplace objects in the database.</returns>
        public static SmartTag4WorkplaceCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            SmartTag4WorkplaceCollection result = new SmartTag4WorkplaceCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    SmartTag4Workplace tmp = new SmartTag4Workplace();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a SmartTag4Workplace object from the database.
        /// </summary>
        /// <param name="tagId">The primary key value</param>
        public static void Delete(Guid tagId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@TagId", tagId) };
            SqlHelper.Default.ExecuteNonQuery("spSmartTag4Workplace_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) tagId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) tagCode = reader.GetString(1);
                if (!reader.IsDBNull(2)) tagName = reader.GetString(2);
                if (!reader.IsDBNull(3)) tagName_Chs = reader.GetString(3);
                if (!reader.IsDBNull(4)) tagName_Cht = reader.GetString(4);
                if (!reader.IsDBNull(5)) priority = reader.GetInt32(5);
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

        public string TagCode
        {
            get { return tagCode; }
            set { tagCode = value; }
        }

        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
        }

        public string TagName_Chs
        {
            get { return tagName_Chs; }
            set { tagName_Chs = value; }
        }

        public string TagName_Cht
        {
            get { return tagName_Cht; }
            set { tagName_Cht = value; }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spSmartTag4Workplace_InsRec", "@TagId", out returnedValue, parameterValues);
            
            tagId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spSmartTag4Workplace_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[6];
            prams[0] = GetSqlParameter("@TagId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.TagId);
            prams[1] = GetSqlParameter("@TagCode", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.TagCode);
            prams[2] = GetSqlParameter("@TagName", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.TagName);
            prams[3] = GetSqlParameter("@TagName_Chs", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.TagName_Chs);
            prams[4] = GetSqlParameter("@TagName_Cht", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.TagName_Cht);
            prams[5] = GetSqlParameter("@Priority", ParameterDirection.Input, SqlDbType.Int, 4, this.Priority);
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
                GetSqlParameterWithoutDirection("@TagCode", SqlDbType.NVarChar, 10, this.TagCode),
                GetSqlParameterWithoutDirection("@TagName", SqlDbType.NVarChar, 64, this.TagName),
                GetSqlParameterWithoutDirection("@TagName_Chs", SqlDbType.NVarChar, 64, this.TagName_Chs),
                GetSqlParameterWithoutDirection("@TagName_Cht", SqlDbType.NVarChar, 64, this.TagName_Cht),
                GetSqlParameterWithoutDirection("@Priority", SqlDbType.Int, 4, this.Priority)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("tagId: " + tagId.ToString()).Append("\r\n");
            builder.Append("tagCode: " + tagCode.ToString()).Append("\r\n");
            builder.Append("tagName: " + tagName.ToString()).Append("\r\n");
            builder.Append("tagName_Chs: " + tagName_Chs.ToString()).Append("\r\n");
            builder.Append("tagName_Cht: " + tagName_Cht.ToString()).Append("\r\n");
            builder.Append("priority: " + priority.ToString()).Append("\r\n");
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
			
			SmartTag4WorkplaceCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = SmartTag4Workplace.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = SmartTag4Workplace.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (SmartTag4Workplace item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = IgnorThis(item, ParentFilter);
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
		
		
		private static bool IgnorThis(SmartTag4Workplace target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			PropertyInfo pi = target.GetType().GetProperty(parsed[0]);
			string filterField = (string)pi.GetValue(target, null);
			if (filterField.ToLower() == parsed[1].ToLower())
			{
				result = false;
			}
			return result;
		}

		private static string GetFormatedText(SmartTag4Workplace target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="SmartTag4Workplace">SmartTag4Workplace</see> objects.
    /// </summary>
    public class SmartTag4WorkplaceCollection : BindingList< SmartTag4Workplace>
    {
	}
}
