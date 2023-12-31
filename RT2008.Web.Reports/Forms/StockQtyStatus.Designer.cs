namespace RT2008.Web.Reports.Forms
{
    partial class StockQtyStatus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPane = new Gizmox.WebGUI.Forms.Panel();
            this.splitContainer = new Gizmox.WebGUI.Forms.SplitContainer();
            this.btnLookup = new Gizmox.WebGUI.Forms.Button();
            this.btnSearch = new Gizmox.WebGUI.Forms.Button();
            this.label1 = new Gizmox.WebGUI.Forms.Label();
            this.lblBarcode = new Gizmox.WebGUI.Forms.Label();
            this.txtBarcode = new Gizmox.WebGUI.Forms.TextBox();
            this.chkQty = new Gizmox.WebGUI.Forms.CheckBox();
            this.lblSkipRecords = new Gizmox.WebGUI.Forms.Label();
            this.lblSIZE = new Gizmox.WebGUI.Forms.Label();
            this.txtSIZE = new Gizmox.WebGUI.Forms.TextBox();
            this.lblRemarks = new Gizmox.WebGUI.Forms.Label();
            this.txtRemarks = new Gizmox.WebGUI.Forms.TextBox();
            this.lblFABRICS = new Gizmox.WebGUI.Forms.Label();
            this.txtFABRICS = new Gizmox.WebGUI.Forms.TextBox();
            this.lblCOLOR = new Gizmox.WebGUI.Forms.Label();
            this.txtCOLOR = new Gizmox.WebGUI.Forms.TextBox();
            this.txtSTYLE = new Gizmox.WebGUI.Forms.TextBox();
            this.lblSTYLE = new Gizmox.WebGUI.Forms.Label();
            this.txtShop = new Gizmox.WebGUI.Forms.TextBox();
            this.lblShop = new Gizmox.WebGUI.Forms.Label();
            this.rptViewer = new RT2008.Web.Reports.Controls.Viewer();
            this.serviceController1 = new System.ServiceProcess.ServiceController();
            this.label2 = new Gizmox.WebGUI.Forms.Label();
            this.SuspendLayout();
            // 
            // mainPane
            // 
            this.mainPane.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.mainPane.Controls.Add(this.splitContainer);
            this.mainPane.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.mainPane.DockPadding.All = 6;
            this.mainPane.Location = new System.Drawing.Point(0, 0);
            this.mainPane.Name = "mainPane";
            this.mainPane.Size = new System.Drawing.Size(690, 600);
            this.mainPane.TabIndex = 0;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.splitContainer.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.Clear;
            this.splitContainer.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = Gizmox.WebGUI.Forms.FixedPanel.None;
            this.splitContainer.Location = new System.Drawing.Point(6, 6);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = Gizmox.WebGUI.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.label2);
            this.splitContainer.Panel1.Controls.Add(this.btnLookup);
            this.splitContainer.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer.Panel1.Controls.Add(this.label1);
            this.splitContainer.Panel1.Controls.Add(this.lblBarcode);
            this.splitContainer.Panel1.Controls.Add(this.txtBarcode);
            this.splitContainer.Panel1.Controls.Add(this.chkQty);
            this.splitContainer.Panel1.Controls.Add(this.lblSkipRecords);
            this.splitContainer.Panel1.Controls.Add(this.lblSIZE);
            this.splitContainer.Panel1.Controls.Add(this.txtSIZE);
            this.splitContainer.Panel1.Controls.Add(this.lblRemarks);
            this.splitContainer.Panel1.Controls.Add(this.txtRemarks);
            this.splitContainer.Panel1.Controls.Add(this.lblFABRICS);
            this.splitContainer.Panel1.Controls.Add(this.txtFABRICS);
            this.splitContainer.Panel1.Controls.Add(this.lblCOLOR);
            this.splitContainer.Panel1.Controls.Add(this.txtCOLOR);
            this.splitContainer.Panel1.Controls.Add(this.txtSTYLE);
            this.splitContainer.Panel1.Controls.Add(this.lblSTYLE);
            this.splitContainer.Panel1.Controls.Add(this.txtShop);
            this.splitContainer.Panel1.Controls.Add(this.lblShop);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.rptViewer);
            this.splitContainer.Size = new System.Drawing.Size(678, 588);
            this.splitContainer.SplitterDistance = 227;
            this.splitContainer.TabIndex = 0;
            // 
            // btnLookup
            // 
            this.btnLookup.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnLookup.Location = new System.Drawing.Point(190, 197);
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Size = new System.Drawing.Size(75, 23);
            this.btnLookup.TabIndex = 9;
            this.btnLookup.Text = "Lookup";
            this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnSearch.Location = new System.Drawing.Point(109, 197);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(568, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Stock Qty Status Enquiry";
            // 
            // lblBarcode
            // 
            this.lblBarcode.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblBarcode.Location = new System.Drawing.Point(355, 58);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(84, 17);
            this.lblBarcode.TabIndex = 0;
            this.lblBarcode.Text = "Barcode";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtBarcode.Location = new System.Drawing.Point(445, 55);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(123, 22);
            this.txtBarcode.TabIndex = 2;
            // 
            // chkQty
            // 
            this.chkQty.Checked = true;
            this.chkQty.CheckState = Gizmox.WebGUI.Forms.CheckState.Unchecked;
            this.chkQty.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Standard;
            this.chkQty.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkQty.Location = new System.Drawing.Point(109, 174);
            this.chkQty.Name = "chkQty";
            this.chkQty.Size = new System.Drawing.Size(213, 20);
            this.chkQty.TabIndex = 7;
            this.chkQty.Text = "(CDQTY=FEPQTY=ATSQTY=0)";
            this.chkQty.ThreeState = false;
            // 
            // lblSkipRecords
            // 
            this.lblSkipRecords.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSkipRecords.Location = new System.Drawing.Point(21, 176);
            this.lblSkipRecords.Name = "lblSkipRecords";
            this.lblSkipRecords.Size = new System.Drawing.Size(90, 17);
            this.lblSkipRecords.TabIndex = 0;
            this.lblSkipRecords.Text = "Skip Record(s)";
            // 
            // lblSIZE
            // 
            this.lblSIZE.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSIZE.Location = new System.Drawing.Point(21, 130);
            this.lblSIZE.Name = "lblSIZE";
            this.lblSIZE.Size = new System.Drawing.Size(74, 17);
            this.lblSIZE.TabIndex = 0;
            this.lblSIZE.Text = "SIZE";
            // 
            // txtSIZE
            // 
            this.txtSIZE.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtSIZE.Location = new System.Drawing.Point(110, 127);
            this.txtSIZE.Name = "txtSIZE";
            this.txtSIZE.Size = new System.Drawing.Size(100, 22);
            this.txtSIZE.TabIndex = 5;
            // 
            // lblRemarks
            // 
            this.lblRemarks.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblRemarks.Location = new System.Drawing.Point(21, 154);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(74, 17);
            this.lblRemarks.TabIndex = 0;
            this.lblRemarks.Text = "Remarks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtRemarks.Location = new System.Drawing.Point(110, 151);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(191, 22);
            this.txtRemarks.TabIndex = 6;
            // 
            // lblFABRICS
            // 
            this.lblFABRICS.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblFABRICS.Location = new System.Drawing.Point(21, 82);
            this.lblFABRICS.Name = "lblFABRICS";
            this.lblFABRICS.Size = new System.Drawing.Size(74, 17);
            this.lblFABRICS.TabIndex = 0;
            this.lblFABRICS.Text = "FABRICS";
            // 
            // txtFABRICS
            // 
            this.txtFABRICS.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtFABRICS.Location = new System.Drawing.Point(110, 79);
            this.txtFABRICS.Name = "txtFABRICS";
            this.txtFABRICS.Size = new System.Drawing.Size(100, 22);
            this.txtFABRICS.TabIndex = 3;
            // 
            // lblCOLOR
            // 
            this.lblCOLOR.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblCOLOR.Location = new System.Drawing.Point(21, 106);
            this.lblCOLOR.Name = "lblCOLOR";
            this.lblCOLOR.Size = new System.Drawing.Size(74, 17);
            this.lblCOLOR.TabIndex = 0;
            this.lblCOLOR.Text = "COLOR";
            // 
            // txtCOLOR
            // 
            this.txtCOLOR.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtCOLOR.Location = new System.Drawing.Point(110, 103);
            this.txtCOLOR.Name = "txtCOLOR";
            this.txtCOLOR.Size = new System.Drawing.Size(100, 22);
            this.txtCOLOR.TabIndex = 4;
            // 
            // txtSTYLE
            // 
            this.txtSTYLE.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtSTYLE.Location = new System.Drawing.Point(110, 55);
            this.txtSTYLE.Name = "txtSTYLE";
            this.txtSTYLE.Size = new System.Drawing.Size(100, 22);
            this.txtSTYLE.TabIndex = 2;
            // 
            // lblSTYLE
            // 
            this.lblSTYLE.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSTYLE.Location = new System.Drawing.Point(21, 58);
            this.lblSTYLE.Name = "lblSTYLE";
            this.lblSTYLE.Size = new System.Drawing.Size(74, 17);
            this.lblSTYLE.TabIndex = 0;
            this.lblSTYLE.Text = "STYLE*";
            // 
            // txtShop
            // 
            this.txtShop.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtShop.Location = new System.Drawing.Point(110, 31);
            this.txtShop.Name = "txtShop";
            this.txtShop.Size = new System.Drawing.Size(100, 22);
            this.txtShop.TabIndex = 1;
            // 
            // lblShop
            // 
            this.lblShop.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblShop.Location = new System.Drawing.Point(21, 34);
            this.lblShop.Name = "lblShop";
            this.lblShop.Size = new System.Drawing.Size(74, 17);
            this.lblShop.TabIndex = 0;
            this.lblShop.Text = "Shop ID";
            // 
            // rptViewer
            // 
            this.rptViewer.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.rptViewer.Datasource = null;
            this.rptViewer.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.rptViewer.Location = new System.Drawing.Point(0, 0);
            this.rptViewer.Name = "rptViewer";
            this.rptViewer.Parameters = null;
            this.rptViewer.ReportDatasourceName = "";
            this.rptViewer.ReportDestination = RT2008.Web.Reports.Controls.Viewer.ReportDestinations.Preview;
            this.rptViewer.ReportName = "";
            this.rptViewer.Size = new System.Drawing.Size(678, 357);
            this.rptViewer.TabIndex = 0;
            this.rptViewer.Text = "Viewer";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(281, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "or";
            // 
            // StockQtyStatus
            // 
            this.AcceptButton = this.btnSearch;
            this.Controls.Add(this.mainPane);
            this.Location = new System.Drawing.Point(15, 15);
            this.Size = new System.Drawing.Size(690, 600);
            this.Text = "StockQtyStatus";
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.Panel mainPane;
        private Gizmox.WebGUI.Forms.SplitContainer splitContainer;
        private RT2008.Web.Reports.Controls.Viewer rptViewer;
        private Gizmox.WebGUI.Forms.Label lblShop;
        private Gizmox.WebGUI.Forms.TextBox txtShop;
        private Gizmox.WebGUI.Forms.Label lblSIZE;
        private Gizmox.WebGUI.Forms.TextBox txtSIZE;
        private Gizmox.WebGUI.Forms.Label lblRemarks;
        private Gizmox.WebGUI.Forms.TextBox txtRemarks;
        private Gizmox.WebGUI.Forms.Label lblFABRICS;
        private Gizmox.WebGUI.Forms.TextBox txtFABRICS;
        private Gizmox.WebGUI.Forms.Label lblCOLOR;
        private Gizmox.WebGUI.Forms.TextBox txtCOLOR;
        private Gizmox.WebGUI.Forms.TextBox txtSTYLE;
        private Gizmox.WebGUI.Forms.Label lblSTYLE;
        private Gizmox.WebGUI.Forms.CheckBox chkQty;
        private Gizmox.WebGUI.Forms.Label lblSkipRecords;
        private System.ServiceProcess.ServiceController serviceController1;
        private Gizmox.WebGUI.Forms.Label lblBarcode;
        private Gizmox.WebGUI.Forms.TextBox txtBarcode;
        private Gizmox.WebGUI.Forms.Label label1;
        private Gizmox.WebGUI.Forms.Button btnLookup;
        private Gizmox.WebGUI.Forms.Button btnSearch;
        private Gizmox.WebGUI.Forms.Label label2;


    }
}