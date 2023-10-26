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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.ProductRemarks.
    /// Date Created:   2020-08-09 02:14:14
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class ProductRemarks
    {
        private Guid key = Guid.Empty;
        private Guid productRemarksId = Guid.Empty;
        private Guid productId = Guid.Empty;
        private string rEMARK1 = String.Empty;
        private string rEMARK2 = String.Empty;
        private string rEMARK3 = String.Empty;
        private string rEMARK4 = String.Empty;
        private string rEMARK5 = String.Empty;
        private string rEMARK6 = String.Empty;
        private string photo = String.Empty;
        private string notes = String.Empty;
        private bool downloadToShop;
        private string binX = String.Empty;
        private string binY = String.Empty;
        private string binZ = String.Empty;
        private bool offDisplayItem;
        private bool downloadToCounter;
        private string photo2 = String.Empty;
        private string photo3 = String.Empty;
        private string photo4 = String.Empty;
        private string photo5 = String.Empty;

        /// <summary>
        /// Initialize an new empty ProductRemarks object.
        /// </summary>
        public ProductRemarks()
        {
        }
		
        /// <summary>
        /// Initialize a new ProductRemarks object with the given parameters.
        /// </summary>
        public ProductRemarks(Guid productRemarksId, Guid productId, string rEMARK1, string rEMARK2, string rEMARK3, string rEMARK4, string rEMARK5, string rEMARK6, string photo, string notes, bool downloadToShop, string binX, string binY, string binZ, bool offDisplayItem, bool downloadToCounter, string photo2, string photo3, string photo4, string photo5)
        {
                this.productRemarksId = productRemarksId;
                this.productId = productId;
                this.rEMARK1 = rEMARK1;
                this.rEMARK2 = rEMARK2;
                this.rEMARK3 = rEMARK3;
                this.rEMARK4 = rEMARK4;
                this.rEMARK5 = rEMARK5;
                this.rEMARK6 = rEMARK6;
                this.photo = photo;
                this.notes = notes;
                this.downloadToShop = downloadToShop;
                this.binX = binX;
                this.binY = binY;
                this.binZ = binZ;
                this.offDisplayItem = offDisplayItem;
                this.downloadToCounter = downloadToCounter;
                this.photo2 = photo2;
                this.photo3 = photo3;
                this.photo4 = photo4;
                this.photo5 = photo5;
        }	
		
        /// <summary>
        /// Loads a ProductRemarks object from the database using the given ProductRemarksId
        /// </summary>
        /// <param name="productRemarksId">The primary key value</param>
        /// <returns>A ProductRemarks object</returns>
        public static ProductRemarks Load(Guid productRemarksId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ProductRemarksId", productRemarksId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spProductRemarks_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    ProductRemarks result = new ProductRemarks();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a ProductRemarks object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A ProductRemarks object</returns>
        public static ProductRemarks LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spProductRemarks_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    ProductRemarks result = new ProductRemarks();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of ProductRemarks objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductRemarks objects in the database.</returns>
        public static ProductRemarksCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spProductRemarks_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductRemarks objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the ProductRemarks objects in the database ordered by the columns specified.</returns>
        public static ProductRemarksCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spProductRemarks_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductRemarks objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductRemarks objects in the database.</returns>
        public static ProductRemarksCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spProductRemarks_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductRemarks objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the ProductRemarks objects in the database ordered by the columns specified.</returns>
        public static ProductRemarksCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spProductRemarks_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductRemarks objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductRemarks objects in the database.</returns>
        public static ProductRemarksCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            ProductRemarksCollection result = new ProductRemarksCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    ProductRemarks tmp = new ProductRemarks();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a ProductRemarks object from the database.
        /// </summary>
        /// <param name="productRemarksId">The primary key value</param>
        public static void Delete(Guid productRemarksId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@ProductRemarksId", productRemarksId) };
            SqlHelper.Default.ExecuteNonQuery("spProductRemarks_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) productRemarksId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) productId = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) rEMARK1 = reader.GetString(2);
                if (!reader.IsDBNull(3)) rEMARK2 = reader.GetString(3);
                if (!reader.IsDBNull(4)) rEMARK3 = reader.GetString(4);
                if (!reader.IsDBNull(5)) rEMARK4 = reader.GetString(5);
                if (!reader.IsDBNull(6)) rEMARK5 = reader.GetString(6);
                if (!reader.IsDBNull(7)) rEMARK6 = reader.GetString(7);
                if (!reader.IsDBNull(8)) photo = reader.GetString(8);
                if (!reader.IsDBNull(9)) notes = reader.GetString(9);
                if (!reader.IsDBNull(10)) downloadToShop = reader.GetBoolean(10);
                if (!reader.IsDBNull(11)) binX = reader.GetString(11);
                if (!reader.IsDBNull(12)) binY = reader.GetString(12);
                if (!reader.IsDBNull(13)) binZ = reader.GetString(13);
                if (!reader.IsDBNull(14)) offDisplayItem = reader.GetBoolean(14);
                if (!reader.IsDBNull(15)) downloadToCounter = reader.GetBoolean(15);
                if (!reader.IsDBNull(16)) photo2 = reader.GetString(16);
                if (!reader.IsDBNull(17)) photo3 = reader.GetString(17);
                if (!reader.IsDBNull(18)) photo4 = reader.GetString(18);
                if (!reader.IsDBNull(19)) photo5 = reader.GetString(19);
            }
        }
		
        public void Delete()
        {
            Delete(this.ProductRemarksId);
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
                if (key != ProductRemarksId)
                    this.Delete();
                Update();
            }
        }

        public Guid ProductRemarksId
        {
            get { return productRemarksId; }
            set { productRemarksId = value; }
        }

        public Guid ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public string REMARK1
        {
            get { return rEMARK1; }
            set { rEMARK1 = value; }
        }

        public string REMARK2
        {
            get { return rEMARK2; }
            set { rEMARK2 = value; }
        }

        public string REMARK3
        {
            get { return rEMARK3; }
            set { rEMARK3 = value; }
        }

        public string REMARK4
        {
            get { return rEMARK4; }
            set { rEMARK4 = value; }
        }

        public string REMARK5
        {
            get { return rEMARK5; }
            set { rEMARK5 = value; }
        }

        public string REMARK6
        {
            get { return rEMARK6; }
            set { rEMARK6 = value; }
        }

        public string Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public bool DownloadToShop
        {
            get { return downloadToShop; }
            set { downloadToShop = value; }
        }

        public string BinX
        {
            get { return binX; }
            set { binX = value; }
        }

        public string BinY
        {
            get { return binY; }
            set { binY = value; }
        }

        public string BinZ
        {
            get { return binZ; }
            set { binZ = value; }
        }

        public bool OffDisplayItem
        {
            get { return offDisplayItem; }
            set { offDisplayItem = value; }
        }

        public bool DownloadToCounter
        {
            get { return downloadToCounter; }
            set { downloadToCounter = value; }
        }

        public string Photo2
        {
            get { return photo2; }
            set { photo2 = value; }
        }

        public string Photo3
        {
            get { return photo3; }
            set { photo3 = value; }
        }

        public string Photo4
        {
            get { return photo4; }
            set { photo4 = value; }
        }

        public string Photo5
        {
            get { return photo5; }
            set { photo5 = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spProductRemarks_InsRec", "@ProductRemarksId", out returnedValue, parameterValues);
            
            productRemarksId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spProductRemarks_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[20];
            prams[0] = GetSqlParameter("@ProductRemarksId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.ProductRemarksId);
            prams[1] = GetSqlParameter("@ProductId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ProductId);
            prams[2] = GetSqlParameter("@REMARK1", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.REMARK1);
            prams[3] = GetSqlParameter("@REMARK2", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.REMARK2);
            prams[4] = GetSqlParameter("@REMARK3", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.REMARK3);
            prams[5] = GetSqlParameter("@REMARK4", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.REMARK4);
            prams[6] = GetSqlParameter("@REMARK5", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.REMARK5);
            prams[7] = GetSqlParameter("@REMARK6", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.REMARK6);
            prams[8] = GetSqlParameter("@Photo", ParameterDirection.Input, SqlDbType.NVarChar, 100, this.Photo);
            prams[9] = GetSqlParameter("@Notes", ParameterDirection.Input, SqlDbType.NText, 16, this.Notes);
            prams[10] = GetSqlParameter("@DownloadToShop", ParameterDirection.Input, SqlDbType.Bit, 1, this.DownloadToShop);
            prams[11] = GetSqlParameter("@BinX", ParameterDirection.Input, SqlDbType.VarChar, 4, this.BinX);
            prams[12] = GetSqlParameter("@BinY", ParameterDirection.Input, SqlDbType.VarChar, 4, this.BinY);
            prams[13] = GetSqlParameter("@BinZ", ParameterDirection.Input, SqlDbType.VarChar, 4, this.BinZ);
            prams[14] = GetSqlParameter("@OffDisplayItem", ParameterDirection.Input, SqlDbType.Bit, 1, this.OffDisplayItem);
            prams[15] = GetSqlParameter("@DownloadToCounter", ParameterDirection.Input, SqlDbType.Bit, 1, this.DownloadToCounter);
            prams[16] = GetSqlParameter("@Photo2", ParameterDirection.Input, SqlDbType.NVarChar, 100, this.Photo2);
            prams[17] = GetSqlParameter("@Photo3", ParameterDirection.Input, SqlDbType.NVarChar, 100, this.Photo3);
            prams[18] = GetSqlParameter("@Photo4", ParameterDirection.Input, SqlDbType.NVarChar, 100, this.Photo4);
            prams[19] = GetSqlParameter("@Photo5", ParameterDirection.Input, SqlDbType.NVarChar, 100, this.Photo5);
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
                GetSqlParameterWithoutDirection("@ProductRemarksId", SqlDbType.UniqueIdentifier, 16, this.ProductRemarksId),
                GetSqlParameterWithoutDirection("@ProductId", SqlDbType.UniqueIdentifier, 16, this.ProductId),
                GetSqlParameterWithoutDirection("@REMARK1", SqlDbType.NVarChar, 10, this.REMARK1),
                GetSqlParameterWithoutDirection("@REMARK2", SqlDbType.NVarChar, 10, this.REMARK2),
                GetSqlParameterWithoutDirection("@REMARK3", SqlDbType.NVarChar, 10, this.REMARK3),
                GetSqlParameterWithoutDirection("@REMARK4", SqlDbType.NVarChar, 10, this.REMARK4),
                GetSqlParameterWithoutDirection("@REMARK5", SqlDbType.NVarChar, 10, this.REMARK5),
                GetSqlParameterWithoutDirection("@REMARK6", SqlDbType.NVarChar, 10, this.REMARK6),
                GetSqlParameterWithoutDirection("@Photo", SqlDbType.NVarChar, 100, this.Photo),
                GetSqlParameterWithoutDirection("@Notes", SqlDbType.NText, 16, this.Notes),
                GetSqlParameterWithoutDirection("@DownloadToShop", SqlDbType.Bit, 1, this.DownloadToShop),
                GetSqlParameterWithoutDirection("@BinX", SqlDbType.VarChar, 4, this.BinX),
                GetSqlParameterWithoutDirection("@BinY", SqlDbType.VarChar, 4, this.BinY),
                GetSqlParameterWithoutDirection("@BinZ", SqlDbType.VarChar, 4, this.BinZ),
                GetSqlParameterWithoutDirection("@OffDisplayItem", SqlDbType.Bit, 1, this.OffDisplayItem),
                GetSqlParameterWithoutDirection("@DownloadToCounter", SqlDbType.Bit, 1, this.DownloadToCounter),
                GetSqlParameterWithoutDirection("@Photo2", SqlDbType.NVarChar, 100, this.Photo2),
                GetSqlParameterWithoutDirection("@Photo3", SqlDbType.NVarChar, 100, this.Photo3),
                GetSqlParameterWithoutDirection("@Photo4", SqlDbType.NVarChar, 100, this.Photo4),
                GetSqlParameterWithoutDirection("@Photo5", SqlDbType.NVarChar, 100, this.Photo5)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("productRemarksId: " + productRemarksId.ToString()).Append("\r\n");
            builder.Append("productId: " + productId.ToString()).Append("\r\n");
            builder.Append("rEMARK1: " + rEMARK1.ToString()).Append("\r\n");
            builder.Append("rEMARK2: " + rEMARK2.ToString()).Append("\r\n");
            builder.Append("rEMARK3: " + rEMARK3.ToString()).Append("\r\n");
            builder.Append("rEMARK4: " + rEMARK4.ToString()).Append("\r\n");
            builder.Append("rEMARK5: " + rEMARK5.ToString()).Append("\r\n");
            builder.Append("rEMARK6: " + rEMARK6.ToString()).Append("\r\n");
            builder.Append("photo: " + photo.ToString()).Append("\r\n");
            builder.Append("notes: " + notes.ToString()).Append("\r\n");
            builder.Append("downloadToShop: " + downloadToShop.ToString()).Append("\r\n");
            builder.Append("binX: " + binX.ToString()).Append("\r\n");
            builder.Append("binY: " + binY.ToString()).Append("\r\n");
            builder.Append("binZ: " + binZ.ToString()).Append("\r\n");
            builder.Append("offDisplayItem: " + offDisplayItem.ToString()).Append("\r\n");
            builder.Append("downloadToCounter: " + downloadToCounter.ToString()).Append("\r\n");
            builder.Append("photo2: " + photo2.ToString()).Append("\r\n");
            builder.Append("photo3: " + photo3.ToString()).Append("\r\n");
            builder.Append("photo4: " + photo4.ToString()).Append("\r\n");
            builder.Append("photo5: " + photo5.ToString()).Append("\r\n");
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
			
			ProductRemarksCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = ProductRemarks.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = ProductRemarks.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (ProductRemarks item in source)
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
                    sourceList.Add(new Common.ComboItem(code, item.ProductRemarksId));
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
		
		
		private static bool IgnorThis(ProductRemarks target, string parentFilter)
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
				ProductRemarks parentTemplate = ProductRemarks.Load(target.ProductId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(ProductRemarks target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="ProductRemarks">ProductRemarks</see> objects.
    /// </summary>
    public class ProductRemarksCollection : BindingList< ProductRemarks>
    {
	}
}