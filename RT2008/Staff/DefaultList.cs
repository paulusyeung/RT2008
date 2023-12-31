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
using Gizmox.WebGUI.Common.Resources;
using System.Data.SqlClient;
using System.Configuration;

#endregion

namespace RT2008.Staff
{
    public partial class DefaultList : RT2008.Controls.DefaultListBase
    {
        // 2007.12.22 paulus: PageId used in StaffPreference
        private Guid PageId = new Guid("3553565B-9484-4310-9763-1E0F130101CB");

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
            BindStaffList();
        }

        #region Bind Staff List

        #region List View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader columnStaffId = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader columnLN = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader columnStaffNumber = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader columnStaffCode = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader columnFullName = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader columnFullName_Chs = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader columnFullName_Cht = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader columnCreatedOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader columnModifiedOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            // 
            // columnStaffId
            // 
            columnStaffId.ClientAction = null;
            columnStaffId.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            columnStaffId.Image = null;
            columnStaffId.Tag = "StaffId";
            columnStaffId.Text = "StaffId";
            columnStaffId.Visible = false;
            columnStaffId.Width = 150;
            // 
            // columnLN
            // 
            columnLN.ClientAction = null;
            columnLN.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            columnLN.Image = null;
            columnLN.Tag = "LN";
            columnLN.Text = RT2008.Controls.Utility.Dictionary.GetWord("LN");
            columnLN.Width = 35;
            // 
            // columnStaffNumber
            // 
            columnStaffNumber.ClientAction = null;
            columnStaffNumber.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            columnStaffNumber.Image = null;
            columnStaffNumber.Tag = "StaffNumber";
            columnStaffNumber.Text = RT2008.Controls.Utility.Dictionary.GetWord("Number");
            columnStaffNumber.Width = 80;
            // 
            // columnStaffCode
            // 
            columnStaffCode.ClientAction = null;
            columnStaffCode.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            columnStaffCode.Image = null;
            columnStaffCode.Tag = "StaffInitial";
            columnStaffCode.Text = RT2008.Controls.Utility.Dictionary.GetWord("Initial");
            columnStaffCode.Width = 150;
            // 
            // columnFullName
            // 
            columnFullName.ClientAction = null;
            columnFullName.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            columnFullName.Image = null;
            columnFullName.Tag = "FullName";
            columnFullName.Text = RT2008.Controls.Utility.Dictionary.GetWord("Name");
            columnFullName.Width = 150;
            // 
            // columnFullName_Chs
            // 
            columnFullName_Chs.ClientAction = null;
            columnFullName_Chs.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            columnFullName_Chs.Image = null;
            columnFullName_Chs.Tag = "FullName_Chs";
            columnFullName_Chs.Text = RT2008.Controls.Utility.Dictionary.GetWord("Name") + " (" + RT2008.Controls.Utility.Dictionary.GetWord("Chs") + ")";
            columnFullName_Chs.Width = 150;
            // 
            // columnFullName_Cht
            // 
            columnFullName_Cht.ClientAction = null;
            columnFullName_Cht.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            columnFullName_Cht.Image = null;
            columnFullName_Cht.Tag = "FullName_Cht";
            columnFullName_Cht.Text = RT2008.Controls.Utility.Dictionary.GetWord("Name") + " (" + RT2008.Controls.Utility.Dictionary.GetWord("Cht") + ")";
            columnFullName_Cht.Width = 150;
            // 
            // columnCreatedOn
            // 
            columnCreatedOn.ClientAction = null;
            columnCreatedOn.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            columnCreatedOn.Image = null;
            columnCreatedOn.Tag = "CreatedOn";
            columnCreatedOn.Text = RT2008.Controls.Utility.Dictionary.GetWord("CreatedOn");
            columnCreatedOn.Width = 150;
            // 
            // columnModifiedOn
            // 
            columnModifiedOn.ClientAction = null;
            columnModifiedOn.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            columnModifiedOn.Image = null;
            columnModifiedOn.Tag = "ModifiedOn";
            columnModifiedOn.Text = RT2008.Controls.Utility.Dictionary.GetWord("ModifiedOn");
            columnModifiedOn.Width = 150;

            lvList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            columnStaffNumber,
            columnLN,
            columnStaffId,
            columnStaffCode,
            columnFullName,
            columnFullName_Chs,
            columnFullName_Cht,
            columnCreatedOn,
            columnModifiedOn});
        }
        #endregion

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{6F24B254-ACFF-485f-9EE2-58C5F75DB672}");

            RT2008.Controls.Preference.Load(ref lvList);
        }


        private void BindStaffList()
        {
            this.lvList.Items.Clear();

            int iCount = 1;
            string sql = BuildSqlQueryString();
            sql += " ORDER BY StaffNumber ";
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType= CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(1));
                    objItem.SmallImage = new IconResourceHandle("16x16.staffSingle_16.png");
                    objItem.LargeImage = new IconResourceHandle("16x16.staffSingle_16.png");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString());
                    objItem.SubItems.Add(reader.GetString(2)); // Initial
                    objItem.SubItems.Add(reader.GetString(3));
                    objItem.SubItems.Add(reader.GetString(4));
                    objItem.SubItems.Add(reader.GetString(5));
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(6), true));
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(8), true));

                    iCount++;
                }
            }
            this.lvList.Sort(); // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效

        }

        #region Build Sql Query String
        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT [StaffId],[StaffNumber],[StaffCode],[FullName],[FullName_Chs],[FullName_Cht]");
            sql.Append(" ,[CreatedOn],[CreatedBy],[ModifiedOn],[ModifiedBy] ");
            sql.Append(" FROM [dbo].[vwStaffList] ");

            sql.Append(" WHERE Status >= 0 AND ");

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
                sql.Append(" AND (StaffNumber LIKE '%").Append(SearchForText).Append("%' ");
                sql.Append(" OR StaffCode LIKE '%").Append(SearchForText).Append("%' ");
                sql.Append(" OR FullName LIKE '%").Append(SearchForText).Append("%')");
            }

            if (SelectedStaff != System.Guid.Empty)
            {
                sql.Append(" AND CreatedBy = '").Append(SelectedStaff.ToString()).Append("'");
            }

            if (!(String.IsNullOrEmpty(AlphaSeacher)) && AlphaSeacher != "All")
            {
                sql.Append(String.Format(" AND ( SUBSTRING([StaffNumber], 1, 1) = N'{0}' )", AlphaSeacher));
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
                    StaffCode wizStaff = new StaffCode();
                    wizStaff.StaffId = new System.Guid(lvList.SelectedItem.SubItems[2].Text);
                    wizStaff.Closed += new EventHandler(wizSupplier_Closed);
                    wizStaff.ShowDialog();
                }
            }
        }

        void wizSupplier_Closed(object sender, EventArgs e)
        {
            StaffCode wizStaff = sender as StaffCode;
            if (wizStaff.StaffId != System.Guid.Empty)
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