#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

#endregion

namespace RT2008.NavPane
{
    public partial class SettingsNav : UserControl
    {
        public SettingsNav()
        {
            InitializeComponent();

            NavPane.NavMenu.FillNavTree("settings", this.navSettings.Nodes);
        }

        private void navSettings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = navSettings.SelectedNode.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)navSettings.SelectedNode.Tag);
            }
        }

        private void ShowWorkspace(ref Panel wspPane, string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                switch (Tag.ToLower())
                {
                    case "staffcare":
                        //RT2008.Staff.StaffList oStaffCare = new RT2008.Staff.StaffList();
                        //oStaffCare.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oStaffCare);

                        RT2008.Staff.DefaultList oStaffList = new RT2008.Staff.DefaultList();
                        oStaffList.DockPadding.All = 6;
                        oStaffList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oStaffList);
                        break;
                    case "settings_supplier":
                        //RT2008.Supplier.SupplierList oSupplierList = new RT2008.Supplier.SupplierList();
                        //oSupplierList.DockPadding.All = 6;
                        //oSupplierList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oSupplierList);

                        RT2008.Supplier.DefaultList oSupplierList = new RT2008.Supplier.DefaultList();
                        oSupplierList.DockPadding.All = 6;
                        oSupplierList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oSupplierList);
                        break;
                    case "settings_workplace":
                        //RT2008.Workplace.WorkplaceList oWorkplaceList = new RT2008.Workplace.WorkplaceList();
                        //oWorkplaceList.DockPadding.All = 6;
                        //oWorkplaceList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oWorkplaceList);

                        RT2008.Workplace.DefaultList oWorkplaceList = new RT2008.Workplace.DefaultList();
                        oWorkplaceList.DockPadding.All = 6;
                        oWorkplaceList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oWorkplaceList);
                        break;
                    case "system_label":
                        RT2008.Settings.SystemLabels oSysLabel = new RT2008.Settings.SystemLabels();
                        oSysLabel.Dock = DockStyle.Fill;
                        oSysLabel.DockPadding.All = 6;
                        wspPane.Controls.Add(oSysLabel);
                        break;
                    case "system_info":
                        RT2008.Settings.SystemInfoForm oSysInfo = new RT2008.Settings.SystemInfoForm();
                        oSysInfo.Dock = DockStyle.Fill;
                        oSysInfo.DockPadding.All = 6;
                        wspPane.Controls.Add(oSysInfo);
                        break;
                    case "system_monthend":
                        RT2008.Settings.SystemMonthEnd oSysMe = new RT2008.Settings.SystemMonthEnd();
                        oSysMe.Dock = DockStyle.Fill;
                        oSysMe.DockPadding.All = 6;
                        wspPane.Controls.Add(oSysMe);
                        break;
                    case "system_security":
                        RT2008.Settings.SystemSecurity oSysSec = new RT2008.Settings.SystemSecurity();
                        oSysSec.Dock = DockStyle.Fill;
                        oSysSec.DockPadding.All = 6;
                        wspPane.Controls.Add(oSysSec);
                        break;
                }
            }
        }
    }
}