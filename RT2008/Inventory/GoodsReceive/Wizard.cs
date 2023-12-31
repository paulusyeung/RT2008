using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Common.Interfaces;

using RT2008.DAL;
using RT2008.Controls;

namespace RT2008.Inventory.GoodsReceive
{
    public partial class Wizard : Form, IGatewayComponent, IWizard
    {
        public Wizard()
        {
            InitializeComponent();
        }

        public Wizard(Guid capId)
        {
            InitializeComponent();

            this.CAPId = capId;
        }

        private void Wizard_Load(object sender, EventArgs e)
        {
            SetSystemLabel();
            SetAttributes();

            ResetTabOrder();
            InitDetailFindingSelection();
            FillCboList();
            SetToolBar();

            if (this.CAPId == System.Guid.Empty)
            {
                txtTxNumber.Text = "Auto-Generated";
                cboStatus.Text = "HOLD";

                InitCurrency(cboCurrency.Text);
            }
            else
            {
                LoadCAPInfo();
            }
        }

        #region Set System Label
        private void SetAttributes()
        {
            this.Text = Utility.Dictionary.GetWord("Goods Receive") + " > " + Utility.Dictionary.GetWord("Wizard");

            lblTxType.Text = Utility.Dictionary.GetWordWithColon("TxType");
            lblTxNumber.Text = Utility.Dictionary.GetWordWithColon("TxNumber");
            lblTotalAmount.Text = string.Format(Utility.Dictionary.GetWordWithColon("total_amount_with_currency"), SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency);

            tpGeneral.Text = Utility.Dictionary.GetWord("General");
            tpDetails.Text = Utility.Dictionary.GetWord("Details");

            lblLocation.Text = Utility.Dictionary.GetWordWithColon("workplace");
            lblRecvDate.Text = Utility.Dictionary.GetWordWithColon("Receive Date");
            lblOperatorCode.Text = Utility.Dictionary.GetWordWithColon("Staff");
            lblSupplier.Text = Utility.Dictionary.GetWordWithColon("Supplier");
            lblSupplierInvoice.Text = Utility.Dictionary.GetWord("Supplier") + " " + Utility.Dictionary.GetWordWithColon("Invoice Number");
            lblRefNumber.Text = Utility.Dictionary.GetWordWithColon("Reference");
            lblCurrency.Text = Utility.Dictionary.GetWordWithColon("Currency");
            lblCoefficient.Text = Utility.Dictionary.GetWordWithColon("Exchange Rate");
            lblStatus.Text = Utility.Dictionary.GetWordWithColon("Status");
            lblRemarks.Text = Utility.Dictionary.GetWordWithColon("Remarks");
            lblTotalQty.Text = Utility.Dictionary.GetWordWithColon("Total Qty");
            lblLastUpdate.Text = Utility.Dictionary.GetWordWithColon("Last Update");
            lblAmendmentRestrict.Text = Utility.Dictionary.GetWordWithColon("Amendment Restrict");
            lblAPLink.Text = Utility.Dictionary.GetWordWithColon("AP Link");

            lblBarcode.Text = Utility.Dictionary.GetWordWithColon("Barcode");
            lblStockCode.Text = Utility.Dictionary.GetWordWithColon("Product");
            lblDescription.Text = Utility.Dictionary.GetWordWithColon("Description");
            lblQty.Text = Utility.Dictionary.GetWordWithColon("Qty");
            lblUnitPrice_1.Text = string.Format(Utility.Dictionary.GetWord("unit_amount_with_currency"), SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency);
            lblUnitPrice_2.Text = string.Format(Utility.Dictionary.GetWord("unit_amount_with_currency"), SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency);
            lblLastCost_1.Text = string.Format(Utility.Dictionary.GetWord("last_cost_with_currency"), SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency);
            lblLastCost_2.Text = string.Format(Utility.Dictionary.GetWord("last_cost_with_currency"), SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency);
            lblAvrCost_1.Text = string.Format(Utility.Dictionary.GetWord("average_cost_with_currency"), SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency);
            lblAvrCost_2.Text = string.Format(Utility.Dictionary.GetWord("average_cost_with_currency"), SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency);
            lblNumberOfLine.Text = Utility.Dictionary.GetWordWithColon("number_of_line");

            colLN.Text = Utility.Dictionary.GetWord("LN");
            colStatus.Text = Utility.Dictionary.GetWord("Status");
            colDescription.Text = Utility.Dictionary.GetWord("Description");
            colQty.Text = Utility.Dictionary.GetWord("Qty");
            colUnitAmount.Text = string.Format(Utility.Dictionary.GetWord("unit_amount_with_currency"), SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency);
            colUnitAmount_HKD.Text = string.Format(Utility.Dictionary.GetWord("unit_amount_with_currency"), SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency);
            colAmount.Text = string.Format(Utility.Dictionary.GetWord("amount_with_currency"), SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency);

            btnAddItem.Text = Utility.Dictionary.GetWord("Add Item");
            btnEditItem.Text = Utility.Dictionary.GetWord("Edit Item");
            btnRemove.Text = Utility.Dictionary.GetWord("Remove Item");
        }

        private void SetSystemLabel()
        {
            colStockCode.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE");
            colAppendix1.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1");
            colAppendix2.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2");
            colAppendix3.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3");
        }
        #endregion

        #region IGatewayComponent Members

        void IGatewayComponent.ProcessRequest(IContext objContext, string strAction)
        {
            DataTable dt = Reports.DataSource.Worksheet(this.txtTxNumber.Text, this.txtTxNumber.Text, this.dtpRecvDate.Value, this.dtpRecvDate.Value);

            string filename = txtTxNumber.Text.Trim() + ".pdf";

            RT2008.Inventory.GoodsReceive.Reports.WorksheetRpt report = new RT2008.Inventory.GoodsReceive.Reports.WorksheetRpt();
            report.DataSource = dt;
            report.TxNumberFrom = this.txtTxNumber.Text.Trim();
            report.TxNumberTo = this.txtTxNumber.Text.Trim();
            report.TxDateFrom = this.dtpRecvDate.Value;
            report.TxDateTo = this.dtpRecvDate.Value;
            HttpResponse objResponse = this.Context.HttpContext.Response;

            System.IO.MemoryStream memStream = new System.IO.MemoryStream();

            objResponse.Clear();
            objResponse.ClearHeaders();

            report.ExportToPdf(memStream);
            objResponse.ContentType = "application/pdf";
            objResponse.AddHeader("content-disposition", "attachment; filename=" + filename);
            objResponse.BinaryWrite(memStream.ToArray());
            objResponse.Flush();
            objResponse.End();
        }

        #endregion

        #region Init Detail
        private void InitDetailFindingSelection()
        {
            txtBarcode.ReadOnly = !rbtnBarcode.Checked;
            txtBarcode.Focus();
            txtBarcode.BackColor = !rbtnBarcode.Checked ? Color.LightYellow : Color.White;
            txtBarcode.TabStop = rbtnBarcode.Checked;

            txtStockCode.ReadOnly = !rbtnStockCode_1.Checked;
            txtStockCode.Focus();
            txtStockCode.BackColor = !rbtnStockCode_1.Checked ? Color.LightYellow : Color.White;
            txtStockCode.TabStop = rbtnStockCode_1.Checked;
            txtStockCode.TabIndex = 0;

            txtAppendix1.ReadOnly = !rbtnStockCode_1.Checked;
            txtAppendix1.BackColor = !rbtnStockCode_1.Checked ? Color.LightYellow : Color.White;
            txtAppendix1.TabStop = rbtnStockCode_1.Checked;
            txtAppendix1.TabIndex = 1;

            txtAppendix2.ReadOnly = !rbtnStockCode_1.Checked;
            txtAppendix2.BackColor = !rbtnStockCode_1.Checked ? Color.LightYellow : Color.White;
            txtAppendix2.TabStop = rbtnStockCode_1.Checked;
            txtAppendix2.TabIndex = 2;

            txtAppendix3.ReadOnly = !rbtnStockCode_1.Checked;
            txtAppendix3.BackColor = !rbtnStockCode_1.Checked ? Color.LightYellow : Color.White;
            txtAppendix3.TabStop = rbtnStockCode_1.Checked;
            txtAppendix3.TabIndex = 3;

            basicProduct.Enabled = rbtnStockCode_2.Checked;
            basicProduct.BackColor = !rbtnStockCode_2.Checked ? Color.LightYellow : Color.White;
            basicProduct.TabStop = rbtnStockCode_2.Checked;
        }

        private void ResetTabOrder()
        {
            ResetTabOrder(rbtnBarcode.Checked, rbtnStockCode_1.Checked, rbtnStockCode_2.Checked);
        }

        private void ResetTabOrder(bool isBChecked, bool isS1Checked, bool isS2Checked)
        {
            basicProduct.TabStop = isS2Checked;
            txtBarcode.TabStop = isBChecked;

            txtStockCode.TabStop = isS1Checked;
            txtAppendix1.TabStop = isS1Checked;
            txtAppendix2.TabStop = isS1Checked;
            txtAppendix3.TabStop = isS1Checked;
        }
        #endregion

        #region Init Currency
        private void InitCurrency(string currencyCode)
        {
            string sql = "CurrencyCode = '" + currencyCode + "'";
            Currency oCny = Currency.LoadWhere(sql);
            if (oCny != null)
            {
                InitCurrency(oCny.CurrencyId);
            }
            else
            {
                InitCurrency(Common.Utility.IsGUID(cboCurrency.SelectedValue.ToString()) ? System.Guid.Empty : new System.Guid(cboCurrency.SelectedValue.ToString()));
            }
        }

        private void InitCurrency(Guid selectedCurrency)
        {
            Currency oCny = Currency.Load(selectedCurrency);
            if (oCny != null)
            {
                lblUnitPrice_1.Text = string.Format(Utility.Dictionary.GetWord("unit_amount_with_currency"), oCny.CurrencyCode);
                lblLastCost_1.Text = string.Format(Utility.Dictionary.GetWord("last_cost_with_currency"), oCny.CurrencyCode);
                lblAvrCost_1.Text = string.Format(Utility.Dictionary.GetWord("average_cost_with_currency"), oCny.CurrencyCode);

                colUnitAmount.Text = string.Format(Utility.Dictionary.GetWord("unit_amount_with_currency"), oCny.CurrencyCode);

                txtCoefficient.Text = oCny.ExchangeRate.ToString("n4");

                decimal unitPrice = Convert.ToDecimal(Common.Utility.IsNumeric(txtUnitPrice_1.Text) ? txtUnitPrice_1.Text : "0");
                txtUnitPrice_2.Text = (unitPrice * oCny.ExchangeRate).ToString("n2");

                GetSummaryCost(this.ProductId);

                ResetCAPDetails(oCny.ExchangeRate);
            }
        }

        private void ResetCAPDetails(decimal xchgRate)
        {
            foreach (ListViewItem item in lvDetailsList.Items)
            {
                decimal qty = Convert.ToDecimal(item.SubItems[8].Text.Length == 0 ? "0" : item.SubItems[8].Text);
                decimal uamtF = Convert.ToDecimal(item.SubItems[9].Text.Length == 0 ? "0" : item.SubItems[9].Text);
                item.SubItems[2].Text = item.SubItems[2].Text.Trim().Length == 0 ? "EDIT" : item.SubItems[2].Text;
                item.SubItems[10].Text = (uamtF * xchgRate).ToString("n2");
                item.SubItems[11].Text = (uamtF * xchgRate * qty).ToString("n2");
            }

            lvDetailsList.Update();
        }
        #endregion

        #region Properties
        private Guid capId = System.Guid.Empty;
        public Guid CAPId
        {
            get
            {
                return capId;
            }
            set
            {
                capId = value;
            }
        }

        private Guid capDetailId = System.Guid.Empty;
        public Guid CAPDetailId
        {
            get
            {
                return capDetailId;
            }
            set
            {
                capDetailId = value;
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
            cmdSaveNew.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);

            this.tbWizardAction.Buttons.Add(cmdSaveNew);

            // cmdSaveClose
            ToolBarButton cmdSaveClose = new ToolBarButton("Save & Close", HttpUtility.UrlDecode(Utility.Dictionary.GetWord("Save_close")));
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

            if (CAPId == System.Guid.Empty)
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
                        MessageBox.Show(Utility.Dictionary.GetWord("q_save_record"), Utility.Dictionary.GetWord("Save Confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveMessageHandler));
                        break;
                    case "save & new":
                        MessageBox.Show(Utility.Dictionary.GetWord("q_save_record"), Utility.Dictionary.GetWord("Save Confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveNewMessageHandler));
                        break;
                    case "save & close":
                        MessageBox.Show(Utility.Dictionary.GetWord("q_save_record_close"), Utility.Dictionary.GetWord("Save Confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveCloseMessageHandler));
                        break;
                    case "delete":
                        MessageBox.Show(Utility.Dictionary.GetWord("q_delete_record"), Utility.Dictionary.GetWord("Delete Confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(DeleteConfirmationHandler));
                        break;
                    case "print":
                        //Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "open"));
                        cmdPreview_Click();
                        break;
                }
            }
        }
        #endregion

        #region Bind Data To Report(ClickPrint)
        private DataTable BindData()
        {
            string sql = @" SELECT TOP 100 PERCENT *
                            FROM vwRptBatchCAP
                            WHERE	TxNumber BETWEEN '" + this.txtTxNumber.Text.Trim() + @"' AND '" + this.txtTxNumber.Text.Trim() + @"' 
                              AND CONVERT(VARCHAR(10), TxDate, 126) BETWEEN '" + this.dtpRecvDate.Value.ToString("yyyy-MM-dd") + @"' 
                                                                        AND '" + this.dtpRecvDate.Value.ToString("yyyy-MM-dd") + @"' 
                              AND TxType = '" + Common.Enums.TxType.CAP.ToString() + @"' 
                            ORDER BY TxNumber, TxDate, LineNumber
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

            RT2008.DAL.Staff curUser = RT2008.DAL.Staff.Load(Common.Config.CurrentUserId);
            string[,] param = {
                { "FromTxNumber", this.txtTxNumber.Text.Trim() },
                { "ToTxNumber", this.txtTxNumber.Text.Trim() },
                { "FromTxDate", this.dtpRecvDate.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "ToTxDate", this.dtpRecvDate.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                { "PrintedBy", curUser.FullName },
                { "StockCode", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() },
                { "CompanyName", RT2008.SystemInfo.CurrentInfo.Default.CompanyName}
                };

            RT2008.Controls.Reporting.Viewer oViewer = new RT2008.Controls.Reporting.Viewer();

            oViewer.Datasource = BindData();
            oViewer.ReportName = "RT2008.Inventory.GoodsReceive.Reports.WorksheetRdl.rdlc";
            oViewer.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptBatchCAP";
            oViewer.Parameters = param;

            oViewer.Show();
        }

        #endregion

        #region Fill Combo List
        private void FillCboList()
        {
            FillLocationList();
            FillStaffList();
            FillSupplierList();
            FillCurrencyList();
        }

        private void FillLocationList()
        {
            RT2008.DAL.Workplace.LoadCombo(ref cboWorkplace, new string[] { "WorkplaceCode", "WorkplaceInitial" }, "{0} - {1}", false, false, string.Empty, string.Empty, null);
        }

        private void FillStaffList()
        {
            RT2008.DAL.Staff.LoadCombo(ref cboOperatorCode, new string[] { "StaffNumber", "FullName" }, "{0} - {1}", false, false, string.Empty, string.Empty, null);

            cboOperatorCode.SelectedValue = Common.Config.CurrentUserId;
        }

        private void FillSupplierList()
        {
            RT2008.DAL.Supplier.LoadCombo(ref cboSupplierList, new string[] { "SupplierCode", "SupplierInitial" }, "{0} - {1}", false, false, string.Empty, string.Empty, null);
        }

        private void FillCurrencyList()
        {
            RT2008.DAL.Currency.LoadCombo(ref cboCurrency, "CurrencyCode", false);

            if (cboCurrency.Items.Count > 0)
            {
                cboCurrency.Text = SystemInfo.CurrentInfo.Default.SysInfo.BasicCurrency;

                InitCurrency(cboCurrency.Text);
            }
        }
        #endregion

        #region Save CAP Header Info
        private bool Save()
        {
            bool isSave = false;
            if (lvDetailsList.Items.Count > 0)
            {
                SaveCAP();
                isSave = true;
            }
            else
            {
                MessageBox.Show(Utility.Dictionary.GetWord("err_save_without_details"), Utility.Dictionary.GetWord("Save") + " " + Utility.Dictionary.GetWord("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabGoodsCAP.SelectedIndex = 1;
            }
            return isSave;
        }

        private void SaveCAP()
        {
            InvtBatchCAP_Header oHeader = InvtBatchCAP_Header.Load(this.CAPId);
            if (oHeader == null)
            {
                oHeader = new InvtBatchCAP_Header();

                txtTxNumber.Text = RT2008.SystemInfo.Settings.QueuingTxNumber(Common.Enums.TxType.CAP);
                oHeader.TxNumber = txtTxNumber.Text;
                oHeader.TxType = Common.Enums.TxType.CAP.ToString();

                oHeader.CreatedBy = Common.Config.CurrentUserId;
                oHeader.CreatedOn = DateTime.Now;
            }
            oHeader.TxDate = dtpRecvDate.Value;
            oHeader.Status = Convert.ToInt32(cboStatus.Text == "HOLD" ? Common.Enums.Status.Draft.ToString("d") : Common.Enums.Status.Active.ToString("d"));

            oHeader.WorkplaceId = new Guid(cboWorkplace.SelectedValue.ToString());
            oHeader.StaffId = new Guid(cboOperatorCode.SelectedValue.ToString());
            oHeader.SupplierId = new Guid(cboSupplierList.SelectedValue.ToString());
            oHeader.SupplierRefernce = txtSupplierInvoice.Text;
            oHeader.Remarks = txtRemarks.Text;
            oHeader.Reference = txtRefNumber.Text;
            oHeader.CurrencyCode = cboCurrency.Text;
            oHeader.ExchangeRate = Convert.ToDecimal(txtCoefficient.Text.Length == 0 ? "1" : txtCoefficient.Text);
            oHeader.LinkToAP = chkAPLink.Checked;

            oHeader.ModifiedBy = Common.Config.CurrentUserId;
            oHeader.ModifiedOn = DateTime.Now;

            oHeader.Save();

            // log activity (New Record)
            RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Create, oHeader.ToString());

            this.CAPId = oHeader.HeaderId;

            SaveCAPDetail();
            UpdateHeaderInfo();
        }

        private void UpdateHeaderInfo()
        {
            InvtBatchCAP_Header oHeader = InvtBatchCAP_Header.Load(this.CAPId);
            if (oHeader != null)
            {
                oHeader.TotalAmount = Convert.ToDecimal(Common.Utility.IsNumeric(txtTotalAmount.Text) ? txtTotalAmount.Text : "0");
                oHeader.Save();

                // log activity (Update)
                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Update, oHeader.ToString());
            }
        }
        #endregion

        #region Load CAP Header Info
        private void LoadCAPInfo()
        {
            InvtBatchCAP_Header oHeader = InvtBatchCAP_Header.Load(this.CAPId);
            if (oHeader != null)
            {
                txtTxNumber.Text = oHeader.TxNumber;
                txtTxType.Text = oHeader.TxType;

                cboWorkplace.SelectedValue = oHeader.WorkplaceId;
                cboOperatorCode.SelectedValue = oHeader.StaffId;
                cboStatus.Text = (oHeader.Status == 0) ? "HOLD" : "POST";

                dtpRecvDate.Value = oHeader.TxDate;

                cboSupplierList.SelectedValue = oHeader.SupplierId;
                txtSupplierInvoice.Text = oHeader.SupplierRefernce;
                txtRemarks.Text = oHeader.Remarks;
                txtRefNumber.Text = oHeader.Reference;

                cboCurrency.Text = oHeader.CurrencyCode;
                txtCoefficient.Text = oHeader.ExchangeRate.ToString("n4");
                InitCurrency(oHeader.CurrencyCode);

                txtLastUpdateOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oHeader.ModifiedOn, false);
                txtLastUpdateBy.Text = GetStaffName(oHeader.ModifiedBy);

                txtAmendmentRetrict.Text = oHeader.ReadOnly ? "Y" : "N";
                chkAPLink.Checked = oHeader.LinkToAP;

                txtTotalQty.Text = GetTotalRequiredQty().ToString("n0");
                txtTotalAmount.Text = GetTotalAmount().ToString("n2");

                this.Text += oHeader.ReadOnly ? " (ReadOnly)" : "";

                BindCAPDetailsInfo();
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

        private decimal GetTotalRequiredQty()
        {
            decimal totalQty = 0;

            string sql = "HeaderId = '" + this.CAPId.ToString() + "'";
            InvtBatchCAP_DetailsCollection oDetails = InvtBatchCAP_Details.LoadCollection(sql);
            foreach (InvtBatchCAP_Details oDetail in oDetails)
            {
                totalQty += oDetail.Qty;
            }

            return totalQty;
        }

        private decimal GetTotalAmount()
        {
            decimal totalAmt = 0;

            string sql = "HeaderId = '" + this.CAPId.ToString() + "'";
            InvtBatchCAP_DetailsCollection oDetails = InvtBatchCAP_Details.LoadCollection(sql);
            foreach (InvtBatchCAP_Details oDetail in oDetails)
            {
                decimal xchgRate = Convert.ToDecimal(Common.Utility.IsNumeric(txtCoefficient.Text) ? txtCoefficient.Text.Trim() : "1");
                totalAmt += oDetail.UnitAmount * oDetail.Qty;
            }

            return totalAmt;
        }
        #endregion

        #region Delete
        private void Delete()
        {
            InvtBatchCAP_Header oHeader = InvtBatchCAP_Header.Load(this.CAPId);
            if (oHeader != null)
            {
                string sql = "HeaderId = '" + oHeader.HeaderId.ToString() + "'";

                DeleteDetails(sql);

                oHeader.Delete();
                // log activity
                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, oHeader.ToString());
            }
        }

        private void DeleteDetails(string sql)
        {
            InvtBatchCAP_DetailsCollection oDetailList = InvtBatchCAP_Details.LoadCollection(sql);
            foreach (InvtBatchCAP_Details oDetail in oDetailList)
            {
                oDetail.Delete();
                // log activity
                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, oDetail.ToString());
            }
        }
        #endregion

        #region Message Handler
        private void SaveMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (Save())
                    {
                        RT2008.SystemInfo.Settings.RefreshMainList<Default>();
                        MessageBox.Show(Utility.Dictionary.GetWord("Success"), Utility.Dictionary.GetWord("Save") + " " + Utility.Dictionary.GetWord("Result"));

                        this.Close();
                        RT2008.Inventory.GoodsReceive.Wizard wizard = new RT2008.Inventory.GoodsReceive.Wizard(this.CAPId);
                        wizard.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show(Utility.Dictionary.GetWord("err_readonly_transaction"), Utility.Dictionary.GetWord("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void SaveNewMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (Save())
                    {
                        RT2008.SystemInfo.Settings.RefreshMainList<Default>();
                        this.Close();
                        RT2008.Inventory.GoodsReceive.Wizard wizard = new RT2008.Inventory.GoodsReceive.Wizard();
                        wizard.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show(Utility.Dictionary.GetWord("err_readonly_transaction"), Utility.Dictionary.GetWord("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void SaveCloseMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (!this.Text.Contains("ReadOnly"))
                {
                    if (Save())
                    {
                        RT2008.SystemInfo.Settings.RefreshMainList<Default>();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(Utility.Dictionary.GetWord("err_readonly_transaction"), Utility.Dictionary.GetWord("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (Save())
                {
                    MessageBox.Show(Utility.Dictionary.GetWord("Success"), Utility.Dictionary.GetWord("Save") + " " + Utility.Dictionary.GetWord("Result"));

                    this.Close();
                    RT2008.Inventory.GoodsReceive.Wizard wizard = new RT2008.Inventory.GoodsReceive.Wizard(this.CAPId);
                    wizard.ShowDialog();
                }
            }
            else
            {
                tabGoodsCAP.SelectedIndex = 0;
            }
        }
        #endregion

        #region CAP Detail

        #region Bind CAP Detail List
        private void BindCAPDetailsInfo()
        {
            lvDetailsList.Items.Clear();

            int iCount = 1;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  DetailsId, LineNumber, STKCODE, APPENDIX1, APPENDIX2, APPENDIX3, ProductName, ");
            sql.Append(" Qty, UnitAmountInForeignCurrency, UnitAmount, ProductId ");
            sql.Append(" FROM vwCAPDetailsList ");
            sql.Append(" WHERE HeaderId = '").Append(this.CAPId.ToString()).Append("'");
            sql.Append(" ORDER BY LineNumber, STKCODE, APPENDIX1, APPENDIX2, APPENDIX3 ");

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql.ToString();
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem listItem = lvDetailsList.Items.Add(reader.GetGuid(0).ToString()); // DetailsId
                    listItem.SubItems.Add(iCount.ToString()); // LineNumber
                    listItem.SubItems.Add(string.Empty);
                    listItem.SubItems.Add(reader.GetString(2)); // STKCode
                    listItem.SubItems.Add(reader.GetString(3)); // Appendix1
                    listItem.SubItems.Add(reader.GetString(4)); // Appendix2
                    listItem.SubItems.Add(reader.GetString(5)); // Appendix3
                    listItem.SubItems.Add(reader.GetString(6)); // ProductName
                    listItem.SubItems.Add(reader.GetDecimal(7).ToString("n0")); // Qty
                    listItem.SubItems.Add(reader.GetDecimal(8).ToString("n2")); // UnitPrice
                    listItem.SubItems.Add(reader.GetDecimal(9).ToString("n2")); // UnitPrice
                    listItem.SubItems.Add((reader.GetDecimal(7) * reader.GetDecimal(9)).ToString("n2")); // Amount
                    listItem.SubItems.Add(reader.GetGuid(10).ToString()); // ProductId

                    iCount++;
                }
            }

            lblLineCount.Text = (iCount - 1).ToString();
        }
        #endregion

        #region Save CAP Detail Info
        private void SaveCAPDetail()
        {
            foreach (ListViewItem listItem in lvDetailsList.Items)
            {
                if (Common.Utility.IsGUID(listItem.Text.Trim()) && Common.Utility.IsGUID(listItem.SubItems[12].Text.Trim()))
                {
                    System.Guid detailId = new Guid(listItem.Text.Trim());
                    InvtBatchCAP_Details oDetail = InvtBatchCAP_Details.Load(detailId);
                    if (oDetail == null)
                    {
                        oDetail = new InvtBatchCAP_Details();
                        oDetail.HeaderId = this.CAPId;
                        oDetail.TxNumber = txtTxNumber.Text;
                        oDetail.TxType = txtTxType.Text;
                        oDetail.LineNumber = Convert.ToInt32(listItem.SubItems[1].Text.Length == 0 ? "1" : listItem.SubItems[1].Text);
                    }
                    oDetail.ProductId = new Guid(listItem.SubItems[12].Text.Trim());
                    oDetail.Qty = Convert.ToDecimal(listItem.SubItems[8].Text.Length == 0 ? "0" : listItem.SubItems[8].Text);
                    oDetail.UnitAmount = Convert.ToDecimal(listItem.SubItems[10].Text.Length == 0 ? "0" : listItem.SubItems[10].Text);
                    oDetail.UnitAmountInForeignCurrency = Convert.ToDecimal(listItem.SubItems[9].Text.Length == 0 ? "0" : listItem.SubItems[9].Text);

                    if (listItem.SubItems[2].Text.Trim().ToUpper() == "REMOVED" && detailId != System.Guid.Empty)
                    {
                        oDetail.Delete();
                    }
                    else
                    {
                        oDetail.Save();
                    }
                }
            }
        }
        #endregion

        #region Load CAP Detail Info
        private void LoadCAPDetailsInfo(ListViewItem detail)
        {
            if (lvDetailsList.SelectedItem != null && lvDetailsList.SelectedItem.SubItems[2].Text != "REMOVED")
            {
                if (Common.Utility.IsGUID(lvDetailsList.SelectedItem.SubItems[12].Text))
                {
                    this.ValidSelection = true;

                    this.SelectedIndex = lvDetailsList.SelectedIndex;

                    Guid prodId = new Guid(lvDetailsList.SelectedItem.SubItems[12].Text);
                    decimal unitPrice = Convert.ToDecimal(Common.Utility.IsNumeric(lvDetailsList.SelectedItem.SubItems[9].Text.Trim()) ? lvDetailsList.SelectedItem.SubItems[9].Text.Trim() : "0"); // UnitAmountInForeignCurrency
                    decimal qty = Convert.ToDecimal(Common.Utility.IsNumeric(lvDetailsList.SelectedItem.SubItems[8].Text.Trim()) ? lvDetailsList.SelectedItem.SubItems[8].Text.Trim() : "0");
                    decimal xchgRate = Convert.ToDecimal(Common.Utility.IsNumeric(txtCoefficient.Text) ? txtCoefficient.Text : "1");

                    txtDescription.Text = lvDetailsList.SelectedItem.SubItems[7].Text;

                    txtQty.Text = qty.ToString("n0");
                    txtUnitPrice_1.Text = unitPrice.ToString("n2");
                    txtUnitPrice_2.Text = (unitPrice * xchgRate).ToString("n2");

                    txtLastCost_1.Text = txtUnitPrice_1.Text;
                    txtLastCost_2.Text = txtUnitPrice_2.Text;
                    txtAvrCost_1.Text = txtUnitPrice_1.Text;
                    txtAvrCost_2.Text = txtUnitPrice_2.Text;

                    this.ProductId = prodId;
                    this.CAPDetailId = new Guid(lvDetailsList.SelectedItem.Text);

                    basicProduct.ResultList = SetDetailData(lvDetailsList.SelectedItem.SubItems[3].Text);
                    SetStockCode(prodId);

                    this.ValidSelection = false;
                }
            }
        }

        private void SetStockCode(Guid productId)
        {
            DAL.Product oProd = DAL.Product.Load(productId);
            if (oProd != null)
            {
                if (rbtnBarcode.Checked)
                {
                    txtBarcode.Text = oProd.STKCODE + oProd.APPENDIX1 + oProd.APPENDIX2 + oProd.APPENDIX3;

                    basicProduct.SelectedItem = System.Guid.Empty;
                }

                if (rbtnStockCode_1.Checked)
                {
                    txtStockCode.Text = oProd.STKCODE;
                    txtAppendix1.Text = oProd.APPENDIX1;
                    txtAppendix2.Text = oProd.APPENDIX2;
                    txtAppendix3.Text = oProd.APPENDIX3;

                    basicProduct.SelectedItem = System.Guid.Empty;
                }

                if (rbtnStockCode_2.Checked)
                {
                    basicProduct.SelectedText = oProd.STKCODE + " " + oProd.APPENDIX1 + " " + oProd.APPENDIX2 + " " + oProd.APPENDIX3;
                    basicProduct.SelectedItem = oProd.ProductId;
                }
            }
        }
        #endregion

        #endregion

        #region Add/Edit/Remove Item
        private bool IsDuplicated(string stkCode, string appendix1, string appendix2, string appendix3)
        {
            bool isDuplicated = false;

            foreach (ListViewItem oItem in lvDetailsList.Items)
            {
                isDuplicated = (oItem.SubItems[3].Text == stkCode);
                isDuplicated = isDuplicated & (oItem.SubItems[4].Text == appendix1);
                isDuplicated = isDuplicated & (oItem.SubItems[5].Text == appendix2);
                isDuplicated = isDuplicated & (oItem.SubItems[6].Text == appendix3);

                if (isDuplicated)
                    break;
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
                    listItem.SubItems.Add(lvDetailsList.Items.Count.ToString());
                    listItem.SubItems.Add("NEW"); // Status
                    listItem.SubItems.Add(stkCode); // Stock Code
                    listItem.SubItems.Add(appendix1); // Appendix1
                    listItem.SubItems.Add(appendix2); // Appendix2
                    listItem.SubItems.Add(appendix3); // Appendix3
                    listItem.SubItems.Add(txtDescription.Text); // Description
                    listItem.SubItems.Add(txtQty.Text.Length == 0 ? "0" : txtQty.Text); // Qty
                    listItem.SubItems.Add(txtUnitPrice_1.Text.Length == 0 ? "0" : txtUnitPrice_1.Text); // Unit Amount
                    listItem.SubItems.Add(txtUnitPrice_2.Text.Length == 0 ? "0" : txtUnitPrice_2.Text); // Unit Amount(HKD)

                    decimal qty = Convert.ToDecimal(txtQty.Text.Length == 0 ? "0" : txtQty.Text);
                    decimal uamt = Convert.ToDecimal(txtUnitPrice_2.Text.Length == 0 ? "0" : txtUnitPrice_2.Text);
                    decimal amt = qty * uamt;
                    listItem.SubItems.Add(amt.ToString("n2")); // Amount
                    listItem.SubItems.Add(this.ProductId.ToString()); // ProductId

                    CalcTotal();
                }
            }
        }

        private void ItemInfo(ref string stkCode, ref string appendix1, ref string appendix2, ref string appendix3)
        {
            if (rbtnBarcode.Checked)
            {
                string sql = "Barcode = '" + txtBarcode.Text + "'";
                ProductBarcode oBarcode = ProductBarcode.LoadWhere(sql);
                if (oBarcode != null)
                {
                    RT2008.DAL.Product oProd = RT2008.DAL.Product.Load(oBarcode.ProductId);
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
            if (rbtnStockCode_1.Checked)
            {
                RT2008.DAL.Product oProd = RT2008.DAL.Product.Load(this.ProductId);
                if (oProd != null)
                {
                    stkCode = oProd.STKCODE;
                    appendix1 = oProd.APPENDIX1;
                    appendix2 = oProd.APPENDIX2;
                    appendix3 = oProd.APPENDIX3;

                    this.ProductId = oProd.ProductId;
                }
            }
            if (rbtnStockCode_2.Checked)
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
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            string stkCode = string.Empty, appendix1 = string.Empty, appendix2 = string.Empty, appendix3 = string.Empty;
            ItemInfo(ref stkCode, ref appendix1, ref appendix2, ref appendix3);

            if (lvDetailsList.SelectedItem != null)
            {
                ListViewItem listItem = lvDetailsList.SelectedItem;
                listItem.SubItems[2].Text = listItem.SubItems[2].Text != "NEW" ? "EDIT" : listItem.SubItems[2].Text; // Status
                listItem.SubItems[3].Text = stkCode; // Stock Code
                listItem.SubItems[4].Text = appendix1; // Appendix1
                listItem.SubItems[5].Text = appendix2; // Appendix2
                listItem.SubItems[6].Text = appendix3; // Appendix3
                listItem.SubItems[7].Text = txtDescription.Text; // Description
                listItem.SubItems[8].Text = txtQty.Text; // Qty
                listItem.SubItems[9].Text = txtUnitPrice_1.Text; // Retail Price
                listItem.SubItems[10].Text = txtUnitPrice_2.Text; // Unit Amount (HKD)

                decimal qty = Convert.ToDecimal(txtQty.Text.Length == 0 ? "0" : txtQty.Text);
                decimal price = Convert.ToDecimal(txtUnitPrice_1.Text.Length == 0 ? "0" : txtUnitPrice_2.Text);
                decimal amt = qty * price;
                listItem.SubItems[11].Text = amt.ToString("n2"); // Amount
                listItem.SubItems[12].Text = this.ProductId.ToString(); // ProductId

                CalcTotal();

                basicProduct.ResultList = SetDetailData(stkCode);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvDetailsList.SelectedItem != null)
            {
                if (lvDetailsList.SelectedItem.Text != System.Guid.Empty.ToString())
                {
                    ListViewItem listItem = lvDetailsList.SelectedItem;
                    listItem.SubItems[2].Text = "REMOVED"; // Status
                }
                else
                {
                    lvDetailsList.Items.Remove(lvDetailsList.SelectedItem);
                    lvDetailsList.Update();
                }

                CalcTotal();
            }
        }

        private void CalcTotal()
        {
            decimal ttlQty = 0, ttlAmount = 0;
            foreach (ListViewItem listItem in lvDetailsList.Items)
            {
                if (listItem.SubItems[2].Text != "REMOVED")
                {
                    ttlQty += Convert.ToDecimal(listItem.SubItems[8].Text.Length > 0 ? listItem.SubItems[8].Text : "0");
                    ttlAmount += Convert.ToDecimal(listItem.SubItems[11].Text.Length > 0 ? listItem.SubItems[11].Text : "0");
                }
            }

            txtTotalQty.Text = ttlQty.ToString("n0");
            txtTotalAmount.Text = ttlAmount.ToString("n2");
        }
        #endregion

        #region Get Product Summary Cost
        private void GetSummaryCost(Guid productId)
        {
            decimal xchgrate = Convert.ToDecimal(Common.Utility.IsNumeric(txtCoefficient.Text.Trim()) ? txtCoefficient.Text.Trim() : "1");

            ProductCurrentSummary oSummary = ProductCurrentSummary.LoadWhere("ProductId = '" + productId.ToString() + "'");
            if (oSummary != null)
            {
                txtLastCost_1.Text = oSummary.LastCost.ToString("n2");
                txtLastCost_2.Text = (oSummary.LastCost * xchgrate).ToString("n2");
                txtAvrCost_1.Text = oSummary.AverageCost.ToString("n2");
                txtAvrCost_2.Text = (oSummary.AverageCost * xchgrate).ToString("n2");
            }
        }
        #endregion

        private void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            InitDetailFindingSelection();
            LoadCAPDetailsInfo(this.lvDetailsList.SelectedItem);
            ResetTabOrder();
            this.Update();
        }

        private void lvDetailsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCAPDetailsInfo(this.lvDetailsList.SelectedItem);
        }

        private void cboCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitCurrency(!Common.Utility.IsGUID(cboCurrency.SelectedValue.ToString()) ? System.Guid.Empty : new System.Guid(cboCurrency.SelectedValue.ToString()));
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            string sql = "Barcode = '" + txtBarcode.Text + "'";
            ProductBarcode oBarcode = ProductBarcode.LoadWhere(sql);
            if (oBarcode != null)
            {
                RT2008.DAL.Product oProd = RT2008.DAL.Product.Load(oBarcode.ProductId);
                if (oProd != null)
                {
                    txtDescription.Text = oProd.ProductName;
                    txtUnitPrice_1.Text = oProd.RetailPrice.ToString("n2");
                    txtUnitPrice_2.Text = (oProd.RetailPrice * Convert.ToDecimal(txtCoefficient.Text.Length == 0 ? "1" : txtCoefficient.Text)).ToString("n2");

                    GetSummaryCost(oProd.ProductId);

                    this.ProductId = oProd.ProductId;
                }
            }
            else
            {
                MessageBox.Show(Utility.Dictionary.GetWord("err_not_existed_barcode"), Utility.Dictionary.GetWord("Warning"));
            }
        }

        private void basicProduct_SelectionChanged(object sender, RT2008.Controls.ProductSearcher.Basic.ProductSelectionEventArgs e)
        {
            if (!this.ValidSelection)
            {
                int iCount = 0;
                int qty = (txtQty.Text.Trim().Length == 0) ? 0 : Convert.ToInt32(txtQty.Text.Trim());

                txtDescription.Text = e.Description;
                txtUnitPrice_1.Text = e.UnitPrice.ToString("n2");
                txtUnitPrice_2.Text = (e.UnitPrice * Convert.ToDecimal(txtCoefficient.Text.Length == 0 ? "1" : txtCoefficient.Text)).ToString("n2");

                GetSummaryCost(e.ProductId);

                foreach (ListViewItem lvItem in lvDetailsList.Items)
                {
                    lvItem.Selected = false;

                    if (lvItem.SubItems[12].Text == e.ProductId.ToString())
                    {
                        if (lvItem.Text != System.Guid.Empty.ToString() && Common.Utility.IsGUID(lvItem.Text))
                        {
                            if (iCount == 0)
                            {
                                txtQty.Text = lvItem.SubItems[8].Text;

                                txtLastCost_1.Text = txtUnitPrice_1.Text;
                                txtLastCost_2.Text = txtUnitPrice_2.Text;
                                txtAvrCost_1.Text = txtUnitPrice_1.Text;
                                txtAvrCost_2.Text = txtUnitPrice_2.Text;

                                this.ProductId = e.ProductId;
                                this.CAPDetailId = new Guid(lvItem.Text);
                                this.SelectedIndex = lvItem.Index;
                                lvItem.Selected = true;
                            }

                            iCount++;
                        }
                    }
                }
            }
        }

        private void txtStockCode_TextChanged(object sender, EventArgs e)
        {
            string sql = "STKCODE = '" + txtStockCode.Text + "'";
            if (FindProduct(sql))
            {
                txtAppendix1.Focus();
            }
            else
            {
                MessageBox.Show(Utility.Dictionary.GetWord("err_not_existed_stockcode"), Utility.Dictionary.GetWord("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtAppendix1_TextChanged(object sender, EventArgs e)
        {
            string sql = "STKCODE = '" + txtStockCode.Text + "' AND APPENDIX1 = '" + txtAppendix1.Text + "'";
            if (FindProduct(sql))
            {
                txtAppendix2.Focus();
            }
            else
            {
                MessageBox.Show(Utility.Dictionary.GetWord("err_not_existed_stockcode_a1"), Utility.Dictionary.GetWord("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtAppendix2_TextChanged(object sender, EventArgs e)
        {
            string sql = "STKCODE = '" + txtStockCode.Text + "' AND APPENDIX1 = '" + txtAppendix1.Text + "' AND APPENDIX2 = '" + txtAppendix2.Text + "'";
            if (FindProduct(sql))
            {
                txtAppendix3.Focus();
            }
            else
            {
                MessageBox.Show(Utility.Dictionary.GetWord("err_not_existed_stockcode_a12"), Utility.Dictionary.GetWord("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtAppendix3_TextChanged(object sender, EventArgs e)
        {
            string sql = "STKCODE = '" + txtStockCode.Text + "' AND APPENDIX1 = '" + txtAppendix1.Text + "' AND APPENDIX2 = '" + txtAppendix2.Text + "' AND APPENDIX3 = '" + txtAppendix3.Text + "'";
            if (FindProduct(sql))
            {
                if (this.CAPDetailId == System.Guid.Empty)
                {
                    GetProductInfo(sql);
                    btnAddItem.Focus();
                }
                else
                {
                    btnEditItem.Focus();
                }
            }
            else
            {
                MessageBox.Show(Utility.Dictionary.GetWord("err_not_existed_stockcode_a123"), Utility.Dictionary.GetWord("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool FindProduct(string whereClause)
        {
            RT2008.DAL.ProductCollection oProdList = RT2008.DAL.Product.LoadCollection(whereClause);
            if (oProdList.Count > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetProductInfo(string whereClause)
        {
            if (whereClause.Length > 0)
            {
                RT2008.DAL.Product oProd = RT2008.DAL.Product.LoadWhere(whereClause);
                if (oProd != null)
                {
                    if (rbtnStockCode_1.Checked)
                    {
                        txtStockCode.Text = oProd.STKCODE;
                        txtAppendix1.Text = oProd.APPENDIX1;
                        txtAppendix2.Text = oProd.APPENDIX2;
                        txtAppendix3.Text = oProd.APPENDIX3;
                    }

                    txtDescription.Text = oProd.ProductName;
                    txtUnitPrice_1.Text = oProd.RetailPrice.ToString("n2");
                    txtUnitPrice_2.Text = (oProd.RetailPrice * Convert.ToDecimal(txtCoefficient.Text.Length == 0 ? "1" : txtCoefficient.Text)).ToString("n2");

                    GetSummaryCost(oProd.ProductId);

                    this.ProductId = oProd.ProductId;
                }
            }
        }

        private void txtUnitPrice_1_TextChanged(object sender, EventArgs e)
        {
            decimal uamt = Convert.ToDecimal(Common.Utility.IsNumeric(txtUnitPrice_1.Text.Trim()) ? txtUnitPrice_1.Text.Trim() : "0");
            decimal xchgrate = Convert.ToDecimal(Common.Utility.IsNumeric(txtCoefficient.Text.Trim()) ? txtCoefficient.Text.Trim() : "1");

            txtUnitPrice_2.Text = (uamt * xchgrate).ToString("n2");
        }

        private void txtCoefficient_TextChanged(object sender, EventArgs e)
        {
            decimal uamt = Convert.ToDecimal(Common.Utility.IsNumeric(txtUnitPrice_1.Text.Trim()) ? txtUnitPrice_1.Text.Trim() : "0");
            decimal xchgrate = Convert.ToDecimal(Common.Utility.IsNumeric(txtCoefficient.Text.Trim()) ? txtCoefficient.Text.Trim() : "1");

            txtUnitPrice_2.Text = (uamt * xchgrate).ToString("n2");

            GetSummaryCost(this.ProductId);

            ResetCAPDetails(xchgrate);
            CalcTotal();
        }

        #region IWizard Members

        public void AddItemByList(List<RT2008.Controls.ProductSearcher.DetailData> resultList)
        {
            foreach (RT2008.Controls.ProductSearcher.DetailData detail in resultList)
            {
                DAL.Product oProduct = DAL.Product.Load(detail.ProductId);
                if (oProduct != null)
                {
                    string stkCode = oProduct.STKCODE;
                    string appendix1 = oProduct.APPENDIX1;
                    string appendix2 = oProduct.APPENDIX2;
                    string appendix3 = oProduct.APPENDIX3;

                    decimal uamt = detail.UnitAmount * Convert.ToDecimal(txtCoefficient.Text.Trim());
                    decimal amt = detail.Qty * uamt;

                    if (IsDuplicated(stkCode, appendix1, appendix2, appendix3))
                    {
                        foreach (ListViewItem lvItem in lvDetailsList.Items)
                        {
                            if (lvItem.SubItems[12].Text == oProduct.ProductId.ToString() && lvItem.SubItems[2].Text != "REMOVED")
                            {
                                if (lvItem.SubItems[8].Text != detail.Qty.ToString("n0") || lvItem.SubItems[9].Text != detail.UnitAmount.ToString("n2"))
                                {
                                    lvItem.SubItems[2].Text = lvItem.SubItems[2].Text != "NEW" ? "EDIT" : lvItem.SubItems[2].Text; // Status
                                    lvItem.SubItems[8].Text = detail.Qty.ToString("n0"); // QTY
                                    lvItem.SubItems[9].Text = detail.UnitAmount.ToString("n2"); // Unit Amount
                                    lvItem.SubItems[10].Text = uamt.ToString("n2"); // Unit Amount(HKD)
                                    lvItem.SubItems[11].Text = amt.ToString("n2"); // Amount
                                }
                            }
                        }
                    }
                    else
                    {
                        ListViewItem listItem = lvDetailsList.Items.Add(System.Guid.Empty.ToString());
                        listItem.SubItems.Add(lvDetailsList.Items.Count.ToString());
                        listItem.SubItems.Add("NEW"); // Status
                        listItem.SubItems.Add(stkCode); // Stock Code
                        listItem.SubItems.Add(appendix1); // Appendix1
                        listItem.SubItems.Add(appendix2); // Appendix2
                        listItem.SubItems.Add(appendix3); // Appendix3
                        listItem.SubItems.Add(oProduct.ProductName); // Description
                        listItem.SubItems.Add(detail.Qty.ToString("n0")); // Qty
                        listItem.SubItems.Add(detail.UnitAmount.ToString("n2")); // Unit Amount
                        listItem.SubItems.Add(uamt.ToString("n2")); // Unit Amount(HKD)
                        listItem.SubItems.Add(amt.ToString("n2")); // Amount
                        listItem.SubItems.Add(oProduct.ProductId.ToString()); // ProductId
                    }
                }
            }

            CalcTotal();
        }

        public List<RT2008.Controls.ProductSearcher.DetailData> SetDetailData(string STKCODE)
        {
            List<RT2008.Controls.ProductSearcher.DetailData> ResultList = new List<RT2008.Controls.ProductSearcher.DetailData>();

            foreach (ListViewItem lvItem in lvDetailsList.Items)
            {
                if (lvItem.SubItems[3].Text.Trim() == STKCODE)
                {
                    Guid prodId = new Guid(lvItem.SubItems[12].Text);
                    decimal unitPrice = Convert.ToDecimal(Common.Utility.IsNumeric(lvItem.SubItems[9].Text.Trim()) ? lvItem.SubItems[9].Text.Trim() : "0"); // UnitAmountInForeignCurrency
                    decimal qty = Convert.ToDecimal(Common.Utility.IsNumeric(lvItem.SubItems[8].Text.Trim()) ? lvItem.SubItems[8].Text.Trim() : "0");

                    RT2008.Controls.ProductSearcher.DetailData detail = ResultList.Find(d => d.ProductId == prodId);

                    if (detail == null)
                    {
                        detail = new RT2008.Controls.ProductSearcher.DetailData();
                        detail.ProductId = prodId;
                        detail.Qty = qty;
                        detail.UnitAmount = unitPrice;
                    }
                    else
                    {
                        ResultList.Remove(detail);

                        detail.Qty = qty;
                        detail.UnitAmount = unitPrice;
                    }

                    ResultList.Add(detail);
                }
            }

            return ResultList;
        }

        #endregion
    }
}
