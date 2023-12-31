#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using System.Data.SqlClient;
using RT2008.DAL;
using RT2008.Controls;
using Gizmox.WebGUI.Common.Resources;
using System.Configuration;

#endregion

namespace RT2008.Supplier
{
    public partial class DefaultList : Controls.DefaultListBase
    {
        public DefaultList()
        {
            InitializeComponent();

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

            tbControl.Visible = false;
        }

        public override void BindList()
        {
            BindSupplierList();
        }

        #region Bind Supplier List

        #region List View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colSupplierId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colSupplierNumber = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colSupplierName = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colSupplierNameChs = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colSupplierNameCht = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colRemarks = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colSupplierInitial = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new ColumnHeader();

            // 
            // colSupplierId
            // 
            colSupplierId.ClientAction = null;
            colSupplierId.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colSupplierId.Image = null;
            colSupplierId.Text = "SupplierId";
            colSupplierId.Visible = false;
            colSupplierId.Width = 150;
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
            // colSupplierNumber
            // 
            colSupplierNumber.ClientAction = null;
            colSupplierNumber.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Left;
            colSupplierNumber.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colSupplierNumber.Image = null;
            colSupplierNumber.Text = Utility.Dictionary.GetWord("Number");
            colSupplierNumber.Width = 80;
            // 
            // colSupplierInitial
            // 
            colSupplierInitial.ClientAction = null;
            colSupplierInitial.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colSupplierInitial.Image = null;
            colSupplierInitial.Text = Utility.Dictionary.GetWord("Initial");
            colSupplierInitial.Width = 120;
            // 
            // colSupplierName
            // 
            colSupplierName.ClientAction = null;
            colSupplierName.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colSupplierName.Image = null;
            colSupplierName.Text = Utility.Dictionary.GetWord("Name");
            colSupplierName.Width = 120;
            // 
            // colSupplierNameChs
            // 
            colSupplierNameChs.ClientAction = null;
            colSupplierNameChs.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colSupplierNameChs.Image = null;
            colSupplierNameChs.Text = Utility.Dictionary.GetWord("Name") + " (" + Utility.Dictionary.GetWord("Chs") + ")";
            colSupplierNameChs.Width = 120;
            // 
            // colSupplierNameCht
            // 
            colSupplierNameCht.ClientAction = null;
            colSupplierNameCht.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colSupplierNameCht.Image = null;
            colSupplierNameCht.Text = Utility.Dictionary.GetWord("Name") + " (" + Utility.Dictionary.GetWord("Cht") + ")";
            colSupplierNameCht.Width = 120;
            // 
            // colRemarks
            // 
            colRemarks.ClientAction = null;
            colRemarks.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colRemarks.Image = null;
            colRemarks.Text = Utility.Dictionary.GetWord("Remarks");
            colRemarks.Width = 120;
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
            colSupplierNumber,
            colLN,
            colSupplierId,
            colSupplierInitial,
            colSupplierName,
            colSupplierNameChs,
            colSupplierNameCht,
            colRemarks,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{BE7AC5B3-B96A-46e8-9124-C3C0CC61AF36}");

            RT2008.Controls.Preference.Load(ref lvList);
        }


        private void BindSupplierList()
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
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(2)); // SupplierId
                    objItem.SmallImage = new IconResourceHandle("16x16.supplierSingle_16.gif");
                    objItem.LargeImage = new IconResourceHandle("16x16.supplierSingle_16.gif");
                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // Supplier Code
                    objItem.SubItems.Add(reader.GetString(3)); // Supplier Initial
                    objItem.SubItems.Add(reader.GetString(4)); // Supplier Name
                    objItem.SubItems.Add(reader.GetString(5)); // Supplier Name Chs
                    objItem.SubItems.Add(reader.GetString(6)); // Supplier name Cht
                    objItem.SubItems.Add(reader.GetString(7)); // Remarks
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(8), true)); // CreatedOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(9), true)); // ModifiedOn

                    iCount++;
                }
            }
            this.lvList.Sort(); // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效

        }

        #region Build Sql Query String
        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT SupplierId,  ROW_NUMBER() OVER (ORDER BY SupplierCode) AS rownum, ");
            sql.Append(" SupplierCode, SupplierInitial, SupplierName, SupplierName_Chs, ");
            sql.Append(" SupplierName_Cht, Remarks, ");
            sql.Append(" CreatedOn, ModifiedOn, CreatedBy, ModifiedBy ");
            sql.Append(" FROM Supplier ");
            sql.Append(" WHERE Status >= 0 AND ");

            switch (base.SelectedViewIndex)
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

            if (!string.IsNullOrEmpty(base.SearchForText))
            {
                sql.Append(" AND (SupplierCode LIKE '%").Append(base.SearchForText).Append("%' ");
                sql.Append(" OR SupplierInitial LIKE '%").Append(base.SearchForText).Append("%' ");
                sql.Append(" OR SupplierName LIKE '%").Append(base.SearchForText).Append("%')");
            }

            if (base.SelectedStaff != System.Guid.Empty)
            {
                sql.Append(" AND CreatedBy = '").Append(base.SelectedStaff.ToString()).Append("'");
            }

            if (!(String.IsNullOrEmpty(AlphaSeacher)) && AlphaSeacher != "All")
            {
                sql.Append(String.Format(" AND ( SUBSTRING([SupplierCode], 1, 1) = N'{0}' )", AlphaSeacher));
            }

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
                    SupplierWizard wizSupplier = new SupplierWizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizSupplier.Closed += new EventHandler(wizSupplier_Closed);
                    wizSupplier.ShowDialog();
                }
            }
        }

        void wizSupplier_Closed(object sender, EventArgs e)
        {
            SupplierWizard wizSupplier = sender as SupplierWizard;
            if (wizSupplier.SupplierId != System.Guid.Empty)
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