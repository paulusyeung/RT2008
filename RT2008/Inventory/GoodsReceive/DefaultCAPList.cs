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

namespace RT2008.Inventory.GoodsReceive
{
    public partial class DefaultCAPList : DefaultListBase
    {
        public DefaultCAPList(Control toolBar)
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
            BindCAPList();
        }

        #region Bind CAP List

        #region Set View Columns
        private void SetColumns()
        {
        Gizmox.WebGUI.Forms.ColumnHeader colInvtCAPId = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colTxNumber = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colTxDate = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colOperator = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colLocation = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colSupplier = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colRef = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colRemarks = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
        Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new ColumnHeader();

        // 
        // colInvtCAPId
        // 
        colInvtCAPId.Image = null;
        colInvtCAPId.Text = "InvtCAPId";
        colInvtCAPId.Visible = false;
        colInvtCAPId.Width = 150;
        // 
        // colTxNumber
        // 
        colTxNumber.Image = null;
        colTxNumber.Text = Utility.Dictionary.GetWord("TxNumber");
        colTxNumber.Width = 110;
        // 
        // colLN
        // 
        colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
        colLN.Image = null;
        colLN.Text = Utility.Dictionary.GetWord("LN");
        colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
        colLN.Width = 30;
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
        // colLocation
        // 
        colLocation.Image = null;
        colLocation.Text = Utility.Dictionary.GetWord("Workplace");
        colLocation.Width = 70;
        // 
        // colSupplier
        // 
        colSupplier.Image = null;
        colSupplier.Text = Utility.Dictionary.GetWord("Supplier");
        colSupplier.Width = 70;
        // 
        // colRef
        // 
        colRef.Image = null;
        colRef.Text = Utility.Dictionary.GetWord("Reference");
        colRef.Width = 100;
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
            colInvtCAPId,
            colTxDate,
            colOperator,
            colLocation,
            colSupplier,
            colRef,
            colRemarks,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{A2F5190F-DE2D-4d68-8519-2BEF70D195AF}");

            RT2008.Controls.Preference.Load(ref lvList);
        }

        private void BindCAPList()
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
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(2)); // TxNumber
                    objItem.SmallImage = reader.GetString(1) == "HOLD" ? new IconResourceHandle("16x16.flag_grey.png") : new IconResourceHandle("16x16.flag_green.png");
                    objItem.LargeImage = reader.GetString(1) == "HOLD" ? new IconResourceHandle("16x16.flag_grey.png") : new IconResourceHandle("16x16.flag_green.png");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // CAPHeaderId
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(3), false)); // TxDate
                    objItem.SubItems.Add(reader.GetString(4)); // StaffNumber
                    objItem.SubItems.Add(reader.GetString(5)); // Location
                    objItem.SubItems.Add(reader.GetString(6)); // Supplier
                    objItem.SubItems.Add(reader.GetString(7)); // Reference
                    objItem.SubItems.Add(reader.GetString(8)); // Remarks
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(9), true)); // CreatedOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(11), true)); // ModifiedOn

                    iCount++;
                }
            }
            this.lvList.Sort();        // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效
        }

        #region Build Sql Query String
        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT HeaderId,
    (
    CASE
        WHEN Status = 0 THEN 'HOLD'
        WHEN Status = 1 THEN 'POST'
    END
    ) AS Status, TxNumber, TxDate, StaffNumber, ");
            sql.Append(" Location, SupplierCode, Reference, Remarks, ");
            sql.Append(" CreatedOn, CreatedBy, ModifiedOn, ModifiedBy ");
            sql.Append(" FROM vwDraftCAPList ");
            sql.Append(" WHERE TxType = 'CAP' AND ReadOnly = 0 AND ");

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
                    RT2008.Inventory.GoodsReceive.Wizard wizCAP = new RT2008.Inventory.GoodsReceive.Wizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizCAP.Closed += new EventHandler(wizCAP_Closed);
                    wizCAP.ShowDialog();
                }
            }
        }

        void wizCAP_Closed(object sender, EventArgs e)
        {
            RT2008.Inventory.GoodsReceive.Wizard wizCAP = sender as RT2008.Inventory.GoodsReceive.Wizard;
            if (wizCAP.CAPId != System.Guid.Empty)
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