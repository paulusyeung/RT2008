#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using RT2008.DAL;
using System.Data.SqlClient;
using Gizmox.WebGUI.Common.Resources;
using RT2008.Controls;

#endregion

namespace RT2008.Product
{
    public partial class DefaultList : Controls.DefaultListBase
    {
        public DefaultList(Control toolBar)
        {
            InitializeComponent();

            ProductToolbar tb = new ProductToolbar(toolBar, ref tbControl);

            base.ExportClick += new MenuEventHandler(DefaultList_ExportClick);
            base.RefreshClick += new EventHandler(DefaultList_RefreshClick);
            base.PreferenceClick += new EventHandler(DefaultList_PreferenceClick);
            base.BindingListDoubleClick += new EventHandler(DefaultList_BindingListDoubleClick);
            base.ComboBoxSelectedIndexChanged += new EventHandler(DefaultList_ComboBoxSelectedIndexChanged);
            base.ButtonClick += new EventHandler(DefaultList_ButtonClick);
            base.ShowClick += new EventHandler(DefaultList_ShowClick);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetColumns();
            SetLvwList();
        }

        public override void BindList()
        {
            BindProductList();
        }

        #region Bind Product List

        #region List View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colProductId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colProductCode = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colProductName = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colProductNameChs = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colProductNameCht = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colNature = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colUnit = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colRemarks = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAPPENDIX1 = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAPPENDIX2 = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAPPENDIX3 = new ColumnHeader();

            // 
            // colProductId
            // 
            colProductId.Image = null;
            colProductId.Text = "ProductId";
            colProductId.Visible = false;
            colProductId.Width = 150;
            // 
            // colLN
            // 
            colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            colLN.Image = null;
            colLN.Text = Utility.Dictionary.GetWord("LN");
            colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            colLN.Width = 30;
            // 
            // colProductCode
            // 
            colProductCode.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Left;
            colProductCode.Image = null;
            colProductCode.Text = Utility.Dictionary.GetWord("Code");
            colProductCode.Width = 150;
            // 
            // colAPPENDIX1
            // 
            colAPPENDIX1.Image = null;
            colAPPENDIX1.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1");
            colAPPENDIX1.Width = 60;
            colAPPENDIX1.Visible = false;
            // 
            // colAPPENDIX2
            // 
            colAPPENDIX2.Image = null;
            colAPPENDIX2.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2");
            colAPPENDIX2.Width = 60;
            colAPPENDIX2.Visible = false;
            // 
            // colAPPENDIX3
            // 
            colAPPENDIX3.Image = null;
            colAPPENDIX3.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3");
            colAPPENDIX3.Width = 60;
            colAPPENDIX3.Visible = false;
            // 
            // colProductName
            // 
            colProductName.Image = null;
            colProductName.Text = Utility.Dictionary.GetWord("description");
            colProductName.Width = 120;
            // 
            // colProductNameChs
            // 
            colProductNameChs.Image = null;
            colProductNameChs.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Chs") + ")";
            colProductNameChs.Width = 120;
            // 
            // colProductNameCht
            // 
            colProductNameCht.Image = null;
            colProductNameCht.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Cht") + ")";
            colProductNameCht.Width = 120;
            // 
            // colNature
            // 
            colNature.Image = null;
            colNature.Text = Utility.Dictionary.GetWord("Nature");
            colNature.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            colNature.Width = 50;
            // 
            // colUnit
            // 
            colUnit.Image = null;
            colUnit.Text = Utility.Dictionary.GetWord("Unit");
            colUnit.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            colUnit.Width = 40;
            // 
            // colRemarks
            // 
            colRemarks.Image = null;
            colRemarks.Text = Utility.Dictionary.GetWord("Remarks");
            colRemarks.Width = 150;
            // 
            // colCreatedOn
            // 
            colCreatedOn.Image = null;
            colCreatedOn.Text = Utility.Dictionary.GetWord("CreatedOn");
            colCreatedOn.Width = 120;
            // 
            // colModifiedOn
            // 
            colModifiedOn.Image = null;
            colModifiedOn.Text = Utility.Dictionary.GetWord("ModifiedOn");
            colModifiedOn.Width = 120;

            lvList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            colProductCode,
            colLN,
            colProductId,
            colAPPENDIX1,
            colAPPENDIX2,
            colAPPENDIX3,
            colProductName,
            colProductNameChs,
            colProductNameCht,
            colNature,
            colUnit,
            colRemarks,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{6F46245A-89AE-4074-BD95-D601FE4DEFB8}");

            RT2008.Controls.Preference.Load(ref lvList);
        }


        private void BindProductList()
        {
            this.lvList.Items.Clear();

            int iCount = 1;
            string sql = BuildSqlQueryString();
            
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(1));  // ProductCode
                    objItem.SmallImage = new IconResourceHandle("16x16.Product16.gif");
                    objItem.LargeImage = new IconResourceHandle("16x16.Product16.gif");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // ProductId
                    objItem.SubItems.Add(reader.GetString(3)); // A1
                    objItem.SubItems.Add(reader.GetString(4)); // A2
                    objItem.SubItems.Add(reader.GetString(5)); // A3
                    objItem.SubItems.Add(reader.GetString(6)); // Product Name
                    objItem.SubItems.Add(reader.GetString(7)); // Product Name Chs
                    objItem.SubItems.Add(reader.GetString(8)); // Product name Cht
                    objItem.SubItems.Add(reader.GetString(9)); // Nature
                    objItem.SubItems.Add(reader.GetString(10)); // UoM
                    objItem.SubItems.Add(reader.GetString(11)); // Remarks
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(12), true)); // CreatedOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(13), true)); // ModifiedOn

                    iCount++;
                }
            }
            this.lvList.Sort(); // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效
        }

        #region Build Sql Query String
        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT Top 1000 ProductId, (STKCODE + ' ' + APPENDIX1 + ' ' + APPENDIX2 + ' ' + APPENDIX3) AS ProductCode, ");
            sql.Append(" STKCODE, APPENDIX1, APPENDIX2, APPENDIX3, ProductName, ProductName_Chs, ");
            sql.Append(" ProductName_Cht, Nature, UOM, ");
            sql.Append(" Remarks, CreatedOn, ModifiedOn, CreatedBy, ModifiedBy ");
            sql.Append(" FROM vwProductList ");
            sql.Append(" WHERE ");

            switch (SelectedViewIndex)
            {
                case 0: // Last 7 days
                    sql.Append(" CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME)");
                    break;
                case 1: // Last 14 days
                    sql.Append(" CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-14).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME)");
                    break;
                case 2: // Last 30 days
                    sql.Append(" CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME)");
                    break;
                case 3: // Last 60 days
                    sql.Append(" CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-60).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME)");
                    break;
                case 4: // Last 90 days
                    sql.Append(" CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-90).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME)");
                    break;
                case 5: // All
                default:
                    sql.Append(" 1 = 1 ");
                    break;
            }

            if (!string.IsNullOrEmpty(SearchForText))
            {
                sql.Append(" AND ((STKCODE + APPENDIX1 + APPENDIX2 + APPENDIX3) LIKE '%").Append(SearchForText).Append("%'");
                sql.Append(" OR ProductName LIKE '%").Append(SearchForText).Append("%')");
            }

            if (SelectedStaff != System.Guid.Empty)
            {
                sql.Append(" AND CreatedBy = '").Append(SelectedStaff.ToString()).Append("'");
            }

            if (!(String.IsNullOrEmpty(AlphaSeacher)) && AlphaSeacher != "All")
            {
                sql.Append(String.Format(" AND ( SUBSTRING([STKCODE], 1, 1) = N'{0}' )", AlphaSeacher));
            }

            sql.Append(" ORDER BY STKCODE, APPENDIX1, APPENDIX2, APPENDIX3 ");

            return sql.ToString();
        }
        #endregion

        #endregion

        void DefaultList_ButtonClick(object sender, EventArgs e)
        {
            BindList();
        }

        void DefaultList_ComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            BindList();
        }

        void DefaultList_BindingListDoubleClick(object sender, EventArgs e)
        {
            ShowItem();
        }

        private void ShowItem()
        {
            if (lvList.SelectedItem != null)
            {
                if (Common.Utility.IsGUID(lvList.SelectedItem.SubItems[2].Text))
                {
                    ProductWizard wizProd = new ProductWizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizProd.Closed += new EventHandler(wizProd_Closed);
                    wizProd.ShowDialog();
                }
            }
        }

        void wizProd_Closed(object sender, EventArgs e)
        {
            ProductWizard wizProd = sender as ProductWizard;
            if (wizProd.ProductId != System.Guid.Empty)
            {
                BindList();
                this.Update();
            }
        }

        void DefaultList_PreferenceClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void DefaultList_RefreshClick(object sender, EventArgs e)
        {
            BindList();
            base.lvList.Update();
        }

        void DefaultList_ExportClick(object objSource, MenuItemEventArgs objArgs)
        {
            throw new NotImplementedException();
        }

        void DefaultList_ShowClick(object sender, EventArgs e)
        {
            ShowItem();
        }
    }
}