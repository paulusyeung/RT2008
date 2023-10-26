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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.ProductWorkplacePeriodicSummary.
    /// Date Created:   2020-08-09 02:14:15
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class ProductWorkplacePeriodicSummary
    {
        private Guid key = Guid.Empty;
        private Guid periodicSummaryId = Guid.Empty;
        private Guid productId = Guid.Empty;
        private Guid workplaceId = Guid.Empty;
        private int fiscalYear = 0;
        private int period = 0;
        private DateTime periodStartedOn = DateTime.Parse("1900-1-1");
        private DateTime periodEndedOn = DateTime.Parse("1900-1-1");
        private decimal bFQTY;
        private decimal cDQTY;
        private decimal fEPQTY;
        private decimal rECQTY;
        private decimal iNVQTY;
        private decimal pOQTY;
        private decimal sOQTY;
        private decimal rEJQTY;
        private decimal ePQTY;

        /// <summary>
        /// Initialize an new empty ProductWorkplacePeriodicSummary object.
        /// </summary>
        public ProductWorkplacePeriodicSummary()
        {
        }
		
        /// <summary>
        /// Initialize a new ProductWorkplacePeriodicSummary object with the given parameters.
        /// </summary>
        public ProductWorkplacePeriodicSummary(Guid periodicSummaryId, Guid productId, Guid workplaceId, int fiscalYear, int period, DateTime periodStartedOn, DateTime periodEndedOn, decimal bFQTY, decimal cDQTY, decimal fEPQTY, decimal rECQTY, decimal iNVQTY, decimal pOQTY, decimal sOQTY, decimal rEJQTY, decimal ePQTY)
        {
                this.periodicSummaryId = periodicSummaryId;
                this.productId = productId;
                this.workplaceId = workplaceId;
                this.fiscalYear = fiscalYear;
                this.period = period;
                this.periodStartedOn = periodStartedOn;
                this.periodEndedOn = periodEndedOn;
                this.bFQTY = bFQTY;
                this.cDQTY = cDQTY;
                this.fEPQTY = fEPQTY;
                this.rECQTY = rECQTY;
                this.iNVQTY = iNVQTY;
                this.pOQTY = pOQTY;
                this.sOQTY = sOQTY;
                this.rEJQTY = rEJQTY;
                this.ePQTY = ePQTY;
        }	
		
        /// <summary>
        /// Loads a ProductWorkplacePeriodicSummary object from the database using the given PeriodicSummaryId
        /// </summary>
        /// <param name="periodicSummaryId">The primary key value</param>
        /// <returns>A ProductWorkplacePeriodicSummary object</returns>
        public static ProductWorkplacePeriodicSummary Load(Guid periodicSummaryId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@PeriodicSummaryId", periodicSummaryId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spProductWorkplacePeriodicSummary_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    ProductWorkplacePeriodicSummary result = new ProductWorkplacePeriodicSummary();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a ProductWorkplacePeriodicSummary object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A ProductWorkplacePeriodicSummary object</returns>
        public static ProductWorkplacePeriodicSummary LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spProductWorkplacePeriodicSummary_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    ProductWorkplacePeriodicSummary result = new ProductWorkplacePeriodicSummary();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of ProductWorkplacePeriodicSummary objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductWorkplacePeriodicSummary objects in the database.</returns>
        public static ProductWorkplacePeriodicSummaryCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spProductWorkplacePeriodicSummary_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductWorkplacePeriodicSummary objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the ProductWorkplacePeriodicSummary objects in the database ordered by the columns specified.</returns>
        public static ProductWorkplacePeriodicSummaryCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spProductWorkplacePeriodicSummary_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductWorkplacePeriodicSummary objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductWorkplacePeriodicSummary objects in the database.</returns>
        public static ProductWorkplacePeriodicSummaryCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spProductWorkplacePeriodicSummary_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductWorkplacePeriodicSummary objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the ProductWorkplacePeriodicSummary objects in the database ordered by the columns specified.</returns>
        public static ProductWorkplacePeriodicSummaryCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spProductWorkplacePeriodicSummary_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductWorkplacePeriodicSummary objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductWorkplacePeriodicSummary objects in the database.</returns>
        public static ProductWorkplacePeriodicSummaryCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            ProductWorkplacePeriodicSummaryCollection result = new ProductWorkplacePeriodicSummaryCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    ProductWorkplacePeriodicSummary tmp = new ProductWorkplacePeriodicSummary();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a ProductWorkplacePeriodicSummary object from the database.
        /// </summary>
        /// <param name="periodicSummaryId">The primary key value</param>
        public static void Delete(Guid periodicSummaryId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@PeriodicSummaryId", periodicSummaryId) };
            SqlHelper.Default.ExecuteNonQuery("spProductWorkplacePeriodicSummary_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) periodicSummaryId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) productId = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) workplaceId = reader.GetGuid(2);
                if (!reader.IsDBNull(3)) fiscalYear = reader.GetInt32(3);
                if (!reader.IsDBNull(4)) period = reader.GetInt32(4);
                if (!reader.IsDBNull(5)) periodStartedOn = reader.GetDateTime(5);
                if (!reader.IsDBNull(6)) periodEndedOn = reader.GetDateTime(6);
                if (!reader.IsDBNull(7)) bFQTY = reader.GetDecimal(7);
                if (!reader.IsDBNull(8)) cDQTY = reader.GetDecimal(8);
                if (!reader.IsDBNull(9)) fEPQTY = reader.GetDecimal(9);
                if (!reader.IsDBNull(10)) rECQTY = reader.GetDecimal(10);
                if (!reader.IsDBNull(11)) iNVQTY = reader.GetDecimal(11);
                if (!reader.IsDBNull(12)) pOQTY = reader.GetDecimal(12);
                if (!reader.IsDBNull(13)) sOQTY = reader.GetDecimal(13);
                if (!reader.IsDBNull(14)) rEJQTY = reader.GetDecimal(14);
                if (!reader.IsDBNull(15)) ePQTY = reader.GetDecimal(15);
            }
        }
		
        public void Delete()
        {
            Delete(this.PeriodicSummaryId);
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
                if (key != PeriodicSummaryId)
                    this.Delete();
                Update();
            }
        }

        public Guid PeriodicSummaryId
        {
            get { return periodicSummaryId; }
            set { periodicSummaryId = value; }
        }

        public Guid ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public Guid WorkplaceId
        {
            get { return workplaceId; }
            set { workplaceId = value; }
        }

        public int FiscalYear
        {
            get { return fiscalYear; }
            set { fiscalYear = value; }
        }

        public int Period
        {
            get { return period; }
            set { period = value; }
        }

        public DateTime PeriodStartedOn
        {
            get { return periodStartedOn; }
            set { periodStartedOn = value; }
        }

        public DateTime PeriodEndedOn
        {
            get { return periodEndedOn; }
            set { periodEndedOn = value; }
        }

        public decimal BFQTY
        {
            get { return bFQTY; }
            set { bFQTY = value; }
        }

        public decimal CDQTY
        {
            get { return cDQTY; }
            set { cDQTY = value; }
        }

        public decimal FEPQTY
        {
            get { return fEPQTY; }
            set { fEPQTY = value; }
        }

        public decimal RECQTY
        {
            get { return rECQTY; }
            set { rECQTY = value; }
        }

        public decimal INVQTY
        {
            get { return iNVQTY; }
            set { iNVQTY = value; }
        }

        public decimal POQTY
        {
            get { return pOQTY; }
            set { pOQTY = value; }
        }

        public decimal SOQTY
        {
            get { return sOQTY; }
            set { sOQTY = value; }
        }

        public decimal REJQTY
        {
            get { return rEJQTY; }
            set { rEJQTY = value; }
        }

        public decimal EPQTY
        {
            get { return ePQTY; }
            set { ePQTY = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spProductWorkplacePeriodicSummary_InsRec", "@PeriodicSummaryId", out returnedValue, parameterValues);
            
            periodicSummaryId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spProductWorkplacePeriodicSummary_UpdRec", parameterValues);
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
            prams[0] = GetSqlParameter("@PeriodicSummaryId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.PeriodicSummaryId);
            prams[1] = GetSqlParameter("@ProductId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ProductId);
            prams[2] = GetSqlParameter("@WorkplaceId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.WorkplaceId);
            prams[3] = GetSqlParameter("@FiscalYear", ParameterDirection.Input, SqlDbType.Int, 4, this.FiscalYear);
            prams[4] = GetSqlParameter("@Period", ParameterDirection.Input, SqlDbType.Int, 4, this.Period);
            prams[5] = GetSqlParameter("@PeriodStartedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.PeriodStartedOn);
            prams[6] = GetSqlParameter("@PeriodEndedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.PeriodEndedOn);
            prams[7] = GetSqlParameter("@BFQTY", ParameterDirection.Input, SqlDbType.Decimal, 9, this.BFQTY);
            prams[8] = GetSqlParameter("@CDQTY", ParameterDirection.Input, SqlDbType.Decimal, 9, this.CDQTY);
            prams[9] = GetSqlParameter("@FEPQTY", ParameterDirection.Input, SqlDbType.Decimal, 9, this.FEPQTY);
            prams[10] = GetSqlParameter("@RECQTY", ParameterDirection.Input, SqlDbType.Decimal, 9, this.RECQTY);
            prams[11] = GetSqlParameter("@INVQTY", ParameterDirection.Input, SqlDbType.Decimal, 9, this.INVQTY);
            prams[12] = GetSqlParameter("@POQTY", ParameterDirection.Input, SqlDbType.Decimal, 9, this.POQTY);
            prams[13] = GetSqlParameter("@SOQTY", ParameterDirection.Input, SqlDbType.Decimal, 9, this.SOQTY);
            prams[14] = GetSqlParameter("@REJQTY", ParameterDirection.Input, SqlDbType.Decimal, 9, this.REJQTY);
            prams[15] = GetSqlParameter("@EPQTY", ParameterDirection.Input, SqlDbType.Decimal, 9, this.EPQTY);
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
                GetSqlParameterWithoutDirection("@PeriodicSummaryId", SqlDbType.UniqueIdentifier, 16, this.PeriodicSummaryId),
                GetSqlParameterWithoutDirection("@ProductId", SqlDbType.UniqueIdentifier, 16, this.ProductId),
                GetSqlParameterWithoutDirection("@WorkplaceId", SqlDbType.UniqueIdentifier, 16, this.WorkplaceId),
                GetSqlParameterWithoutDirection("@FiscalYear", SqlDbType.Int, 4, this.FiscalYear),
                GetSqlParameterWithoutDirection("@Period", SqlDbType.Int, 4, this.Period),
                GetSqlParameterWithoutDirection("@PeriodStartedOn", SqlDbType.DateTime, 8, this.PeriodStartedOn),
                GetSqlParameterWithoutDirection("@PeriodEndedOn", SqlDbType.DateTime, 8, this.PeriodEndedOn),
                GetSqlParameterWithoutDirection("@BFQTY", SqlDbType.Decimal, 9, this.BFQTY),
                GetSqlParameterWithoutDirection("@CDQTY", SqlDbType.Decimal, 9, this.CDQTY),
                GetSqlParameterWithoutDirection("@FEPQTY", SqlDbType.Decimal, 9, this.FEPQTY),
                GetSqlParameterWithoutDirection("@RECQTY", SqlDbType.Decimal, 9, this.RECQTY),
                GetSqlParameterWithoutDirection("@INVQTY", SqlDbType.Decimal, 9, this.INVQTY),
                GetSqlParameterWithoutDirection("@POQTY", SqlDbType.Decimal, 9, this.POQTY),
                GetSqlParameterWithoutDirection("@SOQTY", SqlDbType.Decimal, 9, this.SOQTY),
                GetSqlParameterWithoutDirection("@REJQTY", SqlDbType.Decimal, 9, this.REJQTY),
                GetSqlParameterWithoutDirection("@EPQTY", SqlDbType.Decimal, 9, this.EPQTY)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("periodicSummaryId: " + periodicSummaryId.ToString()).Append("\r\n");
            builder.Append("productId: " + productId.ToString()).Append("\r\n");
            builder.Append("workplaceId: " + workplaceId.ToString()).Append("\r\n");
            builder.Append("fiscalYear: " + fiscalYear.ToString()).Append("\r\n");
            builder.Append("period: " + period.ToString()).Append("\r\n");
            builder.Append("periodStartedOn: " + periodStartedOn.ToString()).Append("\r\n");
            builder.Append("periodEndedOn: " + periodEndedOn.ToString()).Append("\r\n");
            builder.Append("bFQTY: " + bFQTY.ToString()).Append("\r\n");
            builder.Append("cDQTY: " + cDQTY.ToString()).Append("\r\n");
            builder.Append("fEPQTY: " + fEPQTY.ToString()).Append("\r\n");
            builder.Append("rECQTY: " + rECQTY.ToString()).Append("\r\n");
            builder.Append("iNVQTY: " + iNVQTY.ToString()).Append("\r\n");
            builder.Append("pOQTY: " + pOQTY.ToString()).Append("\r\n");
            builder.Append("sOQTY: " + sOQTY.ToString()).Append("\r\n");
            builder.Append("rEJQTY: " + rEJQTY.ToString()).Append("\r\n");
            builder.Append("ePQTY: " + ePQTY.ToString()).Append("\r\n");
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
			
			ProductWorkplacePeriodicSummaryCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = ProductWorkplacePeriodicSummary.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = ProductWorkplacePeriodicSummary.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (ProductWorkplacePeriodicSummary item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.ProductId != Guid.Empty)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.PeriodicSummaryId));
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
		
		
		private static bool IgnorThis(ProductWorkplacePeriodicSummary target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.ProductId == Guid.Empty)
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
				ProductWorkplacePeriodicSummary parentTemplate = ProductWorkplacePeriodicSummary.Load(target.ProductId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(ProductWorkplacePeriodicSummary target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="ProductWorkplacePeriodicSummary">ProductWorkplacePeriodicSummary</see> objects.
    /// </summary>
    public class ProductWorkplacePeriodicSummaryCollection : BindingList< ProductWorkplacePeriodicSummary>
    {
	}
}
