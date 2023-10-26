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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.LineOfOperation.
    /// Date Created:   2020-08-09 02:14:11
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class LineOfOperation
    {
        private Guid key = Guid.Empty;
        private Guid lineOfOperationId = Guid.Empty;
        private Guid parentLine = Guid.Empty;
        private string lineOfOperationCode = String.Empty;
        private string lineOfOperationName = String.Empty;
        private string lineOfOperationName_Chs = String.Empty;
        private string lineOfOperationName_Cht = String.Empty;
        private string currencyCode = String.Empty;
        private bool primaryLine;

        /// <summary>
        /// Initialize an new empty LineOfOperation object.
        /// </summary>
        public LineOfOperation()
        {
        }
		
        /// <summary>
        /// Initialize a new LineOfOperation object with the given parameters.
        /// </summary>
        public LineOfOperation(Guid lineOfOperationId, Guid parentLine, string lineOfOperationCode, string lineOfOperationName, string lineOfOperationName_Chs, string lineOfOperationName_Cht, string currencyCode, bool primaryLine)
        {
                this.lineOfOperationId = lineOfOperationId;
                this.parentLine = parentLine;
                this.lineOfOperationCode = lineOfOperationCode;
                this.lineOfOperationName = lineOfOperationName;
                this.lineOfOperationName_Chs = lineOfOperationName_Chs;
                this.lineOfOperationName_Cht = lineOfOperationName_Cht;
                this.currencyCode = currencyCode;
                this.primaryLine = primaryLine;
        }	
		
        /// <summary>
        /// Loads a LineOfOperation object from the database using the given LineOfOperationId
        /// </summary>
        /// <param name="lineOfOperationId">The primary key value</param>
        /// <returns>A LineOfOperation object</returns>
        public static LineOfOperation Load(Guid lineOfOperationId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@LineOfOperationId", lineOfOperationId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spLineOfOperation_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    LineOfOperation result = new LineOfOperation();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a LineOfOperation object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A LineOfOperation object</returns>
        public static LineOfOperation LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spLineOfOperation_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    LineOfOperation result = new LineOfOperation();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of LineOfOperation objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the LineOfOperation objects in the database.</returns>
        public static LineOfOperationCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spLineOfOperation_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of LineOfOperation objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the LineOfOperation objects in the database ordered by the columns specified.</returns>
        public static LineOfOperationCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spLineOfOperation_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of LineOfOperation objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the LineOfOperation objects in the database.</returns>
        public static LineOfOperationCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spLineOfOperation_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of LineOfOperation objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the LineOfOperation objects in the database ordered by the columns specified.</returns>
        public static LineOfOperationCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spLineOfOperation_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of LineOfOperation objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the LineOfOperation objects in the database.</returns>
        public static LineOfOperationCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            LineOfOperationCollection result = new LineOfOperationCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    LineOfOperation tmp = new LineOfOperation();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a LineOfOperation object from the database.
        /// </summary>
        /// <param name="lineOfOperationId">The primary key value</param>
        public static void Delete(Guid lineOfOperationId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@LineOfOperationId", lineOfOperationId) };
            SqlHelper.Default.ExecuteNonQuery("spLineOfOperation_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) lineOfOperationId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) parentLine = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) lineOfOperationCode = reader.GetString(2);
                if (!reader.IsDBNull(3)) lineOfOperationName = reader.GetString(3);
                if (!reader.IsDBNull(4)) lineOfOperationName_Chs = reader.GetString(4);
                if (!reader.IsDBNull(5)) lineOfOperationName_Cht = reader.GetString(5);
                if (!reader.IsDBNull(6)) currencyCode = reader.GetString(6);
                if (!reader.IsDBNull(7)) primaryLine = reader.GetBoolean(7);
            }
        }
		
        public void Delete()
        {
            Delete(this.LineOfOperationId);
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
                if (key != LineOfOperationId)
                    this.Delete();
                Update();
            }
        }

        public Guid LineOfOperationId
        {
            get { return lineOfOperationId; }
            set { lineOfOperationId = value; }
        }

        public Guid ParentLine
        {
            get { return parentLine; }
            set { parentLine = value; }
        }

        public string LineOfOperationCode
        {
            get { return lineOfOperationCode; }
            set { lineOfOperationCode = value; }
        }

        public string LineOfOperationName
        {
            get { return lineOfOperationName; }
            set { lineOfOperationName = value; }
        }

        public string LineOfOperationName_Chs
        {
            get { return lineOfOperationName_Chs; }
            set { lineOfOperationName_Chs = value; }
        }

        public string LineOfOperationName_Cht
        {
            get { return lineOfOperationName_Cht; }
            set { lineOfOperationName_Cht = value; }
        }

        public string CurrencyCode
        {
            get { return currencyCode; }
            set { currencyCode = value; }
        }

        public bool PrimaryLine
        {
            get { return primaryLine; }
            set { primaryLine = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spLineOfOperation_InsRec", "@LineOfOperationId", out returnedValue, parameterValues);
            
            lineOfOperationId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spLineOfOperation_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[8];
            prams[0] = GetSqlParameter("@LineOfOperationId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.LineOfOperationId);
            prams[1] = GetSqlParameter("@ParentLine", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ParentLine);
            prams[2] = GetSqlParameter("@LineOfOperationCode", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.LineOfOperationCode);
            prams[3] = GetSqlParameter("@LineOfOperationName", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.LineOfOperationName);
            prams[4] = GetSqlParameter("@LineOfOperationName_Chs", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.LineOfOperationName_Chs);
            prams[5] = GetSqlParameter("@LineOfOperationName_Cht", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.LineOfOperationName_Cht);
            prams[6] = GetSqlParameter("@CurrencyCode", ParameterDirection.Input, SqlDbType.Char, 3, this.CurrencyCode);
            prams[7] = GetSqlParameter("@PrimaryLine", ParameterDirection.Input, SqlDbType.Bit, 1, this.PrimaryLine);
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
                GetSqlParameterWithoutDirection("@LineOfOperationId", SqlDbType.UniqueIdentifier, 16, this.LineOfOperationId),
                GetSqlParameterWithoutDirection("@ParentLine", SqlDbType.UniqueIdentifier, 16, this.ParentLine),
                GetSqlParameterWithoutDirection("@LineOfOperationCode", SqlDbType.NVarChar, 10, this.LineOfOperationCode),
                GetSqlParameterWithoutDirection("@LineOfOperationName", SqlDbType.NVarChar, 64, this.LineOfOperationName),
                GetSqlParameterWithoutDirection("@LineOfOperationName_Chs", SqlDbType.NVarChar, 64, this.LineOfOperationName_Chs),
                GetSqlParameterWithoutDirection("@LineOfOperationName_Cht", SqlDbType.NVarChar, 64, this.LineOfOperationName_Cht),
                GetSqlParameterWithoutDirection("@CurrencyCode", SqlDbType.Char, 3, this.CurrencyCode),
                GetSqlParameterWithoutDirection("@PrimaryLine", SqlDbType.Bit, 1, this.PrimaryLine)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("lineOfOperationId: " + lineOfOperationId.ToString()).Append("\r\n");
            builder.Append("parentLine: " + parentLine.ToString()).Append("\r\n");
            builder.Append("lineOfOperationCode: " + lineOfOperationCode.ToString()).Append("\r\n");
            builder.Append("lineOfOperationName: " + lineOfOperationName.ToString()).Append("\r\n");
            builder.Append("lineOfOperationName_Chs: " + lineOfOperationName_Chs.ToString()).Append("\r\n");
            builder.Append("lineOfOperationName_Cht: " + lineOfOperationName_Cht.ToString()).Append("\r\n");
            builder.Append("currencyCode: " + currencyCode.ToString()).Append("\r\n");
            builder.Append("primaryLine: " + primaryLine.ToString()).Append("\r\n");
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
			
			LineOfOperationCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = LineOfOperation.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = LineOfOperation.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (LineOfOperation item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.ParentLine != Guid.Empty)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.LineOfOperationId));
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
		
		
		private static bool IgnorThis(LineOfOperation target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.ParentLine == Guid.Empty)
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
				LineOfOperation parentTemplate = LineOfOperation.Load(target.ParentLine);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(LineOfOperation target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="LineOfOperation">LineOfOperation</see> objects.
    /// </summary>
    public class LineOfOperationCollection : BindingList< LineOfOperation>
    {
	}
}
