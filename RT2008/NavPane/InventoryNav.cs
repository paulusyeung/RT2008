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
    public partial class InventoryNav : UserControl
    {
        public InventoryNav()
        {
            InitializeComponent();

            NavPane.NavMenu.FillNavTree("inventory", this.navInvt.Nodes);
        }

        private void navInvt_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = navInvt.SelectedNode.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)navInvt.SelectedNode.Tag);
                ShowAppToolStrip((string)navInvt.SelectedNode.Tag);
            }
        }

        #region Show private AppToolStrip
        // 2008.03.25 paulus: 淢少使用 Invneotry.InvtToolbar, 方便 code maintenance.
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
                        case "invt_goodsreceive":
                            RT2008.Inventory.GoodsReceive.DefaultAts oRecv = new RT2008.Inventory.GoodsReceive.DefaultAts();
                            oRecv.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(oRecv);
                            break;
                        case "invt_goodsreturn":
                            RT2008.Inventory.GoodsReturn.DefaultAts oReturn = new RT2008.Inventory.GoodsReturn.DefaultAts();
                            oReturn.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(oReturn);
                            break;
                        case "invt_txfer":
                            RT2008.Inventory.Transfer.DefaultAts oTxfer = new RT2008.Inventory.Transfer.DefaultAts();
                            oTxfer.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(oTxfer);
                            break;
                        case "invt_adjustment":
                            RT2008.Inventory.Adjustment.DefaultAts oAdjust = new RT2008.Inventory.Adjustment.DefaultAts();
                            oAdjust.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(oAdjust);
                            break;
                        case "invt_replenishment":
                            RT2008.Inventory.Replenishment.DefaultAts oRepl = new RT2008.Inventory.Replenishment.DefaultAts();
                            oRepl.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(oRepl);
                            break;
                        case "invt_stocktake":
                            RT2008.Inventory.StockTake.DefaultAts oStockTake = new RT2008.Inventory.StockTake.DefaultAts();
                            oStockTake.Dock = DockStyle.Fill;
                            atsPane.Controls.Clear();
                            atsPane.Controls.Add(oStockTake);
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

                // OLAP Viewer
                RT2008.Inventory.Olap.OlapViewForm oOlapViewer = new RT2008.Inventory.Olap.OlapViewForm();
                oOlapViewer.DockPadding.All = 6;
                oOlapViewer.Dock = DockStyle.Fill;

                switch (Tag.ToLower())
                {
                    case "invt_goodsreceive":
                        //RT2008.Inventory.GoodsReceive.Default oRecv = new RT2008.Inventory.GoodsReceive.Default(controls[0]);
                        //oRecv.DockPadding.All = 6;
                        //oRecv.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oRecv);

                        RT2008.Inventory.GoodsReceive.DefaultCAPList oRecv = new RT2008.Inventory.GoodsReceive.DefaultCAPList(controls[0]);
                        oRecv.DockPadding.All = 6;
                        oRecv.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oRecv);
                        break;
                    case "invt_goodsreturn":
                        //RT2008.Inventory.GoodsReturn.Default oReturn = new RT2008.Inventory.GoodsReturn.Default(controls[0]);
                        //oReturn.DockPadding.All = 6;
                        //oReturn.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oReturn);

                        RT2008.Inventory.GoodsReturn.DefaultREJList oReturn = new RT2008.Inventory.GoodsReturn.DefaultREJList(controls[0]);
                        oReturn.DockPadding.All = 6;
                        oReturn.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oReturn);
                        break;
                    case "invt_txfer":
                        //RT2008.Inventory.Transfer.Default oTxfer = new RT2008.Inventory.Transfer.Default(controls[0]);
                        //oTxfer.DockPadding.All = 6;
                        //oTxfer.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oTxfer);

                        RT2008.Inventory.Transfer.DefaultTXFList oTxfer = new RT2008.Inventory.Transfer.DefaultTXFList(controls[0]);
                        oTxfer.DockPadding.All = 6;
                        oTxfer.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oTxfer);
                        break;
                    case "invt_adjustment":
                        //RT2008.Inventory.Adjustment.Default oAdjust = new RT2008.Inventory.Adjustment.Default(controls[0]);
                        //oAdjust.DockPadding.All = 6;
                        //oAdjust.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oAdjust);

                        RT2008.Inventory.Adjustment.DefaultADJList oAdjust = new RT2008.Inventory.Adjustment.DefaultADJList(controls[0]);
                        oAdjust.DockPadding.All = 6;
                        oAdjust.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oAdjust);
                        break;
                    case "invt_replenishment":
                        //RT2008.Inventory.Replenishment.Default oReplenishment = new RT2008.Inventory.Replenishment.Default(controls[0]);
                        //oReplenishment.DockPadding.All = 6;
                        //oReplenishment.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oReplenishment);

                        RT2008.Inventory.Replenishment.DefaultRPLList oReplenishment = new RT2008.Inventory.Replenishment.DefaultRPLList(controls[0]);
                        oReplenishment.DockPadding.All = 6;
                        oReplenishment.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oReplenishment);
                        break;
                    case "invt_stocktake":
                        //RT2008.Inventory.StockTake.Default oStockTake = new RT2008.Inventory.StockTake.Default(controls[0]);
                        //oStockTake.DockPadding.All = 6;
                        //oStockTake.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oStockTake);

                        RT2008.Inventory.StockTake.DefaultSTKTKList oStockTake = new RT2008.Inventory.StockTake.DefaultSTKTKList(controls[0]);
                        oStockTake.DockPadding.All = 6;
                        oStockTake.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oStockTake);
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
                    case "journal_goodsreceive":
                        RT2008.Inventory.GoodsReceive.Reports.JournalWizard wizCAPJournal = new RT2008.Inventory.GoodsReceive.Reports.JournalWizard();
                        wizCAPJournal.ShowDialog();
                        break;
                    case "journal_goodsreturn":
                        RT2008.Inventory.GoodsReturn.Reports.JournalWizard wizRETJournal = new RT2008.Inventory.GoodsReturn.Reports.JournalWizard();
                        wizRETJournal.ShowDialog();
                        break;
                    case "journal_transfer":
                        RT2008.Inventory.Transfer.Reports.JournalWizard wizTransferJournal = new RT2008.Inventory.Transfer.Reports.JournalWizard();
                        wizTransferJournal.ShowDialog();
                        break;
                    case "journal_adjustment":
                        RT2008.Inventory.Adjustment.Reports.JournalWizard wizADJJournal = new RT2008.Inventory.Adjustment.Reports.JournalWizard();
                        wizADJJournal.ShowDialog();
                        break;
                    case "journal_replenishment":
                        RT2008.Inventory.Replenishment.Reports.JournalWizard wizRPLJournal = new RT2008.Inventory.Replenishment.Reports.JournalWizard();
                        wizRPLJournal.ShowDialog();
                        break;
                    case "journal_stocktake":
                        RT2008.Inventory.StockTake.Reports.JournalWizard wizSTKTKJournal = new RT2008.Inventory.StockTake.Reports.JournalWizard();
                        wizSTKTKJournal.ShowDialog();
                        break;
                    case "olap_qohatsnormal":
                        oOlapViewer.ViewerType = RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS;

                        wspPane.Controls.Add(oOlapViewer);
                        break;
                    case "olap_qohatsnormal_withas":
                        RT2008.Controls.Reporting.OlapViewer oOlapViewerAS = new RT2008.Controls.Reporting.OlapViewer();
                        oOlapViewerAS.DockPadding.All = 6;
                        oOlapViewerAS.Dock = DockStyle.Fill;
                        oOlapViewerAS.AspxPagePath = @"Inventory\Olap\QohAtsNormalWithOlapConnection.aspx";
                        wspPane.Controls.Add(oOlapViewerAS);
                        break;
                    case "olap_qohatscutoff":
                        oOlapViewer.ViewerType = RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS_WithCutOffDate;

                        wspPane.Controls.Add(oOlapViewer);
                        break;
                    case "olap_stockreorder":
                        oOlapViewer.ViewerType = RT2008.Controls.InvtUtility.InvtOlapViewerType.StockReorder;

                        wspPane.Controls.Add(oOlapViewer);
                        break;
                    case "olap_stockinout":
                        oOlapViewer.ViewerType = RT2008.Controls.InvtUtility.InvtOlapViewerType.StockIOHistory;

                        wspPane.Controls.Add(oOlapViewer);
                        break;
                    case "olap_invtopeningclosing":
                        oOlapViewer.ViewerType = RT2008.Controls.InvtUtility.InvtOlapViewerType.OCInventory;

                        wspPane.Controls.Add(oOlapViewer);
                        break;
                    case "olap_stockvaluediscrepancyaudit":
                        oOlapViewer.ViewerType = RT2008.Controls.InvtUtility.InvtOlapViewerType.DiscrepancyAudit;

                        wspPane.Controls.Add(oOlapViewer);
                        break;
                    case "olap_stocktransfer":
                        oOlapViewer.ViewerType = RT2008.Controls.InvtUtility.InvtOlapViewerType.StockTransfer;

                        wspPane.Controls.Add(oOlapViewer);
                        break;
                    case "olap_capsummary":
                        oOlapViewer.ViewerType = RT2008.Controls.InvtUtility.InvtOlapViewerType.CAP_Summary;

                        wspPane.Controls.Add(oOlapViewer);
                        break;
                    case "invthistory_monthlyinout":
                        RT2008.Inventory.Reports.History.InOutHistory_Monthly wizMonthlyHist = new RT2008.Inventory.Reports.History.InOutHistory_Monthly();
                        wizMonthlyHist.ShowDialog();
                        break;
                    case "invthistory_inoutlist":
                        RT2008.Inventory.Reports.History.InOutHistory_List wizInOutList = new RT2008.Inventory.Reports.History.InOutHistory_List();
                        wizInOutList.ShowDialog();
                        break;
                    case "invthistory_inoutsummary":
                        RT2008.Inventory.Reports.History.InOutHistory_Summary wizInOutSummary = new RT2008.Inventory.Reports.History.InOutHistory_Summary();
                        wizInOutSummary.ShowDialog();
                        break;
                    case "price_change":
                        //RT2008.PriceMgmt.PriceMgmtList wizPriceChangeList = new RT2008.PriceMgmt.PriceMgmtList(controls[0], RT2008.PriceMgmt.PriceUtility.PriceMgmtType.Price);
                        //wizPriceChangeList.DockPadding.All = 6;
                        //wizPriceChangeList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(wizPriceChangeList);

                        RT2008.PriceMgmt.DefaultList wizPriceChangeList = new RT2008.PriceMgmt.DefaultList(controls[0], RT2008.PriceMgmt.PriceUtility.PriceMgmtType.Price);
                        wizPriceChangeList.DockPadding.All = 6;
                        wizPriceChangeList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(wizPriceChangeList);
                        break;
                    case "discount_change":
                        //RT2008.PriceMgmt.PriceMgmtList wizDiscChangeList = new RT2008.PriceMgmt.PriceMgmtList(controls[0], RT2008.PriceMgmt.PriceUtility.PriceMgmtType.Discount);
                        //wizDiscChangeList.DockPadding.All = 6;
                        //wizDiscChangeList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(wizDiscChangeList);

                        RT2008.PriceMgmt.DefaultList wizDiscChangeList = new RT2008.PriceMgmt.DefaultList(controls[0], RT2008.PriceMgmt.PriceUtility.PriceMgmtType.Discount);
                        wizDiscChangeList.DockPadding.All = 6;
                        wizDiscChangeList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(wizDiscChangeList);
                        break;
                    case "reason_code":
                        //RT2008.PriceMgmt.ReasonCodeList oReasonList = new RT2008.PriceMgmt.ReasonCodeList(controls[0]);
                        //oReasonList.DockPadding.All = 6;
                        //oReasonList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oReasonList);

                        RT2008.PriceMgmt.DefaultReasonList oReasonList = new RT2008.PriceMgmt.DefaultReasonList(controls[0]);
                        oReasonList.DockPadding.All = 6;
                        oReasonList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oReasonList);
                        break;
                    case "sales_input":
                        //RT2008.EmulatedPoS.SalesList oSalesInputList = new RT2008.EmulatedPoS.SalesList(controls[0], RT2008.DAL.Common.Enums.TxType.CAS);
                        //oSalesInputList.DockPadding.All = 6;
                        //oSalesInputList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oSalesInputList);

                        RT2008.EmulatedPoS.DefaultList oSalesInputList = new RT2008.EmulatedPoS.DefaultList(controls[0], RT2008.DAL.Common.Enums.TxType.CAS);
                        oSalesInputList.DockPadding.All = 6;
                        oSalesInputList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oSalesInputList);
                        break;
                    case "sales_return":
                        //RT2008.EmulatedPoS.SalesList oSalesReturnList = new RT2008.EmulatedPoS.SalesList(controls[0], RT2008.DAL.Common.Enums.TxType.CRT);
                        //oSalesReturnList.DockPadding.All = 6;
                        //oSalesReturnList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oSalesReturnList);

                        RT2008.EmulatedPoS.DefaultList oSalesReturnList = new RT2008.EmulatedPoS.DefaultList(controls[0], RT2008.DAL.Common.Enums.TxType.CRT);
                        oSalesReturnList.DockPadding.All = 6;
                        oSalesReturnList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oSalesReturnList);
                        break;
                }
            }
        }
        #endregion
    }
}