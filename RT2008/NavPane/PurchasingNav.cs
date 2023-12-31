#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using RT2008.Product;

#endregion

namespace RT2008.NavPane
{
    public partial class PurchasingNav : UserControl
    {
        public PurchasingNav()
        {
            InitializeComponent();

            NavPane.NavMenu.FillNavTree("purchasing", this.navPurchase.Nodes);
        }

        private void navPurchase_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = navPurchase.SelectedNode.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)navPurchase.SelectedNode.Tag);
                ShowAppToolStrip((string)navPurchase.SelectedNode.Tag);
            }
        }

        #region Show private AppToolStrip
        private void ShowAppToolStrip(string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);
                if (controls.Length > 0)
                {
                    Panel atsPane = (Panel)controls[0];
                    //                atsPane.Controls.Clear();

                    switch (Tag.ToLower())
                    {
                        case "pur_purchaseorders":
                            break;
                        case "pur_goodsreturn":
                            break;
                        case "pur_settleorders":
                            break;
                        case "pur_blankworksheet":
                            break;
                        case "pur_worksheet":
                            break;
                        case "pur_history":
                            break;
                        case "pur_printableorders":
                            break;
                        case "pur_excelmatrix":
                            break;
                        case "pur_ordersummary":
                            break;
                        case "pur_olapordersummary":
                            //RT2008.Inventory.GoodsReceive.DefaultAts oRecv = new RT2008.Inventory.GoodsReceive.DefaultAts();
                            //oRecv.Dock = DockStyle.Fill;
                            //atsPane.Controls.Clear();
                            //atsPane.Controls.Add(oRecv);
                            break;
                    }
                }
            }
        }
        #endregion

        #region Show private Workspace
        private void ShowWorkspace(ref Panel wspPane, string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);

                switch (Tag.ToLower())
                {
                    case "pur_purchaseorders":
                        //RT2008.Purchasing.PurchaseOrdersList oPOList = new RT2008.Purchasing.PurchaseOrdersList();
                        //oPOList.DockPadding.All = 6;
                        //oPOList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oPOList);

                        RT2008.Purchasing.DefaultPOList oPOList = new RT2008.Purchasing.DefaultPOList();
                        oPOList.DockPadding.All = 6;
                        oPOList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oPOList);
                        break;
                    case "pur_poreceiving":
                        //RT2008.Purchasing.ReceivingList oPORecvList = new RT2008.Purchasing.ReceivingList();
                        //oPORecvList.DockPadding.All = 6;
                        //oPORecvList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oPORecvList);

                        RT2008.Purchasing.DefaultRECList oPORecvList = new RT2008.Purchasing.DefaultRECList();
                        oPORecvList.DockPadding.All = 6;
                        oPORecvList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oPORecvList);
                        break;
                    case "pur_settleorders":
                        //RT2008.Purchasing.OutstandingOrdersList oOSTList = new RT2008.Purchasing.OutstandingOrdersList();
                        //oOSTList.DockPadding.All = 6;
                        //oOSTList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oOSTList);

                        RT2008.Purchasing.DefaultOSTList oOSTList = new RT2008.Purchasing.DefaultOSTList();
                        oOSTList.DockPadding.All = 6;
                        oOSTList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oOSTList);
                        break;
                    case "pur_blankworksheet":
                        RT2008.Purchasing.Reports.ReceivingSchedule.BlankSheet wizBlankSheet = new RT2008.Purchasing.Reports.ReceivingSchedule.BlankSheet();
                        wizBlankSheet.ShowDialog();
                        break;
                    case "pur_worksheet":
                        RT2008.Purchasing.Reports.ReceivingSchedule.Worksheet wizWorksheet = new RT2008.Purchasing.Reports.ReceivingSchedule.Worksheet();
                        wizWorksheet.ShowDialog();
                        break;
                    case "pur_history":
                        RT2008.Purchasing.Reports.ReceivingSchedule.History wizHistory = new RT2008.Purchasing.Reports.ReceivingSchedule.History();
                        wizHistory.ShowDialog();
                        break;
                    case "pur_printableorders":
                        RT2008.Purchasing.Reports.OfficialDocument.PrintableOrders wizPOrders = new RT2008.Purchasing.Reports.OfficialDocument.PrintableOrders();
                        wizPOrders.ShowDialog();
                        break;
                    case "pur_excelmatrix":
                        RT2008.Purchasing.Reports.OfficialDocument.ExcelMatrix wizXlsMatrix = new RT2008.Purchasing.Reports.OfficialDocument.ExcelMatrix();
                        wizXlsMatrix.ShowDialog();
                        break;
                    case "pur_ordersummary":
                        RT2008.Purchasing.Reports.OfficialDocument.OrderSummary wizOrderSummary = new RT2008.Purchasing.Reports.OfficialDocument.OrderSummary();
                        wizOrderSummary.ShowDialog();
                        break;
                    case "pur_olapordersummary":
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
                    case "productcare_product":
                        //RT2008.Product.ProductList oProd = new RT2008.Product.ProductList(controls[0]);
                        //oProd.DockPadding.All = 6;
                        //oProd.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oProd);

                        RT2008.Product.DefaultList oProd = new RT2008.Product.DefaultList(controls[0]);
                        oProd.DockPadding.All = 6;
                        oProd.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oProd);
                        break;
                    case "productcare_appendix1":
                        //RT2008.Product.ProductAppendixList oAppendix1List = new RT2008.Product.ProductAppendixList("Appendix1", controls[0]);
                        //oAppendix1List.DockPadding.All = 6;
                        //oAppendix1List.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oAppendix1List);

                        RT2008.Product.DefaultAppendixList oAppendix1List = new RT2008.Product.DefaultAppendixList("Appendix1", controls[0]);
                        oAppendix1List.DockPadding.All = 6;
                        oAppendix1List.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oAppendix1List);
                        break;
                    case "productcare_appendix2":
                        //RT2008.Product.ProductAppendixList oAppendix2List = new RT2008.Product.ProductAppendixList("Appendix2", controls[0]);
                        //oAppendix2List.DockPadding.All = 6;
                        //oAppendix2List.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oAppendix2List);

                        RT2008.Product.DefaultAppendixList oAppendix2List = new RT2008.Product.DefaultAppendixList("Appendix2", controls[0]);
                        oAppendix2List.DockPadding.All = 6;
                        oAppendix2List.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oAppendix2List);
                        break;
                    case "productcare_appendix3":
                        //RT2008.Product.ProductAppendixList oAppendix3List = new RT2008.Product.ProductAppendixList("Appendix3", controls[0]);
                        //oAppendix3List.DockPadding.All = 6;
                        //oAppendix3List.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oAppendix3List);

                        RT2008.Product.DefaultAppendixList oAppendix3List = new RT2008.Product.DefaultAppendixList("Appendix3", controls[0]);
                        oAppendix3List.DockPadding.All = 6;
                        oAppendix3List.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oAppendix3List);
                        break;
                    case "productcare_class1":
                        //RT2008.Product.ProductClassList oClass1List = new RT2008.Product.ProductClassList("Class1", controls[0]);
                        //oClass1List.DockPadding.All = 6;
                        //oClass1List.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oClass1List);

                        RT2008.Product.DefaultClassList oClass1List = new RT2008.Product.DefaultClassList("Class1", controls[0]);
                        oClass1List.DockPadding.All = 6;
                        oClass1List.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oClass1List);
                        break;
                    case "productcare_class2":
                        //RT2008.Product.ProductClassList oClass2List = new RT2008.Product.ProductClassList("Class2", controls[0]);
                        //oClass2List.DockPadding.All = 6;
                        //oClass2List.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oClass2List);

                        RT2008.Product.DefaultClassList oClass2List = new RT2008.Product.DefaultClassList("Class2", controls[0]);
                        oClass2List.DockPadding.All = 6;
                        oClass2List.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oClass2List);
                        break;
                    case "productcare_class3":
                        //RT2008.Product.ProductClassList oClass3List = new RT2008.Product.ProductClassList("Class3", controls[0]);
                        //oClass3List.DockPadding.All = 6;
                        //oClass3List.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oClass3List);

                        RT2008.Product.DefaultClassList oClass3List = new RT2008.Product.DefaultClassList("Class3", controls[0]);
                        oClass3List.DockPadding.All = 6;
                        oClass3List.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oClass3List);
                        break;
                    case "productcare_class4":
                        //RT2008.Product.ProductClassList oClass4List = new RT2008.Product.ProductClassList("Class4", controls[0]);
                        //oClass4List.DockPadding.All = 6;
                        //oClass4List.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oClass4List);

                        RT2008.Product.DefaultClassList oClass4List = new RT2008.Product.DefaultClassList("Class4", controls[0]);
                        oClass4List.DockPadding.All = 6;
                        oClass4List.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oClass4List);
                        break;
                    case "productcare_class5":
                        //RT2008.Product.ProductClassList oClass5List = new RT2008.Product.ProductClassList("Class5", controls[0]);
                        //oClass5List.DockPadding.All = 6;
                        //oClass5List.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oClass5List);

                        RT2008.Product.DefaultClassList oClass5List = new RT2008.Product.DefaultClassList("Class5", controls[0]);
                        oClass5List.DockPadding.All = 6;
                        oClass5List.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oClass5List);
                        break;
                    case "productcare_class6":
                        //RT2008.Product.ProductClassList oClass6List = new RT2008.Product.ProductClassList("Class6", controls[0]);
                        //oClass6List.DockPadding.All = 6;
                        //oClass6List.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oClass6List);

                        RT2008.Product.DefaultClassList oClass6List = new RT2008.Product.DefaultClassList("Class6", controls[0]);
                        oClass6List.DockPadding.All = 6;
                        oClass6List.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oClass6List);
                        break;
                    case "productcare_combination":
                        //RT2008.Product.ProductCombinList oCombinList = new RT2008.Product.ProductCombinList(controls[0]);
                        //oCombinList.DockPadding.All = 6;
                        //oCombinList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oCombinList);

                        RT2008.Product.DefaultCombinList oCombinList = new RT2008.Product.DefaultCombinList(controls[0]);
                        oCombinList.DockPadding.All = 6;
                        oCombinList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oCombinList);
                        break;
                    case "productcare_analysiscode":
                        //RT2008.Product.AnalysisCodeList oAnalysisList = new RT2008.Product.AnalysisCodeList(controls[0]);
                        //oAnalysisList.DockPadding.All = 6;
                        //oAnalysisList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oAnalysisList);

                        RT2008.Product.DefaultAnalysisCodeList oAnalysisList = new RT2008.Product.DefaultAnalysisCodeList(controls[0]);
                        oAnalysisList.DockPadding.All = 6;
                        oAnalysisList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oAnalysisList);
                        break;
                }
            }
        }
        #endregion
    }
}