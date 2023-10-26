#region Using

using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;
using RT2008.DAL;
using RT2008.Controls;

#endregion Using

namespace RT2008.AtsPane
{
    public partial class SettingsAts : UserControl
    {
        public SettingsAts()
        {
            InitializeComponent();

            SetAtsSettings();
        }

        private void SetAtsSettings()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            this.atsSettings.MenuHandle = false;
            this.atsSettings.DragHandle = false;
            this.atsSettings.TextAlign = ToolBarTextAlign.Right;

            ContextMenu ddlNew = new ContextMenu();
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Staff"), string.Empty, "Staff"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Staff") + " " + oDict.GetWord("Dept."), string.Empty, "StaffDept"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Staff") + " " + oDict.GetWord("Group"), string.Empty, "StaffGroup"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Staff") + " " + oDict.GetWord("Job Title"), string.Empty, "StaffJobTitle"));
            ddlNew.MenuItems.Add(new MenuItem(string.Format(oDict.GetWord("Smart Tag"), oDict.GetWord("Staff")), string.Empty, "SmartTag4Staffe"));
            ddlNew.MenuItems.Add(new MenuItem("-"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Workplace"), string.Empty, "Workplace"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Workplace") + " " + oDict.GetWord("Nature"), string.Empty, "WorkplaceNature"));
            ddlNew.MenuItems.Add(new MenuItem(string.Format(oDict.GetWord("Smart Tag"), oDict.GetWord("Workplace")), string.Empty, "SmartTag4Workplace"));
            ddlNew.MenuItems.Add(new MenuItem("-"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Supplier"), string.Empty, "Supplier"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Supplier") + " " + oDict.GetWord("Address Type"), string.Empty, "SupplierAddressType"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Supplier") + " " + oDict.GetWord("Terms"), string.Empty, "SupplierTerms"));
            ddlNew.MenuItems.Add(new MenuItem(string.Format(oDict.GetWord("Smart Tag"), oDict.GetWord("Supplier")), string.Empty, "SmartTag4Supplier"));
            ddlNew.MenuItems.Add(new MenuItem("-"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Currency"), string.Empty, "Currency"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Payment Factor") + " (" + oDict.GetWord("Currency") + ")", string.Empty, "PaymentFactor_Currency"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Payment Factor") + " (" + oDict.GetWord("Event Code") + ")", string.Empty, "PaymentFactor_EventCode"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Country"), string.Empty, "Country"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Province"), string.Empty, "Province"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("City"), string.Empty, "City"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Zone"), string.Empty, "Zone"));
            ddlNew.MenuItems.Add(new MenuItem("-"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Internet Tag"), string.Empty, "InternetTag"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Line Of Operation"), string.Empty, "LineOfOperation"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Market Sector"), string.Empty, "MarketSector"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Salutation"), string.Empty, "Salutation"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Phone Tag"), string.Empty, "PhoneTag"));
            ddlNew.MenuItems.Add(new MenuItem(oDict.GetWord("Job Title"), string.Empty, "JobTitle"));

            ToolBarButton cmdNew = new ToolBarButton("New", oDict.GetWord("New"));
            cmdNew.Style = ToolBarButtonStyle.DropDownButton;
            cmdNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");
            cmdNew.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);
            cmdNew.DropDownMenu = ddlNew;

            this.atsSettings.Buttons.Add(cmdNew);
            cmdNew.MenuClick += new MenuEventHandler(AtsSettings_MenuClick);

            ContextMenu ddlReports = new ContextMenu();
            ddlReports.MenuItems.Add(new MenuItem(oDict.GetWord("Staff") + " " + oDict.GetWord("Reports"), string.Empty, "Staff Reports"));
            ddlReports.MenuItems.Add(new MenuItem(oDict.GetWord("Workplace") + " " + oDict.GetWord("Reports"), string.Empty, "Workplace Reports"));
            ddlReports.MenuItems.Add(new MenuItem(oDict.GetWord("Supplier") + " " + oDict.GetWord("Reports"), string.Empty, "Supplier Reports"));

            ToolBarButton cmdReports = new ToolBarButton("Reports", oDict.GetWord("Reports"));
            cmdReports.Style = ToolBarButtonStyle.DropDownButton;
            cmdReports.Image = new IconResourceHandle("16x16.16_reports.gif");
            cmdReports.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);
            cmdReports.DropDownMenu = ddlReports;

            this.atsSettings.Buttons.Add(cmdReports);
            cmdReports.MenuClick += new MenuEventHandler(AtsSettings_MenuClick);

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;
            this.atsSettings.Buttons.Add(sep);
        }

        private void AtsSettings_MenuClick(object sender, MenuItemEventArgs e)
        {
            //Control[] controls = this.Form.Controls.Find("wspPane", true);
            //if (controls.Length > 0)
            //{
            //    Panel wspPane = (Panel)controls[0];
            //    wspPane.Text = (string)e.MenuItem.Text;
            //}

            if (!(e.MenuItem.Tag == null))
            {
                switch (e.MenuItem.Tag.ToString().ToLower())
                {
                    case "staff":
                        Staff.StaffCode staffCode = new RT2008.Staff.StaffCode();
                        staffCode.ShowDialog();
                        break;
                    case "staffdept":
                        RT2008.Staff.StaffDeptWizard wizDept = new RT2008.Staff.StaffDeptWizard();
                        wizDept.ShowDialog();
                        break;
                    case "staffgroup":
                        RT2008.Staff.StaffGroupWizard wizGroup = new RT2008.Staff.StaffGroupWizard();
                        wizGroup.ShowDialog();
                        break;
                    case "staffjobtitle":
                        RT2008.Staff.StaffJobTitleWizard wizStaffJobTitle = new RT2008.Staff.StaffJobTitleWizard();
                        wizStaffJobTitle.ShowDialog();
                        break;
                    case "smarttag4staffe":
                        RT2008.Settings.SmartTag4StaffWizard wizSmartTag4Staffe = new RT2008.Settings.SmartTag4StaffWizard();
                        wizSmartTag4Staffe.ShowDialog();
                        break;
                    case "workplace":
                        RT2008.Workplace.WorkplaceCode workplace = new RT2008.Workplace.WorkplaceCode();
                        workplace.ShowDialog();
                        break;
                    case "workplacenature":
                        RT2008.Workplace.WorkplaceNatureWizard wizWorkplaceNature = new RT2008.Workplace.WorkplaceNatureWizard();
                        wizWorkplaceNature.ShowDialog();
                        break;
                    case "smarttag4workplace":
                        RT2008.Settings.SmartTag4WorkplaceWizard wizSmartTag4Workplace = new RT2008.Settings.SmartTag4WorkplaceWizard();
                        wizSmartTag4Workplace.ShowDialog();
                        break;
                    case "workplace reports":
                        RT2008.Workplace.Reports.RptWorkplaceList rptWorkplackList = new RT2008.Workplace.Reports.RptWorkplaceList();
                        rptWorkplackList.ShowDialog();
                        break;
                    case "staff reports":
                        RT2008.Staff.Reports.RptStaffList rptStaff = new RT2008.Staff.Reports.RptStaffList();
                        rptStaff.ShowDialog();
                        break;
                    case "supplier reports":
                        RT2008.Supplier.Reports.RptSupplierList rptSupplier = new RT2008.Supplier.Reports.RptSupplierList();
                        rptSupplier.ShowDialog();
                        break;
                    case "supplier":
                        RT2008.Supplier.SupplierWizard wizSupplier = new RT2008.Supplier.SupplierWizard();
                        wizSupplier.ShowDialog();
                        break;
                    case "supplieraddresstype":
                        RT2008.Supplier.SupplierAddressTypeWizard wizAddressType = new RT2008.Supplier.SupplierAddressTypeWizard();
                        wizAddressType.ShowDialog();
                        break;
                    case "supplierterms":
                        RT2008.Supplier.SupplierTermsWizard wizTerms = new RT2008.Supplier.SupplierTermsWizard();
                        wizTerms.ShowDialog();
                        break;
                    case "smarttag4supplier":
                        RT2008.Settings.SmartTag4SupplierWizard wizSmartTag4Supplier = new RT2008.Settings.SmartTag4SupplierWizard();
                        wizSmartTag4Supplier.ShowDialog();
                        break;
                    case "currency":
                        RT2008.Settings.CurrencyWizard wizCny = new RT2008.Settings.CurrencyWizard();
                        wizCny.ShowDialog();
                        break;
                    case "paymentfactor_currency":
                        RT2008.Settings.PaymentFactor wizPaymentFactorCurr = new RT2008.Settings.PaymentFactor(RT2008.Settings.PaymentFactor.FactorType.Currency);
                        wizPaymentFactorCurr.ShowDialog();
                        break;
                    case "paymentfactor_eventcode":
                        RT2008.Settings.PaymentFactor wizPaymentFactorEvnt = new RT2008.Settings.PaymentFactor(RT2008.Settings.PaymentFactor.FactorType.EventCode);
                        wizPaymentFactorEvnt.ShowDialog();
                        break;
                    case "country":
                        RT2008.Settings.CountryWizard wizCountry = new RT2008.Settings.CountryWizard();
                        wizCountry.ShowDialog();
                        break;
                    case "province":
                        RT2008.Settings.ProvinceWizard wizProvince = new RT2008.Settings.ProvinceWizard();
                        wizProvince.ShowDialog();
                        break;
                    case "city":
                        RT2008.Settings.CityWizard wizCity = new RT2008.Settings.CityWizard();
                        wizCity.ShowDialog();
                        break;
                    case "zone":
                        RT2008.Settings.ZoneWizard wizZone = new RT2008.Settings.ZoneWizard();
                        wizZone.ShowDialog();
                        break;
                    case "internettag":
                        RT2008.Settings.InternetTagWizard wizITag = new RT2008.Settings.InternetTagWizard();
                        wizITag.ShowDialog();
                        break;
                    case "lineofoperation":
                        RT2008.Settings.LineOfOperationWizard wizLOO = new RT2008.Settings.LineOfOperationWizard();
                        wizLOO.ShowDialog();
                        break;
                    case "marketsector":
                        RT2008.Settings.MarketSectorWizard wizMarketSector = new RT2008.Settings.MarketSectorWizard();
                        wizMarketSector.ShowDialog();
                        break;
                    case "salutation":
                        RT2008.Settings.SalutationWizard wizSalutation = new RT2008.Settings.SalutationWizard();
                        wizSalutation.ShowDialog();
                        break;
                    case "phonetag":
                        RT2008.Settings.PhoneTagWizard wizPhoneTag = new RT2008.Settings.PhoneTagWizard();
                        wizPhoneTag.ShowDialog();
                        break;
                    case "jobtitle":
                        RT2008.Settings.JobTitleWizard wizJobTitle = new RT2008.Settings.JobTitleWizard();
                        wizJobTitle.ShowDialog();
                        break;
                }
            }
        }
    }
}