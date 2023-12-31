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
using Gizmox.WebGUI.Common.Resources;

#endregion

namespace RT2008.PriceMgmt
{
    public partial class DefaultReasonList : DefaultListBase
    {
        public DefaultReasonList(Control toolBar)
        {
            InitializeComponent();

            PriceToolbar tb = new PriceToolbar(toolBar, ref tbControl);

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

            alphaBinding.Visible = false;
            lblCreatedBy.Visible = false;
            lblView.Visible = false;

            cboStaffList.Visible = false;
            cboView.Visible = false;
        }

        public override void BindList()
        {
            BindReasonList();
        }

        #region Bind Reason List

        #region Set View Columns
        private void SetColumns()
        {
            Gizmox.WebGUI.Forms.ColumnHeader colLN = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colReasonName = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colReasonCode = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colReasonId = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colReasonNameChs = new ColumnHeader();
            Gizmox.WebGUI.Forms.ColumnHeader colReasonNameCht = new ColumnHeader();

            // 
            // colLN
            // 
            colLN.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Center;
            colLN.Image = null;
            colLN.Text = Utility.Dictionary.GetWord("LN");
            colLN.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Center;
            colLN.Width = 30;
            // 
            // colReasonName
            // 
            colReasonName.Image = null;
            colReasonName.Text = Utility.Dictionary.GetWord("description");
            colReasonName.Width = 120;
            // 
            // colReasonCode
            // 
            colReasonCode.ContentAlign = Gizmox.WebGUI.Forms.ExtendedHorizontalAlignment.Left;
            colReasonCode.Image = null;
            colReasonCode.Text = Utility.Dictionary.GetWord("Code");
            colReasonCode.Width = 80;
            // 
            // colReasonId
            // 
            colReasonId.Image = null;
            colReasonId.Text = "ReasonId";
            colReasonId.Visible = false;
            colReasonId.Width = 150;
            // 
            // colReasonNameChs
            // 
            colReasonNameChs.Image = null;
            colReasonNameChs.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Chs") + ")";
            colReasonNameChs.Width = 120;
            // 
            // colReasonNameCht
            // 
            colReasonNameCht.Image = null;
            colReasonNameCht.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Cht") + ")";
            colReasonNameCht.Width = 120;

            lvList.Columns.AddRange(new Gizmox.WebGUI.Forms.ColumnHeader[] {
            colReasonCode,
            colLN,
            colReasonId,
            colReasonName,
            colReasonNameChs,
            colReasonNameCht});
        }
        #endregion

        /// <summary>
        /// Binds the reason list.
        /// </summary>
        /// 
        private void SetLvwList()
        {
            // 提供一個固定的 Guid tag， 在 UserPreference 中用作這個 ListView 的 unique key
            lvList.Tag = new Guid("{35EA7DBA-0C67-42ef-8979-297DA0E34167}");

            RT2008.Controls.Preference.Load(ref lvList);
        }
        
        private void BindReasonList()
        {
            this.lvList.Items.Clear();

            int iCount = 0;
            string query = "ReasonCode LIKE '%" + SearchForText + "%'";
            PriceManagementReasonCollection objReasonList = PriceManagementReason.LoadCollection(query, new string[] { "ReasonCode" }, true);
            for (int i = 0; i < objReasonList.Count; i++)
            {
                PriceManagementReason objReason = objReasonList[i];
                iCount = i + 1;

                ListViewItem objItem = this.lvList.Items.Add(objReason.ReasonCode);
                objItem.SmallImage = new IconResourceHandle("16x16.flag_green.png");
                objItem.LargeImage = new IconResourceHandle("16x16.flag_green.png");

                objItem.SubItems.Add(iCount.ToString()); // Line Number
                objItem.SubItems.Add(objReason.ReasonId.ToString());
                objItem.SubItems.Add(objReason.ReasonName);
                objItem.SubItems.Add(objReason.ReasonName_Chs);
                objItem.SubItems.Add(objReason.ReasonName_Cht);
            }

            this.lvList.Sort(); // 依照當前的 ListView.SortOrder 和 ListView.SortPosition 排序，使 UserPreference 有效

        }

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
                    ReasonCodeWizard wizReason = new ReasonCodeWizard(new System.Guid(lvList.SelectedItem.SubItems[2].Text));
                    wizReason.Closed += new EventHandler(wizReason_Closed);
                    wizReason.ShowDialog();
                }
            }
        }

        void wizReason_Closed(object sender, EventArgs e)
        {
            ReasonCodeWizard wizReason = sender as ReasonCodeWizard;
            if (wizReason.ReasonId != System.Guid.Empty)
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