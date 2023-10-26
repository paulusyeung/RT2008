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
    /// This Data Access Layer Component (DALC) provides access to the data contained in the data table dbo.ProductCode.
    /// Date Created:   2020-08-09 02:14:14
    /// Created By:     Generated by CodeSmith version 7.0.0.15123
    /// Template:       BusinessObjects_v5.0.cst
    /// </summary>
    public class ProductCode
    {
        private Guid key = Guid.Empty;
        private Guid codeId = Guid.Empty;
        private Guid productId = Guid.Empty;
        private Guid appendix1Id = Guid.Empty;
        private Guid appendix2Id = Guid.Empty;
        private Guid appendix3Id = Guid.Empty;
        private Guid class1Id = Guid.Empty;
        private Guid class2Id = Guid.Empty;
        private Guid class3Id = Guid.Empty;
        private Guid class4Id = Guid.Empty;
        private Guid class5Id = Guid.Empty;
        private Guid class6Id = Guid.Empty;

        /// <summary>
        /// Initialize an new empty ProductCode object.
        /// </summary>
        public ProductCode()
        {
        }
		
        /// <summary>
        /// Initialize a new ProductCode object with the given parameters.
        /// </summary>
        public ProductCode(Guid codeId, Guid productId, Guid appendix1Id, Guid appendix2Id, Guid appendix3Id, Guid class1Id, Guid class2Id, Guid class3Id, Guid class4Id, Guid class5Id, Guid class6Id)
        {
                this.codeId = codeId;
                this.productId = productId;
                this.appendix1Id = appendix1Id;
                this.appendix2Id = appendix2Id;
                this.appendix3Id = appendix3Id;
                this.class1Id = class1Id;
                this.class2Id = class2Id;
                this.class3Id = class3Id;
                this.class4Id = class4Id;
                this.class5Id = class5Id;
                this.class6Id = class6Id;
        }	
		
        /// <summary>
        /// Loads a ProductCode object from the database using the given CodeId
        /// </summary>
        /// <param name="codeId">The primary key value</param>
        /// <returns>A ProductCode object</returns>
        public static ProductCode Load(Guid codeId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@CodeId", codeId) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spProductCode_SelRec", parameterValues))
            {
                if (reader.Read())
                {
                    ProductCode result = new ProductCode();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Loads a ProductCode object from the database using the given where clause
        /// </summary>
        /// <param name="whereClause">The filter expression for the query</param>
        /// <returns>A ProductCode object</returns>
        public static ProductCode LoadWhere(string whereClause)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader("spProductCode_SelAll", parameterValues))
            {
                if (reader.Read())
                {
                    ProductCode result = new ProductCode();
                    result.LoadFromReader(reader);
                    return result;
                }
                else
                    return null;
            }
        }
		
        /// <summary>
        /// Loads a collection of ProductCode objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductCode objects in the database.</returns>
        public static ProductCodeCollection LoadCollection()
        {
            SqlParameter[] parms = new SqlParameter[] {};
            return LoadCollection("spProductCode_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductCode objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the ProductCode objects in the database ordered by the columns specified.</returns>
        public static ProductCodeCollection LoadCollection(string[] orderByColumns, bool ascending)
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
            return LoadCollection("spProductCode_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductCode objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductCode objects in the database.</returns>
        public static ProductCodeCollection LoadCollection(string whereClause)
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@WhereClause", whereClause) };
            return LoadCollection("spProductCode_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductCode objects from the database ordered by the columns specified.
        /// </summary>
        /// <returns>A collection containing all of the ProductCode objects in the database ordered by the columns specified.</returns>
        public static ProductCodeCollection LoadCollection(string whereClause, string[] orderByColumns, bool ascending)
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
            return LoadCollection("spProductCode_SelAll", parms);
        }

        /// <summary>
        /// Loads a collection of ProductCode objects from the database.
        /// </summary>
        /// <returns>A collection containing all of the ProductCode objects in the database.</returns>
        public static ProductCodeCollection LoadCollection(string spName, SqlParameter[] parms)
        {
            ProductCodeCollection result = new ProductCodeCollection();
            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(spName, parms))
            {
                while (reader.Read())
                {
                    ProductCode tmp = new ProductCode();
                    tmp.LoadFromReader(reader);
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// Deletes a ProductCode object from the database.
        /// </summary>
        /// <param name="codeId">The primary key value</param>
        public static void Delete(Guid codeId)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@CodeId", codeId) };
            SqlHelper.Default.ExecuteNonQuery("spProductCode_DelRec", parameterValues);
        }

		
        public void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                key = reader.GetGuid(0);
                if (!reader.IsDBNull(0)) codeId = reader.GetGuid(0);
                if (!reader.IsDBNull(1)) productId = reader.GetGuid(1);
                if (!reader.IsDBNull(2)) appendix1Id = reader.GetGuid(2);
                if (!reader.IsDBNull(3)) appendix2Id = reader.GetGuid(3);
                if (!reader.IsDBNull(4)) appendix3Id = reader.GetGuid(4);
                if (!reader.IsDBNull(5)) class1Id = reader.GetGuid(5);
                if (!reader.IsDBNull(6)) class2Id = reader.GetGuid(6);
                if (!reader.IsDBNull(7)) class3Id = reader.GetGuid(7);
                if (!reader.IsDBNull(8)) class4Id = reader.GetGuid(8);
                if (!reader.IsDBNull(9)) class5Id = reader.GetGuid(9);
                if (!reader.IsDBNull(10)) class6Id = reader.GetGuid(10);
            }
        }
		
        public void Delete()
        {
            Delete(this.CodeId);
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
                if (key != CodeId)
                    this.Delete();
                Update();
            }
        }

        public Guid CodeId
        {
            get { return codeId; }
            set { codeId = value; }
        }

        public Guid ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public Guid Appendix1Id
        {
            get { return appendix1Id; }
            set { appendix1Id = value; }
        }

        public Guid Appendix2Id
        {
            get { return appendix2Id; }
            set { appendix2Id = value; }
        }

        public Guid Appendix3Id
        {
            get { return appendix3Id; }
            set { appendix3Id = value; }
        }

        public Guid Class1Id
        {
            get { return class1Id; }
            set { class1Id = value; }
        }

        public Guid Class2Id
        {
            get { return class2Id; }
            set { class2Id = value; }
        }

        public Guid Class3Id
        {
            get { return class3Id; }
            set { class3Id = value; }
        }

        public Guid Class4Id
        {
            get { return class4Id; }
            set { class4Id = value; }
        }

        public Guid Class5Id
        {
            get { return class5Id; }
            set { class5Id = value; }
        }

        public Guid Class6Id
        {
            get { return class6Id; }
            set { class6Id = value; }
        }


        private void Insert()
        {
            SqlParameter[] parameterValues = GetInsertParameterValues();
            object returnedValue = null;
			
            SqlHelper.Default.ExecuteNonQuery("spProductCode_InsRec", "@CodeId", out returnedValue, parameterValues);
            
            codeId = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
            key = returnedValue != null ? (Guid)returnedValue : Guid.Empty;
        }

        private void Update()
        {
            SqlParameter[] parameterValues = GetUpdateParameterValues();
            
			SqlHelper.Default.ExecuteNonQuery("spProductCode_UpdRec", parameterValues);
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
            SqlParameter[] prams = new SqlParameter[11];
            prams[0] = GetSqlParameter("@CodeId", ParameterDirection.Output, SqlDbType.UniqueIdentifier, 16, this.CodeId);
            prams[1] = GetSqlParameter("@ProductId", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.ProductId);
            prams[2] = GetSqlParameter("@Appendix1Id", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.Appendix1Id);
            prams[3] = GetSqlParameter("@Appendix2Id", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.Appendix2Id);
            prams[4] = GetSqlParameter("@Appendix3Id", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.Appendix3Id);
            prams[5] = GetSqlParameter("@Class1Id", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.Class1Id);
            prams[6] = GetSqlParameter("@Class2Id", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.Class2Id);
            prams[7] = GetSqlParameter("@Class3Id", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.Class3Id);
            prams[8] = GetSqlParameter("@Class4Id", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.Class4Id);
            prams[9] = GetSqlParameter("@Class5Id", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.Class5Id);
            prams[10] = GetSqlParameter("@Class6Id", ParameterDirection.Input, SqlDbType.UniqueIdentifier, 16, this.Class6Id);
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
                GetSqlParameterWithoutDirection("@CodeId", SqlDbType.UniqueIdentifier, 16, this.CodeId),
                GetSqlParameterWithoutDirection("@ProductId", SqlDbType.UniqueIdentifier, 16, this.ProductId),
                GetSqlParameterWithoutDirection("@Appendix1Id", SqlDbType.UniqueIdentifier, 16, this.Appendix1Id),
                GetSqlParameterWithoutDirection("@Appendix2Id", SqlDbType.UniqueIdentifier, 16, this.Appendix2Id),
                GetSqlParameterWithoutDirection("@Appendix3Id", SqlDbType.UniqueIdentifier, 16, this.Appendix3Id),
                GetSqlParameterWithoutDirection("@Class1Id", SqlDbType.UniqueIdentifier, 16, this.Class1Id),
                GetSqlParameterWithoutDirection("@Class2Id", SqlDbType.UniqueIdentifier, 16, this.Class2Id),
                GetSqlParameterWithoutDirection("@Class3Id", SqlDbType.UniqueIdentifier, 16, this.Class3Id),
                GetSqlParameterWithoutDirection("@Class4Id", SqlDbType.UniqueIdentifier, 16, this.Class4Id),
                GetSqlParameterWithoutDirection("@Class5Id", SqlDbType.UniqueIdentifier, 16, this.Class5Id),
                GetSqlParameterWithoutDirection("@Class6Id", SqlDbType.UniqueIdentifier, 16, this.Class6Id)
            };
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("codeId: " + codeId.ToString()).Append("\r\n");
            builder.Append("productId: " + productId.ToString()).Append("\r\n");
            builder.Append("appendix1Id: " + appendix1Id.ToString()).Append("\r\n");
            builder.Append("appendix2Id: " + appendix2Id.ToString()).Append("\r\n");
            builder.Append("appendix3Id: " + appendix3Id.ToString()).Append("\r\n");
            builder.Append("class1Id: " + class1Id.ToString()).Append("\r\n");
            builder.Append("class2Id: " + class2Id.ToString()).Append("\r\n");
            builder.Append("class3Id: " + class3Id.ToString()).Append("\r\n");
            builder.Append("class4Id: " + class4Id.ToString()).Append("\r\n");
            builder.Append("class5Id: " + class5Id.ToString()).Append("\r\n");
            builder.Append("class6Id: " + class6Id.ToString()).Append("\r\n");
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
			
			ProductCodeCollection source;
            
            if(OrderBy == null || OrderBy.Length == 0)
            {
			    OrderBy = TextField;
            }
			
			if (WhereClause.Length > 0)
			{
				source = ProductCode.LoadCollection(WhereClause, OrderBy, true);
			}
			else
			{
				source = ProductCode.LoadCollection(OrderBy, true);
			}
			
            Common.ComboList sourceList = new Common.ComboList();

			if (BlankLine)
			{
                sourceList.Add(new Common.ComboItem(BlankLineText, Guid.Empty));
			}
			
			foreach (ProductCode item in source)
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
                    sourceList.Add(new Common.ComboItem(code, item.CodeId));
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
		
		
		private static bool IgnorThis(ProductCode target, string parentFilter)
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
				ProductCode parentTemplate = ProductCode.Load(target.ProductId);
				result = IgnorThis(parentTemplate, parentFilter);
			}
			return result;
		}

		private static string GetFormatedText(ProductCode target, string [] textField, string textFormatString)
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
    /// Represents a collection of <see cref="ProductCode">ProductCode</see> objects.
    /// </summary>
    public class ProductCodeCollection : BindingList< ProductCode>
    {
	}
}