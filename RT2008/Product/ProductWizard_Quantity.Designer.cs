namespace RT2008.Product
{
    partial class ProductWizard_Quantity
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

        #region Visual WebGui UserControl Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvQtyList = new Gizmox.WebGUI.Forms.DataGridView();
            this.lblMaxOLNQty = new Gizmox.WebGUI.Forms.Label();
            this.lblOnHandQty = new Gizmox.WebGUI.Forms.Label();
            this.lblMTDPurQty = new Gizmox.WebGUI.Forms.Label();
            this.lblYTDPurQty = new Gizmox.WebGUI.Forms.Label();
            this.lblMTDSoldQty = new Gizmox.WebGUI.Forms.Label();
            this.lblYTDSoldQty = new Gizmox.WebGUI.Forms.Label();
            this.lblAverageCost = new Gizmox.WebGUI.Forms.Label();
            this.lblLastReceivingCost = new Gizmox.WebGUI.Forms.Label();
            this.lblLastPurDate = new Gizmox.WebGUI.Forms.Label();
            this.lblLastSoldDate = new Gizmox.WebGUI.Forms.Label();
            this.gbQty = new Gizmox.WebGUI.Forms.GroupBox();
            this.btnShowATSQty = new Gizmox.WebGUI.Forms.Button();
            this.txtLastPurDate = new Gizmox.WebGUI.Forms.TextBox();
            this.txtLastSoldDate = new Gizmox.WebGUI.Forms.TextBox();
            this.txtLastReceivingCost = new Gizmox.WebGUI.Forms.TextBox();
            this.txtAverageCost = new Gizmox.WebGUI.Forms.TextBox();
            this.txtMTDPurQty = new Gizmox.WebGUI.Forms.TextBox();
            this.txtYTDPurQty = new Gizmox.WebGUI.Forms.TextBox();
            this.txtMTDSoldQty = new Gizmox.WebGUI.Forms.TextBox();
            this.txtYTDSoldQty = new Gizmox.WebGUI.Forms.TextBox();
            this.txtOnHandQty = new Gizmox.WebGUI.Forms.TextBox();
            this.txtMaxOLNQty = new Gizmox.WebGUI.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQtyList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvQtyList
            // 
            this.dgvQtyList.AllowUserToAddRows = false;
            this.dgvQtyList.AllowUserToDeleteRows = false;
            this.dgvQtyList.AllowUserToOrderColumns = true;
            this.dgvQtyList.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.dgvQtyList.BorderStyle = Gizmox.WebGUI.Forms.BorderStyle.FixedSingle;
            this.dgvQtyList.Dock = Gizmox.WebGUI.Forms.DockStyle.Left;
            this.dgvQtyList.EditMode = Gizmox.WebGUI.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvQtyList.Location = new System.Drawing.Point(3, 16);
            this.dgvQtyList.Name = "dgvQtyList";
            this.dgvQtyList.ReadOnly = true;
            this.dgvQtyList.RowHeadersWidth = 50;
            this.dgvQtyList.Size = new System.Drawing.Size(539, 331);
            this.dgvQtyList.TabIndex = 0;
            // 
            // lblMaxOLNQty
            // 
            this.lblMaxOLNQty.Location = new System.Drawing.Point(548, 16);
            this.lblMaxOLNQty.Name = "lblMaxOLNQty";
            this.lblMaxOLNQty.Size = new System.Drawing.Size(100, 23);
            this.lblMaxOLNQty.TabIndex = 1;
            this.lblMaxOLNQty.Text = "Max OLN Qty:";
            // 
            // lblOnHandQty
            // 
            this.lblOnHandQty.Location = new System.Drawing.Point(548, 39);
            this.lblOnHandQty.Name = "lblOnHandQty";
            this.lblOnHandQty.Size = new System.Drawing.Size(100, 23);
            this.lblOnHandQty.TabIndex = 2;
            this.lblOnHandQty.Text = "On Hand Qty:";
            // 
            // lblMTDPurQty
            // 
            this.lblMTDPurQty.Location = new System.Drawing.Point(548, 62);
            this.lblMTDPurQty.Name = "lblMTDPurQty";
            this.lblMTDPurQty.Size = new System.Drawing.Size(100, 23);
            this.lblMTDPurQty.TabIndex = 3;
            this.lblMTDPurQty.Text = "MTD Pur Qty:";
            // 
            // lblYTDPurQty
            // 
            this.lblYTDPurQty.Location = new System.Drawing.Point(548, 85);
            this.lblYTDPurQty.Name = "lblYTDPurQty";
            this.lblYTDPurQty.Size = new System.Drawing.Size(100, 23);
            this.lblYTDPurQty.TabIndex = 4;
            this.lblYTDPurQty.Text = "YTD Pur Qty:";
            // 
            // lblMTDSoldQty
            // 
            this.lblMTDSoldQty.Location = new System.Drawing.Point(548, 108);
            this.lblMTDSoldQty.Name = "lblMTDSoldQty";
            this.lblMTDSoldQty.Size = new System.Drawing.Size(100, 23);
            this.lblMTDSoldQty.TabIndex = 5;
            this.lblMTDSoldQty.Text = "MTD Sold Qty:";
            // 
            // lblYTDSoldQty
            // 
            this.lblYTDSoldQty.Location = new System.Drawing.Point(548, 131);
            this.lblYTDSoldQty.Name = "lblYTDSoldQty";
            this.lblYTDSoldQty.Size = new System.Drawing.Size(100, 23);
            this.lblYTDSoldQty.TabIndex = 6;
            this.lblYTDSoldQty.Text = "YTD Sold Qty:";
            // 
            // lblAverageCost
            // 
            this.lblAverageCost.Location = new System.Drawing.Point(548, 154);
            this.lblAverageCost.Name = "lblAverageCost";
            this.lblAverageCost.Size = new System.Drawing.Size(100, 23);
            this.lblAverageCost.TabIndex = 7;
            this.lblAverageCost.Text = "Average Cost:";
            // 
            // lblLastReceivingCost
            // 
            this.lblLastReceivingCost.Location = new System.Drawing.Point(548, 177);
            this.lblLastReceivingCost.Name = "lblLastReceivingCost";
            this.lblLastReceivingCost.Size = new System.Drawing.Size(109, 23);
            this.lblLastReceivingCost.TabIndex = 8;
            this.lblLastReceivingCost.Text = "Last Receiving Cost:";
            // 
            // lblLastPurDate
            // 
            this.lblLastPurDate.Location = new System.Drawing.Point(548, 200);
            this.lblLastPurDate.Name = "lblLastPurDate";
            this.lblLastPurDate.Size = new System.Drawing.Size(100, 23);
            this.lblLastPurDate.TabIndex = 9;
            this.lblLastPurDate.Text = "Last Pur Date:";
            // 
            // lblLastSoldDate
            // 
            this.lblLastSoldDate.Location = new System.Drawing.Point(548, 223);
            this.lblLastSoldDate.Name = "lblLastSoldDate";
            this.lblLastSoldDate.Size = new System.Drawing.Size(100, 23);
            this.lblLastSoldDate.TabIndex = 10;
            this.lblLastSoldDate.Text = "Last Sold Date:";
            // 
            // gbQty
            // 
            this.gbQty.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            this.gbQty.Controls.Add(this.btnShowATSQty);
            this.gbQty.Controls.Add(this.txtLastPurDate);
            this.gbQty.Controls.Add(this.txtLastSoldDate);
            this.gbQty.Controls.Add(this.txtLastReceivingCost);
            this.gbQty.Controls.Add(this.txtAverageCost);
            this.gbQty.Controls.Add(this.txtMTDPurQty);
            this.gbQty.Controls.Add(this.txtYTDPurQty);
            this.gbQty.Controls.Add(this.txtMTDSoldQty);
            this.gbQty.Controls.Add(this.txtYTDSoldQty);
            this.gbQty.Controls.Add(this.txtOnHandQty);
            this.gbQty.Controls.Add(this.txtMaxOLNQty);
            this.gbQty.Controls.Add(this.dgvQtyList);
            this.gbQty.Controls.Add(this.lblLastSoldDate);
            this.gbQty.Controls.Add(this.lblMaxOLNQty);
            this.gbQty.Controls.Add(this.lblLastPurDate);
            this.gbQty.Controls.Add(this.lblOnHandQty);
            this.gbQty.Controls.Add(this.lblLastReceivingCost);
            this.gbQty.Controls.Add(this.lblMTDPurQty);
            this.gbQty.Controls.Add(this.lblAverageCost);
            this.gbQty.Controls.Add(this.lblYTDPurQty);
            this.gbQty.Controls.Add(this.lblYTDSoldQty);
            this.gbQty.Controls.Add(this.lblMTDSoldQty);
            this.gbQty.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            this.gbQty.FlatStyle = Gizmox.WebGUI.Forms.FlatStyle.Flat;
            this.gbQty.Location = new System.Drawing.Point(0, 0);
            this.gbQty.Name = "gbQty";
            this.gbQty.Size = new System.Drawing.Size(766, 350);
            this.gbQty.TabIndex = 11;
            // 
            // btnShowATSQty
            // 
            this.btnShowATSQty.Location = new System.Drawing.Point(587, 294);
            this.btnShowATSQty.Name = "btnShowATSQty";
            this.btnShowATSQty.Size = new System.Drawing.Size(134, 23);
            this.btnShowATSQty.TabIndex = 13;
            this.btnShowATSQty.Text = "Show ATS Quantity";
            this.btnShowATSQty.Click += new System.EventHandler(this.btnShowATSQty_Click);
            // 
            // txtLastPurDate
            // 
            this.txtLastPurDate.BackColor = System.Drawing.Color.LightYellow;
            this.txtLastPurDate.Location = new System.Drawing.Point(654, 197);
            this.txtLastPurDate.Name = "txtLastPurDate";
            this.txtLastPurDate.ReadOnly = true;
            this.txtLastPurDate.Size = new System.Drawing.Size(100, 20);
            this.txtLastPurDate.TabIndex = 12;
            this.txtLastPurDate.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // txtLastSoldDate
            // 
            this.txtLastSoldDate.BackColor = System.Drawing.Color.LightYellow;
            this.txtLastSoldDate.Location = new System.Drawing.Point(654, 220);
            this.txtLastSoldDate.Name = "txtLastSoldDate";
            this.txtLastSoldDate.ReadOnly = true;
            this.txtLastSoldDate.Size = new System.Drawing.Size(100, 20);
            this.txtLastSoldDate.TabIndex = 12;
            this.txtLastSoldDate.TabStop = false;
            this.txtLastSoldDate.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // txtLastReceivingCost
            // 
            this.txtLastReceivingCost.BackColor = System.Drawing.Color.LightYellow;
            this.txtLastReceivingCost.Location = new System.Drawing.Point(654, 174);
            this.txtLastReceivingCost.Name = "txtLastReceivingCost";
            this.txtLastReceivingCost.ReadOnly = true;
            this.txtLastReceivingCost.Size = new System.Drawing.Size(100, 20);
            this.txtLastReceivingCost.TabIndex = 12;
            this.txtLastReceivingCost.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // txtAverageCost
            // 
            this.txtAverageCost.BackColor = System.Drawing.Color.LightYellow;
            this.txtAverageCost.Location = new System.Drawing.Point(654, 151);
            this.txtAverageCost.Name = "txtAverageCost";
            this.txtAverageCost.ReadOnly = true;
            this.txtAverageCost.Size = new System.Drawing.Size(100, 20);
            this.txtAverageCost.TabIndex = 12;
            this.txtAverageCost.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // txtMTDPurQty
            // 
            this.txtMTDPurQty.BackColor = System.Drawing.Color.LightYellow;
            this.txtMTDPurQty.Location = new System.Drawing.Point(654, 59);
            this.txtMTDPurQty.Name = "txtMTDPurQty";
            this.txtMTDPurQty.ReadOnly = true;
            this.txtMTDPurQty.Size = new System.Drawing.Size(100, 20);
            this.txtMTDPurQty.TabIndex = 12;
            this.txtMTDPurQty.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // txtYTDPurQty
            // 
            this.txtYTDPurQty.BackColor = System.Drawing.Color.LightYellow;
            this.txtYTDPurQty.Location = new System.Drawing.Point(654, 82);
            this.txtYTDPurQty.Name = "txtYTDPurQty";
            this.txtYTDPurQty.ReadOnly = true;
            this.txtYTDPurQty.Size = new System.Drawing.Size(100, 20);
            this.txtYTDPurQty.TabIndex = 12;
            this.txtYTDPurQty.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // txtMTDSoldQty
            // 
            this.txtMTDSoldQty.BackColor = System.Drawing.Color.LightYellow;
            this.txtMTDSoldQty.Location = new System.Drawing.Point(654, 105);
            this.txtMTDSoldQty.Name = "txtMTDSoldQty";
            this.txtMTDSoldQty.ReadOnly = true;
            this.txtMTDSoldQty.Size = new System.Drawing.Size(100, 20);
            this.txtMTDSoldQty.TabIndex = 12;
            this.txtMTDSoldQty.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // txtYTDSoldQty
            // 
            this.txtYTDSoldQty.BackColor = System.Drawing.Color.LightYellow;
            this.txtYTDSoldQty.Location = new System.Drawing.Point(654, 128);
            this.txtYTDSoldQty.Name = "txtYTDSoldQty";
            this.txtYTDSoldQty.ReadOnly = true;
            this.txtYTDSoldQty.Size = new System.Drawing.Size(100, 20);
            this.txtYTDSoldQty.TabIndex = 12;
            this.txtYTDSoldQty.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // txtOnHandQty
            // 
            this.txtOnHandQty.BackColor = System.Drawing.Color.LightYellow;
            this.txtOnHandQty.Location = new System.Drawing.Point(654, 36);
            this.txtOnHandQty.Name = "txtOnHandQty";
            this.txtOnHandQty.ReadOnly = true;
            this.txtOnHandQty.Size = new System.Drawing.Size(100, 20);
            this.txtOnHandQty.TabIndex = 12;
            this.txtOnHandQty.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // txtMaxOLNQty
            // 
            this.txtMaxOLNQty.Location = new System.Drawing.Point(654, 13);
            this.txtMaxOLNQty.Name = "txtMaxOLNQty";
            this.txtMaxOLNQty.Size = new System.Drawing.Size(100, 20);
            this.txtMaxOLNQty.TabIndex = 11;
            this.txtMaxOLNQty.TextAlign = Gizmox.WebGUI.Forms.HorizontalAlignment.Right;
            // 
            // ProductWizard_Quantity
            // 
            this.Controls.Add(this.gbQty);
            this.Size = new System.Drawing.Size(766, 350);
            this.Text = "ProductWizard_Quantity";
            ((System.ComponentModel.ISupportInitialize)(this.dgvQtyList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Gizmox.WebGUI.Forms.DataGridView dgvQtyList;
        private Gizmox.WebGUI.Forms.Label lblMaxOLNQty;
        private Gizmox.WebGUI.Forms.Label lblOnHandQty;
        private Gizmox.WebGUI.Forms.Label lblMTDPurQty;
        private Gizmox.WebGUI.Forms.Label lblYTDPurQty;
        private Gizmox.WebGUI.Forms.Label lblMTDSoldQty;
        private Gizmox.WebGUI.Forms.Label lblYTDSoldQty;
        private Gizmox.WebGUI.Forms.Label lblAverageCost;
        private Gizmox.WebGUI.Forms.Label lblLastReceivingCost;
        private Gizmox.WebGUI.Forms.Label lblLastPurDate;
        private Gizmox.WebGUI.Forms.Label lblLastSoldDate;
        private Gizmox.WebGUI.Forms.GroupBox gbQty;
        private Gizmox.WebGUI.Forms.Button btnShowATSQty;
        private Gizmox.WebGUI.Forms.TextBox txtLastPurDate;
        private Gizmox.WebGUI.Forms.TextBox txtLastSoldDate;
        private Gizmox.WebGUI.Forms.TextBox txtLastReceivingCost;
        private Gizmox.WebGUI.Forms.TextBox txtAverageCost;
        private Gizmox.WebGUI.Forms.TextBox txtMTDPurQty;
        private Gizmox.WebGUI.Forms.TextBox txtYTDPurQty;
        private Gizmox.WebGUI.Forms.TextBox txtMTDSoldQty;
        private Gizmox.WebGUI.Forms.TextBox txtYTDSoldQty;
        private Gizmox.WebGUI.Forms.TextBox txtOnHandQty;
        public Gizmox.WebGUI.Forms.TextBox txtMaxOLNQty;


    }
}