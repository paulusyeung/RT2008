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
    public partial class DefaultCombinList : DefaultListBase
    {
        public DefaultCombinList(Control toolBar)
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

            lblCreatedBy.Visible = false;
            lblView.Visible = false;

            cboStaffList.Visible = false;
            cboView.Visible = false;
        }

        public override void BindList()
        {
            BindCombinList();
        }

        #region Bind Combin List

        #region Set View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colCombinCode = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colDimId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAPPENDIX1 = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAPPENDIX2 = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colAPPENDIX3 = new ColumnHeader();

            // 
            // colDimId
            // 
            colDimId.ClientAction = null;
            colDimId.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colDimId.Image = null;
            colDimId.Text = "DimensionId";
            colDimId.Visible = false;
            colDimId.Width = 150;
            // 
            // colCombinCode
            // 
            colCombinCode.ClientAction = null;
            colCombinCode.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Left;
            colCombinCode.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colCombinCode.Image = null;
            colCombinCode.Text = Utility.Dictionary.GetWord("Code");
            colCombinCode.Width = 80;
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
            // colAPPENDIX1
            // 
            colAPPENDIX1.ClientAction = null;
            colAPPENDIX1.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAPPENDIX1.Image = null;
            colAPPENDIX1.Text = SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1");
            colAPPENDIX1.Width = 80;
            // 
            // colAPPENDIX2
            // 
            colAPPENDIX2.ClientAction = null;
            colAPPENDIX2.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAPPENDIX2.Image = null;
            colAPPENDIX2.Text = SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2");
            colAPPENDIX2.Width = 80;
            // 
            // colAPPENDIX3
            // 
            colAPPENDIX3.ClientAction = null;
            colAPPENDIX3.DragTargets = new Gizmox.WebGUI.Forms.Component[0];
            colAPPENDIX3.Image = null;
            colAPPENDIX3.Text = SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3");
            colAPPENDIX3.Width = 80;

            lvList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            colCombinCode,
            colLN,
            colDimId,
            colAPPENDIX1,
            colAPPENDIX2,
            colAPPENDIX3});
        }
        #endregion

        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{87A3A3EC-22A8-42bf-90A3-AF89AD870807}");

            RT2008.Controls.Preference.Load(ref lvList);
        }

        private void BindCombinList()
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
                    ListViewItem objItem = this.lvList.Items.Add(reader.GetString(2)); // Combin Code
                    objItem.SmallImage = new IconResourceHandle("16x16.Product16.gif");
                    objItem.LargeImage = new IconResourceHandle("16x16.Product16.gif");

                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetGuid(0).ToString()); // DimensionId
                    objItem.SubItems.Add(reader.GetString(3)); // APPENDIX1
                    objItem.SubItems.Add(reader.GetString(4)); // APPENDIX2
                    objItem.SubItems.Add(reader.GetString(5)); // APPENDIX3

                    iCount++;
                }
            }
            this.lvList.Sort(); // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效
        }

        #region Build Sql Query String
        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT DimensionId,  ROW_NUMBER() OVER (ORDER BY DimCode) AS rownum, ");
            sql.Append("DimCode, APPENDIX1, APPENDIX2, APPENDIX3, DimDetailId ");
            sql.Append(" FROM vwDimensionList");
            sql.Append(" WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(SearchForText))
            {
                sql.Append(" WHERE ");
                sql.Append(" DimCode LIKE '%").Append(SearchForText).Append("%'");
                sql.Append(" OR APPENDIX1 LIKE '%").Append(SearchForText).Append("%'");
                sql.Append(" OR APPENDIX2 LIKE '%").Append(SearchForText).Append("%'");
                sql.Append(" OR APPENDIX3 LIKE '%").Append(SearchForText).Append("%'");
            }

            if (!(String.IsNullOrEmpty(AlphaSeacher)) && AlphaSeacher != "All")
            {
                sql.Append(String.Format(" AND ( SUBSTRING([DimCode], 1, 1) = N'{0}' )", AlphaSeacher));
            }

            sql.Append(" ORDER BY DimCode, APPENDIX1, APPENDIX2, APPENDIX3 ");

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
                    ProductWizard_Combination wizProd = new ProductWizard_Combination(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizProd.Closed += new EventHandler(wizProd_Closed);
                    wizProd.ShowDialog();
                }
            }
        }

        void wizProd_Closed(object sender, EventArgs e)
        {
            ProductWizard_Combination wizCombin = sender as ProductWizard_Combination;
            if (wizCombin.CombinId != System.Guid.Empty)
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