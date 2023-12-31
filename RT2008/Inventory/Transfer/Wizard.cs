namespace RT2008.Inventory.Transfer
{
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

    public partial class Wizard : Form, IGatewayComponent, IWizard
    {
        RT2008.Inventory.Transfer.PickingNote wizPickingNote = null;

        public Wizard(Common.Enums.TxType oType)
        {
            InitializeComponent();

            this.TxType = oType;

            SetSystemLabel();
            SetAttributes();

            InitFormType();
            InitDetailFindingSelection();
            FillCboList();
            SetToolBar();
            txtTxType.Text = oType.ToString();
            txtTxNumber.Text = "Auto-Generated";
            cboStatus.Text = "HOLD";
        }

        public Wizard(Guid txferId)
        {
            InitializeComponent();

            this.TxferId = txferId;

            SetSystemLabel();
            SetAttributes();

            InitFormType();
            InitDetailFindingSelection();
            FillCboList();
            SetToolBar();
            LoadTxferInfo();
        }

        #region Set System Label
        private void SetAttributes()
        {
            this.Text = Utility.Dictionary.GetWord("Transfer") + " > " + Utility.Dictionary.GetWord("Wizard");

            lblTxType.Text = Utility.Dictionary.GetWordWithColon("TxType");
            lblTxNumber.Text = Utility.Dictionary.GetWordWithColon("TxNumber");
            lblTotalAmount.Text = string.Format(Utility.Dictionary.GetWordWithColon("total_amount_with_currency"), "$");

            tpGeneral.Text = Utility.Dictionary.GetWord("General");
            tpDetails.Text = Utility.Dictionary.GetWord("Details");

            lblFromLocation.Text = Utility.Dictionary.GetWordWithColon("from_location");
            lblToLocation.Text = Utility.Dictionary.GetWordWithColon("to_location");
            lblTxferDate.Text = Utility.Dictionary.GetWordWithColon("transfer_date");
            lblCompletionDate.Text = Utility.Dictionary.GetWordWithColon("completion_date");
            lblTransactionDate.Text = Utility.Dictionary.GetWordWithColon("txdate");
            lblOperatorCode.Text = Utility.Dictionary.GetWordWithColon("Staff");
            lblRefNumber.Text = Utility.Dictionary.GetWordWithColon("Reference");
            lblStatus.Text = Utility.Dictionary.GetWordWithColon("Status");
            lblRemarks.Text = Utility.Dictionary.GetWordWithColon("Remarks");
            lblTotalQty.Text = Utility.Dictionary.GetWordWithColon("Total Qty");
            lblLastUpdate.Text = Utility.Dictionary.GetWordWithColon("Last Update");
            lblAmendmentRetrict.Text = Utility.Dictionary.GetWordWithColon("Amendment Restrict");

            lblBarcode.Text = Utility.Dictionary.GetWordWithColon("Barcode");
            lblStockCode.Text = Utility.Dictionary.GetWordWithColon("Product");
            lblDescription.Text = Utility.Dictionary.GetWordWithColon("Description");
            lblRequiredQty.Text = Utility.Dictionary.GetWordWithColon("required_qty");
            lblRetailPrice.Text = Utility.Dictionary.GetWordWithColon("retail_price");
            lblAmount.Text = string.Format(Utility.Dictionary.GetWordWithColon("amount_with_currency"), "$");
            label1.Text = Utility.Dictionary.GetWordWithColon("number_of_line");
            lblRemarks_Details.Text = Utility.Dictionary.GetWordWithColon("remarks");

            colLN.Text = Utility.Dictionary.GetWord("LN");
            colStatus.Text = Utility.Dictionary.GetWord("Status");
            colDescription.Text = Utility.Dictionary.GetWord("Description");
            colRequiredQty.Text = Utility.Dictionary.GetWord("required_qty");
            colRetailPrice.Text = Utility.Dictionary.GetWord("retail_price");
            colAmount.Text = string.Format(Utility.Dictionary.GetWord("amount_with_currency"), "$");
            colRemarks.Text = Utility.Dictionary.GetWord("Remarks");

            btnAddItem.Text = Utility.Dictionary.GetWord("Add Item");
            btnEditItem.Text = Utility.Dictionary.GetWord("Edit Item");
            btnRemove.Text = Utility.Dictionary.GetWord("Remove Item");

            basicProduct.TxType = Common.Enums.TxType.TXF;
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

        /// <summary>
        /// Provides a way to custom handle requests.
        /// </summary>
        /// <param name="objContext">The request context.</param>
        /// <param name="strAction">The request action.</param>
        void IGatewayComponent.ProcessRequest(IContext objContext, string strAction)
        {
            DataTable dt = Reports.DataSource.Worksheet(this.txtTxNumber.Text, this.txtTxNumber.Text, this.dtpTxDate.Value, this.dtpTxDate.Value);

            string filename = txtTxNumber.Text.Trim() + ".pdf";

            RT2008.Inventory.Transfer.Reports.WorksheetRpt report = new RT2008.Inventory.Transfer.Reports.WorksheetRpt();
            report.DataSource = dt;
            report.TxNumberFrom = this.txtTxNumber.Text.Trim();
            report.TxNumberTo = this.txtTxNumber.Text.Trim();
            report.TxDateFrom = this.dtpTxDate.Value;
            report.TxDateTo = this.dtpTxDate.Value;
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

        private void InitFormType()
        {
            InvtBatchTXF_Header oHeader = InvtBatchTXF_Header.Load(this.TxferId);
            if (oHeader != null)
            {
                this.TxType = (Common.Enums.TxType)Enum.Parse(typeof(Common.Enums.TxType), oHeader.TxType, true);
            }

            switch (this.TxType)
            {
                case Common.Enums.TxType.TXF:
                    this.Text = "Transfer > Wizard";
                    this.lblStatus.Visible = true;
                    this.cboStatus.Visible = true;
                    break;
                case Common.Enums.TxType.PNQ:
                    this.Text = "Transfer > Picking Note";
                    this.lblStatus.Visible = false;
                    this.cboStatus.Visible = false;
                    this.lblAmendmentRetrict.Visible = false;
                    this.txtAmendmentRetrict.Visible = false;
                    this.lblTotalAmount.Visible = false;
                    this.txtTotalAmount.Visible = false;
                    this.tpDetails.Controls.Clear();

                    wizPickingNote = new RT2008.Inventory.Transfer.PickingNote(this.TxferId);
                    this.tpDetails.Controls.Add(wizPickingNote);
                    break;
            }
        }
        #endregion

        #region Properties
        private Common.Enums.TxType txType;
        public Common.Enums.TxType TxType
        {
            get
            {
                return txType;
            }
            set
            {
                txType = value;
            }
        }

        private Guid txferId = System.Guid.Empty;
        public Guid TxferId
        {
            get
            {
                return txferId;
            }
            set
            {
                txferId = value;
            }
        }

        private Guid txferDetailId = System.Guid.Empty;
        public Guid TxferDetailId
        {
            get
            {
                return txferDetailId;
            }
            set
            {
                txferDetailId = value;
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

            if (TxferId == System.Guid.Empty)
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
                        //Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "open"));
                        btnPreview_Click();
                        break;
                }
            }
        }
        #endregion

        #region Bind Data to Report(ClickPrint)
        private DataTable BindData()
        {
            string sql = @"SELECT TOP 100 PERCENT *
                          FROM vwRptBatchTXF
                          WHERE	TxNumber BETWEEN '" + this.txtTxNumber.Text.Trim() + @"' AND '" + this.txtTxNumber.Text.Trim() + @"' 
                          AND CONVERT(VARCHAR(10), TxDate, 126) BETWEEN '" + this.dtpTxDate.Value.ToString("yyyy-MM-dd") + @"' 
                                                                        AND '" + this.dtpTxDate.Value.ToString("yyyy-MM-dd") + @"'
                          ORDER BY STKCODE, APPENDIX1, APPENDIX2, APPENDIX3
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

        private void btnPreview_Click()
        {
            RT2008.DAL.Staff curUser = RT2008.DAL.Staff.Load(Common.Config.CurrentUserId);
            RT2008.Controls.Reporting.Viewer viewer = new RT2008.Controls.Reporting.Viewer();

            string[,] param = {
             {"FromToTxNumber",this.txtTxNumber.Text.Trim()},
             {"ToTxNumber",this.txtTxNumber.Text.Trim()},
             {"FromTxDate",this.dtpTxDate.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat())},
             {"ToTxDate",this.dtpTxDate.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat())},
             {"PrintedBy",curUser.FullName},
             {"PrintedOn",DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateFormat())},
             {"DateFormat",RT2008.SystemInfo.Settings.GetDateFormat()},
             { "CompanyName", RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                    { "StockCode", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                    { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                    { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                    { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") }
             };
            viewer.Datasource = BindData();
            viewer.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptBatchTXF";
            viewer.ReportName = "RT2008.Inventory.Transfer.Reports.WorksheetRdl.rdlc";
            viewer.Parameters = param;
            viewer.Show();
        }
        #endregion

        #region Fill Combo List
        private void FillCboList()
        {
            FillFromLocationList();
            FillToLocationList();
            FillStaffList();
        }

        private void FillFromLocationList()
        {
            RT2008.DAL.Workplace.LoadCombo(ref cboFromLocation, new string[] { "WorkplaceCode", "WorkplaceInitial" }, "{0} - {1}", false, false, string.Empty, string.Empty, null);
        }

        private void FillToLocationList()
        {
            RT2008.DAL.Workplace.LoadCombo(ref cboToLocation, new string[] { "WorkplaceCode", "WorkplaceInitial" }, "{0} - {1}", false, false, string.Empty, string.Empty, null);
            cboToLocation.SelectedIndex = cboToLocation.Items.Count - 1;
        }

        private void FillStaffList()
        {
            RT2008.DAL.Staff.LoadCombo(ref cboOperatorCode, new string[] { "StaffNumber", "FullName" }, "{0} - {1}", false, false, string.Empty, string.Empty, null);

            cboOperatorCode.SelectedValue = Common.Config.CurrentUserId;
        }
        #endregion

        #region Save Txfer Header Info

        private bool IsValid()
        {
            bool result = (errorProvider.GetError(cboFromLocation).Trim().Length == 0);
            result = result & (errorProvider.GetError(cboToLocation).Trim().Length == 0);
            result = result & (errorProvider.GetError(cboOperatorCode).Trim().Length == 0);
            result = result & (errorProvider.GetError(cboStatus).Trim().Length == 0);

            return result;
        }

        private bool Save()
        {
            bool isSave = false;
            bool hasDetails = false;

            switch (this.TxType)
            {
                case Common.Enums.TxType.TXF:
                    hasDetails = (lvDetailsList.Items.Count > 0);
                    break;
                case Common.Enums.TxType.PNQ:
                    hasDetails = (wizPickingNote.lvDetailsList.Items.Count > 0);
                    break;
            }

            if (IsValid())
            {
                if (hasDetails)
                {
                    if (!cboFromLocation.SelectedValue.Equals(cboToLocation.SelectedValue))
                    {
                        SaveTXF();
                        isSave = true;
                    }
                    else
                    {
                        MessageBox.Show("Cannot transfer to a same location!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Does not allow saving record without details", "Save Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabGoodsTxfer.SelectedIndex = 1;
                }
            }
            else
            {
                MessageBox.Show("Cannot save record until the errors are fixed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isSave;
        }

        private void SaveTXF()
        {
            bool isNew = false;
            InvtBatchTXF_Header oHeader = InvtBatchTXF_Header.Load(this.TxferId);
            if (oHeader == null)
            {
                oHeader = new InvtBatchTXF_Header();

                txtTxNumber.Text = RT2008.SystemInfo.Settings.QueuingTxNumber(this.TxType);
                oHeader.TxNumber = txtTxNumber.Text;
                oHeader.TxType = this.TxType.ToString();

                oHeader.CreatedBy = Common.Config.CurrentUserId;
                oHeader.CreatedOn = DateTime.Now;
                isNew = true;
            }
            oHeader.Status = Convert.ToInt32(cboStatus.Text == "HOLD" ? Common.Enums.Status.Draft.ToString("d") : Common.Enums.Status.Active.ToString("d"));

            oHeader.FromLocation = (cboFromLocation.SelectedValue != null) ? new Guid(cboFromLocation.SelectedValue.ToString()) : System.Guid.Empty;
            oHeader.ToLocation = (cboToLocation.SelectedValue != null) ? new Guid(cboToLocation.SelectedValue.ToString()) : System.Guid.Empty;
            oHeader.StaffId = (cboOperatorCode.SelectedValue != null) ? new Guid(cboOperatorCode.SelectedValue.ToString()) : System.Guid.Empty;
            oHeader.TxDate = dtpTxDate.Value;
            oHeader.TransferredOn = dtpTxferDate.Value;
            oHeader.CompletedOn = dtpCompDate.Value;
            oHeader.Remarks = txtRemarks.Text;

            switch (this.TxType)
            {
                case Common.Enums.TxType.TXF:
                    oHeader.Reference = txtRefNumber.Text;
                    break;
                case Common.Enums.TxType.PNQ:
                    oHeader.Picked = true;
                    oHeader.PickingNoteRef = txtRefNumber.Text;
                    break;
            }

            oHeader.ModifiedBy = Common.Config.CurrentUserId;
            oHeader.ModifiedOn = DateTime.Now;

            oHeader.Save();

            if (isNew)
            {
                // log activity (New Record)
                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Create, oHeader.ToString());
            }
            else
            {
                // log activity (Update)
                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Update, oHeader.ToString());
            }   

            this.TxferId = oHeader.HeaderId;
            
            switch (this.TxType)
            {
                case Common.Enums.TxType.TXF:
                    SaveTxferDetail();
                    break;
                case Common.Enums.TxType.PNQ:
                    SavePickingNoteDetail();
                    break;
            }
        }
        #endregion

        #region Load Txfer Header Info
        private void LoadTxferInfo()
        {
            InvtBatchTXF_Header oHeader = InvtBatchTXF_Header.Load(this.TxferId);
            if (oHeader != null)
            {
                txtTxNumber.Text = oHeader.TxNumber;
                txtTxType.Text = oHeader.TxType;

                cboFromLocation.SelectedValue = oHeader.FromLocation;
                cboToLocation.SelectedValue = oHeader.ToLocation;
                cboOperatorCode.SelectedValue = oHeader.StaffId;
                cboStatus.Text = (oHeader.Status == 0) ? "HOLD" : "POST";

                dtpTxDate.Value = oHeader.TxDate;
                dtpTxferDate.Value = oHeader.TransferredOn;
                dtpCompDate.Value = oHeader.CompletedOn;

                txtRemarks.Text = oHeader.Remarks;

                switch (this.TxType)
                {
                    case Common.Enums.TxType.TXF:
                        txtRefNumber.Text = oHeader.Reference;
                        break;
                    case Common.Enums.TxType.PNQ:
                        txtRefNumber.Text = oHeader.PickingNoteRef;
                        break;
                }

                txtLastUpdateOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(oHeader.ModifiedOn, false);
                txtLastUpdateBy.Text = GetStaffName(oHeader.ModifiedBy);

                txtAmendmentRetrict.Text = oHeader.ReadOnly ? "Y" : "N";

                tbWizardAction.Buttons[0].Enabled = !oHeader.ReadOnly;
                tbWizardAction.Buttons[1].Enabled = !oHeader.ReadOnly;
                tbWizardAction.Buttons[2].Enabled = !oHeader.ReadOnly;
                tbWizardAction.Buttons[4].Enabled = !oHeader.ReadOnly;

                txtTotalQty.Text = GetTotalRequiredQty().ToString("n0");
                txtTotalAmount.Text = GetTotalAmount().ToString("n2");

                BindTxferDetailsInfo();
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

            string sql = "HeaderId = '" + this.TxferId.ToString() + "'";
            InvtBatchTXF_DetailsCollection oDetails = InvtBatchTXF_Details.LoadCollection(sql);
            foreach (InvtBatchTXF_Details oDetail in oDetails)
            {
                totalQty += oDetail.QtyRequested;
            }

            return totalQty;
        }

        private decimal GetTotalAmount()
        {
            decimal totalAmt = 0;

            string sql = "HeaderId = '" + this.TxferId.ToString() + "'";
            InvtBatchTXF_DetailsCollection oDetails = InvtBatchTXF_Details.LoadCollection(sql);
            foreach (InvtBatchTXF_Details oDetail in oDetails)
            {
                totalAmt += oDetail.QtyRequested * GetRetailPrice(oDetail.ProductId);
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
            InvtBatchTXF_Header oHeader = InvtBatchTXF_Header.Load(this.TxferId);
            if (oHeader != null)
            {
                string sql = "HeaderId = '" + oHeader.HeaderId.ToString() + "'";

                DeleteDetails(sql);

                oHeader.Delete();

                // log activity (Delete)
                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, oHeader.ToString());
            }
        }

        private void DeleteDetails(string sql)
        {
            InvtBatchTXF_DetailsCollection oDetailList = InvtBatchTXF_Details.LoadCollection(sql);
            foreach (InvtBatchTXF_Details oDetail in oDetailList)
            {
                oDetail.Delete();

                // log activity (Delete)
                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, oDetail.ToString());
            }
        }
        #endregion

        #region Message Handler
        private void SaveMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save())
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<Default>();
                    MessageBox.Show("Success!", "Save Result");

                    this.Close();
                    RT2008.Inventory.Transfer.Wizard wizard = new RT2008.Inventory.Transfer.Wizard(this.TxferId);
                    wizard.ShowDialog();
                }
            }
        }

        private void SaveNewMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save())
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<Default>();
                    this.Close();
                    RT2008.Inventory.Transfer.Wizard wizard = new RT2008.Inventory.Transfer.Wizard(this.TxType);
                    wizard.ShowDialog();
                }
            }
        }

        private void SaveCloseMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save())
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<Default>();
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
                if (Save())
                {
                    MessageBox.Show("Success!", "Save Result");

                    this.Close();
                    RT2008.Inventory.Transfer.Wizard wizard = new RT2008.Inventory.Transfer.Wizard(this.TxferId);
                    wizard.ShowDialog();
                }
            }
            else
            {
                tabGoodsTxfer.SelectedIndex = 0;
            }
        }
        #endregion

        #region Txfer Detail

        #region Bind Txfer Detail List
        private void BindTxferDetailsInfo()
        {
            int iCount = 1;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  DetailsId, LineNumber, STKCODE, APPENDIX1, APPENDIX2, APPENDIX3, ProductName, ");
            sql.Append(" QtyRequested, RetailPrice, Amount, Remarks, ProductId ");
            sql.Append(" FROM vwTxferDetailsList ");
            sql.Append(" WHERE HeaderId = '").Append(this.TxferId.ToString()).Append("'");
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
                    listItem.SubItems.Add(reader.GetDecimal(7).ToString("n0")); // QtyRequested
                    listItem.SubItems.Add(reader.GetDecimal(8).ToString("n2")); // RetailPrice
                    listItem.SubItems.Add(reader.GetDecimal(9).ToString("n2")); // Amount
                    listItem.SubItems.Add(reader.GetString(10)); // Remarks
                    listItem.SubItems.Add(reader.GetGuid(11).ToString()); // ProductId

                    iCount++;
                }
            }

            lblLineCount.Text = (iCount - 1).ToString();
        }
        #endregion

        #region Save Txfer Detail Info
        private void SaveTxferDetail()
        {
            foreach (ListViewItem listItem in lvDetailsList.Items)
            {
                if (Common.Utility.IsGUID(listItem.Text.Trim()) && Common.Utility.IsGUID(listItem.SubItems[12].Text.Trim()))
                {
                    System.Guid detailId = new Guid(listItem.Text.Trim());
                    InvtBatchTXF_Details oDetail = InvtBatchTXF_Details.Load(detailId);
                    if (oDetail == null)
                    {
                        oDetail = new InvtBatchTXF_Details();
                        oDetail.HeaderId = this.TxferId;
                        oDetail.TxNumber = txtTxNumber.Text;
                        oDetail.TxType = txtTxType.Text;
                        oDetail.LineNumber = Convert.ToInt32(listItem.SubItems[1].Text.Length == 0 ? "1" : listItem.SubItems[1].Text);
                    }
                    oDetail.ProductId = new Guid(listItem.SubItems[12].Text.Trim());
                    oDetail.QtyRequested = Convert.ToDecimal(listItem.SubItems[8].Text.Length == 0 ? "0" : listItem.SubItems[8].Text);
                    oDetail.QtyConfirmed = 0;
                    oDetail.QtyHHT = 0;
                    oDetail.QtyManualInput = 0;
                    oDetail.QtyReceived = 0;
                    oDetail.Remarks = listItem.SubItems[11].Text;

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

        #region Load Txfer Detail Info
        private void LoadTxferDetailsInfo()
        {
            if (lvDetailsList.SelectedItem != null)
            {
                if (Common.Utility.IsGUID(lvDetailsList.SelectedItem.Text))
                {
                    this.ValidSelection = true;

                    this.TxferDetailId = new Guid(lvDetailsList.SelectedItem.Text);
                    this.SelectedIndex = lvDetailsList.SelectedIndex;
                    this.ProductId = new Guid(lvDetailsList.SelectedItem.SubItems[12].Text);

                    SetStockCode();

                    txtDescription.Text = lvDetailsList.SelectedItem.SubItems[7].Text;
                    txtRequiredQty.Text = lvDetailsList.SelectedItem.SubItems[8].Text;
                    txtRetailPrice.Text = lvDetailsList.SelectedItem.SubItems[9].Text;
                    txtAmount.Text = lvDetailsList.SelectedItem.SubItems[10].Text;
                    txtRemarks_Detail.Text = lvDetailsList.SelectedItem.SubItems[11].Text;

                    this.txtRemarks_Detail.Text = lvDetailsList.SelectedItem.SubItems[11].Text;

                    basicProduct.ResultList = SetDetailData(lvDetailsList.SelectedItem.SubItems[3].Text);

                    this.ValidSelection = false;
                }
            }
        }

        private void SetStockCode()
        {
            DAL.Product oProd = DAL.Product.Load(this.ProductId);
            if (oProd != null)
            {
                if (rbtnBarcode.Checked)
                {
                    txtBarcode.Text = oProd.STKCODE + oProd.APPENDIX1 + oProd.APPENDIX2 + oProd.APPENDIX3;

                    txtStockCode.Text = string.Empty;
                    txtAppendix1.Text = string.Empty;
                    txtAppendix2.Text = string.Empty;
                    txtAppendix3.Text = string.Empty;

                    basicProduct.SelectedItem = System.Guid.Empty;
                }

                if (rbtnStockCode_1.Checked)
                {
                    txtStockCode.Text = oProd.STKCODE;
                    txtAppendix1.Text = oProd.APPENDIX1;
                    txtAppendix2.Text = oProd.APPENDIX2;
                    txtAppendix3.Text = oProd.APPENDIX3;

                    txtBarcode.Text = string.Empty;
                    basicProduct.SelectedItem = System.Guid.Empty;
                }

                if (rbtnStockCode_2.Checked)
                {
                    basicProduct.SelectedText = oProd.STKCODE + " " + oProd.APPENDIX1 + " " + oProd.APPENDIX2 + " " + oProd.APPENDIX3;
                    basicProduct.SelectedItem = oProd.ProductId;

                    txtBarcode.Text = string.Empty;

                    txtStockCode.Text = string.Empty;
                    txtAppendix1.Text = string.Empty;
                    txtAppendix2.Text = string.Empty;
                    txtAppendix3.Text = string.Empty;
                }
            }
        }
        #endregion

        #endregion

        #region Picking Note

        #region Save Picking Note Detail Info
        private void SavePickingNoteDetail()
        {
            foreach (ListViewItem listItem in wizPickingNote.lvDetailsList.Items)
            {
                if (Common.Utility.IsGUID(listItem.Text.Trim()) && Common.Utility.IsGUID(listItem.SubItems[10].Text.Trim()))
                {
                    System.Guid detailId = new Guid(listItem.Text.Trim());
                    InvtBatchTXF_Details oDetail = InvtBatchTXF_Details.Load(detailId);
                    if (oDetail == null)
                    {
                        oDetail = new InvtBatchTXF_Details();
                        oDetail.HeaderId = this.TxferId;
                        oDetail.TxNumber = txtTxNumber.Text;
                        oDetail.TxType = txtTxType.Text;
                        oDetail.LineNumber = Convert.ToInt32(listItem.SubItems[1].Text.Length == 0 ? "1" : listItem.SubItems[1].Text);
                    }
                    oDetail.ProductId = new Guid(listItem.SubItems[10].Text.Trim());
                    oDetail.QtyRequested = Convert.ToDecimal(listItem.SubItems[8].Text.Length == 0 ? "0" : listItem.SubItems[8].Text);
                    oDetail.QtyConfirmed = 0;
                    oDetail.QtyHHT = 0;
                    oDetail.QtyManualInput = 0;
                    oDetail.QtyReceived = 0;
                    oDetail.Remarks = listItem.SubItems[9].Text;

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

        #endregion

        #region Add/Edit/Remove Item
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

                    if (isDuplicated)
                        break;
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
                //MessageBox.Show(string.Format(Resources.Common.DuplicatedCode, "Stock Item"), string.Format(Resources.Common.DuplicatedCode, string.Empty));
                foreach (ListViewItem oItem in lvDetailsList.Items)
                {
                    if (oItem.SubItems[3].Text == stkCode &&
                        oItem.SubItems[4].Text == appendix1 &&
                        oItem.SubItems[5].Text == appendix2 &&
                        oItem.SubItems[6].Text == appendix3)
                    {
                        oItem.SubItems[8].Text = Convert.ToString(Convert.ToInt32(oItem.SubItems[8].Text) + Convert.ToInt32(this.txtRequiredQty.Text));
                        detailItemInput_Clear();
                        break;
                    }
                }

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
                    listItem.SubItems.Add(txtRequiredQty.Text.Length == 0 ? "0" : txtRequiredQty.Text); // Required Qty
                    listItem.SubItems.Add(txtRetailPrice.Text); // Retail Price
                    listItem.SubItems.Add(txtAmount.Text); // Amount
                    listItem.SubItems.Add(txtRemarks_Detail.Text); // Remarks
                    listItem.SubItems.Add(this.ProductId.ToString()); // ProductId

                    CalcTotal();

                    detailItemInput_Clear();
                }
                else
                {
                    MessageBox.Show("Record not found!", "Warning");
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
                string query = string.Empty;
                if (basicProduct.SelectedItem != null)
                {
                    query = "ProductId = '" + basicProduct.SelectedItem.ToString() + "'";
                }
                else if (basicProduct.cboFullStockCode.Text.Trim().Length > 0)
                {
                    query = BuildWhereClause(basicProduct.cboFullStockCode.Text.Trim());
                }

                if (query.Length > 0)
                {
                    RT2008.DAL.Product oProd = RT2008.DAL.Product.LoadWhere(query);
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

        private string BuildWhereClause(string value)
        {
            StringBuilder whereClause = new StringBuilder();
            string a1 = string.Empty, a2 = string.Empty, a3 = string.Empty;

            if (value.Contains(" "))
            {
                string[] prodCode = value.Split(new char[] { ' ' });
                switch (prodCode.Length)
                {
                    case 1:
                        whereClause.Append(" STKCODE = '").Append(prodCode[0]).Append("'");
                        break;
                    case 2:
                        whereClause.Append(" STKCODE = '").Append(prodCode[0]).Append("'");
                        whereClause.Append(" AND APPENDIX1 = '").Append(prodCode[1]).Append("'");
                        break;
                    case 3:
                        whereClause.Append(" STKCODE = '").Append(prodCode[0]).Append("'");
                        whereClause.Append(" AND APPENDIX1 = '").Append(prodCode[1]).Append("'");
                        whereClause.Append(" AND APPENDIX2 = '").Append(prodCode[2]).Append("'");
                        break;
                    case 4:
                        whereClause.Append(" STKCODE = '").Append(prodCode[0]).Append("'");
                        whereClause.Append(" AND APPENDIX1 = '").Append(prodCode[1]).Append("'");
                        whereClause.Append(" AND APPENDIX2 = '").Append(prodCode[2]).Append("'");
                        whereClause.Append(" AND APPENDIX3 = '").Append(prodCode[3]).Append("'");
                        break;
                }
            }
            else
            {
                whereClause.Append(" STKCODE LIKE '" + value + "%' ");
            }

            return whereClause.ToString();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (lvDetailsList.SelectedItem != null)
            {
                string stkCode = string.Empty, appendix1 = string.Empty, appendix2 = string.Empty, appendix3 = string.Empty;
                ItemInfo(ref stkCode, ref appendix1, ref appendix2, ref appendix3);

                ListViewItem listItem = lvDetailsList.SelectedItem;
                listItem.SubItems[2].Text = listItem.SubItems[2].Text != "NEW" ? "EDIT" : listItem.SubItems[2].Text; // Status
                listItem.SubItems[3].Text = stkCode; // Stock Code
                listItem.SubItems[4].Text = appendix1; // Appendix1
                listItem.SubItems[5].Text = appendix2; // Appendix2
                listItem.SubItems[6].Text = appendix3; // Appendix3
                listItem.SubItems[7].Text = txtDescription.Text; // Description
                listItem.SubItems[8].Text = txtRequiredQty.Text; // Required Qty
                listItem.SubItems[9].Text = txtRetailPrice.Text; // Retail Price
                listItem.SubItems[10].Text = txtAmount.Text; // Amount
                listItem.SubItems[11].Text = txtRemarks_Detail.Text; // Remarks
                listItem.SubItems[12].Text = this.ProductId.ToString(); // ProductId

                basicProduct.ResultList = SetDetailData(lvDetailsList.SelectedItem.SubItems[3].Text);

                CalcTotal();
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
                    ttlAmount += Convert.ToDecimal(listItem.SubItems[10].Text.Length > 0 ? listItem.SubItems[10].Text : "0");
                }
            }

            txtTotalQty.Text = ttlQty.ToString("n0");
            txtTotalAmount.Text = ttlAmount.ToString("n2");
        }
        #endregion

        private void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            InitDetailFindingSelection();
            LoadTxferDetailsInfo();
            this.Update();
        }

        private void txtRequiredQty_TextChanged(object sender, EventArgs e)
        {
            int testValue;
            if (int.TryParse(this.txtRequiredQty.Text, out testValue))
            {
                int qty = (txtRequiredQty.Text.Trim().Length == 0) ? 0 : Convert.ToInt32(txtRequiredQty.Text.Trim());
                decimal price = (txtRetailPrice.Text.Trim().Length == 0) ? 0 : Convert.ToDecimal(txtRetailPrice.Text.Trim());
                decimal amt = (decimal)qty * price;
                txtAmount.Text = amt.ToString("n2");
            }
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBarcode.Text == string.Empty)
            {
                detailItemInput_Clear();
            }
            else
            {
                string sql = "Barcode = '" + txtBarcode.Text + "'";
                ProductBarcode oBarcode = ProductBarcode.LoadWhere(sql);
                if (oBarcode != null)
                {
                    RT2008.DAL.Product oProd = RT2008.DAL.Product.Load(oBarcode.ProductId);
                    if (oProd != null)
                    {
                        int qty = (txtRequiredQty.Text.Trim().Length == 0) ? 0 : Convert.ToInt32(txtRequiredQty.Text.Trim());

                        txtDescription.Text = oProd.ProductName;
                        txtRetailPrice.Text = oProd.RetailPrice.ToString("n2");
                        decimal amt = qty * oProd.RetailPrice;
                        txtAmount.Text = amt.ToString("n2");

                        this.ProductId = oProd.ProductId;
                    }
                }
                else
                {
                    MessageBox.Show("Barcode does not exist!", "Not existing barcode");
                    detailItemInput_Clear();
                }
            }
        }

        private void basicProduct_SelectionChanged(object sender, RT2008.Controls.ProductSearcher.Basic.ProductSelectionEventArgs e)
        {
            if (!this.ValidSelection)
            {
                int qty = (txtRequiredQty.Text.Trim().Length == 0) ? 0 : Convert.ToInt32(txtRequiredQty.Text.Trim());
                int iCount = 0;

                txtDescription.Text = e.Description;
                txtRetailPrice.Text = e.UnitPrice.ToString("n2");
                decimal amt = qty * e.UnitPrice;
                txtAmount.Text = amt.ToString("n2");

                foreach (ListViewItem lvItem in lvDetailsList.Items)
                {
                    lvItem.Selected = false;

                    if (lvItem.SubItems[12].Text == e.ProductId.ToString())
                    {
                        if (lvItem.Text != System.Guid.Empty.ToString() && Common.Utility.IsGUID(lvItem.Text))
                        {
                            if (iCount == 0)
                            {
                                txtRequiredQty.Text = lvItem.SubItems[8].Text;
                                txtAmount.Text = lvItem.SubItems[10].Text;
                                txtRemarks_Detail.Text = lvItem.SubItems[11].Text;

                                this.ProductId = e.ProductId;
                                this.TxferDetailId = new Guid(lvItem.Text);
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
            if (this.txtStockCode.Text == string.Empty)
            {
                detailItemInput_Clear();
            }
            else
            {
                string sql = "STKCODE = '" + txtStockCode.Text + "'";
                if (FindProduct(sql))
                {
                    txtAppendix1.Focus();
                }
                else
                {
                    MessageBox.Show("STKCODE does not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtAppendix1_TextChanged(object sender, EventArgs e)
        {
            if (this.txtStockCode.Text == string.Empty)
            {
                detailItemInput_Clear();
            }
            else
            {
                string sql = "STKCODE = '" + txtStockCode.Text + "' AND APPENDIX1 = '" + txtAppendix1.Text + "'";
                if (FindProduct(sql))
                {
                    txtAppendix2.Focus();
                }
                else
                {
                    MessageBox.Show("STKCODE with APPENDIX1 does not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtAppendix2_TextChanged(object sender, EventArgs e)
        {
            if (this.txtStockCode.Text == string.Empty)
            {
                detailItemInput_Clear();
            }
            else
            {
                string sql = "STKCODE = '" + txtStockCode.Text + "' AND APPENDIX1 = '" + txtAppendix1.Text + "' AND APPENDIX2 = '" + txtAppendix2.Text + "'";
                if (FindProduct(sql))
                {
                    txtAppendix3.Focus();
                }
                else
                {
                    MessageBox.Show("STKCODE with (APPENDIX1, APPENDIX2) does not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtAppendix3_TextChanged(object sender, EventArgs e)
        {
            if (this.txtStockCode.Text == string.Empty)
            {
                detailItemInput_Clear();
            }
            else
            {
                string sql = "STKCODE = '" + txtStockCode.Text + "' AND APPENDIX1 = '" + txtAppendix1.Text + "' AND APPENDIX2 = '" + txtAppendix2.Text + "' AND APPENDIX3 = '" + txtAppendix3.Text + "'";
                if (FindProduct(sql))
                {
                    if (this.TxferDetailId == System.Guid.Empty)
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
                    MessageBox.Show("STKCODE with (APPENDIX1, APPENDIX2, APPENDIX3) does not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private bool FindProduct(string whereClause)
        {
            RT2008.DAL.ProductCollection oProdList = RT2008.DAL.Product.LoadCollection(whereClause);
            if (oProdList.Count > 0)
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
                    int qty = (txtRequiredQty.Text.Trim().Length == 0) ? 0 : Convert.ToInt32(txtRequiredQty.Text.Trim());

                    txtDescription.Text = oProd.ProductName;
                    txtRetailPrice.Text = oProd.RetailPrice.ToString("n2");
                    decimal amt = qty * oProd.RetailPrice;
                    txtAmount.Text = amt.ToString("n2");

                    this.ProductId = oProd.ProductId;
                }
            }
        }

        private void lvDetailsList_Click(object sender, EventArgs e)
        {
            LoadTxferDetailsInfo();
        }

        private void CheckComboInput(ref ComboBox cboCtrl)
        {
            string value = cboCtrl.Text.Trim();

            if (value.Length > 4)
            {
                value = value.Substring(0, 4);
            }
            else if (value.Length < 4)
            {
                errorProvider.SetError(cboCtrl, "Please input four digits!");
            }

            bool existed = true;

            if (cboCtrl.Name.Contains("Location"))
            {
                RT2008.DAL.Workplace wp = RT2008.DAL.Workplace.LoadWhere("WorkplaceCode = '" + value + "'");
                if (wp == null)
                {
                    existed = false;
                }
            }
            else if (cboCtrl.Name.Contains("Operator"))
            {
                RT2008.DAL.Staff staff = RT2008.DAL.Staff.LoadWhere("StaffNumber = '" + value + "'");
                if (staff == null)
                {
                    existed = false;
                }
            }
            else
            {
                existed = (value == "HOLD" || value == "POST");
            }

            if (!existed)
            {
                errorProvider.SetError(cboCtrl, "Does not existe.");
            }
            else
            {
                errorProvider.SetError(cboCtrl, string.Empty);
            }
        }

        private void cboFromLocation_TextChanged(object sender, EventArgs e)
        {
            CheckComboInput(ref cboFromLocation);
        }

        private void cboToLocation_TextChanged(object sender, EventArgs e)
        {
            CheckComboInput(ref cboToLocation);
        }

        private void cboOperatorCode_TextChanged(object sender, EventArgs e)
        {
            CheckComboInput(ref cboOperatorCode);
        }

        private void cboStatus_TextChanged(object sender, EventArgs e)
        {
            CheckComboInput(ref cboStatus);
        }

        private void detailItemInput_Clear()
        {
            this.ProductId = System.Guid.Empty;
            this.txtBarcode.Text = String.Empty;
            this.txtStockCode.Text = String.Empty;
            this.txtAppendix1.Text = String.Empty;
            this.txtAppendix2.Text = String.Empty;
            this.txtAppendix3.Text = String.Empty;
            this.basicProduct.Text = String.Empty;
            this.txtDescription.Text = String.Empty;
            this.txtRemarks_Detail.Text = String.Empty;
            this.txtRetailPrice.Text = String.Empty;
            this.txtAmount.Text = String.Empty;

            //2013.12.21 paulus: #645 Opera 要求
            //this.txtRequiredQty.Text = "1";

            if (this.rbtnBarcode.Checked)
            {
                this.txtBarcode.Focus();
            }
            else if (this.rbtnStockCode_1.Checked)
            {
                this.txtStockCode.Focus();
            }
            else
            {
                this.basicProduct.Focus();
            }


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

                    decimal amt = detail.Qty * detail.UnitAmount;

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
                                    lvItem.SubItems[10].Text = amt.ToString("n2"); // Amount
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
                        listItem.SubItems.Add(amt.ToString("n2")); // Amount
                        listItem.SubItems.Add(string.Empty); // Remarks
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

        private void txtBarcode_EnterKeyDown(object sender, EventArgs e)
        {
            if (this.ProductId != System.Guid.Empty) {
                this.btnAddItem_Click(sender, e);
            }
        }
    }
}