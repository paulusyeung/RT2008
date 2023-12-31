#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using RT2008.Controls;
using RT2008.DAL;
using System.Data.SqlClient;
using Gizmox.WebGUI.Common.Resources;
using System.Configuration;

#endregion

namespace RT2008.Product
{
    public partial class DefaultAppendixList : DefaultListBase
    {
        public string TypeName { get; set; }

        public DefaultAppendixList(string typeName, Control toolBar)
        {
            InitializeComponent();

            ProductToolbar tb = new ProductToolbar(toolBar, ref tbControl);

            this.TypeName = typeName;

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
            BindList();

        }

        public override void BindList()
        {
            if (!string.IsNullOrEmpty(this.TypeName))
            {
                BindAppendixList();
            }
        }

        #region Bind Appendix List

        #region List View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colAppendixNameCht = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAppendixId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAppendixCode = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAppendixName = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAppendixNameChs = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAppendixInitial = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new ColumnHeader();

            // 
            // colLN
            // 
            colLN.ClientAction = null;
            colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            colLN.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colLN.Image = null;
            colLN.Text = Utility.Dictionary.GetWord("LN");
            colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            colLN.Width = 30;
            // 
            // colAppendixId
            // 
            colAppendixId.ClientAction = null;
            colAppendixId.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAppendixId.Image = null;
            colAppendixId.Text = "AppendixId";
            colAppendixId.Visible = false;
            colAppendixId.Width = 150;
            // 
            // colAppendixCode
            // 
            colAppendixCode.ClientAction = null;
            colAppendixCode.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Left;
            colAppendixCode.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAppendixCode.Image = null;
            colAppendixCode.Text = Utility.Dictionary.GetWord("Code");
            colAppendixCode.Width = 80;
            // 
            // colAppendixInitial
            // 
            colAppendixInitial.ClientAction = null;
            colAppendixInitial.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAppendixInitial.Image = null;
            colAppendixInitial.Text = Utility.Dictionary.GetWord("Initial");
            colAppendixInitial.Width = 80;
            // 
            // colAppendixName
            // 
            colAppendixName.ClientAction = null;
            colAppendixName.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAppendixName.Image = null;
            colAppendixName.Text = Utility.Dictionary.GetWord("description");
            colAppendixName.Width = 120;
            // 
            // colAppendixNameChs
            // 
            colAppendixNameChs.ClientAction = null;
            colAppendixNameChs.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAppendixNameChs.Image = null;
            colAppendixNameChs.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Chs") + ")";
            colAppendixNameChs.Width = 120;
            // 
            // colAppendixNameCht
            // 
            colAppendixNameCht.ClientAction = null;
            colAppendixNameCht.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAppendixNameCht.Image = null;
            colAppendixNameCht.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Cht") + ")";
            colAppendixNameCht.Width = 120;
            // 
            // colModifiedOn
            // 
            colModifiedOn.ClientAction = null;
            colModifiedOn.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colModifiedOn.Image = null;
            colModifiedOn.Text = Utility.Dictionary.GetWord("ModifiedOn");
            colModifiedOn.Width = 120;
            // 
            // colCreatedOn
            // 
            colCreatedOn.ClientAction = null;
            colCreatedOn.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colCreatedOn.Image = null;
            colCreatedOn.Text = Utility.Dictionary.GetWord("CreatedOn");
            colCreatedOn.Width = 120;

            lvList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            colAppendixCode,
            colLN,
            colAppendixId,
            colAppendixInitial,
            colAppendixName,
            colAppendixNameChs,
            colAppendixNameCht,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion


        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{91F0A08C-4726-4829-94F2-5FBAE2ECA392}");

            RT2008.Controls.Preference.Load(ref lvList);
        }

        private void BindAppendixList()
        {
            this.lvList.Items.Clear();

            int iCount = 1;
            string sql = BuildSqlQueryString();
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType= CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(2)); // Appendix Code
                    objItem.SmallImage = new IconResourceHandle("16x16.Product16.gif");
                    objItem.LargeImage = new IconResourceHandle("16x16.Product16.gif");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // AppendixId
                    objItem.SubItems.Add(reader.GetString(3)); // Appendix Initial
                    objItem.SubItems.Add(reader.GetString(4)); // Appendix Name
                    objItem.SubItems.Add(reader.GetString(5)); // Appendix Name Chs
                    objItem.SubItems.Add(reader.GetString(6)); // Appendix name Cht
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(7), false)); // CreatedOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(8), false)); // ModifiedOn

                    iCount++;
                }
            }

            this.lvList.Sort(); // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效
        }

        #region Build Sql Query String
        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(this.TypeName);
            sql.Append("Id,  ROW_NUMBER() OVER (ORDER BY ");
            sql.Append(this.TypeName);
            sql.Append("Code) AS rownum, ISNULL(");
            sql.Append(this.TypeName);
            sql.Append("Code, ''), ISNULL(");
            sql.Append(this.TypeName);
            sql.Append("Initial, ''), ISNULL(");
            sql.Append(this.TypeName);
            sql.Append("Name, ''), ISNULL(");
            sql.Append(this.TypeName);
            sql.Append("Name_Chs, ''), ISNULL(");
            sql.Append(this.TypeName);
            sql.Append("Name_Cht, ''), ");
            sql.Append(" CreatedOn, ModifiedOn, CreatedBy, ModifiedBy ");
            sql.Append(" FROM Product");
            sql.Append(this.TypeName);
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
                sql.Append(" AND (");
                sql.Append(this.TypeName);
                sql.Append("Code LIKE '%").Append(SearchForText).Append("%'");
                sql.Append(" OR ");
                sql.Append(this.TypeName);
                sql.Append("Name LIKE '%").Append(SearchForText).Append("%')");
            }

            if (SelectedStaff != System.Guid.Empty)
            {
                sql.Append(" AND CreatedBy = '").Append(SelectedStaff.ToString()).Append("'");
            }

            if (!(String.IsNullOrEmpty(AlphaSeacher)) && AlphaSeacher != "All")
            {
                sql.Append(String.Format(" AND ( SUBSTRING([" + this.TypeName + "Code], 1, 1) = N'{0}' )", AlphaSeacher));
            }

            sql.Append(" AND Retired = 0 ");
            sql.Append(" ORDER BY " + this.TypeName + "Code ");

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
                    ProductAppendixWizard wizProd = new ProductAppendixWizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizProd.Closed += new EventHandler(wizProd_Closed);
                    wizProd.ShowDialog();
                }
            }
        }

        void wizProd_Closed(object sender, EventArgs e)
        {
            ProductAppendixWizard wizProd = sender as ProductAppendixWizard;
            if (wizProd.AppendixId != System.Guid.Empty)
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