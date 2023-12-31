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
    public partial class DefaultClassList : DefaultListBase
    {
        public string TypeName { get; set; }

        public DefaultClassList(string typeName, Control toolBar)
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
                BindClassList();
            }
        }

        #region Bind Class List

        #region List View Columns
        void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colClassNameCht = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colClassId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colClassCode = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colClassName = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colClassNameChs = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colClassInitial = new ColumnHeader();
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
            // colClassId
            // 
            colClassId.ClientAction = null;
            colClassId.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colClassId.Image = null;
            colClassId.Text = "ClassId";
            colClassId.Visible = false;
            colClassId.Width = 150;
            // 
            // colClassCode
            // 
            colClassCode.ClientAction = null;
            colClassCode.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Left;
            colClassCode.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colClassCode.Image = null;
            colClassCode.Text = Utility.Dictionary.GetWord("Code");
            colClassCode.Width = 80;
            // 
            // colClassInitial
            // 
            colClassInitial.ClientAction = null;
            colClassInitial.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colClassInitial.Image = null;
            colClassInitial.Text = Utility.Dictionary.GetWord("description_short");         // 2013.06.19 paulus: 本來係用 GetWord("Initial")
            colClassInitial.Width = 120;                                                    //   因為個英文 header 會長咗，加闊啲
            // 
            // colClassName
            // 
            colClassName.ClientAction = null;
            colClassName.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colClassName.Image = null;
            colClassName.Text = Utility.Dictionary.GetWord("description");
            colClassName.Width = 120;
            // 
            // colClassNameChs
            // 
            colClassNameChs.ClientAction = null;
            colClassNameChs.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colClassNameChs.Image = null;
            colClassNameChs.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Chs") + ")";
            colClassNameChs.Width = 120;
            // 
            // colClassNameCht
            // 
            colClassNameCht.ClientAction = null;
            colClassNameCht.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colClassNameCht.Image = null;
            colClassNameCht.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Cht") + ")";
            colClassNameCht.Width = 120;
            // 
            // colCreatedOn
            // 
            colCreatedOn.ClientAction = null;
            colCreatedOn.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colCreatedOn.Image = null;
            colCreatedOn.Text = Utility.Dictionary.GetWord("CreatedOn");
            colCreatedOn.Width = 120;
            // 
            // colModifiedOn
            // 
            colModifiedOn.ClientAction = null;
            colModifiedOn.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colModifiedOn.Image = null;
            colModifiedOn.Text = Utility.Dictionary.GetWord("ModifiedOn");
            colModifiedOn.Width = 120;

            lvList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            colClassCode,
            colLN,
            colClassId,
            colClassInitial,
            colClassName,
            colClassNameChs,
            colClassNameCht,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{E2322A52-36E3-4eb4-8A5D-391C44D17C98}");

            RT2008.Controls.Preference.Load(ref lvList);
        }

        private void BindClassList()
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
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(2)); // Class Code
                    objItem.SmallImage = new IconResourceHandle("16x16.Product16.gif");
                    objItem.LargeImage = new IconResourceHandle("16x16.Product16.gif");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // ClassId
                    objItem.SubItems.Add(reader.GetString(3)); // Class Initial
                    objItem.SubItems.Add(reader.GetString(4)); // Class Name
                    objItem.SubItems.Add(reader.GetString(5)); // Class Name Chs
                    objItem.SubItems.Add(reader.GetString(6)); // Class name Cht
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(7), true)); // CreatedOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(8), true)); // ModifiedOn

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
                    ProductClassWizard wizProd = new ProductClassWizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizProd.Closed += new EventHandler(wizProd_Closed);
                    wizProd.ShowDialog();
                }
            }
        }

        void wizProd_Closed(object sender, EventArgs e)
        {
            ProductClassWizard wizProd = sender as ProductClassWizard;
            if (wizProd.ClassId != System.Guid.Empty)
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