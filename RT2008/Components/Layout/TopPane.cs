#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using RT2008.Controls;
using RT2008.DAL;

#endregion

namespace RT2008.Components.Layout
{
    public partial class TopPane : UserControl
    {
        private ContextMenu _LanguageMenu = new ContextMenu();

        public TopPane()
        {
            InitializeComponent();
        }

        private void TopPane_Load(object sender, EventArgs e)
        {
            SetAttributes();
        }

        /// <summary>
        /// 為咗方便 designer view，有啲 Attributes 喺 codebehide 搞
        /// </summary>
        private void SetAttributes()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            toolTip.SetToolTip(butPower, "Logout");
            toolTip.SetToolTip(butLanguage, "Switch Language");
            toolTip.SetToolTip(butTheme, "Toggle Theme");
            toolTip.SetToolTip(butDrawer, "Toggle Navigation Drawer");

            picLogo.BackgroundImage = new ImageResourceHandle("RT2020.Logo.jpg");
            picLogo.BackgroundImageLayout = ImageLayout.Zoom;
            picLogo.Click += Logo_OnClick;
            picLogo.MouseClick += PicLogo_MouseClick;

            pnlTaskbarLeft.Dock = DockStyle.Left;
            butDrawer.Image = new IconResourceHandle("24x24.power_24.png");
            butDrawer.Click += Drawer_OnClick;

            pnlTaskbarRight.Dock = DockStyle.Fill;
            butPower.Image = new IconResourceHandle("24x24.power_24.png");
            butPower.Click += Power_OnClick;
            //
            butTheme.Image = new IconResourceHandle("24x24.half.png");
            butTheme.Click += Theme_OnClick;
            //
            butLanguage.Image = new IconResourceHandle("24x24.earth.png");
            SetLanguageMenu();
        }

        private void PicLogo_MouseClick(object sender, MouseEventArgs e)
        {
            Help.About oAbout = new RT2008.Help.About();
            oAbout.ShowDialog();
        }

        private void SetLanguageMenu()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            _LanguageMenu.AllowDrop = true;

            MenuItem en = new MenuItem()
            {
                Index = 0,
                Icon = new IconResourceHandle("16x16.English16.gif"),
                Tag = "en",
                Text = "English"
            };
            en.Click += Language_OnClick;

            MenuItem chs = new MenuItem()
            {
                Index = 1,
                Icon = new IconResourceHandle("16x16.SimpChinese16.gif"),
                Tag = "chs",
                Text = oDict.GetWord("chs_main_menu")
        };
            chs.Click += Language_OnClick;

            MenuItem cht = new MenuItem()
            {
                Index = 2,
                Icon = new IconResourceHandle("16x16.TradChinese16.gif"),
                Tag = "cht",
                Text = oDict.GetWord("cht_main_menu")
        };
            cht.Click += Language_OnClick;

            _LanguageMenu.MenuItems.AddRange(new MenuItem[] { en, chs, cht });
            butLanguage.DropDownMenu = _LanguageMenu;
        }

        private void Logo_OnClick(object sender, EventArgs e)
        {
            Help.About oAbout = new RT2008.Help.About();
            oAbout.ShowDialog();
        }

        private void Language_OnClick(object sender, EventArgs e)
        {
            var target = (MenuItem)sender;
            switch (((String)target.Tag).ToLower())
            {
                case "chs":
                    System.Web.HttpContext.Current.Session["UserLanguage"] = "zh-CHS";
                    Context.Redirect("Desktop.wgx");
                    break;
                case "cht":
                    System.Web.HttpContext.Current.Session["UserLanguage"] = "zh-CHT";
                    Context.Redirect("Desktop.wgx");
                    break;
                case "en":
                default:
                    System.Web.HttpContext.Current.Session["UserLanguage"] = "en-US";
                    Context.Redirect("Desktop.wgx");
                    break;
            }
        }

        private void Theme_OnClick(object sender, EventArgs e)
        {
            if (Utility.Default.CurrentTheme.ToLower() == "vista")
            {
                VWGContext.Current.CurrentTheme = "Graphite";
                //this.Context.CurrentTheme = "Graphite";
                Utility.Default.CurrentTheme = "Graphite";
            }
            else
            {
                VWGContext.Current.CurrentTheme = "Vista";
                //this.Context.CurrentTheme = "Vista";
                Utility.Default.CurrentTheme = "Vista";
            }
        }

        private void Power_OnClick(object sender, EventArgs e)
        {
            DAL.Common.Config.CurrentUserId = Guid.Empty;

            Log4net.LogInfo(Log4net.LogAction.Logout, this.ToString());

            // set the IsLoggedOn to false will redirect to Logon Page.
            this.Context.Session.IsLoggedOn = false;
            VWGContext.Current.HttpContext.Session.Abandon();

            VWGContext.Current.Transfer(new Public.Logon());
        }

        private void Drawer_OnClick(object sender, EventArgs e)
        {
            Help.About oAbout = new RT2008.Help.About();
            oAbout.ShowDialog();
        }
    }
}