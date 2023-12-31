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
using System.Configuration;

#endregion

namespace RT2008.Workplace
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
            BindWorkplaceList();
        }

        #region Bind Workplace List

        #region List View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colWorkplaceId = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colWorkplaceCode = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colWorkplaceInitial = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colWorkplaceName = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colWorkplaceNameChs = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colWorkplaceNameCht = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colEmail = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new Gizmox.WebGUI.Forms.ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new Gizmox.WebGUI.Forms.ColumnHeader();

            // 
            // colWorkplaceId
            // 
            colWorkplaceId.ClientAction = null;
            colWorkplaceId.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colWorkplaceId.Image = null;
            colWorkplaceId.Text = "WorkplaceId";
            colWorkplaceId.Visible = false;
            colWorkplaceId.Width = 150;
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
            // colWorkplaceCode
            // 
            colWorkplaceCode.ClientAction = null;
            colWorkplaceCode.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Left;
            colWorkplaceCode.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colWorkplaceCode.Image = null;
            colWorkplaceCode.Text = Utility.Dictionary.GetWord("Code");
            colWorkplaceCode.Width = 80;
            // 
            // colWorkplaceInitial
            // 
            colWorkplaceInitial.ClientAction = null;
            colWorkplaceInitial.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colWorkplaceInitial.Image = null;
            colWorkplaceInitial.Text = Utility.Dictionary.GetWord("Initial");
            colWorkplaceInitial.Width = 120;
            // 
            // colWorkplaceName
            // 
            colWorkplaceName.ClientAction = null;
            colWorkplaceName.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colWorkplaceName.Image = null;
            colWorkplaceName.Text = Utility.Dictionary.GetWord("Name");
            colWorkplaceName.Width = 120;
            // 
            // colWorkplaceNameChs
            // 
            colWorkplaceNameChs.ClientAction = null;
            colWorkplaceNameChs.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colWorkplaceNameChs.Image = null;
            colWorkplaceNameChs.Text = Utility.Dictionary.GetWord("Name") + " (" + Utility.Dictionary.GetWord("Chs") + ")";
            colWorkplaceNameChs.Width = 120;
            // 
            // colWorkplaceNameCht
            // 
            colWorkplaceNameCht.ClientAction = null;
            colWorkplaceNameCht.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colWorkplaceNameCht.Image = null;
            colWorkplaceNameCht.Text = Utility.Dictionary.GetWord("Name") + " (" + Utility.Dictionary.GetWord("Cht") + ")";
            colWorkplaceNameCht.Width = 120;
            // 
            // colEmail
            // 
            colEmail.ClientAction = null;
            colEmail.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colEmail.Image = null;
            colEmail.Text = Utility.Dictionary.GetWord("Email");
            colEmail.Width = 120;
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
            colWorkplaceCode,
            colLN,
            colWorkplaceId,
            colWorkplaceInitial,
            colWorkplaceName,
            colWorkplaceNameChs,
            colWorkplaceNameCht,
            colEmail,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{CD2A7928-69A9-4c79-8D69-5A5BA8842076}");

            RT2008.Controls.Preference.Load(ref lvList);
        }


        public void BindWorkplaceList()
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
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(2)); // WorkplaceId
                    objItem.SmallImage = new IconResourceHandle("16x16.ico_16_4000.gif");
                    objItem.LargeImage = new IconResourceHandle("16x16.ico_16_4000.gif");
                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // WorkplaceCode
                    objItem.SubItems.Add(reader.GetString(3)); // Workplace Initial
                    objItem.SubItems.Add(reader.GetString(4)); // Workplace Name
                    objItem.SubItems.Add(reader.GetString(5)); // Workplace Name Chs
                    objItem.SubItems.Add(reader.GetString(6)); // Workplace name Cht
                    objItem.SubItems.Add(reader.GetString(7)); // Email
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
            sql.Append("SELECT WorkplaceId,  ROW_NUMBER() OVER (ORDER BY WorkplaceCode) AS rownum, ");
            sql.Append(" WorkplaceCode, WorkplaceInitial, WorkplaceName, WorkplaceName_Chs, ");
            sql.Append(" WorkplaceName_Cht, ISNULL(Email, ''), ");
            sql.Append(" CreatedOn, ModifiedOn, CreatedBy, ModifiedBy ");
            sql.Append(" FROM vwWorkplaceList ");

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
                sql.Append(" AND (WorkplaceCode LIKE '%").Append(SearchForText).Append("%' ");
                sql.Append(" OR WorkplaceInitial LIKE '%").Append(SearchForText).Append("%' ");
                sql.Append(" OR WorkplaceName LIKE '%").Append(SearchForText).Append("%')");
            }

            if (SelectedStaff != System.Guid.Empty)
            {
                sql.Append(" AND CreatedBy = '").Append(SelectedStaff.ToString()).Append("'");
            }

            if (!(String.IsNullOrEmpty(AlphaSeacher)) && AlphaSeacher != "All")
            {
                sql.Append(String.Format(" AND ( SUBSTRING([WorkplaceCode], 1, 1) = N'{0}' )", AlphaSeacher));
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
                    WorkplaceCode wizWorkplace = new WorkplaceCode(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizWorkplace.Closed += new EventHandler(wizSupplier_Closed);
                    wizWorkplace.ShowDialog();
                }
            }
        }

        void wizSupplier_Closed(object sender, EventArgs e)
        {
            WorkplaceCode wizWorkplace = sender as WorkplaceCode;
            if (wizWorkplace.WorkplaceID != System.Guid.Empty)
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