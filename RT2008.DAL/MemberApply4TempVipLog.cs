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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.MemberApply4TempVipLog.
    /// Date Created:   2020-08-09 02:14:12
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class MemberApply4TempVipLog
    {
        private Guid key = Guid.Empty;
        private Guid logId = Guid.Empty;
        private Guid tempVipId = Guid.Empty;
        private Guid memberId = Guid.Empty;
        private string memberNumber = String.Empty;

        /// <summary>
        /// Initialize an new empty MemberApply4TempVipLog object.
        /// </summary>
        public MemberApply4TempVipLog()
        {
        }
		
        /// <summary>
        /// Initialize a new MemberApply4TempVipLog object with the given parameters.
        /// </summary>
        public MemberApply4TempVipLog(Guid logId, Guid tempVipId, Guid memberId, string memberNumber)
        {
                this.logId = logId;
                this.tempVipId = tempVipId;
                this.memberId = memberId;
                this.memberNumber = memberNumber;
        }	
		
        /// <summary>
        /// Loads a MemberApply4TempVipLog object from the database using the given LogId
        /// </summary>
        /// <param name="logId">The primary key value</param>
        /// <returns>A MemberApply4TempVipLog object</returns>
        public static MemberApply4TempVipLog Load(Guid logId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@LogId", logId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spMemberApply4TempVipLog_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    MemberApply4TempVipLog result = new MemberApply4TempVipLog();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a MemberApply4TempVipLog object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A MemberApply4TempVipLog object</returns>
        public static MemberApply4TempVipLog LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spMemberApply4TempVipLog_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    MemberApply4TempVipLog result = new MemberApply4TempVipLog();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of MemberApply4TempVipLog objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the MemberApply4TempVipLog objects in the database.</returns>
        public static MemberApply4TempVipLogCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spMemberApply4TempVipLog_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of MemberApply4TempVipLog objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the MemberApply4TempVipLog objects in the database ordered by the columns specified.</returns>
        public static MemberApply4TempVipLogCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spMemberApply4TempVipLog_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of MemberApply4TempVipLog objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the MemberApply4TempVipLog objects in the database.</returns>
        public static MemberApply4TempVipLogCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spMemberApply4TempVipLog_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of MemberApply4TempVipLog objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the MemberApply4TempVipLog objects in the database ordered by the columns specified.</returns>
        public static MemberApply4TempVipLogCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spMemberApply4TempVipLog_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of MemberApply4TempVipLog objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the MemberApply4TempVipLog objects in the database.</returns>
        public static MemberApply4TempVipLogCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            MemberApply4TempVipLogCollection result = new MemberApply4TempVipLogCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    MemberApply4TempVipLog tmp = new MemberApply4TempVipLog();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a MemberApply4TempVipLog object from the database.
        /// </summary>
        /// <param name="logId">The primary key value</param>
        public static void Delete(Guid logId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@LogId", logId) };
            SqlHelper.Default.ExecuteNonQuery("spMemberApply4TempVipLog_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) logId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) tempVipId = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) memberId = reader.GetGuid(2);
                if (!reader.IsDBNull(3)) memberNumber = reader.GetString(3);
            }
        }
		
        public void Delete()
        {
            Delete(this.LogId);
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
                if (key != LogId)
                    this.Delete();
                Update();
            }
        }

        public Guid LogId
        {
            get { return logId; }
            set { logId = value; }
        }

        public Guid TempVipId
        {
            get { return tempVipId; }
            set { tempVipId = value; }
        }

        public Guid MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }

        public string MemberNumber
        {
            get { return memberNumber; }
            set { memberNumber = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spMemberApply4TempVipLog_InsRec", "@LogId", out returnedValue, parameterValues);
            
            logId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spMemberApply4TempVipLog_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[4];
            prams[0] = GetSqlParameter("@LogId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.LogId);
            prams[1] = GetSqlParameter("@TempVipId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.TempVipId);
            prams[2] = GetSqlParameter("@MemberId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.MemberId);
            prams[3] = GetSqlParameter("@MemberNumber", ParameterDirection.Input, SqlDbType.VarChar, 13, this.MemberNumber);
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
                GetSqlParameterWithoutDirection("@LogId", SqlDbType.UniqueIdentifier, 16, this.LogId),
                GetSqlParameterWithoutDirection("@TempVipId", SqlDbType.UniqueIdentifier, 16, this.TempVipId),
                GetSqlParameterWithoutDirection("@MemberId", SqlDbType.UniqueIdentifier, 16, this.MemberId),
                GetSqlParameterWithoutDirection("@MemberNumber", SqlDbType.VarChar, 13, this.MemberNumber)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("logId: " + logId.ToString()).Append("\r\n");
            builder.Append("tempVipId: " + tempVipId.ToString()).Append("\r\n");
            builder.Append("memberId: " + memberId.ToString()).Append("\r\n");
            builder.Append("memberNumber: " + memberNumber.ToString()).Append("\r\n");
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
			
			MemberApply4TempVipLogCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = MemberApply4TempVipLog.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = MemberApply4TempVipLog.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (MemberApply4TempVipLog item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.TempVipId != Guid.Empty)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.LogId));
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
		
		
		private static bool IgnorThis(MemberApply4TempVipLog target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.TempVipId == Guid.Empty)
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
				MemberApply4TempVipLog parentTemplate = MemberApply4TempVipLog.Load(target.TempVipId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(MemberApply4TempVipLog target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="MemberApply4TempVipLog">MemberApply4TempVipLog</see> objects.
    /// </summary>
    public class MemberApply4TempVipLogCollection : BindingList< MemberApply4TempVipLog>
    {
	}
}