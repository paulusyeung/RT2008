#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using RT2008.Controls;
using System.Data.SqlClient;
using RT2008.DAL;
using Gizmox.WebGUI.Forms.Dialogs;

#endregion

namespace RT2008.Controls
{
    public abstract partial class DefaultListBase : UserControl, IRTList
    {
        public event MenuEventHandler ExportClick;
        public event EventHandler RefreshClick;
        public event EventHandler PreferenceClick;
        public event EventHandler BindingListDoubleClick;
        public event EventHandler ComboBoxSelectedIndexChanged;
        public event EventHandler ButtonClick;
        public event EventHandler ShowClick;

        public DefaultListBase()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();

                SetAttribute();
                SetTheme();
                FillStaffList();
                SetAnsCare();

                //2013.07.05 paulus: 不用一開始就 load data
                //BindList();
            }
        }

        #region Properties

        public string AlphaSeacher { get; set; }

        public string SearchForText
        {
            get
            {
                return txtLookup.Text.Trim();
            }
        }

        public Guid SelectedStaff
        {
            get
            {
                if (RT2008.DAL.Common.Utility.IsGUID(cboStaffList.SelectedValue.ToString()))
                {
                    return new Guid(cboStaffList.SelectedValue.ToString());
                }
                else
                {
                    return System.Guid.Empty;
                }
            }
        }

        public int SelectedViewIndex
        {
            get
            {
                return cboView.SelectedIndex;
            }
        }

        #endregion

        #region Set Attributes, Themes

        /// <summary>
        /// Sets the attribute.
        /// </summary>
        private void SetAttribute()
        {
            RT2008.Controls.Utility.BindingViewCombo(ref cboView);

            lblLookup.Text = Utility.Dictionary.GetWordWithColon("Look For");
            lblCreatedBy.Text = Utility.Dictionary.GetWordWithColon("CreatedBy");
            lblView.Text = Utility.Dictionary.GetWordWithColon("View");

            txtLookup.EnterKeyDown += new KeyEventHandler(this.cmdLookup_Click);

            this.lvList.ListViewItemSorter = new ListViewItemSorter(this.lvList);
            this.lvList.Dock = DockStyle.Fill;
            this.lvList.GridLines = true;

        }

        /// <summary>
        /// Sets the theme.
        /// </summary>
        private void SetTheme()
        {
            this.BackColor = Color.FromName("#ACC0E9");
        }

        /// <summary>
        /// Fills the staff list.
        /// </summary>
        private void FillStaffList()
        {
            cboStaffList.Items.Clear();

            RT2008.DAL.Staff.LoadCombo(ref cboStaffList, new string[] { "StaffNumber", "FullName" }, "{0} - {1}", true, true, "All", "Retired = 0", null);

            cboStaffList.SelectedIndex = 0;
        }

        #endregion

        #region Set Action Strip
        private void SetAnsCare()
        {
            this.ansCare.MenuHandle = false;
            this.ansCare.DragHandle = false;
            this.ansCare.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            #region cmdButtons   - Buttons [0~3]

            this.ansCare.Buttons.Add(new ToolBarButton("Columns", String.Empty));
            this.ansCare.Buttons[0].Image = new IconResourceHandle("16x16.listview_columns.gif");
            this.ansCare.Buttons[0].ToolTipText = @"Hide/Unhide Columns";
            this.ansCare.Buttons.Add(new ToolBarButton("Sorting", String.Empty));
            this.ansCare.Buttons[1].Image = new IconResourceHandle("16x16.listview_sorting.gif");
            this.ansCare.Buttons[1].ToolTipText = @"Sorting";
            this.ansCare.Buttons.Add(new ToolBarButton("Checkbox", String.Empty));
            this.ansCare.Buttons[2].Image = new IconResourceHandle("16x16.listview_checkbox.gif");
            this.ansCare.Buttons[2].ToolTipText = @"Toggle Checkbox";
            this.ansCare.Buttons[2].Visible = true;
            this.ansCare.Buttons.Add(new ToolBarButton("MultiSelect", String.Empty));
            this.ansCare.Buttons[3].Image = new IconResourceHandle("16x16.listview_multiselect.gif");
            this.ansCare.Buttons[3].ToolTipText = @"Toggle Multi-Select";
            this.ansCare.Buttons[3].Visible = false;

            #endregion

            this.ansCare.Buttons.Add(sep);

            #region cmdViews    - Buttons[5]

            ContextMenu ddlViews = new ContextMenu();
            ddlViews.MenuItems.Add(new MenuItem(Utility.Dictionary.GetWord("Icon"), string.Empty, "Icon"));
            ddlViews.MenuItems.Add(new MenuItem(Utility.Dictionary.GetWord("Tile"), string.Empty, "Tile"));
            ddlViews.MenuItems.Add(new MenuItem(Utility.Dictionary.GetWord("List"), string.Empty, "List"));
            ddlViews.MenuItems.Add(new MenuItem(Utility.Dictionary.GetWord("Details"), string.Empty, "Details"));

            ddlViews.MenuItems[0].Icon = new IconResourceHandle("16x16.appView_icons.png");
            ddlViews.MenuItems[1].Icon = new IconResourceHandle("16x16.appView_tile.png");
            ddlViews.MenuItems[2].Icon = new IconResourceHandle("16x16.appView_columns.png");
            ddlViews.MenuItems[3].Icon = new IconResourceHandle("16x16.appView_list.png");

            ToolBarButton cmdViews = new ToolBarButton("Views", Utility.Dictionary.GetWord("Views"));
            cmdViews.Style = ToolBarButtonStyle.DropDownButton;
            cmdViews.Image = new IconResourceHandle("16x16.appView_xp.png");
            cmdViews.DropDownMenu = ddlViews;
            this.ansCare.Buttons.Add(cmdViews);
            cmdViews.MenuClick += new MenuEventHandler(ansViews_MenuClick);

            #endregion

            #region cmdPreference    - Buttons[6]
            ContextMenu ddlPreference = new ContextMenu();
            RT2008.Controls.Data.AppendMenuItem_AppPref(ref ddlPreference);
            ToolBarButton cmdPreference = new ToolBarButton("Preference", Utility.Dictionary.GetWord("Preference"));
            cmdPreference.Style = ToolBarButtonStyle.DropDownButton;
            cmdPreference.Image = new IconResourceHandle("16x16.ico_16_1039_default.gif");
            cmdPreference.DropDownMenu = ddlPreference;
            this.ansCare.Buttons.Add(cmdPreference);
            cmdPreference.MenuClick += new MenuEventHandler(ansPreference_MenuClick);
            #endregion

            this.ansCare.Buttons.Add(sep);

            // Helper       - Buttons[8]
            this.ansCare.Buttons.Add(new ToolBarButton("Refresh", Utility.Dictionary.GetWord("Refresh")));
            this.ansCare.Buttons[8].Image = new IconResourceHandle("16x16.16_L_refresh.gif");

            #region cmdExport    - Buttons[9]

            ContextMenu ddlExport = new ContextMenu();
            ddlExport.MenuItems.Add(new MenuItem("PDF"));
            ddlExport.MenuItems.Add(new MenuItem("Excel"));
            ddlExport.MenuItems.Add(new MenuItem("CSV"));
            ddlExport.MenuItems.Add(new MenuItem("UIFD"));

            ToolBarButton cmdExport = new ToolBarButton("Export", string.Empty);
            cmdExport.Style = ToolBarButtonStyle.DropDownButton;
            cmdExport.Image = new IconResourceHandle("16x16.16_export.gif");
            cmdExport.DropDownMenu = ddlExport;
            cmdExport.Enabled = false;
            this.ansCare.Buttons.Add(cmdExport);
            cmdExport.MenuClick += new MenuEventHandler(ansExport_MenuClick);

            #endregion

            this.ansCare.Buttons.Add(new ToolBarButton("Show", Utility.Dictionary.GetWord("Show")));
            this.ansCare.Buttons[10].Image = new IconResourceHandle("16x16.ico_18_127.gif");
            this.ansCare.ButtonClick += new ToolBarButtonClickEventHandler(ansCare_ButtonClick);

            this.ansCare.Buttons.Add(sep);

       }

        void ansCare_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Button.Name))
            {
                switch (e.Button.Name.ToLower())
                {
                    case "refresh":
                        this.RefreshClick(e.Button, new EventArgs());
                        this.Update();
                        break;
                    case "preference":
                        this.PreferenceClick(e.Button, new EventArgs());
                        this.Update();
                        break;
                    case "columns":
                        ListViewColumnOptions objListViewColumnOptions = new ListViewColumnOptions(this.lvList);
                        objListViewColumnOptions.ShowDialog();
                        break;
                    case "sorting":
                        ListViewSortingOptions objListViewSortingOptions = new ListViewSortingOptions(this.lvList);
                        objListViewSortingOptions.ShowDialog();
                        break;
                    case "checkbox":
                        this.lvList.CheckBoxes = !this.lvList.CheckBoxes;
                        break;
                    case "multiselect":
                        this.lvList.MultiSelect = !this.lvList.MultiSelect;
                        e.Button.Pushed = true;
                        break;
                    case "show":
                        this.ShowClick(e.Button, new EventArgs());
                        break;
                }
            }
        }

        private void ansExport_MenuClick(object sender, MenuItemEventArgs e)
        {
            this.ExportClick(sender, e);
        }

        private void ansViews_MenuClick(object sender, MenuItemEventArgs e)
        {
            if (e.MenuItem.Tag != null)
            {
                switch (e.MenuItem.Tag.ToString())
                {
                    case "Icon":
                        this.lvList.View = View.SmallIcon;
                        break;
                    case "Tile":
                        this.lvList.View = View.LargeIcon;
                        break;
                    case "List":
                        this.lvList.View = View.List;
                        break;
                    case "Details":
                        this.lvList.View = View.Details;
                        break;
                }
            }
        }

        private void ansPreference_MenuClick(object sender, MenuItemEventArgs e)
        {
            switch (e.MenuItem.Tag.ToString())
            {
                case "Save":
                    RT2008.Controls.Preference.Save(lvList);
                    break;
                case "Reset":
                    RT2008.Controls.Preference.Delete(lvList);
                    break;
            }
            MessageBox.Show(Utility.Dictionary.GetWord("finish"));
        }
        #endregion

        #region Bind List

        /// <summary>
        /// Binds the Sales list.
        /// </summary>
        public virtual void BindList()
        {
        }

        #endregion

        #region IRTList Members

        /// <summary>
        /// Binds the RT list.
        /// </summary>
        /// <param name="bFind">if set to <c>true</c> [b find].</param>
        public void BindRTList(bool bFind)
        {
            this.BindList();
        }

        #endregion

        private void lvSalesList_DoubleClick(object sender, EventArgs e)
        {
            this.BindingListDoubleClick(sender, e);
        }

        private void cboView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ComboBoxSelectedIndexChanged(sender, e);
        }

        private void cmdLookup_Click(object sender, EventArgs e)
        {
            this.ButtonClick(sender, e);
        }

        private void alphaBinding_ButtonClick(object sender, EventArgs e)
        {
            this.AlphaSeacher = ((Button)sender).Tag.ToString();
            this.BindList();
        }
    }
}