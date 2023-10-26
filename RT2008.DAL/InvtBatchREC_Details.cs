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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.InvtBatchREC_Details.
    /// Date Created:   2020-08-09 02:14:10
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class InvtBatchREC_Details
    {
        private Guid key = Guid.Empty;
        private Guid detailsId = Guid.Empty;
        private Guid headerId = Guid.Empty;
        private string txType = String.Empty;
        private string txNumber = String.Empty;
        private int lineNumber = 0;
        private Guid productId = Guid.Empty;
        private decimal qtyOrdered;
        private decimal qtyReceived;
        private decimal qtyReceivedSum;
        private decimal unitCost;
        private decimal discount;
        private decimal netCost;
        private string remarks = String.Empty;
        private decimal nETRATECOST;
        private decimal cOEFCOST;
        private decimal bILLQTY;
        private decimal bILLUAMT;

        /// <summary>
        /// Initialize an new empty InvtBatchREC_Details object.
        /// </summary>
        public InvtBatchREC_Details()
        {
        }
		
        /// <summary>
        /// Initialize a new InvtBatchREC_Details object with the given parameters.
        /// </summary>
        public InvtBatchREC_Details(Guid detailsId, Guid headerId, string txType, string txNumber, int lineNumber, Guid productId, decimal qtyOrdered, decimal qtyReceived, decimal qtyReceivedSum, decimal unitCost, decimal discount, decimal netCost, string remarks, decimal nETRATECOST, decimal cOEFCOST, decimal bILLQTY, decimal bILLUAMT)
        {
                this.detailsId = detailsId;
                this.headerId = headerId;
                this.txType = txType;
                this.txNumber = txNumber;
                this.lineNumber = lineNumber;
                this.productId = productId;
                this.qtyOrdered = qtyOrdered;
                this.qtyReceived = qtyReceived;
                this.qtyReceivedSum = qtyReceivedSum;
                this.unitCost = unitCost;
                this.discount = discount;
                this.netCost = netCost;
                this.remarks = remarks;
                this.nETRATECOST = nETRATECOST;
                this.cOEFCOST = cOEFCOST;
                this.bILLQTY = bILLQTY;
                this.bILLUAMT = bILLUAMT;
        }	
		
        /// <summary>
        /// Loads a InvtBatchREC_Details object from the database using the given DetailsId
        /// </summary>
        /// <param name="detailsId">The primary key value</param>
        /// <returns>A InvtBatchREC_Details object</returns>
        public static InvtBatchREC_Details Load(Guid detailsId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@DetailsId", detailsId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spInvtBatchREC_Details_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    InvtBatchREC_Details result = new InvtBatchREC_Details();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a InvtBatchREC_Details object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A InvtBatchREC_Details object</returns>
        public static InvtBatchREC_Details LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spInvtBatchREC_Details_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    InvtBatchREC_Details result = new InvtBatchREC_Details();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of InvtBatchREC_Details objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the InvtBatchREC_Details objects in the database.</returns>
        public static InvtBatchREC_DetailsCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spInvtBatchREC_Details_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of InvtBatchREC_Details objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the InvtBatchREC_Details objects in the database ordered by the columns specified.</returns>
        public static InvtBatchREC_DetailsCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spInvtBatchREC_Details_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of InvtBatchREC_Details objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the InvtBatchREC_Details objects in the database.</returns>
        public static InvtBatchREC_DetailsCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spInvtBatchREC_Details_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of InvtBatchREC_Details objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the InvtBatchREC_Details objects in the database ordered by the columns specified.</returns>
        public static InvtBatchREC_DetailsCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spInvtBatchREC_Details_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of InvtBatchREC_Details objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the InvtBatchREC_Details objects in the database.</returns>
        public static InvtBatchREC_DetailsCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            InvtBatchREC_DetailsCollection result = new InvtBatchREC_DetailsCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    InvtBatchREC_Details tmp = new InvtBatchREC_Details();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a InvtBatchREC_Details object from the database.
        /// </summary>
        /// <param name="detailsId">The primary key value</param>
        public static void Delete(Guid detailsId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@DetailsId", detailsId) };
            SqlHelper.Default.ExecuteNonQuery("spInvtBatchREC_Details_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) detailsId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) headerId = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) txType = reader.GetString(2);
                if (!reader.IsDBNull(3)) txNumber = reader.GetString(3);
                if (!reader.IsDBNull(4)) lineNumber = reader.GetInt32(4);
                if (!reader.IsDBNull(5)) productId = reader.GetGuid(5);
                if (!reader.IsDBNull(6)) qtyOrdered = reader.GetDecimal(6);
                if (!reader.IsDBNull(7)) qtyReceived = reader.GetDecimal(7);
                if (!reader.IsDBNull(8)) qtyReceivedSum = reader.GetDecimal(8);
                if (!reader.IsDBNull(9)) unitCost = reader.GetDecimal(9);
                if (!reader.IsDBNull(10)) discount = reader.GetDecimal(10);
                if (!reader.IsDBNull(11)) netCost = reader.GetDecimal(11);
                if (!reader.IsDBNull(12)) remarks = reader.GetString(12);
                if (!reader.IsDBNull(13)) nETRATECOST = reader.GetDecimal(13);
                if (!reader.IsDBNull(14)) cOEFCOST = reader.GetDecimal(14);
                if (!reader.IsDBNull(15)) bILLQTY = reader.GetDecimal(15);
                if (!reader.IsDBNull(16)) bILLUAMT = reader.GetDecimal(16);
            }
        }
		
        public void Delete()
        {
            Delete(this.DetailsId);
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
                if (key != DetailsId)
                    this.Delete();
                Update();
            }
        }

        public Guid DetailsId
        {
            get { return detailsId; }
            set { detailsId = value; }
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

        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        public Guid ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public decimal QtyOrdered
        {
            get { return qtyOrdered; }
            set { qtyOrdered = value; }
        }

        public decimal QtyReceived
        {
            get { return qtyReceived; }
            set { qtyReceived = value; }
        }

        public decimal QtyReceivedSum
        {
            get { return qtyReceivedSum; }
            set { qtyReceivedSum = value; }
        }

        public decimal UnitCost
        {
            get { return unitCost; }
            set { unitCost = value; }
        }

        public decimal Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public decimal NetCost
        {
            get { return netCost; }
            set { netCost = value; }
        }

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        public decimal NETRATECOST
        {
            get { return nETRATECOST; }
            set { nETRATECOST = value; }
        }

        public decimal COEFCOST
        {
            get { return cOEFCOST; }
            set { cOEFCOST = value; }
        }

        public decimal BILLQTY
        {
            get { return bILLQTY; }
            set { bILLQTY = value; }
        }

        public decimal BILLUAMT
        {
            get { return bILLUAMT; }
            set { bILLUAMT = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spInvtBatchREC_Details_InsRec", "@DetailsId", out returnedValue, parameterValues);
            
            detailsId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spInvtBatchREC_Details_UpdRec", parameterValues);
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
            prams[0] = GetSqlParameter("@DetailsId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.DetailsId);
            prams[1] = GetSqlParameter("@HeaderId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.HeaderId);
            prams[2] = GetSqlParameter("@TxType", ParameterDirection.Input, SqlDbType.VarChar, 3, this.TxType);
            prams[3] = GetSqlParameter("@TxNumber", ParameterDirection.Input, SqlDbType.VarChar, 12, this.TxNumber);
            prams[4] = GetSqlParameter("@LineNumber", ParameterDirection.Input, SqlDbType.Int, 4, this.LineNumber);
            prams[5] = GetSqlParameter("@ProductId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ProductId);
            prams[6] = GetSqlParameter("@QtyOrdered", ParameterDirection.Input, SqlDbType.Decimal, 9, this.QtyOrdered);
            prams[7] = GetSqlParameter("@QtyReceived", ParameterDirection.Input, SqlDbType.Decimal, 9, this.QtyReceived);
            prams[8] = GetSqlParameter("@QtyReceivedSum", ParameterDirection.Input, SqlDbType.Decimal, 9, this.QtyReceivedSum);
            prams[9] = GetSqlParameter("@UnitCost", ParameterDirection.Input, SqlDbType.Money, 8, this.UnitCost);
            prams[10] = GetSqlParameter("@Discount", ParameterDirection.Input, SqlDbType.Decimal, 5, this.Discount);
            prams[11] = GetSqlParameter("@NetCost", ParameterDirection.Input, SqlDbType.Money, 8, this.NetCost);
            prams[12] = GetSqlParameter("@Remarks", ParameterDirection.Input, SqlDbType.NVarChar, 512, this.Remarks);
            prams[13] = GetSqlParameter("@NETRATECOST", ParameterDirection.Input, SqlDbType.Money, 8, this.NETRATECOST);
            prams[14] = GetSqlParameter("@COEFCOST", ParameterDirection.Input, SqlDbType.Money, 8, this.COEFCOST);
            prams[15] = GetSqlParameter("@BILLQTY", ParameterDirection.Input, SqlDbType.Decimal, 9, this.BILLQTY);
            prams[16] = GetSqlParameter("@BILLUAMT", ParameterDirection.Input, SqlDbType.Money, 8, this.BILLUAMT);
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
                GetSqlParameterWithoutDirection("@DetailsId", SqlDbType.UniqueIdentifier, 16, this.DetailsId),
                GetSqlParameterWithoutDirection("@HeaderId", SqlDbType.UniqueIdentifier, 16, this.HeaderId),
                GetSqlParameterWithoutDirection("@TxType", SqlDbType.VarChar, 3, this.TxType),
                GetSqlParameterWithoutDirection("@TxNumber", SqlDbType.VarChar, 12, this.TxNumber),
                GetSqlParameterWithoutDirection("@LineNumber", SqlDbType.Int, 4, this.LineNumber),
                GetSqlParameterWithoutDirection("@ProductId", SqlDbType.UniqueIdentifier, 16, this.ProductId),
                GetSqlParameterWithoutDirection("@QtyOrdered", SqlDbType.Decimal, 9, this.QtyOrdered),
                GetSqlParameterWithoutDirection("@QtyReceived", SqlDbType.Decimal, 9, this.QtyReceived),
                GetSqlParameterWithoutDirection("@QtyReceivedSum", SqlDbType.Decimal, 9, this.QtyReceivedSum),
                GetSqlParameterWithoutDirection("@UnitCost", SqlDbType.Money, 8, this.UnitCost),
                GetSqlParameterWithoutDirection("@Discount", SqlDbType.Decimal, 5, this.Discount),
                GetSqlParameterWithoutDirection("@NetCost", SqlDbType.Money, 8, this.NetCost),
                GetSqlParameterWithoutDirection("@Remarks", SqlDbType.NVarChar, 512, this.Remarks),
                GetSqlParameterWithoutDirection("@NETRATECOST", SqlDbType.Money, 8, this.NETRATECOST),
                GetSqlParameterWithoutDirection("@COEFCOST", SqlDbType.Money, 8, this.COEFCOST),
                GetSqlParameterWithoutDirection("@BILLQTY", SqlDbType.Decimal, 9, this.BILLQTY),
                GetSqlParameterWithoutDirection("@BILLUAMT", SqlDbType.Money, 8, this.BILLUAMT)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("detailsId: " + detailsId.ToString()).Append("\r\n");
            builder.Append("headerId: " + headerId.ToString()).Append("\r\n");
            builder.Append("txType: " + txType.ToString()).Append("\r\n");
            builder.Append("txNumber: " + txNumber.ToString()).Append("\r\n");
            builder.Append("lineNumber: " + lineNumber.ToString()).Append("\r\n");
            builder.Append("productId: " + productId.ToString()).Append("\r\n");
            builder.Append("qtyOrdered: " + qtyOrdered.ToString()).Append("\r\n");
            builder.Append("qtyReceived: " + qtyReceived.ToString()).Append("\r\n");
            builder.Append("qtyReceivedSum: " + qtyReceivedSum.ToString()).Append("\r\n");
            builder.Append("unitCost: " + unitCost.ToString()).Append("\r\n");
            builder.Append("discount: " + discount.ToString()).Append("\r\n");
            builder.Append("netCost: " + netCost.ToString()).Append("\r\n");
            builder.Append("remarks: " + remarks.ToString()).Append("\r\n");
            builder.Append("nETRATECOST: " + nETRATECOST.ToString()).Append("\r\n");
            builder.Append("cOEFCOST: " + cOEFCOST.ToString()).Append("\r\n");
            builder.Append("bILLQTY: " + bILLQTY.ToString()).Append("\r\n");
            builder.Append("bILLUAMT: " + bILLUAMT.ToString()).Append("\r\n");
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
			
			InvtBatchREC_DetailsCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = InvtBatchREC_Details.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = InvtBatchREC_Details.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (InvtBatchREC_Details item in source)
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
                    sourceList.Add(new Common.ComboItem(code, item.DetailsId));
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
		
		
		private static bool IgnorThis(InvtBatchREC_Details target, string parentFilter)
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
				InvtBatchREC_Details parentTemplate = InvtBatchREC_Details.Load(target.HeaderId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(InvtBatchREC_Details target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="InvtBatchREC_Details">InvtBatchREC_Details</see> objects.
    /// </summary>
    public class InvtBatchREC_DetailsCollection : BindingList< InvtBatchREC_Details>
    {
	}
}
