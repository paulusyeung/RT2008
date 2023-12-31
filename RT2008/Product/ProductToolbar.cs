﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using RT2008.DAL;

namespace RT2008.Product
{
    public class ProductToolbar
    {
        private Control atsPaneCtrl;

        public ProductToolbar(Control toolBar, ref ToolBar tbProducts)
        {
            atsPaneCtrl = toolBar;
            ClearToolBar();
            AddItemToToolBar(tbProducts);
        }

        private void ClearToolBar()
        {
            atsPaneCtrl.Controls.Clear();
        }

        private void AddItemToToolBar(ToolBar tbProducts)
        {
            tbProducts.MenuHandle = false;
            tbProducts.DragHandle = false;
            tbProducts.TextAlign = ToolBarTextAlign.Right;

            // cmdNew
            ContextMenu ddlNew = new ContextMenu();
            ddlNew.MenuItems.Add(new MenuItem("Product Code", string.Empty, "Product_Code"));
            ddlNew.MenuItems.Add(new MenuItem("Product Code [Fast]", string.Empty, "Product_Code_Fast"));
            ddlNew.MenuItems.Add(new MenuItem("Product Code [Batch]", string.Empty, "Product_Code_Batch"));
            ddlNew.MenuItems.Add(new MenuItem("Product Code [Batch] - Posting", string.Empty, "Product_Code_Batch_Posting"));
            ddlNew.MenuItems.Add(new MenuItem("Product Code - Mass Update", string.Empty, "Product_Code_Mass_Update"));
            ddlNew.MenuItems.Add(new MenuItem("-"));
            ddlNew.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1"), string.Empty, "Product_Appendix1"));
            ddlNew.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2"), string.Empty, "Product_Appendix2"));
            ddlNew.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3"), string.Empty, "Product_Appendix3"));
            ddlNew.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS1"), string.Empty, "Product_Class1"));
            ddlNew.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS2"), string.Empty, "Product_Class2"));
            ddlNew.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS3"), string.Empty, "Product_Class3"));
            ddlNew.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS4"), string.Empty, "Product_Class4"));
            ddlNew.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS5"), string.Empty, "Product_Class5"));
            ddlNew.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS6"), string.Empty, "Product_Class6"));
            ddlNew.MenuItems.Add(new MenuItem("-"));
            ddlNew.MenuItems.Add(new MenuItem("Combination", string.Empty, "Combination"));
            ddlNew.MenuItems.Add(new MenuItem("Analysis Code", string.Empty, "AnalysisCode"));
            ddlNew.MenuItems.Add(new MenuItem("-"));
            ddlNew.MenuItems.Add(new MenuItem("PoS Tender Type", string.Empty, "PoSTenderType"));

            ToolBarButton cmdNew = new ToolBarButton("New", "New");
            cmdNew.Style = ToolBarButtonStyle.DropDownButton;
            cmdNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");
            cmdNew.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);
            cmdNew.DropDownMenu = ddlNew;

            tbProducts.Buttons.Add(cmdNew);
            cmdNew.MenuClick += new MenuEventHandler(cmdMenuClick);

            // cmdImport
            ContextMenu ddlImport = new ContextMenu();
            ddlImport.MenuItems.Add(new MenuItem("Product Code [Excel]", string.Empty, "ProductCodeImport"));
            ddlImport.MenuItems.Add(new MenuItem("Barcode Code [UIFD]", string.Empty, "BarcodeImport"));
            ddlImport.MenuItems[1].Enabled = false;

            ToolBarButton cmdImport = new ToolBarButton("Import", "Import");
            cmdImport.Style = ToolBarButtonStyle.DropDownButton;
            cmdImport.Image = new IconResourceHandle("16x16.ico_16_4407.gif");
            cmdImport.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);
            cmdImport.DropDownMenu = ddlImport;

            tbProducts.Buttons.Add(cmdImport);
            cmdImport.MenuClick += new MenuEventHandler(cmdMenuClick);

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;
            tbProducts.Buttons.Add(sep);

            //cmdReports
            ContextMenu ddlReports = new ContextMenu();
            ddlReports.MenuItems.Add(new MenuItem("Product Master List", string.Empty, "Product Master List"));
            ddlReports.MenuItems.Add(new MenuItem("Product Barcode List", string.Empty, "Product Barcode List"));
            ddlReports.MenuItems.Add(new MenuItem("-"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") + " List", string.Empty, "Appendix1 List"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") + " List", string.Empty, "Appendix2 List"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") + " List", string.Empty, "Appendix3 List"));
            ddlReports.MenuItems.Add(new MenuItem("-"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS1") + " List", string.Empty, "Class1 List"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS2") + " List", string.Empty, "Class2 List"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS3") + " List", string.Empty, "Class3 List"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS4") + " List", string.Empty, "Class4 List"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS5") + " List", string.Empty, "Class5 List"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS6") + " List", string.Empty, "Class6 List"));
            ddlReports.MenuItems.Add(new MenuItem("-"));
            ddlReports.MenuItems.Add(new MenuItem("Dimension List", string.Empty, "Dimension List"));
            ddlReports.MenuItems.Add(new MenuItem("Currency List", string.Empty, "Currency List"));
            ddlReports.MenuItems.Add(new MenuItem("Payment List", string.Empty, "Payment List"));
            ddlReports.MenuItems.Add(new MenuItem("-"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") + " ComBin List", string.Empty, "A1ComBin List"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") + " ComBin List", string.Empty, "A2ComBin List"));
            ddlReports.MenuItems.Add(new MenuItem(RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") + " ComBin List", string.Empty, "A3ComBin List"));

            ToolBarButton cmdReports = new ToolBarButton("Reports", "Reports");
            cmdReports.Style = ToolBarButtonStyle.DropDownButton;
            cmdReports.Image = new IconResourceHandle("16x16.16_reports.gif");
            cmdReports.DropDownMenu = ddlReports;
            cmdReports.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);

            tbProducts.Buttons.Add(cmdReports);
            cmdReports.MenuClick += new MenuEventHandler(cmdMenuClick);

            tbProducts.Buttons.Add(sep);

            if (atsPaneCtrl != null)
            {
                atsPaneCtrl.Controls.Add(tbProducts);
            }
        }

        void cmdMenuClick(object sender, MenuItemEventArgs e)
        {
            if (!(e.MenuItem.Tag == null))
            {
                switch (e.MenuItem.Tag.ToString().ToLower())
                {
                    case "product_code":
                        ProductWizard wizProduct = new ProductWizard();
                        wizProduct.ShowDialog();
                        break;
                    case "product_code_fast":
                        ProductWizard_FastCreation wizProduct_Fast = new ProductWizard_FastCreation();
                        wizProduct_Fast.ShowDialog();
                        break;
                    case "product_code_batch":
                        ProductWizard_Batch wizProduct_Batch = new ProductWizard_Batch();
                        wizProduct_Batch.ShowDialog();
                        break;
                    case "product_code_batch_posting":
                        ProductWizard_Authorization wizProduct_Auth = new ProductWizard_Authorization();
                        wizProduct_Auth.ShowDialog();
                        break;
                    case "product_code_mass_update":
                        ProductWizard_MassUpdate wizProduct_MU = new ProductWizard_MassUpdate();
                        wizProduct_MU.ShowDialog();
                        break;
                    case "productcodeimport":
                        RT2008.Product.Import.ProductCodeImport prodImport = new RT2008.Product.Import.ProductCodeImport();
                        prodImport.ShowDialog();
                        break;
                    case "product_appendix1":
                        ProductAppendixWizard wizAppendix1 = new ProductAppendixWizard(typeof(ProductAppendix1));
                        wizAppendix1.ShowDialog();
                        break;
                    case "product_appendix2":
                        ProductAppendixWizard wizAppendix2 = new ProductAppendixWizard(typeof(ProductAppendix2));
                        wizAppendix2.ShowDialog();
                        break;
                    case "product_appendix3":
                        ProductAppendixWizard wizAppendix3 = new ProductAppendixWizard(typeof(ProductAppendix3));
                        wizAppendix3.ShowDialog();
                        break;
                    case "product_class1":
                        ProductClassWizard wizClass1 = new ProductClassWizard(typeof(ProductClass1));
                        wizClass1.ShowDialog();
                        break;
                    case "product_class2":
                        ProductClassWizard wizClass2 = new ProductClassWizard(typeof(ProductClass2));
                        wizClass2.ShowDialog();
                        break;
                    case "product_class3":
                        ProductClassWizard wizClass3 = new ProductClassWizard(typeof(ProductClass3));
                        wizClass3.ShowDialog();
                        break;
                    case "product_class4":
                        ProductClassWizard wizClass4 = new ProductClassWizard(typeof(ProductClass4));
                        wizClass4.ShowDialog();
                        break;
                    case "product_class5":
                        ProductClassWizard wizClass5 = new ProductClassWizard(typeof(ProductClass5));
                        wizClass5.ShowDialog();
                        break;
                    case "product_class6":
                        ProductClassWizard wizClass6 = new ProductClassWizard(typeof(ProductClass6));
                        wizClass6.ShowDialog();
                        break;
                    case "combination":
                        ProductWizard_Combination wizCombin = new ProductWizard_Combination();
                        wizCombin.ShowDialog();
                        break;
                    case "analysiscode":
                        AnalysisCodeWizard wizAnalysis = new AnalysisCodeWizard();
                        wizAnalysis.ShowDialog();
                        break;
                    case "postendertype":
                        RT2008.Settings.PosTenderTypeWizard wizTender = new RT2008.Settings.PosTenderTypeWizard();
                        wizTender.ShowDialog();
                        break;
                    case "product master list":
                        RT2008.Product.Reports.RptProductMasterList productlist = new RT2008.Product.Reports.RptProductMasterList();
                        productlist.ShowDialog();
                        break;
                    case "product barcode list":
                        RT2008.Product.Reports.RptProductBarcodeList productBarcodeList = new RT2008.Product.Reports.RptProductBarcodeList();
                        productBarcodeList.ShowDialog();
                        break;
                    case "appendix1 list":
                        RT2008.Product.Reports.RptAppendixList a1List = new RT2008.Product.Reports.RptAppendixList();
                        a1List.AppendixType = "Appendix1";
                        a1List.ShowDialog();
                        break;
                    case "appendix2 list":
                        RT2008.Product.Reports.RptAppendixList a2List = new RT2008.Product.Reports.RptAppendixList();
                        a2List.AppendixType = "Appendix2";
                        a2List.ShowDialog();
                        break;
                    case "appendix3 list":
                        RT2008.Product.Reports.RptAppendixList a3List = new RT2008.Product.Reports.RptAppendixList();
                        a3List.AppendixType = "Appendix3";
                        a3List.ShowDialog();
                        break;
                    case "class1 list":
                        RT2008.Product.Reports.RptClassList c1List = new RT2008.Product.Reports.RptClassList();
                        c1List.ClassType = "Class1";
                        c1List.ShowDialog();
                        break;
                    case "class2 list":
                        RT2008.Product.Reports.RptClassList c2List = new RT2008.Product.Reports.RptClassList();
                        c2List.ClassType = "Class2";
                        c2List.ShowDialog();
                        break;
                    case "class3 list":
                        RT2008.Product.Reports.RptClassList c3List = new RT2008.Product.Reports.RptClassList();
                        c3List.ClassType = "Class3";
                        c3List.ShowDialog();
                        break;
                    case "class4 list":
                        RT2008.Product.Reports.RptClassList c4List = new RT2008.Product.Reports.RptClassList();
                        c4List.ClassType = "Class4";
                        c4List.ShowDialog();
                        break;
                    case "class5 list":
                        RT2008.Product.Reports.RptClassList c5List = new RT2008.Product.Reports.RptClassList();
                        c5List.ClassType = "Class5";
                        c5List.ShowDialog();
                        break;
                    case "class6 list":
                        RT2008.Product.Reports.RptClassList c6List = new RT2008.Product.Reports.RptClassList();
                        c6List.ClassType = "Class6";
                        c6List.ShowDialog();
                        break;
                    case "currency list":
                        RT2008.Settings.Reports.RptCurrencyList currencyList = new RT2008.Settings.Reports.RptCurrencyList();
                        currencyList.ShowDialog();
                        break;
                    case "dimension list":
                        RT2008.Product.Reports.RptDimensionList dimList = new RT2008.Product.Reports.RptDimensionList();
                        dimList.ShowDialog();
                        break;
                    case "payment list":
                        RT2008.Product.Reports.RptPaymentList paymentList = new RT2008.Product.Reports.RptPaymentList();
                        paymentList.ShowDialog();
                        break;
                    case "a1combin list":
                        RT2008.Product.Reports.RptAppendix1CombinList a1combinList = new RT2008.Product.Reports.RptAppendix1CombinList();
                        a1combinList.ShowDialog();
                        break;
                    case "a2combin list":
                        RT2008.Product.Reports.RptAppendix2CombinList a2combinList = new RT2008.Product.Reports.RptAppendix2CombinList();
                        a2combinList.ShowDialog();
                        break;
                    case "a3combin list":
                        RT2008.Product.Reports.RptAppendix3CombinList a3combinList = new RT2008.Product.Reports.RptAppendix3CombinList();
                        a3combinList.ShowDialog();
                        break;
                }
            }
        }
    }
}
