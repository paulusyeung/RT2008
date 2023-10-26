#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Common.Interfaces;
using System.Diagnostics;
using System.Reflection;
using RT2008.Controls;
using RT2008.DAL;
using System.Web.Caching;
using RT2008.Components.Layout;

#endregion

namespace RT2008
{
    public partial class Desktop : Form
    {
        private enum AtsStyle { Inventory, Purchasing, Product, MemberMgmt, PriceMgmt, Settings };

        public Desktop()
        {
            InitializeComponent();

            SetTheme();
            SetAttributes();
            //SetMenuItems();
            SetNavPanes();

            SetAppToolStrip(AtsStyle.Inventory);

            //SetCloseButton();
        }

        private void SetAttributes()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            amsFile.Text = oDict.GetWord("file_main_menu");
            amsView.Text = oDict.GetWord("view_main_menu");
            amsViewChs.Text = oDict.GetWord("chs_main_menu");
            amsViewCht.Text = oDict.GetWord("cht_main_menu");
            amsHelp.Text = oDict.GetWord("help_main_menu");
            amsFileExit.Text = oDict.GetWord("exit_main_menu");
            amsHelpAbout.Text = oDict.GetWord("about_main_menu");

            navTabs.Dock = DockStyle.Fill;

            wspPane.Text = oDict.GetWord("dashboard");
            wspPane.Dock = DockStyle.Fill;

            this.Menu = null;
            atsPane.Height = 68;
            //atsPane.Padding = new Padding(16);
            //
            SetLogo();
            //SetTaskbar();
            //
            //var oTaskbar = new RT2008.Controls.Taskbar();
            //oTaskbar.Controls[0].Dock = DockStyle.Fill;
            //oTaskbar.Dock = DockStyle.Fill;
            //atsPane.Controls.Add(oTaskbar);
            //
            var oTopPane = new TopPane();
            oTopPane.Dock = DockStyle.Fill;
            atsPane.Controls.Add(oTopPane);
        }

        /// <summary>
        /// 2020.08.19 paulus:
        /// 
        /// </summary>
        private void SetLogo()
        { 
            var icon = new Button()
            {
                //Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None,
                //BackColor = Color.Transparent,
                //ButtonStyle = ButtonStyle.Custom,
                //CustomStyle = "Flat",
                Image = new IconResourceHandle("24x24.Settings_24x24.gif"),
                ImageAlign = ContentAlignment.MiddleCenter,
                Location = new Point(16, 16),
                Size = new Size(36, 36),
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = true
            };
            var logo = new PictureBox()
            {
                BackgroundImage = new ImageResourceHandle("RT2020.logo.jpg"),
                BackgroundImageLayout = ImageLayout.Zoom,
                Location = new Point(0,0),
                Size = new Size(150, 68)
            };
            atsPane.Controls.Add(logo);
        }

        private void SetTaskbar()
        {
            var taskbar = new Panel()
            {
                Dock = DockStyle.Fill
            };
            var user = new Button()
            {
                Anchor = AnchorStyles.Right,
                Image = new IconResourceHandle("24x24.Settings_24x24.gif"),
                ImageAlign = ContentAlignment.MiddleCenter,
                Location = new Point(16, 0),
                Size = new Size(36, 36),
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = true
            };
            var theme = new Button()
            {
                Anchor = AnchorStyles.Right,
                Image = new IconResourceHandle("24x24.Services_24x24.gif"),
                ImageAlign = ContentAlignment.MiddleCenter,
                Location = new Point(56, 0),
                Size = new Size(36, 36),
                TextImageRelation = TextImageRelation.ImageBeforeText,
                UseVisualStyleBackColor = true
            };
            taskbar.Controls.AddRange(new Control[] { user, theme });
            atsPane.Controls.Add(taskbar);
        }

        private Panel GetLogoBlock()
        {
            var panel = new Gizmox.WebGUI.Forms.Panel();
            panel.Size = new System.Drawing.Size(129, 68);
            var icon = new Gizmox.WebGUI.Forms.Button();
            icon.Image = new IconResourceHandle("24x24.Settings_24x24.gif");
            var img = new Gizmox.WebGUI.Forms.PictureBox();
            img.BackgroundImage = new ImageResourceHandle("RT2020.jpg");

            return panel;
        }

        private void SetTheme()
        {
            ImageResourceHandle bgImage = new ImageResourceHandle("RT2020.jpg");

            //this.picBgImage.Image = bgImage;
            this.picBgImage.Dock = DockStyle.Fill;
            this.picBgImage.BackgroundImage = bgImage;
            this.picBgImage.BackgroundImageLayout = ImageLayout.Center;

            /**
             * 2020.03.20 paulus: 根據個 theme 改個 background color
             */
            Context.CurrentTheme = RT2008.Controls.Utility.Default.CurrentTheme;
            wspPane.BackColor = RT2008.Controls.Utility.Default.TopPanelBackgroundColor;
        }

        /// <summary>
        /// 2020.0812 paulus: 手動砌 Menu
        /// 先將 designer 嘅 MenuItems 取消，然後利用 NavMenu.xml 一個一個砌
        /// </summary>
        private void SetMenuItems()
        {
            amsMain.MenuItems.Clear();

            var oRT2020 = MenuStrip.RT2020Ams.GetLoyatyAms();
            var oInventory = MenuStrip.InventoryAms.GetLoyatyAms();
            var oPurchasing = MenuStrip.PurchaseAms.GetLoyatyAms();
            var oProduct = MenuStrip.ProductAms.GetLoyatyAms();
            var oLoyaty = MenuStrip.LoyaltyAms.GetLoyatyAms();
            var oSettings = MenuStrip.SettingsAms.GetLoyatyAms();
            var oTools = MenuStrip.ToolsAms.GetLoyatyAms();
            var oHelp = MenuStrip.HelpAms.GetLoyatyAms();
            var oFile = MenuStrip.FileAms.GetLoyatyAms();
            var oView = MenuStrip.ViewAms.GetLoyatyAms();

            amsMain.MenuItems.AddRange(new Gizmox.WebGUI.Forms.MenuItem[] {
                oRT2020, oView, oInventory, oPurchasing, oProduct, oLoyaty, oSettings, oTools });
        }

        /// <summary>
        /// 2020.08.19 paulus: 取消 module Ats
        /// </summary>
        /// <param name="index"></param>
        private void SetAppToolStrip(AtsStyle index)
        {
            return;

            this.atsPane.Controls.Clear();

            switch (index)
            {
                case AtsStyle.Inventory:
                    RT2008.AtsPane.InventoryAts oAtsInvt = new RT2008.AtsPane.InventoryAts();
                    oAtsInvt.Dock = DockStyle.Fill;
                    this.atsPane.Controls.Add(oAtsInvt);
                    navTabs.SelectedIndex = 0;
                    break;
                case AtsStyle.Purchasing:
                    RT2008.AtsPane.PurchasingAts oAtsPurchasing = new RT2008.AtsPane.PurchasingAts();
                    oAtsPurchasing.Dock = DockStyle.Fill;
                    this.atsPane.Controls.Add(oAtsPurchasing);
                    navTabs.SelectedIndex = 1;
                    break;
                case AtsStyle.Product:
                    //RT2008.AtsPane.PurchasingAts oAtsPurchasing = new RT2008.AtsPane.PurchasingAts();
                    //oAtsPurchasing.Dock = DockStyle.Fill;
                    //this.atsPane.Controls.Add(oAtsPurchasing);
                    navTabs.SelectedIndex = 2;
                    break;
                case AtsStyle.MemberMgmt:
                    RT2008.AtsPane.MemberMgmtAts oAtsMemberMgmt = new RT2008.AtsPane.MemberMgmtAts();
                    oAtsMemberMgmt.Dock = DockStyle.Fill;
                    this.atsPane.Controls.Add(oAtsMemberMgmt);
                    navTabs.SelectedIndex = 3;
                    break;
                case AtsStyle.Settings:
                    RT2008.AtsPane.SettingsAts oAtsSettings = new RT2008.AtsPane.SettingsAts();
                    oAtsSettings.Dock = DockStyle.Fill;
                    this.atsPane.Controls.Add(oAtsSettings);
                    navTabs.SelectedIndex = 4;
                    break;
            }
        }

        private void SetNavPanes()
        {
            RT2008.NavPane.NavMenu.FillNavPane(ref this.navTabs);

            //RT2008.NavPane.InventoryNav navInvt = new RT2008.NavPane.InventoryNav();
            //navInvt.Dock = DockStyle.Fill;
            //tabInvt.Controls.Add(navInvt);

            //RT2008.NavPane.PurchasingNav navPurchasing = new RT2008.NavPane.PurchasingNav();
            //navPurchasing.Dock = DockStyle.Fill;
            //tabPurchasing.Controls.Add(navPurchasing);

            //RT2008.NavPane.MemberMgmtNav navMemberMgmt = new RT2008.NavPane.MemberMgmtNav();
            //navMemberMgmt.Dock = DockStyle.Fill;
            //tabMemberMgmt.Controls.Add(navMemberMgmt);

            //RT2008.NavPane.SettingsNav navSettings = new RT2008.NavPane.SettingsNav();
            //navSettings.Dock = DockStyle.Fill;
            //tabSettings.Controls.Add(navSettings);
        }

        #region Close Button
        private void SetCloseButton()
        {
            Button cmdClose = new Button();
            cmdClose.Name = "cmdClose";
            cmdClose.Location = new System.Drawing.Point(this.Width - 43, 3);
            cmdClose.Size = new System.Drawing.Size(38, 38);
            cmdClose.Image = new IconResourceHandle("32x32.shutdown32.png");
            cmdClose.ImageAlign = ContentAlignment.MiddleCenter;
            cmdClose.TextImageRelation = Gizmox.WebGUI.Forms.TextImageRelation.ImageAboveText;
            cmdClose.Anchor = ((Gizmox.WebGUI.Forms.AnchorStyles)((Gizmox.WebGUI.Forms.AnchorStyles.Top | Gizmox.WebGUI.Forms.AnchorStyles.Right)));

            cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            this.Controls.Add(cmdClose);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Shutdown();
        }

        private void Shutdown()
        {
            DAL.Common.Config.CurrentUserId = System.Guid.Empty;

            RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Logout, this.ToString());

            // set the IsLoggedOn to false will redirect to Logon Page.
            this.Context.Session.IsLoggedOn = false;
            VWGContext.Current.HttpContext.Session.Abandon();
            //VWGContext.Current.Transfer(new Desktop());
            // 2020.0812 paulus: 直接跳去 logon
            VWGContext.Current.Transfer(new Public.Logon());
        }
        #endregion

        private void amsMain_MenuClick(object objSource, MenuItemEventArgs objArgs)
        {
            MenuItemEventArgs oArg = (MenuItemEventArgs)objArgs;
            string strAction = oArg.MenuItem.Tag as string;
            if (strAction != null)
            {
                switch (strAction)
                {
                    #region Section: File
                    case "amsFileExit":
                    case "File.Exit":
                    case "RT2020.Quit":
                        //RT2008.DAL.Common.Config.CurrentUserId = System.Guid.Empty;
                        // While setting the IsLoggedOn to false, will redirect to Logon Page.
                        //this.Context.Session.IsLoggedOn = false;
                        // VWGContext.Current.HttpContext.Session.Abandon();
                        //Context.Redirect("Desktop.wgx");
                        Shutdown();
                        break;
                    case "Print":
//                        MessageBox.Show(((Gizmox.WebGUI.Common.Interfaces.ISessionRegistry)this.Context.Session).Count.ToString());
                        break;
                    #endregion

                    #region Section: Help
                    case "amsHelpAbout":
                    case "Help.About":
                    case "RT2020.About":
                        Help.About oAbout = new RT2008.Help.About();
                        oAbout.ShowDialog();
                        break;
                    #endregion
                    
                    #region Section View
                    case "amsViewEn": // English
                    case "View.English":
                        //VWGContext.Current.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
                        System.Web.HttpContext.Current.Session["UserLanguage"] = "en-US";
                        Context.Redirect("Desktop.wgx");
                        break;
                    case "amsViewChs": // Simplified Chinese
                    case "View.SimplifiedChinese":
                        //VWGContext.Current.CurrentUICulture = new System.Globalization.CultureInfo("zh-CHS");
                        System.Web.HttpContext.Current.Session["UserLanguage"] = "zh-CHS";
                        Context.Redirect("Desktop.wgx");
                        break;
                    case "amsViewCht": // Tradictional Chinese
                    case "View.TraditionalChinese":
                        //VWGContext.Current.CurrentUICulture = new System.Globalization.CultureInfo("zh-CHT");
                        System.Web.HttpContext.Current.Session["UserLanguage"] = "zh-CHT";
                        Context.Redirect("Desktop.wgx");
                        break;
                    case "amsViewWinXP":
                    case "View.WinXPTheme":
                        this.Context.CurrentTheme = "iOS";          // Theme.Default;
                        break;
                    case "amsViewVista":
                    case "View.VistaTheme":
                        this.Context.CurrentTheme = "Vista";        // new Theme("Vista");
                        break;
                    case "amsViewBlack":
                    case "View.BlackTheme":
                        this.Context.CurrentTheme = "Graphite";     // new Theme("Black");
                        break;
                    #endregion

                    default:
                        //MessageBox.Show(strAction);
                        break;
                }
            }
        }

        #region Deselect selected TreeNodes on switching navTabs
        private void DeSelectTreeNodes()
        {
            Control[] invt = this.Form.Controls.Find("navInvt", true);
            if (invt.Length > 0)
            {
                TreeView tvInvt = (TreeView)invt[0];
                tvInvt.SelectedNode = null;
            }
            Control[] purchase = this.Form.Controls.Find("navPurchasing", true);
            if (purchase.Length > 0)
            {
                TreeView tvInvt = (TreeView)purchase[0];
                tvInvt.SelectedNode = null;
            }
            Control[] mbrMgmt = this.Form.Controls.Find("navMemberMgmt", true);
            if (mbrMgmt.Length > 0)
            {
                TreeView tvInvt = (TreeView)mbrMgmt[0];
                tvInvt.SelectedNode = null;
            }
            Control[] prcMgmt = this.Form.Controls.Find("navPriceMgmt", true);
            if (prcMgmt.Length > 0)
            {
                TreeView tvInvt = (TreeView)prcMgmt[0];
                tvInvt.SelectedNode = null;
            }
            Control[] settings = this.Form.Controls.Find("navSettings", true);
            if (settings.Length > 0)
            {
                TreeView tvInvt = (TreeView)settings[0];
                tvInvt.SelectedNode = null;
            }
        }
        #endregion

        private void navTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeSelectTreeNodes();

            if (navTabs.SelectedItem != null)
            {
                if (navTabs.SelectedItem.Tag != null)
                {
                    switch (navTabs.SelectedItem.Tag.ToString().ToLower())
                    {
                        case "inventory":
                            SetAppToolStrip(AtsStyle.Inventory);
                            break;
                        case "purchasing":
                            SetAppToolStrip(AtsStyle.Purchasing);
                            break;
                        case "product care":
                            SetAppToolStrip(AtsStyle.Product);
                            break;
                        case "member":
                            SetAppToolStrip(AtsStyle.MemberMgmt);
                            break;
                        case "settings":
                            SetAppToolStrip(AtsStyle.Settings);
                            break;
                    }
                }
            }
        }
    }
}