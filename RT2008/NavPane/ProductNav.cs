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
    public partial class ProductNav : UserControl
    {
        public ProductNav()
        {
            InitializeComponent();

            NavPane.NavMenu.FillNavTree("product", this.navProduct.Nodes);
        }

        private void navSettings_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = navProduct.SelectedNode.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)navProduct.SelectedNode.Tag);

                ShowAppToolStrip((string)navProduct.SelectedNode.Tag);
            }
        }

        private void ShowAppToolStrip(string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);
                if (controls.Length > 0)
                {
                    Panel atsPane = (Panel)controls[0];
                    //atsPane.Controls.Clear();

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

        private void ShowWorkspace(ref Panel wspPane, string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                Control[] controls = this.Form.Controls.Find("atsPane", true);

                switch (Tag.ToLower())
                {
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
                }
            }
        }
    }
}