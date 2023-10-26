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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.FepBatchTender.
    /// Date Created:   2020-08-09 02:14:10
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class FepBatchTender
    {
        private Guid key = Guid.Empty;
        private Guid tenderId = Guid.Empty;
        private Guid headerId = Guid.Empty;
        private string txType = String.Empty;
        private string txNumber = String.Empty;
        private DateTime txDate = DateTime.Parse("1900-1-1");
        private Guid typeId = Guid.Empty;
        private string currencyCode = String.Empty;
        private string cardNumber = String.Empty;
        private string authorizationCode = String.Empty;
        private decimal tenderAmount;
        private decimal exchangeRate;
        private decimal inLocalCurrency;
        private string eCR_TRACENUM = String.Empty;
        private string eCR_RESPONSE = String.Empty;

        /// <summary>
        /// Initialize an new empty FepBatchTender object.
        /// </summary>
        public FepBatchTender()
        {
        }
		
        /// <summary>
        /// Initialize a new FepBatchTender object with the given parameters.
        /// </summary>
        public FepBatchTender(Guid tenderId, Guid headerId, string txType, string txNumber, DateTime txDate, Guid typeId, string currencyCode, string cardNumber, string authorizationCode, decimal tenderAmount, decimal exchangeRate, decimal inLocalCurrency, string eCR_TRACENUM, string eCR_RESPONSE)
        {
                this.tenderId = tenderId;
                this.headerId = headerId;
                this.txType = txType;
                this.txNumber = txNumber;
                this.txDate = txDate;
                this.typeId = typeId;
                this.currencyCode = currencyCode;
                this.cardNumber = cardNumber;
                this.authorizationCode = authorizationCode;
                this.tenderAmount = tenderAmount;
                this.exchangeRate = exchangeRate;
                this.inLocalCurrency = inLocalCurrency;
                this.eCR_TRACENUM = eCR_TRACENUM;
                this.eCR_RESPONSE = eCR_RESPONSE;
        }	
		
        /// <summary>
        /// Loads a FepBatchTender object from the database using the given TenderId
        /// </summary>
        /// <param name="tenderId">The primary key value</param>
        /// <returns>A FepBatchTender object</returns>
        public static FepBatchTender Load(Guid tenderId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@TenderId", tenderId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spFepBatchTender_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    FepBatchTender result = new FepBatchTender();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a FepBatchTender object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A FepBatchTender object</returns>
        public static FepBatchTender LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spFepBatchTender_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    FepBatchTender result = new FepBatchTender();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of FepBatchTender objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the FepBatchTender objects in the database.</returns>
        public static FepBatchTenderCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spFepBatchTender_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of FepBatchTender objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the FepBatchTender objects in the database ordered by the columns specified.</returns>
        public static FepBatchTenderCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spFepBatchTender_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of FepBatchTender objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the FepBatchTender objects in the database.</returns>
        public static FepBatchTenderCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spFepBatchTender_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of FepBatchTender objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the FepBatchTender objects in the database ordered by the columns specified.</returns>
        public static FepBatchTenderCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spFepBatchTender_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of FepBatchTender objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the FepBatchTender objects in the database.</returns>
        public static FepBatchTenderCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            FepBatchTenderCollection result = new FepBatchTenderCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    FepBatchTender tmp = new FepBatchTender();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a FepBatchTender object from the database.
        /// </summary>
        /// <param name="tenderId">The primary key value</param>
        public static void Delete(Guid tenderId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@TenderId", tenderId) };
            SqlHelper.Default.ExecuteNonQuery("spFepBatchTender_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) tenderId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) headerId = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) txType = reader.GetString(2);
                if (!reader.IsDBNull(3)) txNumber = reader.GetString(3);
                if (!reader.IsDBNull(4)) txDate = reader.GetDateTime(4);
                if (!reader.IsDBNull(5)) typeId = reader.GetGuid(5);
                if (!reader.IsDBNull(6)) currencyCode = reader.GetString(6);
                if (!reader.IsDBNull(7)) cardNumber = reader.GetString(7);
                if (!reader.IsDBNull(8)) authorizationCode = reader.GetString(8);
                if (!reader.IsDBNull(9)) tenderAmount = reader.GetDecimal(9);
                if (!reader.IsDBNull(10)) exchangeRate = reader.GetDecimal(10);
                if (!reader.IsDBNull(11)) inLocalCurrency = reader.GetDecimal(11);
                if (!reader.IsDBNull(12)) eCR_TRACENUM = reader.GetString(12);
                if (!reader.IsDBNull(13)) eCR_RESPONSE = reader.GetString(13);
            }
        }
		
        public void Delete()
        {
            Delete(this.TenderId);
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
                if (key != TenderId)
                    this.Delete();
                Update();
            }
        }

        public Guid TenderId
        {
            get { return tenderId; }
            set { tenderId = value; }
        }

        public Guid HeaderId
        {
            get { return headerId; }
            set { headerId = value; }
        }

        public string TxType
        {
            get { return txType; }
            set { txType = value; }
        }

        public string TxNumber
        {
            get { return txNumber; }
            set { txNumber = value; }
        }

        public DateTime TxDate
        {
            get { return txDate; }
            set { txDate = value; }
        }

        public Guid TypeId
        {
            get { return typeId; }
            set { typeId = value; }
        }

        public string CurrencyCode
        {
            get { return currencyCode; }
            set { currencyCode = value; }
        }

        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; }
        }

        public string AuthorizationCode
        {
            get { return authorizationCode; }
            set { authorizationCode = value; }
        }

        public decimal TenderAmount
        {
            get { return tenderAmount; }
            set { tenderAmount = value; }
        }

        public decimal ExchangeRate
        {
            get { return exchangeRate; }
            set { exchangeRate = value; }
        }

        public decimal InLocalCurrency
        {
            get { return inLocalCurrency; }
            set { inLocalCurrency = value; }
        }

        public string ECR_TRACENUM
        {
            get { return eCR_TRACENUM; }
            set { eCR_TRACENUM = value; }
        }

        public string ECR_RESPONSE
        {
            get { return eCR_RESPONSE; }
            set { eCR_RESPONSE = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spFepBatchTender_InsRec", "@TenderId", out returnedValue, parameterValues);
            
            tenderId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spFepBatchTender_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[14];
            prams[0] = GetSqlParameter("@TenderId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.TenderId);
            prams[1] = GetSqlParameter("@HeaderId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.HeaderId);
            prams[2] = GetSqlParameter("@TxType", ParameterDirection.Input, SqlDbType.VarChar, 3, this.TxType);
            prams[3] = GetSqlParameter("@TxNumber", ParameterDirection.Input, SqlDbType.VarChar, 12, this.TxNumber);
            prams[4] = GetSqlParameter("@TxDate", ParameterDirection.Input, SqlDbType.DateTime, 8, this.TxDate);
            prams[5] = GetSqlParameter("@TypeId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.TypeId);
            prams[6] = GetSqlParameter("@CurrencyCode", ParameterDirection.Input, SqlDbType.VarChar, 3, this.CurrencyCode);
            prams[7] = GetSqlParameter("@CardNumber", ParameterDirection.Input, SqlDbType.VarChar, 20, this.CardNumber);
            prams[8] = GetSqlParameter("@AuthorizationCode", ParameterDirection.Input, SqlDbType.VarChar, 6, this.AuthorizationCode);
            prams[9] = GetSqlParameter("@TenderAmount", ParameterDirection.Input, SqlDbType.Money, 8, this.TenderAmount);
            prams[10] = GetSqlParameter("@ExchangeRate", ParameterDirection.Input, SqlDbType.Decimal, 5, this.ExchangeRate);
            prams[11] = GetSqlParameter("@InLocalCurrency", ParameterDirection.Input, SqlDbType.Money, 8, this.InLocalCurrency);
            prams[12] = GetSqlParameter("@ECR_TRACENUM", ParameterDirection.Input, SqlDbType.VarChar, 6, this.ECR_TRACENUM);
            prams[13] = GetSqlParameter("@ECR_RESPONSE", ParameterDirection.Input, SqlDbType.NText, 16, this.ECR_RESPONSE);
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
                GetSqlParameterWithoutDirection("@TenderId", SqlDbType.UniqueIdentifier, 16, this.TenderId),
                GetSqlParameterWithoutDirection("@HeaderId", SqlDbType.UniqueIdentifier, 16, this.HeaderId),
                GetSqlParameterWithoutDirection("@TxType", SqlDbType.VarChar, 3, this.TxType),
                GetSqlParameterWithoutDirection("@TxNumber", SqlDbType.VarChar, 12, this.TxNumber),
                GetSqlParameterWithoutDirection("@TxDate", SqlDbType.DateTime, 8, this.TxDate),
                GetSqlParameterWithoutDirection("@TypeId", SqlDbType.UniqueIdentifier, 16, this.TypeId),
                GetSqlParameterWithoutDirection("@CurrencyCode", SqlDbType.VarChar, 3, this.CurrencyCode),
                GetSqlParameterWithoutDirection("@CardNumber", SqlDbType.VarChar, 20, this.CardNumber),
                GetSqlParameterWithoutDirection("@AuthorizationCode", SqlDbType.VarChar, 6, this.AuthorizationCode),
                GetSqlParameterWithoutDirection("@TenderAmount", SqlDbType.Money, 8, this.TenderAmount),
                GetSqlParameterWithoutDirection("@ExchangeRate", SqlDbType.Decimal, 5, this.ExchangeRate),
                GetSqlParameterWithoutDirection("@InLocalCurrency", SqlDbType.Money, 8, this.InLocalCurrency),
                GetSqlParameterWithoutDirection("@ECR_TRACENUM", SqlDbType.VarChar, 6, this.ECR_TRACENUM),
                GetSqlParameterWithoutDirection("@ECR_RESPONSE", SqlDbType.NText, 16, this.ECR_RESPONSE)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("tenderId: " + tenderId.ToString()).Append("\r\n");
            builder.Append("headerId: " + headerId.ToString()).Append("\r\n");
            builder.Append("txType: " + txType.ToString()).Append("\r\n");
            builder.Append("txNumber: " + txNumber.ToString()).Append("\r\n");
            builder.Append("txDate: " + txDate.ToString()).Append("\r\n");
            builder.Append("typeId: " + typeId.ToString()).Append("\r\n");
            builder.Append("currencyCode: " + currencyCode.ToString()).Append("\r\n");
            builder.Append("cardNumber: " + cardNumber.ToString()).Append("\r\n");
            builder.Append("authorizationCode: " + authorizationCode.ToString()).Append("\r\n");
            builder.Append("tenderAmount: " + tenderAmount.ToString()).Append("\r\n");
            builder.Append("exchangeRate: " + exchangeRate.ToString()).Append("\r\n");
            builder.Append("inLocalCurrency: " + inLocalCurrency.ToString()).Append("\r\n");
            builder.Append("eCR_TRACENUM: " + eCR_TRACENUM.ToString()).Append("\r\n");
            builder.Append("eCR_RESPONSE: " + eCR_RESPONSE.ToString()).Append("\r\n");
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
			
			FepBatchTenderCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = FepBatchTender.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = FepBatchTender.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (FepBatchTender item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.HeaderId != Guid.Empty)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.TenderId));
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
		
		
		private static bool IgnorThis(FepBatchTender target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.HeaderId == Guid.Empty)
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
				FepBatchTender parentTemplate = FepBatchTender.Load(target.HeaderId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(FepBatchTender target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="FepBatchTender">FepBatchTender</see> objects.
    /// </summary>
    public class FepBatchTenderCollection : BindingList< FepBatchTender>
    {
	}
}
