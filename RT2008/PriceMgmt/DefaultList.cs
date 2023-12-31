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

namespace RT2008.PriceMgmt
{
    public partial class DefaultList : DefaultListBase
    {
        public PriceUtility.PriceMgmtType ListType { get; set; }

        public DefaultList(Control toolBar, PriceUtility.PriceMgmtType listType)
        {
            InitializeComponent();

            PriceToolbar tb = new PriceToolbar(toolBar, ref tbControl);

            this.ListType = listType;

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
            BindList();
            SetLvwList();

            alphaBinding.Visible = false;
        }

        public override void BindList()
        {
            BindPriceList();
        }

        #region Bind Price List

        #region Set View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colTxNumber = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colHeaderId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colEffectDate = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colReasonCode = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colRemarks = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new ColumnHeader();

            // 
            // colHeaderId
            // 
            colHeaderId.Image = null;
            colHeaderId.Text = "HeaderId";
            colHeaderId.Visible = false;
            colHeaderId.Width = 150;
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
            colTxNumber.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Left;
            colTxNumber.Image = null;
            colTxNumber.Text = Utility.Dictionary.GetWord("TxNumber");
            colTxNumber.Width = 110;
            // 
            // colEffectDate
            // 
            colEffectDate.Image = null;
            colEffectDate.Text = Utility.Dictionary.GetWord("Effective Date");
            colEffectDate.Width = 80;
            // 
            // colReasonCode
            // 
            colReasonCode.Image = null;
            colReasonCode.Text = Utility.Dictionary.GetWord("Reason");
            colReasonCode.Width = 120;
            // 
            // colRemarks
            // 
            colRemarks.Image = null;
            colRemarks.Text = Utility.Dictionary.GetWord("Remarks");
            colRemarks.Width = 120;
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
            colTxNumber,
            colLN,
            colHeaderId,
            colEffectDate,
            colReasonCode,
            colRemarks,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        /// <summary>
        /// Binds the Price list.
        /// </summary>
        /// 
        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{32D0234E-A4ED-4550-B899-48CB5474B7BB}");

            RT2008.Controls.Preference.Load(ref lvList);
        }

        private void BindPriceList()
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
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(1)); // Tx Number
                    objItem.SmallImage = new IconResourceHandle("16x16.flag_green.png");
                    objItem.LargeImage = new IconResourceHandle("16x16.flag_green.png");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // HeaderId
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(3), false)); // Effective Date
                    objItem.SubItems.Add(reader.GetString(5)); // Reason Code
                    objItem.SubItems.Add(reader.GetString(6)); // Remarks
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(7), true)); // CreatedOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(8), true)); // ModifiedOn

                    iCount++;
                }
            }
            this.lvList.Sort(); // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效
        }

        #region Build Sql Query String

        /// <summary>
        /// Builds the SQL query string.
        /// </summary>
        /// <returns></returns>
        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT HeaderId, TxNumber, TxType, EffectDate, PM_TYPE, 
                (CASE WHEN PriceManagementBatchHeader.ReasonId IS NOT NULL THEN (SELECT ReasonCode FROM PriceManagementReason WHERE ReasonId = PriceManagementBatchHeader.ReasonId)
                ELSE '' END) AS ReasonCode, Remarks, ");
            sql.Append(" CreatedOn, ModifiedOn, CreatedBy, ModifiedBy ");
            sql.Append(" FROM PriceManagementBatchHeader ");
            sql.Append(" WHERE 1 = 1 ");
            sql.Append(" AND PM_TYPE = '").Append(this.ListType.ToString().Substring(0, 1)).Append("'");

            switch (SelectedViewIndex)
            {
                case 0: // Last 7 days
                    sql.Append(" AND (CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME))");
                    break;
                case 1: // Last 14 days
                    sql.Append(" AND (CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-14).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME))");
                    break;
                case 2: // Last 30 days
                    sql.Append(" AND (CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME))");
                    break;
                case 3: // Last 60 days
                    sql.Append(" AND (CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-60).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME))");
                    break;
                case 4: // Last 90 days
                    sql.Append(" AND (CreatedOn BETWEEN CAST('").Append(DateTime.Today.AddDays(-90).ToString("yyyy-MM-dd 00:00:00")).Append("' AS DATETIME)");
                    sql.Append(" AND CAST('").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Append("' AS DATETIME))");
                    break;
                case 5: // All
                default:
                    sql.Append("");
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

            sql.Append(" ORDER BY TxNumber ");

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
                    PriceMgmtWizard wizList = new PriceMgmtWizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizList.Closed += new EventHandler(wizList_Closed);
                    wizList.ShowDialog();
                }
            }
        }

        void wizList_Closed(object sender, EventArgs e)
        {
            PriceMgmtWizard wizList = sender as PriceMgmtWizard;
            if (wizList.HeaderId != System.Guid.Empty)
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