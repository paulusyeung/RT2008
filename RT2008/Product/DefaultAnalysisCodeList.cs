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
    public partial class DefaultAnalysisCodeList : DefaultListBase
    {
        public DefaultAnalysisCodeList(Control toolBar)
        {
            InitializeComponent();

            ProductToolbar tb = new ProductToolbar(toolBar, ref tbControl);

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
        }

        public override void BindList()
        {
            BindAnalysisCodeList();
        }

        #region Bind AnalysisCode List

        #region Set Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colAnalysisCodeId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colType = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAnalysisCodeCode = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAnalysisCodeInitial = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAnalysisCodeName = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAnalysisCodeNameCht = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAnalysisCodeNameChs = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colCreatedOn = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colModifiedOn = new ColumnHeader();

            // 
            // colAnalysisCodeId
            // 
            colAnalysisCodeId.ClientAction = null;
            colAnalysisCodeId.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAnalysisCodeId.Image = null;
            colAnalysisCodeId.Text = "AnalysisCodeId";
            colAnalysisCodeId.Visible = false;
            colAnalysisCodeId.Width = 150;
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
            // colType
            // 
            colType.ClientAction = null;
            colType.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colType.Image = null;
            colType.Text = Utility.Dictionary.GetWord("Type");
            colType.Width = 80;
            // 
            // colAnalysisCodeCode
            // 
            colAnalysisCodeCode.ClientAction = null;
            colAnalysisCodeCode.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Left;
            colAnalysisCodeCode.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAnalysisCodeCode.Image = null;
            colAnalysisCodeCode.Text = Utility.Dictionary.GetWord("Code");
            colAnalysisCodeCode.Width = 80;
            // 
            // colAnalysisCodeInitial
            // 
            colAnalysisCodeInitial.ClientAction = null;
            colAnalysisCodeInitial.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAnalysisCodeInitial.Image = null;
            colAnalysisCodeInitial.Text = Utility.Dictionary.GetWord("Initial");
            colAnalysisCodeInitial.Width = 80;
            // 
            // colAnalysisCodeName
            // 
            colAnalysisCodeName.ClientAction = null;
            colAnalysisCodeName.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAnalysisCodeName.Image = null;
            colAnalysisCodeName.Text = Utility.Dictionary.GetWord("description");
            colAnalysisCodeName.Width = 120;
            // 
            // colAnalysisCodeNameChs
            // 
            colAnalysisCodeNameChs.ClientAction = null;
            colAnalysisCodeNameChs.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAnalysisCodeNameChs.Image = null;
            colAnalysisCodeNameChs.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Chs") + ")";
            colAnalysisCodeNameChs.Width = 120;
            // 
            // colAnalysisCodeNameCht
            // 
            colAnalysisCodeNameCht.ClientAction = null;
            colAnalysisCodeNameCht.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAnalysisCodeNameCht.Image = null;
            colAnalysisCodeNameCht.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Cht") + ")";
            colAnalysisCodeNameCht.Width = 120;
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
            colType,
            colLN,
            colAnalysisCodeId,
            colAnalysisCodeCode,
            colAnalysisCodeInitial,
            colAnalysisCodeName,
            colAnalysisCodeNameChs,
            colAnalysisCodeNameCht,
            colCreatedOn,
            colModifiedOn});
        }
        #endregion

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{952898E5-BC7E-4b3f-B8D2-464DE67FC417}");

            RT2008.Controls.Preference.Load(ref lvList);
        }

        private void BindAnalysisCodeList()
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
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(3)); // AnalysisCode Type
                    objItem.SmallImage = new IconResourceHandle("16x16.Product16.gif");
                    objItem.LargeImage = new IconResourceHandle("16x16.Product16.gif");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // AnalysisCodeId
                    objItem.SubItems.Add(reader.GetString(2)); // AnalysisCode Code
                    objItem.SubItems.Add(reader.GetString(4)); // AnalysisCode Initial
                    objItem.SubItems.Add(reader.GetString(5)); // AnalysisCode Name
                    objItem.SubItems.Add(reader.GetString(6)); // AnalysisCode Name Chs
                    objItem.SubItems.Add(reader.GetString(7)); // AnalysisCode name Cht
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(9), true)); // CreatedOn
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(10), true)); // ModifiedOn

                    iCount++;
                }
            }
            this.lvList.Sort(); // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效
        }

        #region Build Sql Query String
        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT AnalysisCodeId,  ROW_NUMBER() OVER (ORDER BY AnalysisCode) AS rownum, ");
            sql.Append(" AnalysisType, AnalysisCode, CodeInitial, CodeName, CodeName_Chs, CodeName_Cht, Mandatory, ");
            sql.Append(" CreatedOn, ModifiedOn, CreatedBy, ModifiedBy ");
            sql.Append(" FROM PosAnalysisCode ");
            sql.Append(" WHERE 1 = 1 ");

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
                sql.Append(" AND (AnalysisCode LIKE '%").Append(SearchForText).Append("%'");
                sql.Append(" OR CodeName LIKE '%").Append(SearchForText).Append("%')");
            }

            if (SelectedStaff != System.Guid.Empty)
            {
                sql.Append(" AND CreatedBy = '").Append(SelectedStaff.ToString()).Append("'");
            }

            if (!(String.IsNullOrEmpty(AlphaSeacher)) && AlphaSeacher != "All")
            {
                sql.Append(String.Format(" AND ( SUBSTRING([AnalysisCode], 1, 1) = N'{0}' )", AlphaSeacher));
            }

            sql.Append(" ORDER BY AnalysisCode ");

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
                    AnalysisCodeWizard wizAC = new AnalysisCodeWizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizAC.Closed += new EventHandler(wizAC_Closed);
                    wizAC.ShowDialog();
                }
            }
        }

        void wizAC_Closed(object sender, EventArgs e)
        {
            AnalysisCodeWizard wizAC = sender as AnalysisCodeWizard;
            if (wizAC.AnalysisCodeId != System.Guid.Empty)
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