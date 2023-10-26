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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.PosAnalysisCode.
    /// Date Created:   2020-08-09 02:14:12
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class PosAnalysisCode
    {
        private Guid key = Guid.Empty;
        private Guid analysisCodeId = Guid.Empty;
        private Guid parentCode = Guid.Empty;
        private string analysisType = String.Empty;
        private string analysisCode = String.Empty;
        private string codeInitial = String.Empty;
        private string codeName = String.Empty;
        private string codeName_Chs = String.Empty;
        private string codeName_Cht = String.Empty;
        private bool mandatory;
        private bool downloadToPOS;
        private DateTime createdOn = DateTime.Parse("1900-1-1");
        private Guid createdBy = Guid.Empty;
        private DateTime modifiedOn = DateTime.Parse("1900-1-1");
        private Guid modifiedBy = Guid.Empty;
        private bool retired;
        private DateTime retiredOn = DateTime.Parse("1900-1-1");
        private Guid retiredBy = Guid.Empty;

        /// <summary>
        /// Initialize an new empty PosAnalysisCode object.
        /// </summary>
        public PosAnalysisCode()
        {
        }
		
        /// <summary>
        /// Initialize a new PosAnalysisCode object with the given parameters.
        /// </summary>
        public PosAnalysisCode(Guid analysisCodeId, Guid parentCode, string analysisType, string analysisCode, string codeInitial, string codeName, string codeName_Chs, string codeName_Cht, bool mandatory, bool downloadToPOS, DateTime createdOn, Guid createdBy, DateTime modifiedOn, Guid modifiedBy, bool retired, DateTime retiredOn, Guid retiredBy)
        {
                this.analysisCodeId = analysisCodeId;
                this.parentCode = parentCode;
                this.analysisType = analysisType;
                this.analysisCode = analysisCode;
                this.codeInitial = codeInitial;
                this.codeName = codeName;
                this.codeName_Chs = codeName_Chs;
                this.codeName_Cht = codeName_Cht;
                this.mandatory = mandatory;
                this.downloadToPOS = downloadToPOS;
                this.createdOn = createdOn;
                this.createdBy = createdBy;
                this.modifiedOn = modifiedOn;
                this.modifiedBy = modifiedBy;
                this.retired = retired;
                this.retiredOn = retiredOn;
                this.retiredBy = retiredBy;
        }	
		
        /// <summary>
        /// Loads a PosAnalysisCode object from the database using the given AnalysisCodeId
        /// </summary>
        /// <param name="analysisCodeId">The primary key value</param>
        /// <returns>A PosAnalysisCode object</returns>
        public static PosAnalysisCode Load(Guid analysisCodeId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@AnalysisCodeId", analysisCodeId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spPosAnalysisCode_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    PosAnalysisCode result = new PosAnalysisCode();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a PosAnalysisCode object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A PosAnalysisCode object</returns>
        public static PosAnalysisCode LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spPosAnalysisCode_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    PosAnalysisCode result = new PosAnalysisCode();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of PosAnalysisCode objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the PosAnalysisCode objects in the database.</returns>
        public static PosAnalysisCodeCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spPosAnalysisCode_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PosAnalysisCode objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the PosAnalysisCode objects in the database ordered by the columns specified.</returns>
        public static PosAnalysisCodeCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spPosAnalysisCode_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PosAnalysisCode objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the PosAnalysisCode objects in the database.</returns>
        public static PosAnalysisCodeCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spPosAnalysisCode_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PosAnalysisCode objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the PosAnalysisCode objects in the database ordered by the columns specified.</returns>
        public static PosAnalysisCodeCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spPosAnalysisCode_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PosAnalysisCode objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the PosAnalysisCode objects in the database.</returns>
        public static PosAnalysisCodeCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            PosAnalysisCodeCollection result = new PosAnalysisCodeCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    PosAnalysisCode tmp = new PosAnalysisCode();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a PosAnalysisCode object from the database.
        /// </summary>
        /// <param name="analysisCodeId">The primary key value</param>
        public static void Delete(Guid analysisCodeId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@AnalysisCodeId", analysisCodeId) };
            SqlHelper.Default.ExecuteNonQuery("spPosAnalysisCode_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) analysisCodeId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) parentCode = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) analysisType = reader.GetString(2);
                if (!reader.IsDBNull(3)) analysisCode = reader.GetString(3);
                if (!reader.IsDBNull(4)) codeInitial = reader.GetString(4);
                if (!reader.IsDBNull(5)) codeName = reader.GetString(5);
                if (!reader.IsDBNull(6)) codeName_Chs = reader.GetString(6);
                if (!reader.IsDBNull(7)) codeName_Cht = reader.GetString(7);
                if (!reader.IsDBNull(8)) mandatory = reader.GetBoolean(8);
                if (!reader.IsDBNull(9)) downloadToPOS = reader.GetBoolean(9);
                if (!reader.IsDBNull(10)) createdOn = reader.GetDateTime(10);
                if (!reader.IsDBNull(11)) createdBy = reader.GetGuid(11);
                if (!reader.IsDBNull(12)) modifiedOn = reader.GetDateTime(12);
                if (!reader.IsDBNull(13)) modifiedBy = reader.GetGuid(13);
                if (!reader.IsDBNull(14)) retired = reader.GetBoolean(14);
                if (!reader.IsDBNull(15)) retiredOn = reader.GetDateTime(15);
                if (!reader.IsDBNull(16)) retiredBy = reader.GetGuid(16);
            }
        }
		
        public void Delete()
        {
            Delete(this.AnalysisCodeId);
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
                if (key != AnalysisCodeId)
                    this.Delete();
                Update();
            }
        }

        public Guid AnalysisCodeId
        {
            get { return analysisCodeId; }
            set { analysisCodeId = value; }
        }

        public Guid ParentCode
        {
            get { return parentCode; }
            set { parentCode = value; }
        }

        public string AnalysisType
        {
            get { return analysisType; }
            set { analysisType = value; }
        }

        public string AnalysisCode
        {
            get { return analysisCode; }
            set { analysisCode = value; }
        }

        public string CodeInitial
        {
            get { return codeInitial; }
            set { codeInitial = value; }
        }

        public string CodeName
        {
            get { return codeName; }
            set { codeName = value; }
        }

        public string CodeName_Chs
        {
            get { return codeName_Chs; }
            set { codeName_Chs = value; }
        }

        public string CodeName_Cht
        {
            get { return codeName_Cht; }
            set { codeName_Cht = value; }
        }

        public bool Mandatory
        {
            get { return mandatory; }
            set { mandatory = value; }
        }

        public bool DownloadToPOS
        {
            get { return downloadToPOS; }
            set { downloadToPOS = value; }
        }

        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }

        public Guid CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        public DateTime ModifiedOn
        {
            get { return modifiedOn; }
            set { modifiedOn = value; }
        }

        public Guid ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }

        public bool Retired
        {
            get { return retired; }
            set { retired = value; }
        }

        public DateTime RetiredOn
        {
            get { return retiredOn; }
            set { retiredOn = value; }
        }

        public Guid RetiredBy
        {
            get { return retiredBy; }
            set { retiredBy = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spPosAnalysisCode_InsRec", "@AnalysisCodeId", out returnedValue, parameterValues);
            
            analysisCodeId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spPosAnalysisCode_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[17];
            prams[0] = GetSqlParameter("@AnalysisCodeId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.AnalysisCodeId);
            prams[1] = GetSqlParameter("@ParentCode", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ParentCode);
            prams[2] = GetSqlParameter("@AnalysisType", ParameterDirection.Input, SqlDbType.VarChar, 2, this.AnalysisType);
            prams[3] = GetSqlParameter("@AnalysisCode", ParameterDirection.Input, SqlDbType.VarChar, 2, this.AnalysisCode);
            prams[4] = GetSqlParameter("@CodeInitial", ParameterDirection.Input, SqlDbType.NVarChar, 20, this.CodeInitial);
            prams[5] = GetSqlParameter("@CodeName", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.CodeName);
            prams[6] = GetSqlParameter("@CodeName_Chs", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.CodeName_Chs);
            prams[7] = GetSqlParameter("@CodeName_Cht", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.CodeName_Cht);
            prams[8] = GetSqlParameter("@Mandatory", ParameterDirection.Input, SqlDbType.Bit, 1, this.Mandatory);
            prams[9] = GetSqlParameter("@DownloadToPOS", ParameterDirection.Input, SqlDbType.Bit, 1, this.DownloadToPOS);
            prams[10] = GetSqlParameter("@CreatedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.CreatedOn);
            prams[11] = GetSqlParameter("@CreatedBy", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.CreatedBy);
            prams[12] = GetSqlParameter("@ModifiedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.ModifiedOn);
            prams[13] = GetSqlParameter("@ModifiedBy", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ModifiedBy);
            prams[14] = GetSqlParameter("@Retired", ParameterDirection.Input, SqlDbType.Bit, 1, this.Retired);
            prams[15] = GetSqlParameter("@RetiredOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.RetiredOn);
            prams[16] = GetSqlParameter("@RetiredBy", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.RetiredBy);
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
                GetSqlParameterWithoutDirection("@AnalysisCodeId", SqlDbType.UniqueIdentifier, 16, this.AnalysisCodeId),
                GetSqlParameterWithoutDirection("@ParentCode", SqlDbType.UniqueIdentifier, 16, this.ParentCode),
                GetSqlParameterWithoutDirection("@AnalysisType", SqlDbType.VarChar, 2, this.AnalysisType),
                GetSqlParameterWithoutDirection("@AnalysisCode", SqlDbType.VarChar, 2, this.AnalysisCode),
                GetSqlParameterWithoutDirection("@CodeInitial", SqlDbType.NVarChar, 20, this.CodeInitial),
                GetSqlParameterWithoutDirection("@CodeName", SqlDbType.NVarChar, 64, this.CodeName),
                GetSqlParameterWithoutDirection("@CodeName_Chs", SqlDbType.NVarChar, 64, this.CodeName_Chs),
                GetSqlParameterWithoutDirection("@CodeName_Cht", SqlDbType.NVarChar, 64, this.CodeName_Cht),
                GetSqlParameterWithoutDirection("@Mandatory", SqlDbType.Bit, 1, this.Mandatory),
                GetSqlParameterWithoutDirection("@DownloadToPOS", SqlDbType.Bit, 1, this.DownloadToPOS),
                GetSqlParameterWithoutDirection("@CreatedOn", SqlDbType.DateTime, 8, this.CreatedOn),
                GetSqlParameterWithoutDirection("@CreatedBy", SqlDbType.UniqueIdentifier, 16, this.CreatedBy),
                GetSqlParameterWithoutDirection("@ModifiedOn", SqlDbType.DateTime, 8, this.ModifiedOn),
                GetSqlParameterWithoutDirection("@ModifiedBy", SqlDbType.UniqueIdentifier, 16, this.ModifiedBy),
                GetSqlParameterWithoutDirection("@Retired", SqlDbType.Bit, 1, this.Retired),
                GetSqlParameterWithoutDirection("@RetiredOn", SqlDbType.DateTime, 8, this.RetiredOn),
                GetSqlParameterWithoutDirection("@RetiredBy", SqlDbType.UniqueIdentifier, 16, this.RetiredBy)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("analysisCodeId: " + analysisCodeId.ToString()).Append("\r\n");
            builder.Append("parentCode: " + parentCode.ToString()).Append("\r\n");
            builder.Append("analysisType: " + analysisType.ToString()).Append("\r\n");
            builder.Append("analysisCode: " + analysisCode.ToString()).Append("\r\n");
            builder.Append("codeInitial: " + codeInitial.ToString()).Append("\r\n");
            builder.Append("codeName: " + codeName.ToString()).Append("\r\n");
            builder.Append("codeName_Chs: " + codeName_Chs.ToString()).Append("\r\n");
            builder.Append("codeName_Cht: " + codeName_Cht.ToString()).Append("\r\n");
            builder.Append("mandatory: " + mandatory.ToString()).Append("\r\n");
            builder.Append("downloadToPOS: " + downloadToPOS.ToString()).Append("\r\n");
            builder.Append("createdOn: " + createdOn.ToString()).Append("\r\n");
            builder.Append("createdBy: " + createdBy.ToString()).Append("\r\n");
            builder.Append("modifiedOn: " + modifiedOn.ToString()).Append("\r\n");
            builder.Append("modifiedBy: " + modifiedBy.ToString()).Append("\r\n");
            builder.Append("retired: " + retired.ToString()).Append("\r\n");
            builder.Append("retiredOn: " + retiredOn.ToString()).Append("\r\n");
            builder.Append("retiredBy: " + retiredBy.ToString()).Append("\r\n");
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
			
			PosAnalysisCodeCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			// Filter the retired records
			if (WhereClause.Length > 0)
			{
				WhereClause += " AND Retired = 0";
			}
			else
			{
				WhereClause = "Retired = 0";
			}
			
			if (WhereClause.Length > 0)
			{
				source = PosAnalysisCode.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = PosAnalysisCode.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (PosAnalysisCode item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.ParentCode != Guid.Empty)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.AnalysisCodeId));
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
		
		
		private static bool IgnorThis(PosAnalysisCode target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.ParentCode == Guid.Empty)
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
				PosAnalysisCode parentTemplate = PosAnalysisCode.Load(target.ParentCode);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(PosAnalysisCode target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="PosAnalysisCode">PosAnalysisCode</see> objects.
    /// </summary>
    public class PosAnalysisCodeCollection : BindingList< PosAnalysisCode>
    {
	}
}
