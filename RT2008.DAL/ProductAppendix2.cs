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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.ProductAppendix2.
    /// Date Created:   2020-08-09 02:14:13
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class ProductAppendix2
    {
        private Guid key = Guid.Empty;
        private Guid appendix2Id = Guid.Empty;
        private Guid parentAppendix = Guid.Empty;
        private string appendix2Code = String.Empty;
        private string appendix2Initial = String.Empty;
        private string appendix2Name = String.Empty;
        private string appendix2Name_Chs = String.Empty;
        private string appendix2Name_Cht = String.Empty;
        private DateTime createdOn = DateTime.Parse("1900-1-1");
        private Guid createdBy = Guid.Empty;
        private DateTime modifiedOn = DateTime.Parse("1900-1-1");
        private Guid modifiedBy = Guid.Empty;
        private bool retired;
        private DateTime retiredOn = DateTime.Parse("1900-1-1");
        private Guid retiredBy = Guid.Empty;

        /// <summary>
        /// Initialize an new empty ProductAppendix2 object.
        /// </summary>
        public ProductAppendix2()
        {
        }
		
        /// <summary>
        /// Initialize a new ProductAppendix2 object with the given parameters.
        /// </summary>
        public ProductAppendix2(Guid appendix2Id, Guid parentAppendix, string appendix2Code, string appendix2Initial, string appendix2Name, string appendix2Name_Chs, string appendix2Name_Cht, DateTime createdOn, Guid createdBy, DateTime modifiedOn, Guid modifiedBy, bool retired, DateTime retiredOn, Guid retiredBy)
        {
                this.appendix2Id = appendix2Id;
                this.parentAppendix = parentAppendix;
                this.appendix2Code = appendix2Code;
                this.appendix2Initial = appendix2Initial;
                this.appendix2Name = appendix2Name;
                this.appendix2Name_Chs = appendix2Name_Chs;
                this.appendix2Name_Cht = appendix2Name_Cht;
                this.createdOn = createdOn;
                this.createdBy = createdBy;
                this.modifiedOn = modifiedOn;
                this.modifiedBy = modifiedBy;
                this.retired = retired;
                this.retiredOn = retiredOn;
                this.retiredBy = retiredBy;
        }	
		
        /// <summary>
        /// Loads a ProductAppendix2 object from the database using the given Appendix2Id
        /// </summary>
        /// <param name="appendix2Id">The primary key value</param>
        /// <returns>A ProductAppendix2 object</returns>
        public static ProductAppendix2 Load(Guid appendix2Id)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@Appendix2Id", appendix2Id) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spProductAppendix2_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    ProductAppendix2 result = new ProductAppendix2();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a ProductAppendix2 object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A ProductAppendix2 object</returns>
        public static ProductAppendix2 LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spProductAppendix2_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    ProductAppendix2 result = new ProductAppendix2();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of ProductAppendix2 objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductAppendix2 objects in the database.</returns>
        public static ProductAppendix2Collection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spProductAppendix2_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductAppendix2 objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the ProductAppendix2 objects in the database ordered by the columns specified.</returns>
        public static ProductAppendix2Collection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spProductAppendix2_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductAppendix2 objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductAppendix2 objects in the database.</returns>
        public static ProductAppendix2Collection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spProductAppendix2_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductAppendix2 objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the ProductAppendix2 objects in the database ordered by the columns specified.</returns>
        public static ProductAppendix2Collection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spProductAppendix2_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductAppendix2 objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductAppendix2 objects in the database.</returns>
        public static ProductAppendix2Collection LoadCollection(string spName, SqlParameter[] parms)
        {
            ProductAppendix2Collection result = new ProductAppendix2Collection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    ProductAppendix2 tmp = new ProductAppendix2();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a ProductAppendix2 object from the database.
        /// </summary>
        /// <param name="appendix2Id">The primary key value</param>
        public static void Delete(Guid appendix2Id)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@Appendix2Id", appendix2Id) };
            SqlHelper.Default.ExecuteNonQuery("spProductAppendix2_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) appendix2Id = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) parentAppendix = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) appendix2Code = reader.GetString(2);
                if (!reader.IsDBNull(3)) appendix2Initial = reader.GetString(3);
                if (!reader.IsDBNull(4)) appendix2Name = reader.GetString(4);
                if (!reader.IsDBNull(5)) appendix2Name_Chs = reader.GetString(5);
                if (!reader.IsDBNull(6)) appendix2Name_Cht = reader.GetString(6);
                if (!reader.IsDBNull(7)) createdOn = reader.GetDateTime(7);
                if (!reader.IsDBNull(8)) createdBy = reader.GetGuid(8);
                if (!reader.IsDBNull(9)) modifiedOn = reader.GetDateTime(9);
                if (!reader.IsDBNull(10)) modifiedBy = reader.GetGuid(10);
                if (!reader.IsDBNull(11)) retired = reader.GetBoolean(11);
                if (!reader.IsDBNull(12)) retiredOn = reader.GetDateTime(12);
                if (!reader.IsDBNull(13)) retiredBy = reader.GetGuid(13);
            }
        }
		
        public void Delete()
        {
            Delete(this.Appendix2Id);
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
                if (key != Appendix2Id)
                    this.Delete();
                Update();
            }
        }

        public Guid Appendix2Id
        {
            get { return appendix2Id; }
            set { appendix2Id = value; }
        }

        public Guid ParentAppendix
        {
            get { return parentAppendix; }
            set { parentAppendix = value; }
        }

        public string Appendix2Code
        {
            get { return appendix2Code; }
            set { appendix2Code = value; }
        }

        public string Appendix2Initial
        {
            get { return appendix2Initial; }
            set { appendix2Initial = value; }
        }

        public string Appendix2Name
        {
            get { return appendix2Name; }
            set { appendix2Name = value; }
        }

        public string Appendix2Name_Chs
        {
            get { return appendix2Name_Chs; }
            set { appendix2Name_Chs = value; }
        }

        public string Appendix2Name_Cht
        {
            get { return appendix2Name_Cht; }
            set { appendix2Name_Cht = value; }
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
			
            SqlHelper.Default.ExecuteNonQuery("spProductAppendix2_InsRec", "@Appendix2Id", out returnedValue, parameterValues);
            
            appendix2Id = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spProductAppendix2_UpdRec", parameterValues);
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
            prams[0] = GetSqlParameter("@Appendix2Id", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.Appendix2Id);
            prams[1] = GetSqlParameter("@ParentAppendix", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ParentAppendix);
            prams[2] = GetSqlParameter("@Appendix2Code", ParameterDirection.Input, SqlDbType.VarChar, 4, this.Appendix2Code);
            prams[3] = GetSqlParameter("@Appendix2Initial", ParameterDirection.Input, SqlDbType.NVarChar, 10, this.Appendix2Initial);
            prams[4] = GetSqlParameter("@Appendix2Name", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.Appendix2Name);
            prams[5] = GetSqlParameter("@Appendix2Name_Chs", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.Appendix2Name_Chs);
            prams[6] = GetSqlParameter("@Appendix2Name_Cht", ParameterDirection.Input, SqlDbType.NVarChar, 64, this.Appendix2Name_Cht);
            prams[7] = GetSqlParameter("@CreatedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.CreatedOn);
            prams[8] = GetSqlParameter("@CreatedBy", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.CreatedBy);
            prams[9] = GetSqlParameter("@ModifiedOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.ModifiedOn);
            prams[10] = GetSqlParameter("@ModifiedBy", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ModifiedBy);
            prams[11] = GetSqlParameter("@Retired", ParameterDirection.Input, SqlDbType.Bit, 1, this.Retired);
            prams[12] = GetSqlParameter("@RetiredOn", ParameterDirection.Input, SqlDbType.DateTime, 8, this.RetiredOn);
            prams[13] = GetSqlParameter("@RetiredBy", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.RetiredBy);
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
                GetSqlParameterWithoutDirection("@Appendix2Id", SqlDbType.UniqueIdentifier, 16, this.Appendix2Id),
                GetSqlParameterWithoutDirection("@ParentAppendix", SqlDbType.UniqueIdentifier, 16, this.ParentAppendix),
                GetSqlParameterWithoutDirection("@Appendix2Code", SqlDbType.VarChar, 4, this.Appendix2Code),
                GetSqlParameterWithoutDirection("@Appendix2Initial", SqlDbType.NVarChar, 10, this.Appendix2Initial),
                GetSqlParameterWithoutDirection("@Appendix2Name", SqlDbType.NVarChar, 64, this.Appendix2Name),
                GetSqlParameterWithoutDirection("@Appendix2Name_Chs", SqlDbType.NVarChar, 64, this.Appendix2Name_Chs),
                GetSqlParameterWithoutDirection("@Appendix2Name_Cht", SqlDbType.NVarChar, 64, this.Appendix2Name_Cht),
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
            builder.Append("appendix2Id: " + appendix2Id.ToString()).Append("\r\n");
            builder.Append("parentAppendix: " + parentAppendix.ToString()).Append("\r\n");
            builder.Append("appendix2Code: " + appendix2Code.ToString()).Append("\r\n");
            builder.Append("appendix2Initial: " + appendix2Initial.ToString()).Append("\r\n");
            builder.Append("appendix2Name: " + appendix2Name.ToString()).Append("\r\n");
            builder.Append("appendix2Name_Chs: " + appendix2Name_Chs.ToString()).Append("\r\n");
            builder.Append("appendix2Name_Cht: " + appendix2Name_Cht.ToString()).Append("\r\n");
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
			
			ProductAppendix2Collection source;
            
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
				source = ProductAppendix2.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = ProductAppendix2.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (ProductAppendix2 item in source)
			{
				bool filter = false;
				if (ParentFilter.Trim() != String.Empty)
				{
					filter = true;
					if (item.ParentAppendix != Guid.Empty)
					{
						filter = IgnorThis(item, ParentFilter);
					}
				}
				if (!(filter))
				{
                    string code = GetFormatedText(item, TextField, TextFormatString);
                    sourceList.Add(new Common.ComboItem(code, item.Appendix2Id));
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
		
		
		private static bool IgnorThis(ProductAppendix2 target, string parentFilter)
		{
			bool result = true;
			parentFilter = parentFilter.Replace(" ", "");		// remove spaces
			parentFilter = parentFilter.Replace("'", "");		// remove '
			string [] parsed = parentFilter.Split('=');			// parse

			if (target.ParentAppendix == Guid.Empty)
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
				ProductAppendix2 parentTemplate = ProductAppendix2.Load(target.ParentAppendix);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(ProductAppendix2 target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="ProductAppendix2">ProductAppendix2</see> objects.
    /// </summary>
    public class ProductAppendix2Collection : BindingList< ProductAppendix2>
    {
	}
}
