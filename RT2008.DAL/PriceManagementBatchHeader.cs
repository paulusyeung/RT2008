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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.PriceManagementBatchHeader.
    /// Date Created:   2020-08-09 02:14:13
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class PriceManagementBatchHeader
    {
        private Guid key = Guid.Empty;
        private Guid headerId = Guid.Empty;
        private string txNumber = String.Empty;
        private string txType = String.Empty;
        private DateTime effectDate = DateTime.Parse("1900-1-1");
        private string pM_TYPE = String.Empty;
        private Guid reasonId = Guid.Empty;
        private DateTime startOn = DateTime.Parse("1900-1-1");
        private DateTime endOn = DateTime.Parse("1900-1-1");
        private string remarks = String.Empty;
        private bool sEGMENT_LOCATION;
        private bool posted;
        private DateTime postedOn = DateTime.Parse("1900-1-1");
        private Guid postedBy = Guid.Empty;
        private DateTime createdOn = DateTime.Parse("1900-1-1");
        private Guid createdBy = Guid.Empty;
        private DateTime modifiedOn = DateTime.Parse("1900-1-1");
        private Guid modifiedBy = Guid.Empty;

        /// <summary>
        /// Initialize an new empty PriceManagementBatchHeader object.
        /// </summary>
        public PriceManagementBatchHeader()
        {
        }
		
        /// <summary>
        /// Initialize a new PriceManagementBatchHeader object with the given parameters.
        /// </summary>
        public PriceManagementBatchHeader(Guid headerId, string txNumber, string txType, DateTime effectDate, string pM_TYPE, Guid reasonId, DateTime startOn, DateTime endOn, string remarks, bool sEGMENT_LOCATION, bool posted, DateTime postedOn, Guid postedBy, DateTime createdOn, Guid createdBy, DateTime modifiedOn, Guid modifiedBy)
        {
                this.headerId = headerId;
                this.txNumber = txNumber;
                this.txType = txType;
                this.effectDate = effectDate;
                this.pM_TYPE = pM_TYPE;
                this.reasonId = reasonId;
                this.startOn = startOn;
                this.endOn = endOn;
                this.remarks = remarks;
                this.sEGMENT_LOCATION = sEGMENT_LOCATION;
                this.posted = posted;
                this.postedOn = postedOn;
                this.postedBy = postedBy;
                this.createdOn = createdOn;
                this.createdBy = createdBy;
                this.modifiedOn = modifiedOn;
                this.modifiedBy = modifiedBy;
        }	
		
        /// <summary>
        /// Loads a PriceManagementBatchHeader object from the database using the given HeaderId
        /// </summary>
        /// <param name="headerId">The primary key value</param>
        /// <returns>A PriceManagementBatchHeader object</returns>
        public static PriceManagementBatchHeader Load(Guid headerId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@HeaderId", headerId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spPriceManagementBatchHeader_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    PriceManagementBatchHeader result = new PriceManagementBatchHeader();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a PriceManagementBatchHeader object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A PriceManagementBatchHeader object</returns>
        public static PriceManagementBatchHeader LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spPriceManagementBatchHeader_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    PriceManagementBatchHeader result = new PriceManagementBatchHeader();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of PriceManagementBatchHeader objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the PriceManagementBatchHeader objects in the database.</returns>
        public static PriceManagementBatchHeaderCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spPriceManagementBatchHeader_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PriceManagementBatchHeader objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the PriceManagementBatchHeader objects in the database ordered by the columns specified.</returns>
        public static PriceManagementBatchHeaderCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spPriceManagementBatchHeader_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PriceManagementBatchHeader objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the PriceManagementBatchHeader objects in the database.</returns>
        public static PriceManagementBatchHeaderCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spPriceManagementBatchHeader_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PriceManagementBatchHeader objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the PriceManagementBatchHeader objects in the database ordered by the columns specified.</returns>
        public static PriceManagementBatchHeaderCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spPriceManagementBatchHeader_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of PriceManagementBatchHeader objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the PriceManagementBatchHeader objects in the database.</returns>
        public static PriceManagementBatchHeaderCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            PriceManagementBatchHeaderCollection result = new PriceManagementBatchHeaderCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    PriceManagementBatchHeader tmp = new PriceManagementBatchHeader();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a PriceManagementBatchHeader object from the database.
        /// </summary>
        /// <param name="headerId">The primary key value</param>
        public static void Delete(Guid headerId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@HeaderId", headerId) };
            SqlHelper.Default.ExecuteNonQuery("spPriceManagementBatchHeader_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) headerId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) txNumber = reader.GetString(1);
                if (!reader.IsDBNull(2)) txType = reader.GetString(2);
                if (!reader.IsDBNull(3)) effectDate = reader.GetDateTime(3);
                if (!reader.IsDBNull(4)) pM_TYPE = reader.GetString(4);
                if (!reader.IsDBNull(5)) reasonId = reader.GetGuid(5);
                if (!reader.IsDBNull(6)) startOn = reader.GetDateTime(6);
                if (!reader.IsDBNull(7)) endOn = reader.GetDateTime(7);
                if (!reader.IsDBNull(8)) remarks = reader.GetString(8);
                if (!reader.IsDBNull(9)) sEGMENT_LOCATION = reader.GetBoolean(9);
                if (!reader.IsDBNull(10)) posted = reader.GetBoolean(10);
                if (!reader.IsDBNull(11)) postedOn = reader.GetDateTime(11);
                if (!reader.IsDBNull(12)) postedBy = reader.GetGuid(12);
                if (!reader.IsDBNull(13)) createdOn = reader.GetDateTime(13);
                if (!reader.IsDBNull(14)) createdBy = reader.GetGuid(14);
                if (!reader.IsDBNull(15)) modifiedOn = reader.GetDateTime(15);
                if (!reader.IsDBNull(16)) modifiedBy = reader.GetGuid(16);
            }
        }
		
        public void Delete()
        {
            Delete(this.HeaderId);
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
                if (key != HeaderId)
                    this.Delete();
                Update();
            }
        }

        public Guid HeaderId
        {
            get { return headerId; }
            set { headerId = value; }
        }

        public string TxNumber
        {
            get { return txNumber; }
            set { txNumber = value; }
        }

        public string TxType
        {
            get { return txType; }
            set { txType = value; }
        }

        public DateTime EffectDate
        {
            get { return effectDate; }
            set { effectDate = value; }
        }

        public string PM_TYPE
        {
            get { return pM_TYPE; }
            set { pM_TYPE = value; }
        }

        public Guid ReasonId
        {
            get { return reasonId; }
            set { reasonId = value; }
        }

        public DateTime StartOn
        {
            get { return startOn; }
            set { startOn = value; }
        }

        public DateTime EndOn
        {
            get { return endOn; }
            set { endOn = value; }
        }

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        public bool SEGMENT_LOCATION
        {
            get { return sEGMENT_LOCATION; }
            set { sEGMENT_LOCATION = value; }
        }

        public bool Posted
        {
            get { return posted; }
            set { posted = value; }
        }

        public DateTime PostedOn
        {
            get { return postedOn; }
            set { postedOn = value; }
        }

        public Guid PostedBy
        {
            get { return postedBy; }
            set { postedBy = value; }
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


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spPriceManagementBatchHeader_InsRec", "@HeaderId", out returnedValue, parameterValues);
            
            headerId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spPriceManagementBatchHeader_UpdRec", parameterValues);
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
            prams[0] = GetSqlParameter("@HeaderId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.HeaderId);
            prams[1] = GetSqlParameter("@TxNumber", ParameterDirection.Input, SqlDbType.NVarChar, 12, this.TxNumber);
            prams[2] = GetSqlParameter("@TxType", ParameterDirection.Input, SqlDbType.VarChar, 3, this.TxType);
            prams[3] = GetSqlParameter("@EffectDate", ParameterDirection.Input, SqlDbType.DateTime, 8, this.EffectDate);
            prams[4] = GetSqlParameter("@PM_TYPE", ParameterDirection.Input, SqlDbType.VarChar, 1, this.PM_TYPE);
            prams[5] = GetSqlParameter("@ReasonId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ReasonId);
            prams[6] = GetSqlParameter("@StartOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.StartOn);
            prams[7] = GetSqlParameter("@EndOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.EndOn);
            prams[8] = GetSqlParameter("@Remarks", ParameterDirection.Input, SqlDbType.NVarChar, 30, this.Remarks);
            prams[9] = GetSqlParameter("@SEGMENT_LOCATION", ParameterDirection.Input, SqlDbType.Bit, 1, this.SEGMENT_LOCATION);
            prams[10] = GetSqlParameter("@Posted", ParameterDirection.Input, SqlDbType.Bit, 1, this.Posted);
            prams[11] = GetSqlParameter("@PostedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.PostedOn);
            prams[12] = GetSqlParameter("@PostedBy", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.PostedBy);
            prams[13] = GetSqlParameter("@CreatedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.CreatedOn);
            prams[14] = GetSqlParameter("@CreatedBy", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.CreatedBy);
            prams[15] = GetSqlParameter("@ModifiedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.ModifiedOn);
            prams[16] = GetSqlParameter("@ModifiedBy", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ModifiedBy);
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
                GetSqlParameterWithoutDirection("@HeaderId", SqlDbType.UniqueIdentifier, 16, this.HeaderId),
                GetSqlParameterWithoutDirection("@TxNumber", SqlDbType.NVarChar, 12, this.TxNumber),
                GetSqlParameterWithoutDirection("@TxType", SqlDbType.VarChar, 3, this.TxType),
                GetSqlParameterWithoutDirection("@EffectDate", SqlDbType.DateTime, 8, this.EffectDate),
                GetSqlParameterWithoutDirection("@PM_TYPE", SqlDbType.VarChar, 1, this.PM_TYPE),
                GetSqlParameterWithoutDirection("@ReasonId", SqlDbType.UniqueIdentifier, 16, this.ReasonId),
                GetSqlParameterWithoutDirection("@StartOn", SqlDbType.DateTime, 8, this.StartOn),
                GetSqlParameterWithoutDirection("@EndOn", SqlDbType.DateTime, 8, this.EndOn),
                GetSqlParameterWithoutDirection("@Remarks", SqlDbType.NVarChar, 30, this.Remarks),
                GetSqlParameterWithoutDirection("@SEGMENT_LOCATION", SqlDbType.Bit, 1, this.SEGMENT_LOCATION),
                GetSqlParameterWithoutDirection("@Posted", SqlDbType.Bit, 1, this.Posted),
                GetSqlParameterWithoutDirection("@PostedOn", SqlDbType.DateTime, 8, this.PostedOn),
                GetSqlParameterWithoutDirection("@PostedBy", SqlDbType.UniqueIdentifier, 16, this.PostedBy),
                GetSqlParameterWithoutDirection("@CreatedOn", SqlDbType.DateTime, 8, this.CreatedOn),
                GetSqlParameterWithoutDirection("@CreatedBy", SqlDbType.UniqueIdentifier, 16, this.CreatedBy),
                GetSqlParameterWithoutDirection("@ModifiedOn", SqlDbType.DateTime, 8, this.ModifiedOn),
                GetSqlParameterWithoutDirection("@ModifiedBy", SqlDbType.UniqueIdentifier, 16, this.ModifiedBy)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("headerId: " + headerId.ToString()).Append("\r\n");
            builder.Append("txNumber: " + txNumber.ToString()).Append("\r\n");
            builder.Append("txType: " + txType.ToString()).Append("\r\n");
            builder.Append("effectDate: " + effectDate.ToString()).Append("\r\n");
            builder.Append("pM_TYPE: " + pM_TYPE.ToString()).Append("\r\n");
            builder.Append("reasonId: " + reasonId.ToString()).Append("\r\n");
            builder.Append("startOn: " + startOn.ToString()).Append("\r\n");
            builder.Append("endOn: " + endOn.ToString()).Append("\r\n");
            builder.Append("remarks: " + remarks.ToString()).Append("\r\n");
            builder.Append("sEGMENT_LOCATION: " + sEGMENT_LOCATION.ToString()).Append("\r\n");
            builder.Append("posted: " + posted.ToString()).Append("\r\n");
            builder.Append("postedOn: " + postedOn.ToString()).Append("\r\n");
            builder.Append("postedBy: " + postedBy.ToString()).Append("\r\n");
            builder.Append("createdOn: " + createdOn.ToString()).Append("\r\n");
            builder.Append("createdBy: " + createdBy.ToString()).Append("\r\n");
            builder.Append("modifiedOn: " + modifiedOn.ToString()).Append("\r\n");
            builder.Append("modifiedBy: " + modifiedBy.ToString()).Append("\r\n");
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
			
			PriceManagementBatchHeaderCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = PriceManagementBatchHeader.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = PriceManagementBatchHeader.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (PriceManagementBatchHeader item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.ReasonId != Guid.Empty)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.HeaderId));
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
		
		
		private static bool IgnorThis(PriceManagementBatchHeader target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.ReasonId == Guid.Empty)
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
				PriceManagementBatchHeader parentTemplate = PriceManagementBatchHeader.Load(target.ReasonId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(PriceManagementBatchHeader target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="PriceManagementBatchHeader">PriceManagementBatchHeader</see> objects.
    /// </summary>
    public class PriceManagementBatchHeaderCollection : BindingList< PriceManagementBatchHeader>
    {
	}
}