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

namespace RT2008.Purchasing
{
    public partial class DefaultOSTList : Controls.DefaultListBase
    {
        public DefaultOSTList()
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
            alphaBinding.Visible = false;
        }

        public override void BindList()
        {
            BindSettleOrderList();
        }

        #region Bind Purchase List

        #region List View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colTxDate = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colOrderHeaderId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLocation = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colTxNumber = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colOperator = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colSupplier = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colRemarks = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new ColumnHeader();

            // 
            // colTxDate
            // 
            colTxDate.ClientAction = null;
            colTxDate.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colTxDate.Image = null;
            colTxDate.Text = Utility.Dictionary.GetWord("TxDate");
            colTxDate.Width = 80;
            // 
            // colCreatedOn
            // 
            colCreatedOn.ClientAction = null;
            colCreatedOn.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colCreatedOn.Image = null;
            colCreatedOn.Text = Utility.Dictionary.GetWord("CreatedOn");
            colCreatedOn.Width = 110;
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
            // colOrderHeaderId
            // 
            colOrderHeaderId.ClientAction = null;
            colOrderHeaderId.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colOrderHeaderId.Image = null;
            colOrderHeaderId.Text = "OrderHeaderId";
            colOrderHeaderId.Visible = false;
            colOrderHeaderId.Width = 150;
            // 
            // colLocation
            // 
            colLocation.ClientAction = null;
            colLocation.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colLocation.Image = null;
            colLocation.Text = Utility.Dictionary.GetWord("Workplace");
            colLocation.Width = 70;
            // 
            // colTxNumber
            // 
            colTxNumber.ClientAction = null;
            colTxNumber.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colTxNumber.Image = null;
            colTxNumber.Text = Utility.Dictionary.GetWord("TxNumber");
            colTxNumber.Width = 110;
            // 
            // colOperator
            // 
            colOperator.ClientAction = null;
            colOperator.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colOperator.Image = null;
            colOperator.Text = Utility.Dictionary.GetWord("Staff");
            colOperator.Width = 70;
            // 
            // colSupplier
            // 
            colSupplier.ClientAction = null;
            colSupplier.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colSupplier.Image = null;
            colSupplier.Text = Utility.Dictionary.GetWord("Supplier");
            colSupplier.Width = 70;
            // 
            // colRemarks
            // 
            colRemarks.ClientAction = null;
            colRemarks.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colRemarks.Image = null;
            colRemarks.Text = Utility.Dictionary.GetWord("Remarks");
            colRemarks.Width = 150;
            // 
            // colModifiedOn
            // 
            colModifiedOn.ClientAction = null;
            colModifiedOn.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colModifiedOn.Image = null;
            colModifiedOn.Text = Utility.Dictionary.GetWord("ModifiedOn");
            colModifiedOn.Width = 110;

            lvList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            colTxNumber,
            colLN,
            colOrderHeaderId,
            colTxDate,
            colOperator,
            colLocation,
            colSupplier,
            colRemarks,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        /// <summary>
        /// Binds the purchase list.
        /// </summary>
        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{ABBEF7D0-1838-4d72-AE70-C29F0DE73B39}");

            RT2008.Controls.Preference.Load(ref lvList);
        }

        private void BindSettleOrderList()
        {
            this.lvList.Items.Clear();   ////Çå³ýlisPurchaseOrderList¿Ø¼þÖÐµÄÊý¾Ý

            int iCount = 1;
            string sql = this.BuildSqlQueryString();
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType= CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem objItem = this.lvList.Items.Add(reader.IsDBNull(2) ? string.Empty : reader.GetString(2)); //// TxNumber
                    objItem.SmallImage = reader.GetString(1) == "HOLD" ? new IconResourceHandle("16x16.flag_grey.png") : new IconResourceHandle("16x16.flag_green.png");
                    objItem.LargeImage = reader.GetString(1) == "HOLD" ? new IconResourceHandle("16x16.flag_grey.png") : new IconResourceHandle("16x16.flag_green.png");

                    objItem.SubItems.Add(iCount.ToString()); //// Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); //// CAPHeaderId
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(3), false)); //// TxDate
                    objItem.SubItems.Add(reader.IsDBNull(4) ? string.Empty : reader.GetString(4)); //// Operator
                    objItem.SubItems.Add(reader.IsDBNull(5) ? string.Empty : reader.GetString(5)); //// Location
                    objItem.SubItems.Add(reader.IsDBNull(6) ? string.Empty : reader.GetString(6)); //// SupplierInvoiceNumber
                    objItem.SubItems.Add(reader.GetString(11) + reader.GetString(12) + reader.GetString(13)); //// Remarks
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(7), true)); //// CreatedOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(9), true)); //// ModifiedOn

                    iCount++;
                }
            }
            this.lvList.Sort(); // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效
        }

        #region Build Sql Query String
        /// <summary>
        /// Builds the SQL query string.
        /// </summary>
        /// <returns>The joined Sql</returns>
        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
SELECT ReceiveHeaderId,
    (
    CASE
        WHEN Status = 0 THEN 'HOLD'
        WHEN Status = 1 THEN 'POST'
    END
    ) AS Status, TxNumber, TxDate, StaffNumber, ");
            sql.Append(" Location, SupplierCode, ");
            sql.Append(" CreatedOn, CreatedBy, ModifiedOn, ModifiedBy, Remarks1, Remarks2, Remarks3 ");
            sql.Append(" FROM vwSettleOrderList ");
            sql.Append(" WHERE 1 = 1 AND ");

            switch (SelectedViewIndex)
            {
                case 0: //// Last 7 days
                    sql.Append(" CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME)");
                    break;
                case 1: //// Last 14 days
                    sql.Append(" CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-14).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME)");
                    break;
                case 2: //// Last 30 days
                    sql.Append(" CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME)");
                    break;
                case 3: //// Last 60 days
                    sql.Append(" CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-60).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME)");
                    break;
                case 4: //// Last 90 days
                    sql.Append(" CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-90).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME)");
                    break;
                case 5: //// All
                default:
                    sql.Append(" 1 = 1 ");
                    break;
            }

            if (!string.IsNullOrEmpty(SearchForText))
            {
                string objTxNumber = PurchasingUtils.GenSafeChars(SearchForText);
                sql.Append(" AND TxNumber LIKE '%").Append(objTxNumber).Append("%'");
            }

            if (SelectedStaff != System.Guid.Empty)
            {
                sql.Append(" AND CreatedBy = '").Append(SelectedStaff.ToString()).Append("'");
            }

            sql.Append("ORDER BY TxNumber");

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
                    RT2008.Purchasing.Wizard.SettleOrder settleOrder = new RT2008.Purchasing.Wizard.SettleOrder(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    settleOrder.Closed += new EventHandler(settleOrder_Closed);
                    settleOrder.ShowDialog();
                }
            }
        }

        void settleOrder_Closed(object sender, EventArgs e)
        {
            RT2008.Purchasing.Wizard.SettleOrder settleOrder = sender as RT2008.Purchasing.Wizard.SettleOrder;
            if (settleOrder.ReceivingHeaderId != System.Guid.Empty)
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