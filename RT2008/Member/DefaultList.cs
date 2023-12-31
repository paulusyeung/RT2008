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

namespace RT2008.Member
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
            BindMemberList();
        }

        #region Bind Member List

        #region List View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colMemberId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colMemberNumber = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colMemberName = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colMemberNameChs = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colMemberNameCht = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colRemarks = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colMemberInitial = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new ColumnHeader();

            // 
            // colMemberId
            // 
            colMemberId.Image = null;
            colMemberId.Text = "MemberId";
            colMemberId.Visible = false;
            colMemberId.Width = 150;
            // 
            // colLN
            // 
            colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            colLN.Image = null;
            colLN.Text = Utility.Dictionary.GetWord("LN");
            colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            colLN.Width = 30;
            // 
            // colMemberNumber
            // 
            colMemberNumber.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Left;
            colMemberNumber.Image = null;
            colMemberNumber.Text = Utility.Dictionary.GetWord("Number");
            colMemberNumber.Width = 100;
            // 
            // colMemberInitial
            // 
            colMemberInitial.Image = null;
            colMemberInitial.Text = Utility.Dictionary.GetWord("Nick Name");
            colMemberInitial.Width = 120;
            // 
            // colMemberName
            // 
            colMemberName.Image = null;
            colMemberName.Text = Utility.Dictionary.GetWord("Name");
            colMemberName.Width = 120;
            // 
            // colMemberNameChs
            // 
            colMemberNameChs.Image = null;
            colMemberNameChs.Text = Utility.Dictionary.GetWord("Name") + " (" + Utility.Dictionary.GetWord("Chs") + ")";
            colMemberNameChs.Width = 120;
            // 
            // colMemberNameCht
            // 
            colMemberNameCht.Image = null;
            colMemberNameCht.Text = Utility.Dictionary.GetWord("Name") + " (" + Utility.Dictionary.GetWord("Cht") + ")";
            colMemberNameCht.Width = 120;
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
            colMemberNumber,
            colLN,
            colMemberId,
            colMemberInitial,
            colMemberName,
            colMemberNameChs,
            colMemberNameCht,
            colRemarks,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{DD367635-5BC3-474a-8086-363932092561}");

            RT2008.Controls.Preference.Load(ref lvList);
        }


        private void BindMemberList()
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
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(1)); // MemberNumber
                    objItem.SmallImage = new IconResourceHandle("16x16.CustomerSingle_16.png");
                    objItem.LargeImage = new IconResourceHandle("16x16.CustomerSingle_16.png");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // MemberCode
                    objItem.SubItems.Add(reader.GetString(2)); // Member Initial
                    objItem.SubItems.Add(reader.GetString(3)); // Member Name
                    objItem.SubItems.Add(reader.GetString(4)); // Member Name Chs
                    objItem.SubItems.Add(reader.GetString(5)); // Member name Cht
                    objItem.SubItems.Add(reader.GetString(6)); // Remarks
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
            sql.Append("SELECT MemberId, MemberNumber, MemberInitial, ISNULL(FullName, '') AS FullName, ISNULL(FullName_Chs, '') AS FullName_Chs, ");
            sql.Append(" ISNULL(FullName_Cht, '') AS FullName_Cht, ISNULL(Remarks, '') AS Remarks, ");
            sql.Append(" CreatedOn, ModifiedOn, CreatedBy, ModifiedBy ");
            sql.Append(" FROM Member ");
            sql.Append(" WHERE ");

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
                sql.Append(" AND (MemberNumber LIKE '%").Append(base.SearchForText).Append("%' ");
                sql.Append(" OR MemberInitial LIKE '%").Append(base.SearchForText).Append("%' ");
                sql.Append(" OR FullName LIKE '%").Append(base.SearchForText).Append("%')");
            }

            if (SelectedStaff != System.Guid.Empty)
            {
                sql.Append(" AND CreatedBy = '").Append(SelectedStaff.ToString()).Append("'");
            }

            if (!(String.IsNullOrEmpty(AlphaSeacher)) && AlphaSeacher != "All")
            {
                sql.Append(String.Format(" AND ( SUBSTRING([MemberNumber], 1, 1) = N'{0}' )", AlphaSeacher));
            }

            sql.Append(" ORDER BY MemberNumber, MemberInitial ");

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
                    MemberWizard wizMember = new MemberWizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizMember.Closed += new EventHandler(wizSupplier_Closed);
                    wizMember.ShowDialog();
                }
            }
        }

        void wizSupplier_Closed(object sender, EventArgs e)
        {
            MemberWizard wizMember = sender as MemberWizard;
            if (wizMember.MemberId != System.Guid.Empty)
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