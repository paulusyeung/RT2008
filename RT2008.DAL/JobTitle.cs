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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.JobTitle.
    /// Date Created:   2020-08-09 02:14:11
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class JobTitle
    {
        private Guid key = Guid.Empty;
        private Guid jobTitleId = Guid.Empty;
        private Guid parentJobTitle = Guid.Empty;
        private string jobTitleCode = String.Empty;
        private string jobTitleName = String.Empty;
        private string jobTitleName_Chs = String.Empty;
        private string jobTitleName_Cht = String.Empty;

        /// <summary>
        /// Initialize an new empty JobTitle object.
        /// </summary>
        public JobTitle()
        {
        }
		
        /// <summary>
        /// Initialize a new JobTitle object with the given parameters.
        /// </summary>
        public JobTitle(Guid jobTitleId, Guid parentJobTitle, string jobTitleCode, string jobTitleName, string jobTitleName_Chs, string jobTitleName_Cht)
        {
                this.jobTitleId = jobTitleId;
                this.parentJobTitle = parentJobTitle;
                this.jobTitleCode = jobTitleCode;
                this.jobTitleName = jobTitleName;
                this.jobTitleName_Chs = jobTitleName_Chs;
                this.jobTitleName_Cht = jobTitleName_Cht;
        }	
		
        /// <summary>
        /// Loads a JobTitle object from the database using the given JobTitleId
        /// </summary>
        /// <param name="jobTitleId">The primary key value</param>
        /// <returns>A JobTitle object</returns>
        public static JobTitle Load(Guid jobTitleId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@JobTitleId", jobTitleId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spJobTitle_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    JobTitle result = new JobTitle();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a JobTitle object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A JobTitle object</returns>
        public static JobTitle LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spJobTitle_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    JobTitle result = new JobTitle();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of JobTitle objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the JobTitle objects in the database.</returns>
        public static JobTitleCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spJobTitle_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of JobTitle objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the JobTitle objects in the database ordered by the columns specified.</returns>
        public static JobTitleCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spJobTitle_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of JobTitle objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the JobTitle objects in the database.</returns>
        public static JobTitleCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spJobTitle_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of JobTitle objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the JobTitle objects in the database ordered by the columns specified.</returns>
        public static JobTitleCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spJobTitle_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of JobTitle objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the JobTitle objects in the database.</returns>
        public static JobTitleCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            JobTitleCollection result = new JobTitleCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    JobTitle tmp = new JobTitle();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a JobTitle object from the database.
        /// </summary>
        /// <param name="jobTitleId">The primary key value</param>
        public static void Delete(Guid jobTitleId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@JobTitleId", jobTitleId) };
            SqlHelper.Default.ExecuteNonQuery("spJobTitle_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) jobTitleId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) parentJobTitle = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) jobTitleCode = reader.GetString(2);
                if (!reader.IsDBNull(3)) jobTitleName = reader.GetString(3);
                if (!reader.IsDBNull(4)) jobTitleName_Chs = reader.GetString(4);
                if (!reader.IsDBNull(5)) jobTitleName_Cht = reader.GetString(5);
            }
        }
		
        public void Delete()
        {
            Delete(this.JobTitleId);
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
                if (key != JobTitleId)
                    this.Delete();
                Update();
            }
        }

        public Guid JobTitleId
        {
            get { return jobTitleId; }
            set { jobTitleId = value; }
        }

        public Guid ParentJobTitle
        {
            get { return parentJobTitle; }
            set { parentJobTitle = value; }
        }

        public string JobTitleCode
        {
            get { return jobTitleCode; }
            set { jobTitleCode = value; }
        }

        public string JobTitleName
        {
            get { return jobTitleName; }
            set { jobTitleName = value; }
        }

        public string JobTitleName_Chs
        {
            get { return jobTitleName_Chs; }
            set { jobTitleName_Chs = value; }
        }

        public string JobTitleName_Cht
        {
            get { return jobTitleName_Cht; }
            set { jobTitleName_Cht = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spJobTitle_InsRec", "@JobTitleId", out returnedValue, parameterValues);
            
            jobTitleId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spJobTitle_UpdRec", parameterValues);
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
            prams[0] = GetSqlParameter("@JobTitleId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.JobTitleId);
            prams[1] = GetSqlParameter("@ParentJobTitle", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ParentJobTitle);
            prams[2] = GetSqlParameter("@JobTitleCode", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.JobTitleCode);
            prams[3] = GetSqlParameter("@JobTitleName", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.JobTitleName);
            prams[4] = GetSqlParameter("@JobTitleName_Chs", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.JobTitleName_Chs);
            prams[5] = GetSqlParameter("@JobTitleName_Cht", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.JobTitleName_Cht);
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
                GetSqlParameterWithoutDirection("@JobTitleId", SqlDbType.UniqueIdentifier, 16, this.JobTitleId),
                GetSqlParameterWithoutDirection("@ParentJobTitle", SqlDbType.UniqueIdentifier, 16, this.ParentJobTitle),
                GetSqlParameterWithoutDirection("@JobTitleCode", SqlDbType.NVarChar, 10, this.JobTitleCode),
                GetSqlParameterWithoutDirection("@JobTitleName", SqlDbType.NVarChar, 64, this.JobTitleName),
                GetSqlParameterWithoutDirection("@JobTitleName_Chs", SqlDbType.NVarChar, 64, this.JobTitleName_Chs),
                GetSqlParameterWithoutDirection("@JobTitleName_Cht", SqlDbType.NVarChar, 64, this.JobTitleName_Cht)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("jobTitleId: " + jobTitleId.ToString()).Append("\r\n");
            builder.Append("parentJobTitle: " + parentJobTitle.ToString()).Append("\r\n");
            builder.Append("jobTitleCode: " + jobTitleCode.ToString()).Append("\r\n");
            builder.Append("jobTitleName: " + jobTitleName.ToString()).Append("\r\n");
            builder.Append("jobTitleName_Chs: " + jobTitleName_Chs.ToString()).Append("\r\n");
            builder.Append("jobTitleName_Cht: " + jobTitleName_Cht.ToString()).Append("\r\n");
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
			
			JobTitleCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = JobTitle.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = JobTitle.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (JobTitle item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.ParentJobTitle != Guid.Empty)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.JobTitleId));
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
		
		
		private static bool IgnorThis(JobTitle target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.ParentJobTitle == Guid.Empty)
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
				JobTitle parentTemplate = JobTitle.Load(target.ParentJobTitle);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(JobTitle target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="JobTitle">JobTitle</see> objects.
    /// </summary>
    public class JobTitleCollection : BindingList< JobTitle>
    {
	}
}
