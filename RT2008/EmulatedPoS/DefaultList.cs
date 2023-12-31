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

#endregion

namespace RT2008.EmulatedPoS
{
    public partial class DefaultList : DefaultListBase
    {
        public RT2008.DAL.Common.Enums.TxType SalesType { get; set; }

        public DefaultList(Control toolBar, RT2008.DAL.Common.Enums.TxType txType)
        {
            InitializeComponent();

            SalesToolbar tb = new SalesToolbar(toolBar, ref tbControl);

            this.SalesType = txType;

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
            BindSalesList();
        }

        #region Bind Sales List

        #region Set list View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colHeaderId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colTxNumber = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colTxDate = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colTotalAmount = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colRemarks = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colStaff = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colWorkplace = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colMember = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colReference = new ColumnHeader();

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
            // colTxDate
            // 
            colTxDate.Image = null;
            colTxDate.Text = Utility.Dictionary.GetWord("TxDate");
            colTxDate.Width = 80;
            // 
            // colTotalAmount
            // 
            colTotalAmount.Image = null;
            colTotalAmount.Text = Utility.Dictionary.GetWord("Total Amount");
            colTotalAmount.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            colTotalAmount.Width = 80;
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
            // 
            // colStaff
            // 
            colStaff.Image = null;
            colStaff.Text = Utility.Dictionary.GetWord("Staff");
            colStaff.Width = 60;
            // 
            // colWorkplace
            // 
            colWorkplace.Image = null;
            colWorkplace.Text = Utility.Dictionary.GetWord("Workplace");
            colWorkplace.Width = 60;
            // 
            // colMember
            // 
            colMember.Image = null;
            colMember.Text = Utility.Dictionary.GetWord("Member");
            colMember.Width = 80;
            // 
            // colReference
            // 
            colReference.Image = null;
            colReference.Text = Utility.Dictionary.GetWord("Reference");
            colReference.Width = 100;

            lvList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            colTxNumber,
            colLN,
            colHeaderId,
            colTxDate,
            colTotalAmount,
            colStaff,
            colWorkplace,
            colMember,
            colReference,
            colRemarks,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        /// <summary>
        /// Binds the Sales list.
        /// </summary>

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{E36C793A-EC38-4467-AD3D-30C00FB82360}");

            RT2008.Controls.Preference.Load(ref lvList);
        }
        
        private void BindSalesList()
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
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(2)); // Tx Number
                    objItem.SmallImage = reader.GetString(1) == "HOLD" ? new IconResourceHandle("16x16.flag_grey.png") : new IconResourceHandle("16x16.flag_green.png");
                    objItem.LargeImage = reader.GetString(1) == "HOLD" ? new IconResourceHandle("16x16.flag_grey.png") : new IconResourceHandle("16x16.flag_green.png");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // HeaderId
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(3), false)); // Tx Date
                    objItem.SubItems.Add(reader.GetDecimal(4).ToString("n2")); // Total Amount
                    objItem.SubItems.Add(reader.GetString(6)); // Staff Number
                    objItem.SubItems.Add(reader.GetString(7)); // Workplace
                    objItem.SubItems.Add(reader.GetString(8)); // MemberNumber
                    objItem.SubItems.Add(reader.GetString(9)); // Reference
                    objItem.SubItems.Add(reader.GetString(10)); // Remarks
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(11), true)); // CreatedOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(12), true)); // ModifiedOn

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
            sql.Append(" SELECT [HeaderId],[Status],[TxNumber],[TxDate],[TotalAmount],[DepositAmount],[StaffNumber],[WorkplaceCode] ");
            sql.Append(" ,[MemberNumber],[Reference],[Remarks],[CreatedOn],[ModifiedOn] ");
            sql.Append(" FROM [dbo].[vwDraftEPosHeaderList]");
            sql.Append(" WHERE TxType = '").Append(this.SalesType.ToString().Substring(0, 3)).Append("'");

            switch (cboView.SelectedIndex)
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
                sql.Append(" AND TxNumber LIKE '%").Append(SearchForText.Trim()).Append("%'");
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
                    SalesWizard wizList = new SalesWizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text), this.SalesType);
                    //wizList.SalesType = this.SalesType;
                    wizList.Closed += new EventHandler(wizList_Closed);
                    wizList.ShowDialog();
                }
            }
        }

        void wizList_Closed(object sender, EventArgs e)
        {
            SalesWizard wizList = sender as SalesWizard;
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