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
using RT2008.DAL;
using System.Data.SqlClient;
using RT2008.Controls;
using System.Web;

#endregion

namespace RT2008.Inventory.StockTake
{
    public partial class Wizard : Form
    {
        string bookNumLabel = "Book# {0} Qty";
        public Wizard()
        {
            InitializeComponent();

            SetAttributes();
            SetSystemLabel();

            FillCboList();
            SetToolBar();
            txtTxNumber.Text = "Auto-Generated";
            txtStatus.Text = "HOLD";

            cboBookNumber.SelectedIndex = 0;
            lblBookNum1Qty.Text = string.Format(bookNumLabel, cboBookNumber.Text);
        }

        public Wizard(Guid stkId)
        {
            InitializeComponent();

            SetAttributes();
            SetSystemLabel();

            this.StkId = stkId;

            SetCtrlEditable();
            FillCboList();
            SetToolBar();
            LoadStkInfo();

            cboBookNumber.SelectedIndex = 0;
            lblBookNum1Qty.Text = string.Format(bookNumLabel, cboBookNumber.Text);
        }


        #region Set System Label
        private void SetAttributes()
        {
            bookNumLabel = Utility.Dictionary.GetWord("stocktake_book_qty_replace");
            this.Text = Utility.Dictionary.GetWord("stock_take") + " > " + Utility.Dictionary.GetWord("Wizard");

            lblTxNumber.Text = Utility.Dictionary.GetWordWithColon("TxNumber");
            lblBookNumber.Text = Utility.Dictionary.GetWordWithColon("stocktake_book");
            lblTotalRetailAmount.Text = string.Format(Utility.Dictionary.GetWordWithColon("total_retail_amount_replace"), "$");

            tpGeneral.Text = Utility.Dictionary.GetWord("General");
            tpDetails.Text = Utility.Dictionary.GetWord("Details");

            lblLocation.Text = Utility.Dictionary.GetWordWithColon("workplace");
            lblCapturedOn.Text = Utility.Dictionary.GetWordWithColon("captured_on");
            lblTransactionDate.Text = Utility.Dictionary.GetWordWithColon("txdate");
            lblTotalQty.Text = Utility.Dictionary.GetWordWithColon("qty");
            lblTotalAmount.Text = Utility.Dictionary.GetWordWithColon("amount");
            lblStatus.Text = Utility.Dictionary.GetWordWithColon("Status");
            lblLastUpdate.Text = Utility.Dictionary.GetWordWithColon("Last Update");
            lblCreatedOn.Text = Utility.Dictionary.GetWordWithColon("createdon");

            lblCurrentTotalQty.Text = Utility.Dictionary.GetWord("current");
            lblStockTakeTotalQty.Text = Utility.Dictionary.GetWord("stock_take");

            lblStockCode.Text = Utility.Dictionary.GetWordWithColon("Product");
            lblDescription.Text = Utility.Dictionary.GetWordWithColon("Description");
            lblCurrentQty.Text = Utility.Dictionary.GetWordWithColon("current_qty");
            lblHHTQty.Text = Utility.Dictionary.GetWordWithColon("hht_qty");
            lblRetailPrice.Text = Utility.Dictionary.GetWordWithColon("retail_price");
            lblRetailAmount.Text = string.Format(Utility.Dictionary.GetWordWithColon("retail_amount_replace"), "$");
            lblLineCount.Text = Utility.Dictionary.GetWord("number_of_line");

            colLN.Text = Utility.Dictionary.GetWord("LN");
            colStatus.Text = Utility.Dictionary.GetWord("Status");
            colOnHandQty.Text = Utility.Dictionary.GetWord("on-hand_quantity");
            colHHTQty.Text = Utility.Dictionary.GetWord("hht_qty");
            colAverageCost.Text = Utility.Dictionary.GetWord("average_cost");
            colRetailPrice.Text = Utility.Dictionary.GetWordWithColon("retail_price");
            colTotalRetailAmount.Text = string.Format(Utility.Dictionary.GetWordWithColon("retail_amount_replace"), "$");

            btnAddItem.Text = Utility.Dictionary.GetWord("Add Item");
            btnEditItem.Text = Utility.Dictionary.GetWord("Edit Item");
            btnCancelUpdate.Text = Utility.Dictionary.GetWord("Cancel update");

            basicProduct.HasMatrix = false;

            //2013.06.24 paulus: 如果太多 stock items 個 browser 頂唔順就會 hang 機，所以採用 pagination
            lvDetailsList.UseInternalPaging = true;
            lvDetailsList.ItemsPerPage = 100;
        }

        private void SetSystemLabel()
        {
            colStockCode.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE");
            colAppendix1.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1");
            colAppendix2.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2");
            colAppendix3.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3");
        }
        #endregion

        #region Ctrl Editable
        private void SetCtrlEditable()
        {
            cboLocation.Enabled = false;
            cboLocation.BackColor = Color.LightYellow;
        }
        #endregion

        #region Properties
        private Guid stkId = System.Guid.Empty;
        public Guid StkId
        {
            get
            {
                return stkId;
            }
            set
            {
                stkId = value;
            }
        }

        private Guid stkDetailId = System.Guid.Empty;
        public Guid StkDetailId
        {
            get
            {
                return stkDetailId;
            }
            set
            {
                stkDetailId = value;
            }
        }

        private Guid productId = System.Guid.Empty;
        public Guid ProductId
        {
            get
            {
                return productId;
            }
            set
            {
                productId = value;
            }
        }

        private int SelectedIndex = 0;
        private bool ValidSelection = false;

        #endregion

        #region ToolBar
        private void SetToolBar()
        {
            this.tbWizardAction.MenuHandle = false;
            this.tbWizardAction.DragHandle = false;
            this.tbWizardAction.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            // cmdSave
            ToolBarButton cmdSave = new ToolBarButton("Save", Utility.Dictionary.GetWord("Save"));
            cmdSave.Tag = "Save";
            cmdSave.Image = new IconResourceHandle("16x16.16_L_save.gif");
            cmdSave.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);

            this.tbWizardAction.Buttons.Add(cmdSave);

            // cmdSaveNew
            ToolBarButton cmdSaveNew = new ToolBarButton("Save & New", HttpUtility.UrlDecode(Utility.Dictionary.GetWord("Save_New")));
            cmdSaveNew.Tag = "Save & New";
            cmdSaveNew.Image = new IconResourceHandle("16x16.16_L_saveOpen.gif");
            cmdSaveNew.Enabled = false;
            this.tbWizardAction.Buttons.Add(cmdSaveNew);

            // cmdSaveClose
            ToolBarButton cmdSaveClose = new ToolBarButton("Save & Close", HttpUtility.UrlDecode(Utility.Dictionary.GetWord("Save_Close")));
            cmdSaveClose.Tag = "Save & Close";
            cmdSaveClose.Image = new IconResourceHandle("16x16.16_saveClose.gif");
            cmdSaveClose.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);

            this.tbWizardAction.Buttons.Add(cmdSaveClose);
            this.tbWizardAction.Buttons.Add(sep);

            // cmdDelete
            ToolBarButton cmdDelete = new ToolBarButton("Delete", Utility.Dictionary.GetWord("Delete"));
            cmdDelete.Tag = "Delete";
            cmdDelete.Image = new IconResourceHandle("16x16.16_L_remove.gif");

            // cmdPrint
            ToolBarButton cmdPrint = new ToolBarButton("Print", Utility.Dictionary.GetWord("Print"));
            cmdPrint.Tag = "Print";
            cmdPrint.Image = new IconResourceHandle("16x16.16_print.gif");

            if (StkId == System.Guid.Empty)
            {
                cmdDelete.Enabled = false;
                cmdPrint.Enabled = false;
            }
            else
            {
                cmdDelete.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Delete);
                cmdPrint.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);
            }

            this.tbWizardAction.Buttons.Add(cmdDelete);
            this.tbWizardAction.Buttons.Add(sep);
            this.tbWizardAction.Buttons.Add(cmdPrint);

            this.tbWizardAction.ButtonClick += new ToolBarButtonClickEventHandler(tbWizardAction_ButtonClick);
        }

        void tbWizardAction_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "save":
                        MessageBox.Show("Save Record?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveMessageHandler));
                        break;
                    case "save & new":
                        MessageBox.Show("Save Record?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveNewMessageHandler));
                        break;
                    case "save & close":
                        MessageBox.Show("Save Record And Close?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveCloseMessageHandler));
                        break;
                    case "delete":
                        MessageBox.Show("Delete Record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(DeleteConfirmationHandler));
                        break;
                    case "print":
                        cmdPreview_Click();
                        break;
                }
            }
        }
        #endregion

        #region Bind Data To Report(ClickPrint)
        private DataTable BindData()
        {
            string sql = @"
                          SELECT TOP 100 PERCENT *
                          FROM vwRptStkTkList
                          WHERE	TxNumber BETWEEN '" + this.txtTxNumber.Text.Trim() + @"' AND '" + this.txtTxNumber.Text.Trim() + @"' 
                          AND CONVERT(VARCHAR(10), TxDate, 126) BETWEEN '" + this.dtpTxDate.Value.ToString("yyyy-MM-dd") + @"' 
                                                                        AND '" + this.dtpTxDate.Value.ToString("yyyy-MM-dd") + @"'
                          ORDER BY TxNumber, TxDate
                          ";

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (DataSet dataset = SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }
        }

        private void cmdPreview_Click()
        {
            string[,] param = {
                {"FromTxNumber",this.txtTxNumber.Text.Trim()},
                {"ToTxNumber",this.txtTxNumber.Text.Trim()},
                {"FromTxDate",this.dtpTxDate.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat())},
                {"ToTxDate",this.dtpTxDate.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat())},
                {"PrintedOn",DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat())},
                {"DateFormat",RT2008.SystemInfo.Settings.GetDateFormat()},
                { "CompanyName", RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                { "StockCode", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") }
                };

            RT2008.Controls.Reporting.Viewer view = new RT2008.Controls.Reporting.Viewer();

            view.Datasource = BindData();
            view.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptStkTkList";
            view.ReportName = "RT2008.Inventory.StockTake.Reports.WorksheetRdl.rdlc";
            view.Parameters = param;

            view.Show();
        }

        #endregion

        #region Fill Combo List
        private void FillCboList()
        {
            FillLocationList();
        }

        private void FillLocationList()
        {
            cboLocation.Items.Clear();

            string[] orderBy = new string[] { "WorkplaceCode" };
            WorkplaceCollection oWorkplaceList = RT2008.DAL.Workplace.LoadCollection(orderBy, true);
            cboLocation.DataSource = oWorkplaceList;
            cboLocation.DisplayMember = "WorkplaceCode";
            cboLocation.ValueMember = "WorkplaceId";
        }
        #endregion

        #region Save Stk Header Info
        private void Save()
        {
            StockTakeHeader oHeader = StockTakeHeader.Load(this.StkId);
            if (oHeader == null)
            {
                oHeader = new StockTakeHeader();

                oHeader.TxNumber = RT2008.SystemInfo.Settings.QueuingTxNumber(Common.Enums.TxType.STK);

                oHeader.CreatedBy = Common.Config.CurrentUserId;
                oHeader.CreatedOn = DateTime.Now;

                oHeader.Status = Convert.ToInt32(Common.Enums.Status.Draft.ToString("d"));
            }
            oHeader.TxDate = dtpTxDate.Value;

            oHeader.WorkplaceId = new Guid(cboLocation.SelectedValue.ToString());

            oHeader.CapturedAmount = GetTotalOnhandAmount();
            oHeader.TotalQty = GetTotalStkTkQty();
            oHeader.TotalAmount = GetTotalStkTkAmount();

            oHeader.ModifiedBy = Common.Config.CurrentUserId;
            oHeader.ModifiedOn = DateTime.Now;

            oHeader.Save();

            this.StkId = oHeader.HeaderId;

            SaveStkDetail();

            CalcTotal();
        }
        #endregion

        #region Load Stk Header Info
        private void LoadStkInfo()
        {
            StockTakeHeader oHeader = StockTakeHeader.Load(this.StkId);
            if (oHeader != null)
            {
                txtTxNumber.Text = oHeader.TxNumber;

                cboLocation.SelectedValue = oHeader.WorkplaceId;
                txtStatus.Text = (oHeader.Status == 0) ? "HOLD" : "POST";

                dtpTxDate.Value = oHeader.TxDate;
                txtCapturedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oHeader.CapturedOn, true);

                txtCurrentTotalQty.Text = oHeader.CapturedQty.ToString("n0");
                txtCurrentTotalAmount.Text = oHeader.CapturedAmount.ToString("n2");
                txtStockTakeTotalQty.Text = oHeader.TotalQty.ToString("n0");
                txtStockTakeTotalAmount.Text = oHeader.TotalAmount.ToString("n2");

                txtTotalRetailAmount.Text = GetTotalStkTkAmount().ToString("n2");

                txtLastUpdateOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oHeader.ModifiedOn, false);
                txtLastUpdateBy.Text = GetStaffName(oHeader.ModifiedBy);
                txtCreatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oHeader.CreatedOn, false);

                if (oHeader.Status == (int)Common.Enums.Status.Active)
                {
                    btnAddItem.Enabled = false;
                    btnEditItem.Enabled = false;
                    btnCancelUpdate.Enabled = false;

                    foreach (ToolBarButton tb in tbWizardAction.Buttons)
                    {
                        if (tb.Name.Contains("Save") || tb.Name.Contains("Delete"))
                        {
                            tb.Enabled = false;
                        }
                    }
                }

                BindStkDetailsInfo();
            }
        }

        private string GetStaffName(Guid staffId)
        {
            RT2008.DAL.Staff oStaff = RT2008.DAL.Staff.Load(staffId);
            if (oStaff != null)
            {
                return oStaff.StaffNumber;
            }
            else
            {
                return string.Empty;
            }
        }

        private decimal GetTotalOnhandQty()
        {
            decimal totalQty = 0;

            #region 2013.06.24 paulus: 咁樣 loop 法，計倒都死得啦，取消！改用 ExecuteReader
            //string sql = "HeaderId = '" + this.StkId.ToString() + "'";
            //StockTakeDetailsCollection oDetails = StockTakeDetails.LoadCollection(sql);
            //foreach (StockTakeDetails oDetail in oDetails)
            //{
            //    totalQty += oDetail.CapturedQty;
            //}
            #endregion

            String sql = String.Format(@"
SELECT  SUM(CapturedQty) AS TotalOnhandQty 
FROM    vwSTKDetailsList  
WHERE   HeaderId = '{0}'", this.StkId.ToString());

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    totalQty = reader.GetDecimal(0);
                }
            }

            return totalQty;
        }

        private decimal GetTotalStkTkQty()
        {
            decimal totalQty = 0;

            #region 2013.06.24 paulus: 咁樣 loop 法，計倒都死得啦，取消！改用 ExecuteReader
            //string sql = "HeaderId = '" + this.StkId.ToString() + "'";
            //StockTakeDetailsCollection oDetails = StockTakeDetails.LoadCollection(sql);
            //foreach (StockTakeDetails oDetail in oDetails)
            //{
            //    totalQty += (oDetail.HHTQty + oDetail.Book1Qty + oDetail.Book2Qty + oDetail.Book3Qty + oDetail.Book4Qty + oDetail.Book5Qty);
            //}
            #endregion

            String sql = String.Format(@"
SELECT  SUM(HHTQty + Book1Qty + Book2Qty + Book3Qty + Book4Qty + Book5Qty) AS TotalQty 
FROM    vwSTKDetailsList  
WHERE   HeaderId = '{0}'", this.StkId.ToString());

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    totalQty = reader.GetDecimal(0);
                }
            }

            return totalQty;
        }

        private decimal GetTotalOnhandAmount()
        {
            decimal totalAmt = 0;

            #region 2013.06.24 paulus: 咁樣 loop 法，計倒都死得啦，取消！改用 ExecuteReader
            //string sql = "HeaderId = '" + this.StkId.ToString() + "'";
            //StockTakeDetailsCollection oDetails = StockTakeDetails.LoadCollection(sql);
            //foreach (StockTakeDetails oDetail in oDetails)
            //{
            //    totalAmt += oDetail.CapturedQty * GetRetailPrice(oDetail.ProductId);
            //}
            #endregion

            String sql = String.Format(@"
SELECT  SUM(CapturedQty * RetailPrice) AS OnhandAmount 
FROM    vwSTKDetailsList  
WHERE   HeaderId = '{0}'", this.StkId.ToString());

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    totalAmt = reader.GetDecimal(0);
                }
            }

            return totalAmt;
        }

        private decimal GetTotalStkTkAmount()
        {
            decimal totalAmt = 0;

            #region 2013.06.24 paulus: 咁樣 loop 法，計倒都死得啦，取消！改用 ExecuteReader
            //string sql = "HeaderId = '" + this.StkId.ToString() + "'";
            //StockTakeDetailsCollection oDetails = StockTakeDetails.LoadCollection(sql);
            //foreach (StockTakeDetails oDetail in oDetails)
            //{
            //    decimal retailPrice = GetRetailPrice(oDetail.ProductId);

            //    totalAmt += (oDetail.HHTQty + oDetail.Book1Qty + oDetail.Book2Qty + oDetail.Book3Qty + oDetail.Book4Qty + oDetail.Book5Qty) * retailPrice;
            //}
            #endregion

            String sql = String.Format(@"
SELECT  SUM((HHTQty + Book1Qty + Book2Qty + Book3Qty + Book4Qty + Book5Qty) * RetailPrice) AS RetailAmount 
FROM    vwSTKDetailsList  
WHERE   HeaderId = '{0}'", this.StkId.ToString());

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    totalAmt = reader.GetDecimal(0);
                }
            }

            return totalAmt;
        }

        private decimal GetRetailPrice(Guid productId)
        {
            decimal rtPrice = 1;

            RT2008.DAL.Product oProd = RT2008.DAL.Product.Load(productId);
            if (oProd != null)
            {
                rtPrice = oProd.RetailPrice;
            }

            return rtPrice;
        }
        #endregion

        #region Delete
        private void Delete()
        {
            StockTakeHeader oHeader = StockTakeHeader.Load(this.StkId);
            if (oHeader != null)
            {
                string sql = "HeaderId = '" + oHeader.HeaderId.ToString() + "'";

                DeleteDetails(sql);

                oHeader.Delete();
            }
        }

        private void DeleteDetails(string sql)
        {
            InvtBatchTXF_DetailsCollection oDetailList = InvtBatchTXF_Details.LoadCollection(sql);
            foreach (InvtBatchTXF_Details oDetail in oDetailList)
            {
                oDetail.Delete();
            }
        }
        #endregion

        #region Message Handler
        private void SaveMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                Save();

                if (this.StkId != System.Guid.Empty)
                {
                    MessageBox.Show("Success!", "Save Result");

                    this.Close();
                    RT2008.Inventory.StockTake.Wizard wizard = new RT2008.Inventory.StockTake.Wizard(this.StkId);
                    wizard.ShowDialog();
                }
            }
        }

        private void SaveNewMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                Save();

                if (this.StkId != System.Guid.Empty)
                {
                    this.Close();
                    RT2008.Inventory.StockTake.Wizard wizard = new RT2008.Inventory.StockTake.Wizard();
                    wizard.ShowDialog();
                }
            }
        }

        private void SaveCloseMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                Save();

                if (this.StkId != System.Guid.Empty)
                {
                    this.Close();
                }
            }
        }

        private void DeleteConfirmationHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                Delete();

                this.Close();
            }
        }

        private void TabChangedMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                Save();

                if (this.StkId != System.Guid.Empty)
                {
                    MessageBox.Show("Success!", "Save Result");

                    this.Close();
                    RT2008.Inventory.StockTake.Wizard wizard = new RT2008.Inventory.StockTake.Wizard(this.StkId);
                    wizard.ShowDialog();
                }
            }
            else
            {
                tabGoodsStk.SelectedIndex = 0;
            }
        }
        #endregion

        #region Stk Detail

        #region Bind Stk Detail List
        private void BindStkDetailsInfo()
        {
            int iCount = 1;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  DetailsId, ProductId, ProductName, STKCODE, APPENDIX1, APPENDIX2, APPENDIX3, ");
            sql.Append(" CapturedQty, HHTQty, Book1Qty, Book2Qty, Book3Qty, Book4Qty, Book5Qty, RetailPrice, AverageCost, ");
            sql.Append(" (HHTQty + Book1Qty + Book2Qty + Book3Qty + Book4Qty + Book5Qty) * RetailPrice AS RetailAmount");
            sql.Append(" FROM vwSTKDetailsList ");
            sql.Append(" WHERE HeaderId = '").Append(this.StkId.ToString()).Append("'");
            sql.Append(" ORDER BY STKCODE, APPENDIX1, APPENDIX2, APPENDIX3 ");

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql.ToString();
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem listItem = lvDetailsList.Items.Add(reader.GetGuid(0).ToString()); // DetailsId
                    listItem.SubItems.Add(string.Empty);
                    listItem.SubItems.Add(iCount.ToString()); // LineNumber
                    listItem.SubItems.Add(reader.GetString(3)); // STKCode
                    listItem.SubItems.Add(reader.GetString(4)); // Appendix1
                    listItem.SubItems.Add(reader.GetString(5)); // Appendix2
                    listItem.SubItems.Add(reader.GetString(6)); // Appendix3
                    listItem.SubItems.Add(reader.GetDecimal(7).ToString("n0")); // On-Hand
                    listItem.SubItems.Add(reader.GetDecimal(8).ToString("n0")); // HHT
                    listItem.SubItems.Add(reader.GetDecimal(9).ToString("n0")); // Book1 Qty
                    listItem.SubItems.Add(reader.GetDecimal(10).ToString("n0")); // Book2 Qty
                    listItem.SubItems.Add(reader.GetDecimal(11).ToString("n0")); // Book3 Qty
                    listItem.SubItems.Add(reader.GetDecimal(12).ToString("n0")); // Book4 Qty
                    listItem.SubItems.Add(reader.GetDecimal(13).ToString("n0")); // Book5 Qty
                    listItem.SubItems.Add(reader.GetDecimal(15).ToString("n2")); // Average Cost
                    listItem.SubItems.Add(reader.GetDecimal(14).ToString("n2")); // Retail Price
                    listItem.SubItems.Add(reader.GetDecimal(16).ToString("n2")); // Retail Amount
                    listItem.SubItems.Add(reader.GetGuid(1).ToString()); // ProductId

                    iCount++;
                }
            }

            txtLineCount.Text = (iCount - 1).ToString();
        }
        #endregion

        #region Save Stk Detail Info
        private void SaveStkDetail()
        {
            foreach (ListViewItem listItem in lvDetailsList.Items)
            {
                if (Common.Utility.IsGUID(listItem.Text.Trim()) && Common.Utility.IsGUID(listItem.SubItems[17].Text.Trim()))
                {
                    if (listItem.SubItems[1].Text == "EDIT" || listItem.SubItems[1].Text == "NEW")
                    {
                        System.Guid detailId = new Guid(listItem.Text.Trim());
                        StockTakeDetails oDetail = StockTakeDetails.Load(detailId);
                        if (oDetail == null)
                        {
                            oDetail = new StockTakeDetails();
                            oDetail.HeaderId = this.StkId;
                            oDetail.TxNumber = txtTxNumber.Text;
                        }
                        oDetail.ProductId = new Guid(listItem.SubItems[17].Text.Trim());
                        oDetail.CapturedQty = Convert.ToDecimal(listItem.SubItems[7].Text);
                        oDetail.HHTQty = Convert.ToDecimal(listItem.SubItems[8].Text);
                        oDetail.Book1Qty = Convert.ToDecimal(listItem.SubItems[9].Text);
                        oDetail.Book2Qty = Convert.ToDecimal(listItem.SubItems[10].Text);
                        oDetail.Book3Qty = Convert.ToDecimal(listItem.SubItems[11].Text);
                        oDetail.Book4Qty = Convert.ToDecimal(listItem.SubItems[12].Text);
                        oDetail.Book5Qty = Convert.ToDecimal(listItem.SubItems[13].Text);
                        oDetail.AverageCost = Convert.ToDecimal(listItem.SubItems[14].Text);

                        if (listItem.SubItems[1].Text.Length > 0)
                        {
                            oDetail.ModifiedBy = RT2008.DAL.Common.Config.CurrentUserId;
                            oDetail.ModifiedOn = DateTime.Now;

                            oDetail.Save();
                        }
                    }
                }
            }
        }
        #endregion

        #region Load Stk Detail Info
        private void LoadStkDetailsInfo(Guid detailId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  DetailsId, ProductId, ProductName , CapturedQty, HHTQty, STKCODE, APPENDIX1, APPENDIX2, APPENDIX3,");
            sql.Append(" Book1Qty, Book2Qty, Book3Qty, Book4Qty, Book5Qty, AverageCost, RetailPrice");
            sql.Append(" FROM vwSTKDetailsList ");
            sql.Append(" WHERE DetailsId = '").Append(detailId.ToString()).Append("'");

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql.ToString();
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    txtDescription.Text = reader.GetString(2);
                    txtHHTQty.Text = reader.GetDecimal(4).ToString("n0");
                    txtCurrentQty.Text = reader.GetDecimal(3).ToString("n0");
                    txtAverageCost.Text = reader.GetDecimal(14).ToString("n2");
                    txtRetailPrice.Text = reader.GetDecimal(15).ToString("n2");

                    switch (cboBookNumber.Text.Trim())
                    {
                        case "1":
                            txtBookNum1Qty.Text = reader.GetDecimal(9).ToString("n0");
                            break;
                        case "2":
                            txtBookNum1Qty.Text = reader.GetDecimal(10).ToString("n0");
                            break;
                        case "3":
                            txtBookNum1Qty.Text = reader.GetDecimal(11).ToString("n0");
                            break;
                        case "4":
                            txtBookNum1Qty.Text = reader.GetDecimal(12).ToString("n0");
                            break;
                        case "5":
                            txtBookNum1Qty.Text = reader.GetDecimal(13).ToString("n0");
                            break;
                    }

                    basicProduct.SelectedItem = reader.GetGuid(1);
                    this.ProductId = reader.GetGuid(1);
                }
            }
        }
        #endregion

        private void CalcTotal()
        {
            decimal totalQty = 0, totalAmt = 0, qty = 0, retailAmt = 0;
            StockTakeDetailsCollection detailList = StockTakeDetails.LoadCollection("HeaderId = '" + StkId + "'");
            foreach (StockTakeDetails detail in detailList)
            {
                qty = detail.HHTQty + detail.Book1Qty + detail.Book2Qty + detail.Book3Qty + detail.Book4Qty + detail.Book5Qty;
                totalQty += qty;

                RT2008.DAL.Product prod = RT2008.DAL.Product.Load(detail.ProductId);
                if (prod != null)
                {
                    retailAmt += qty * prod.RetailPrice;
                }

                totalAmt += qty * detail.AverageCost;
            }

            StockTakeHeader oHeader = StockTakeHeader.Load(this.StkId);
            if (oHeader != null)
            {
                oHeader.TotalQty = totalQty;
                oHeader.TotalAmount = totalAmt;

                oHeader.Save();
            }

            txtStockTakeTotalQty.Text = totalQty.ToString("n0");
            txtStockTakeTotalAmount.Text = totalAmt.ToString("n2");
            txtTotalRetailAmount.Text = retailAmt.ToString("n2");
        }

        #endregion

        #region Add/Edit Item & Cancel Update

        private void ReCalcTotalOnBooks()
        {
            decimal hhtQty = 0, b1Qty = 0, b2Qty = 0, b3Qty = 0, b4Qty = 0, b5Qty = 0, qty = 0;
            decimal retailPrice = 0, avgCost = 0, totalQty = 0, totalAmt = 0, retailAmt = 0;

            foreach (ListViewItem listItem in lvDetailsList.Items)
            {
                decimal.TryParse(listItem.SubItems[8].Text, out hhtQty);
                decimal.TryParse(listItem.SubItems[9].Text, out b1Qty); // Book1 Qty
                decimal.TryParse(listItem.SubItems[10].Text, out b2Qty); // Book2 Qty
                decimal.TryParse(listItem.SubItems[11].Text, out b3Qty); // Book3 Qty
                decimal.TryParse(listItem.SubItems[12].Text, out b4Qty); // Book4 Qty
                decimal.TryParse(listItem.SubItems[13].Text, out b5Qty); // Book5 Qty

                decimal.TryParse(listItem.SubItems[15].Text, out retailPrice); // Retail Price
                decimal.TryParse(listItem.SubItems[14].Text, out avgCost); // Average Cost

                qty = hhtQty + b1Qty + b2Qty + b3Qty + b4Qty + b5Qty;
                totalQty += qty;
                retailAmt += qty * retailPrice;
                totalAmt += qty * avgCost;
            }

            txtTotalRetailAmount.Text = retailAmt.ToString("n2");
            txtStockTakeTotalAmount.Text = totalAmt.ToString("n2");
            txtStockTakeTotalQty.Text = totalQty.ToString("n0");
        }

        private bool IsDuplicated(string stkCode, string appendix1, string appendix2, string appendix3)
        {
            bool isDuplicated = false;

            foreach (ListViewItem oItem in lvDetailsList.Items)
            {
                if (stkCode.Length > 0)
                {
                    isDuplicated = (oItem.SubItems[3].Text == stkCode);
                    isDuplicated = isDuplicated & (oItem.SubItems[4].Text == appendix1);
                    isDuplicated = isDuplicated & (oItem.SubItems[5].Text == appendix2);
                    isDuplicated = isDuplicated & (oItem.SubItems[6].Text == appendix3);
                }
            }

            return isDuplicated;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string stkCode = string.Empty, appendix1 = string.Empty, appendix2 = string.Empty, appendix3 = string.Empty;
            ItemInfo(ref stkCode, ref appendix1, ref appendix2, ref appendix3);

            if (IsDuplicated(stkCode, appendix1, appendix2, appendix3))
            {
                //this.Invoke(new EventHandler(btnEditItem_Click), new object[] { this, e });
                MessageBox.Show(string.Format(Resources.Common.DuplicatedCode, "Stock Item"), string.Format(Resources.Common.DuplicatedCode, string.Empty));
            }
            else
            {
                if (this.ProductId != System.Guid.Empty)
                {
                    ListViewItem listItem = lvDetailsList.Items.Add(System.Guid.Empty.ToString());
                    listItem.SubItems.Add("NEW"); // Status
                    listItem.SubItems.Add(lvDetailsList.Items.Count.ToString());
                    listItem.SubItems.Add(stkCode); // Stock Code
                    listItem.SubItems.Add(appendix1); // Appendix1
                    listItem.SubItems.Add(appendix2); // Appendix2
                    listItem.SubItems.Add(appendix3); // Appendix3
                    listItem.SubItems.Add(txtCurrentQty.Text.Length == 0 ? "0" : txtCurrentQty.Text); // On-Hand
                    listItem.SubItems.Add(txtHHTQty.Text.Length == 0 ? "0" : txtHHTQty.Text); // HHT

                    if (cboBookNumber.Text.Trim() == "1")
                    {
                        listItem.SubItems.Add(txtBookNum1Qty.Text.Length == 0 ? "0" : txtBookNum1Qty.Text); // Book1 Qty
                    }
                    else
                    {
                        listItem.SubItems.Add("0"); // Book1 Qty
                    }

                    if (cboBookNumber.Text.Trim() == "2")
                    {
                        listItem.SubItems.Add(txtBookNum1Qty.Text.Length == 0 ? "0" : txtBookNum1Qty.Text); // Book2 Qty
                    }
                    else
                    {
                        listItem.SubItems.Add("0"); // Book2 Qty
                    }

                    if (cboBookNumber.Text.Trim() == "3")
                    {
                        listItem.SubItems.Add(txtBookNum1Qty.Text.Length == 0 ? "0" : txtBookNum1Qty.Text); // Book3 Qty
                    }
                    else
                    {
                        listItem.SubItems.Add("0"); // Book3 Qty
                    }

                    if (cboBookNumber.Text.Trim() == "4")
                    {
                        listItem.SubItems.Add(txtBookNum1Qty.Text.Length == 0 ? "0" : txtBookNum1Qty.Text); // Book4 Qty
                    }
                    else
                    {
                        listItem.SubItems.Add("0"); // Book4 Qty
                    }

                    if (cboBookNumber.Text.Trim() == "5")
                    {
                        listItem.SubItems.Add(txtBookNum1Qty.Text.Length == 0 ? "0" : txtBookNum1Qty.Text); // Book5 Qty
                    }
                    else
                    {
                        listItem.SubItems.Add("0"); // Book5 Qty
                    }

                    listItem.SubItems.Add(txtAverageCost.Text.Length == 0 ? "0" : txtAverageCost.Text); // Average Cost
                    listItem.SubItems.Add(txtRetailPrice.Text.Length == 0 ? "0" : txtRetailPrice.Text); // Retail Price
                    listItem.SubItems.Add(txtRetailAmount.Text.Length == 0 ? "0" : txtRetailAmount.Text); // Retail Amount
                    listItem.SubItems.Add(this.ProductId.ToString()); // ProductId

                    txtLineCount.Text = lvDetailsList.Items.Count.ToString();

                    ReCalcTotalOnBooks();
                }
            }
        }

        private void ItemInfo(ref string stkCode, ref string appendix1, ref string appendix2, ref string appendix3)
        {
            if (basicProduct.SelectedItem != null)
            {
                RT2008.DAL.Product oProd = RT2008.DAL.Product.Load(new Guid(basicProduct.SelectedItem.ToString()));
                if (oProd != null)
                {
                    stkCode = oProd.STKCODE;
                    appendix1 = oProd.APPENDIX1;
                    appendix2 = oProd.APPENDIX2;
                    appendix3 = oProd.APPENDIX3;

                    this.ProductId = oProd.ProductId;
                }
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (lvDetailsList.SelectedItem != null)
            {
                string stkCode = string.Empty, appendix1 = string.Empty, appendix2 = string.Empty, appendix3 = string.Empty;
                ItemInfo(ref stkCode, ref appendix1, ref appendix2, ref appendix3);

                ListViewItem listItem = lvDetailsList.SelectedItem;
                listItem.SubItems[1].Text = "EDIT"; // Status
                listItem.SubItems[3].Text = stkCode; // Stock Code
                listItem.SubItems[4].Text = appendix1; // Appendix1
                listItem.SubItems[5].Text = appendix2; // Appendix2
                listItem.SubItems[6].Text = appendix3; // Appendix3
                listItem.SubItems[7].Text = txtCurrentQty.Text.Length == 0 ? "0" : txtCurrentQty.Text; // On-Hand
                listItem.SubItems[8].Text = txtHHTQty.Text.Length == 0 ? "0" : txtHHTQty.Text; // HHT

                if (cboBookNumber.Text.Trim() == "1")
                {
                    listItem.SubItems[9].Text = txtBookNum1Qty.Text.Length == 0 ? "0" : txtBookNum1Qty.Text; // Book1 Qty
                }

                if (cboBookNumber.Text.Trim() == "2")
                {
                    listItem.SubItems[10].Text = txtBookNum1Qty.Text.Length == 0 ? "0" : txtBookNum1Qty.Text; // Book2 Qty
                }

                if (cboBookNumber.Text.Trim() == "3")
                {
                    listItem.SubItems[11].Text = txtBookNum1Qty.Text.Length == 0 ? "0" : txtBookNum1Qty.Text; // Book3 Qty
                }

                if (cboBookNumber.Text.Trim() == "4")
                {
                    listItem.SubItems[12].Text = txtBookNum1Qty.Text.Length == 0 ? "0" : txtBookNum1Qty.Text; // Book4 Qty
                }

                if (cboBookNumber.Text.Trim() == "5")
                {
                    listItem.SubItems[13].Text = txtBookNum1Qty.Text.Length == 0 ? "0" : txtBookNum1Qty.Text; // Book5 Qty
                }

                listItem.SubItems[14].Text = txtAverageCost.Text.Length == 0 ? "0" : txtAverageCost.Text; // Average Cost
                listItem.SubItems[15].Text = txtRetailPrice.Text.Length == 0 ? "0" : txtRetailPrice.Text; // Retail Price
                listItem.SubItems[16].Text = txtRetailAmount.Text.Length == 0 ? "0" : txtRetailAmount.Text; // Retail Amount
                listItem.SubItems[17].Text = this.ProductId.ToString(); // ProductId

                ReCalcTotalOnBooks();
            }
        }

        private void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            ListViewItem listItem = lvDetailsList.SelectedItem;
            if (listItem.SubItems[1].Text == "EDIT" && Common.Utility.IsGUID(listItem.Text))
            {
                Guid detailId = new Guid(listItem.Text);
                StockTakeDetails stkDetail = StockTakeDetails.Load(detailId);
                if (stkDetail != null)
                {
                    listItem.SubItems[1].Text = string.Empty; // Status
                    listItem.SubItems[9].Text = stkDetail.Book1Qty.ToString("n0"); // Book1 Qty
                    listItem.SubItems[10].Text = stkDetail.Book2Qty.ToString("n0"); // Book2 Qty
                    listItem.SubItems[11].Text = stkDetail.Book3Qty.ToString("n0"); // Book3 Qty
                    listItem.SubItems[12].Text = stkDetail.Book4Qty.ToString("n0"); // Book4 Qty
                    listItem.SubItems[13].Text = stkDetail.Book5Qty.ToString("n0"); // Book5 Qty
                    listItem.SubItems[16].Text = txtRetailAmount.Text.Length == 0 ? "0" : txtRetailAmount.Text; // Retail Amount
                }

                ReCalcTotalOnBooks();
            }

            this.ProductId = System.Guid.Empty;
            this.StkDetailId = System.Guid.Empty;
            basicProduct.cboFullStockCode.Text = string.Empty;
            basicProduct.SelectedItem = null;
            basicProduct.SelectedText = string.Empty;
            txtDescription.Text = string.Empty;
            txtCurrentQty.Text = string.Empty;
            txtHHTQty.Text = string.Empty;
            txtBookNum1Qty.Text = string.Empty;
            txtRetailPrice.Text = string.Empty;
            txtRetailAmount.Text = string.Empty;
            txtAverageCost.Text = string.Empty;
        }
        #endregion

        private void lvDetailsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDetailsList.SelectedItem != null)
            {
                if (Common.Utility.IsGUID(lvDetailsList.SelectedItem.Text))
                {
                    this.ValidSelection = true;
                    this.SelectedIndex = lvDetailsList.SelectedIndex;

                    if (lvDetailsList.SelectedItem.Text != System.Guid.Empty.ToString())
                    {
                        this.StkDetailId = new Guid(lvDetailsList.SelectedItem.Text);
                        LoadStkDetailsInfo(new Guid(lvDetailsList.SelectedItem.Text));
                    }
                    else
                    {
                        basicProduct.SelectedItem = lvDetailsList.SelectedItem.SubItems[17].Text;

                        txtCurrentQty.Text = lvDetailsList.SelectedItem.SubItems[7].Text; // On-Hand
                        txtHHTQty.Text = lvDetailsList.SelectedItem.SubItems[8].Text; // HHT

                        if (cboBookNumber.Text.Trim() == "1")
                        {
                            txtBookNum1Qty.Text = lvDetailsList.SelectedItem.SubItems[9].Text; // Book1 Qty
                        }

                        if (cboBookNumber.Text.Trim() == "2")
                        {
                            txtBookNum1Qty.Text = lvDetailsList.SelectedItem.SubItems[10].Text; // Book2 Qty
                        }

                        if (cboBookNumber.Text.Trim() == "3")
                        {
                            txtBookNum1Qty.Text = lvDetailsList.SelectedItem.SubItems[11].Text; // Book3 Qty
                        }

                        if (cboBookNumber.Text.Trim() == "4")
                        {
                            txtBookNum1Qty.Text = lvDetailsList.SelectedItem.SubItems[12].Text; // Book4 Qty
                        }

                        if (cboBookNumber.Text.Trim() == "5")
                        {
                            txtBookNum1Qty.Text = lvDetailsList.SelectedItem.SubItems[13].Text; // Book5 Qty
                        }

                        txtAverageCost.Text = lvDetailsList.SelectedItem.SubItems[14].Text; // Average Cost
                        txtRetailPrice.Text = lvDetailsList.SelectedItem.SubItems[15].Text; // Retail Price
                        txtRetailAmount.Text = lvDetailsList.SelectedItem.SubItems[16].Text; // Retail Amount
                    }

                    this.ValidSelection = false;
                }
            }
        }

        private decimal GetCurrentQty(Guid prodId)
        {
            string sql = "ProductId = '" + prodId.ToString() + "' AND WorkplaceId = '" + cboLocation.SelectedValue.ToString() + "'";
            ProductWorkplace wpProd = ProductWorkplace.LoadWhere(sql);
            if (wpProd != null)
            {
                return wpProd.CDQTY;
            }
            else
            {
                return 0;
            }
        }

        private void cboBookNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblBookNum1Qty.Text = string.Format(bookNumLabel, cboBookNumber.SelectedItem.ToString());

            ListViewItem listItem = lvDetailsList.SelectedItem;
            if (listItem != null)
            {
                if (Common.Utility.IsGUID(listItem.Text))
                {
                    switch (cboBookNumber.SelectedItem.ToString())
                    {
                        case "1":
                            txtBookNum1Qty.Text = listItem.SubItems[9].Text; // Book1 Qty
                            break;
                        case "2":
                            txtBookNum1Qty.Text = listItem.SubItems[10].Text; // Book2 Qty
                            break;
                        case "3":
                            txtBookNum1Qty.Text = listItem.SubItems[11].Text; // Book3 Qty
                            break;
                        case "4":
                            txtBookNum1Qty.Text = listItem.SubItems[12].Text; // Book4 Qty
                            break;
                        case "5":
                            txtBookNum1Qty.Text = listItem.SubItems[13].Text; // Book5 Qty
                            break;
                    }
                }
            }
        }

        private void txtBookNum1Qty_TextChanged(object sender, EventArgs e)
        {
            if (txtBookNum1Qty.Text.Trim().Length > 0)
            {
                if (Common.Utility.IsNumeric(txtBookNum1Qty.Text))
                {
                    errorProvider.SetError(txtBookNum1Qty, string.Empty);

                    decimal hhtQty = 0, b1Qty = 0, b2Qty = 0, b3Qty = 0, b4Qty = 0, b5Qty = 0, qty = 0;
                    ListViewItem listItem = lvDetailsList.SelectedItem;
                    if (listItem != null)
                    {
                        if (listItem.SubItems[1].Text == "EDIT" && Common.Utility.IsGUID(listItem.Text))
                        {
                            decimal.TryParse(listItem.SubItems[8].Text, out hhtQty);
                            decimal.TryParse(listItem.SubItems[9].Text, out b1Qty); // Book1 Qty
                            decimal.TryParse(listItem.SubItems[10].Text, out b2Qty); // Book2 Qty
                            decimal.TryParse(listItem.SubItems[11].Text, out b3Qty); // Book3 Qty
                            decimal.TryParse(listItem.SubItems[12].Text, out b4Qty); // Book4 Qty
                            decimal.TryParse(listItem.SubItems[13].Text, out b5Qty); // Book5 Qty
                        }
                    }

                    decimal retailPrice = Convert.ToDecimal(txtRetailPrice.Text.Length == 0 ? "0" : txtRetailPrice.Text);

                    switch (cboBookNumber.SelectedItem.ToString())
                    {
                        case "1":
                            decimal.TryParse(txtBookNum1Qty.Text, out b1Qty);
                            break;
                        case "2":
                            decimal.TryParse(txtBookNum1Qty.Text, out b2Qty);
                            break;
                        case "3":
                            decimal.TryParse(txtBookNum1Qty.Text, out b3Qty);
                            break;
                        case "4":
                            decimal.TryParse(txtBookNum1Qty.Text, out b4Qty);
                            break;
                        case "5":
                            decimal.TryParse(txtBookNum1Qty.Text, out b5Qty);
                            break;
                    }

                    qty = hhtQty + b1Qty + b2Qty + b3Qty + b4Qty + b5Qty;
                    decimal amount = retailPrice * qty;
                    txtRetailAmount.Text = amount.ToString("n2");
                }
                else
                {
                    errorProvider.SetError(txtBookNum1Qty, "Please input the correct quantity number.");
                }
            }
        }

        private void basicProduct_SelectionChanged(object sender, RT2008.Controls.ProductSearcher.Basic.ProductSelectionEventArgs e)
        {
            if (!this.ValidSelection)
            {
                int iCount = 0;
                txtDescription.Text = e.Description;
                txtCurrentQty.Text = GetCurrentQty(e.ProductId).ToString("n0");
                txtRetailPrice.Text = e.UnitPrice.ToString("n2");
                txtRetailAmount.Text = (GetCurrentQty(e.ProductId) * e.UnitPrice).ToString("n2");
                txtAverageCost.Text = e.AverageCost.ToString("n2");

                foreach (ListViewItem lvItem in lvDetailsList.Items)
                {
                    lvItem.Selected = false;

                    if (lvItem.SubItems[17].Text == e.ProductId.ToString())
                    {
                        if (lvItem.Text != System.Guid.Empty.ToString() && Common.Utility.IsGUID(lvItem.Text))
                        {
                            if (iCount == 0)
                            {
                                txtHHTQty.Text = lvItem.SubItems[8].Text;
                                txtCurrentQty.Text = lvItem.SubItems[7].Text;

                                switch (cboBookNumber.Text.Trim())
                                {
                                    case "1":
                                        txtBookNum1Qty.Text = lvItem.SubItems[9].Text;
                                        break;
                                    case "2":
                                        txtBookNum1Qty.Text = lvItem.SubItems[10].Text;
                                        break;
                                    case "3":
                                        txtBookNum1Qty.Text = lvItem.SubItems[11].Text;
                                        break;
                                    case "4":
                                        txtBookNum1Qty.Text = lvItem.SubItems[12].Text;
                                        break;
                                    case "5":
                                        txtBookNum1Qty.Text = lvItem.SubItems[13].Text;
                                        break;
                                }

                                this.ProductId = e.ProductId;
                                this.StkDetailId = new Guid(lvItem.Text);
                                this.SelectedIndex = lvItem.Index;
                                lvItem.Selected = true;
                            }

                            iCount++;
                        }
                    }
                }
            }
        }
    }
}