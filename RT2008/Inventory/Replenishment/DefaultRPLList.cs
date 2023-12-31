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

namespace RT2008.Inventory.Replenishment
{
    public partial class DefaultRPLList : DefaultListBase
    {
        public DefaultRPLList(Control toolBar)
        {
            InitializeComponent();

            InvtToolbar tb = new InvtToolbar(toolBar, ref tbControl, InvtToolbar.FormType.Adjustment);

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
            alphaBinding.Visible = false;

            //2013.07.05 paulus: 預設 view 7 days
            this.cboView.SelectedIndex = 0;
        }

        public override void BindList()
        {
            BindRplList();
        }

        #region Bind Rpl List

        #region Set view Columns
        private void SetColumns()
        {
        Gizmox.WebGUI.Forms.ColumnHeader colInvtRplId = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colTxNumber = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colTxDate = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colOperator = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colFromLocation = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colToLocation = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colRemarks = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colConfirmedOn = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colCompletedOn = new ColumnHeader();

        // 
        // colInvtRplId
        // 
        colInvtRplId.Image = null;
        colInvtRplId.Text = "InvtRplId";
        colInvtRplId.Visible = false;
        colInvtRplId.Width = 150;
        // 
        // colLN
        // 
        colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
        colLN.Image = null;
        colLN.Text = Utility.Dictionary.GetWord("LN");
        colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
        colLN.Width = 30;
        // 
        // colTxNumber
        // 
        colTxNumber.Image = null;
        colTxNumber.Text = Utility.Dictionary.GetWord("TxNumber");
        colTxNumber.Width = 110;
        // 
        // colTxDate
        // 
        colTxDate.Image = null;
        colTxDate.Text = Utility.Dictionary.GetWord("TxDate");
        colTxDate.Width = 80;
        // 
        // colOperator
        // 
        colOperator.Image = null;
        colOperator.Text = Utility.Dictionary.GetWord("Staff");
        colOperator.Width = 70;
        // 
        // colFromLocation
        // 
        colFromLocation.Image = null;
        colFromLocation.Text = Utility.Dictionary.GetWord("From_location");
        colFromLocation.Width = 80;
        // 
        // colToLocation
        // 
        colToLocation.Image = null;
        colToLocation.Text = Utility.Dictionary.GetWord("To_location");
        colToLocation.Width = 80;
        // 
        // colConfirmedOn
        // 
        colConfirmedOn.Image = null;
        colConfirmedOn.Text = Utility.Dictionary.GetWord("Confirmed On");
        colConfirmedOn.Width = 100;
        // 
        // colCompletedOn
        // 
        colCompletedOn.Image = null;
        colCompletedOn.Text = Utility.Dictionary.GetWord("Completed On");
        colCompletedOn.Width = 100;
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
        colCreatedOn.Width = 110;
        // 
        // colModifiedOn
        // 
        colModifiedOn.Image = null;
        colModifiedOn.Text = Utility.Dictionary.GetWord("ModifiedOn");
        colModifiedOn.Width = 110;

        lvList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            colTxNumber,
            colLN,
            colInvtRplId,
            colTxDate,
            colOperator,
            colFromLocation,
            colToLocation,
            colConfirmedOn,
            colCompletedOn,
            colRemarks,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{5EB543FE-1FEC-4c96-9328-09DE95E7E78B}");

            RT2008.Controls.Preference.Load(ref lvList);
        }

        private void BindRplList()
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
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(1)); // TxNumber
                    objItem.SmallImage = reader.GetString(15) == "HOLD" ? new IconResourceHandle("16x16.flag_grey.png") : new IconResourceHandle("16x16.flag_green.png");
                    objItem.LargeImage = reader.GetString(15) == "HOLD" ? new IconResourceHandle("16x16.flag_grey.png") : new IconResourceHandle("16x16.flag_green.png");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // RplHeaderId
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(2), false)); // TxDate
                    objItem.SubItems.Add(reader.GetString(3)); // StaffNumber
                    objItem.SubItems.Add(reader.GetString(4)); // FromLocation
                    objItem.SubItems.Add(reader.GetString(5)); // ToLocation
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(8), false)); // ConfirmedOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(6), false)); // CompletedOn
                    objItem.SubItems.Add(reader.GetString(10)); // Remarks
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(11), true)); // TransferredOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(13), true)); // CompletedOn

                    iCount++;
                }
            }
            this.lvList.Sort();        // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效
        }

        #region Build Sql Query String
        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT HeaderId, TxNumber, TxDate, StaffNumber, ");
            sql.Append(" FromLocation, ToLocation, CompletedOn, Confirmed, ConfirmedOn, ConfirmedBy, Remarks, ");
            sql.Append(@" CreatedOn, CreatedBy, ModifiedOn, ModifiedBy,
    (
    CASE
        WHEN Status = 0 THEN 'HOLD'
        WHEN Status = 1 THEN 'POST'
    END
    ) AS Status  ");
            sql.Append(" FROM vwDraftRPLList ");
            sql.Append(" WHERE Posted = 0 AND ");

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
                sql.Append(" AND TxNumber LIKE '%").Append(SearchForText).Append("%'");
            }

            if (SelectedStaff != System.Guid.Empty)
            {
                sql.Append(" AND CreatedBy = '").Append(SelectedStaff.ToString()).Append("'");
            }

            sql.Append("ORDER BY TxNumber DESC");

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
                    RT2008.Inventory.Replenishment.Wizard wizRPL = new RT2008.Inventory.Replenishment.Wizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizRPL.Closed += new EventHandler(wizRPL_Closed);
                    wizRPL.ShowDialog();
                }
            }
        }

        void wizRPL_Closed(object sender, EventArgs e)
        {
            RT2008.Inventory.Replenishment.Wizard wizRPL = sender as RT2008.Inventory.Replenishment.Wizard;
            if (wizRPL.RplId != System.Guid.Empty)
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