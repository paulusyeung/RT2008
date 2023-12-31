////RT2008.Purchasing.Reports.PurchaseOrder
namespace RT2008.Purchasing.Reports.PurchaseOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web;

    using FileHelpers.DataLink;
    using FileHelpers.MasterDetail;

    using Gizmox.WebGUI.Common;
    using Gizmox.WebGUI.Common.Interfaces;
    using Gizmox.WebGUI.Forms;

    using RT2008.DAL;

    /// <summary>
    /// Purchase Order Status
    /// </summary>
    public partial class OrderStatus : Form, IGatewayComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderStatus"/> class.
        /// </summary>
        public OrderStatus()
        {
            this.InitializeComponent();
            this.FillComboList();
        }

        /// <summary>
        /// Handles the Load event of the OrderStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OrderStatus_Load(object sender, EventArgs e)
        {
            this.mainPanel.Padding = new Padding(6);
        }

        #region Fill Combo List
        /// <summary>
        /// Fills the combo list.
        /// </summary>
        private void FillComboList()
        {
            this.FillSuppFromList();
            this.FillSuppToList();
            this.FillFromList();
            this.FillToList();
        }

        /// <summary>
        /// Fills OrderNumber From
        /// </summary>
        private void FillFromList()
        {
            cboFrom.Items.Clear();

            string[] orderBy = { "OrderNumber" };
            string sql = " Retired = 0 ";

            PurchaseOrderHeaderCollection purchaseOrderList = PurchaseOrderHeader.LoadCollection(sql, orderBy, true);
            cboFrom.DataSource = purchaseOrderList;
            cboFrom.DisplayMember = "OrderNumber";
            cboFrom.ValueMember = "OrderHeaderId";
        }

        /// <summary>
        /// Fills OrderNumber To
        /// </summary>
        private void FillToList()
        {
            cboTo.Items.Clear();

            string[] orderBy = { "OrderNumber" };
            string sql = " Retired = 0 ";

            PurchaseOrderHeaderCollection purchaseOrderList = PurchaseOrderHeader.LoadCollection(sql, orderBy, true);
            cboTo.DataSource = purchaseOrderList;
            cboTo.DisplayMember = "OrderNumber";
            cboTo.ValueMember = "OrderHeaderId";

            cboTo.SelectedIndex = purchaseOrderList.Count - 1;
        }

        /// <summary>
        /// Fills SupplierCode From
        /// </summary>
        private void FillSuppFromList()
        {
            cboFrom.Items.Clear();

            string[] orderBy = { "SupplierCode" };
            string sql = " Retired = 0";

            SupplierCollection supplierList = RT2008.DAL.Supplier.LoadCollection(sql, orderBy, true);
            cboSuppFrom.DataSource = supplierList;
            cboSuppFrom.DisplayMember = "SupplierCode";
            cboSuppFrom.ValueMember = "SupplierId";
        }

        /// <summary>
        /// Fills SupplierNumber To
        /// </summary>
        private void FillSuppToList()
        {
            cboTo.Items.Clear();

            string[] orderBy = { "SupplierCode" };
            string sql = " Retired = 0";

            SupplierCollection supplierList = RT2008.DAL.Supplier.LoadCollection(sql, orderBy, true);
            cboSuppTo.DataSource = supplierList;
            cboSuppTo.DisplayMember = "SupplierCode";
            cboSuppTo.ValueMember = "SupplierId";

            cboSuppTo.SelectedIndex = supplierList.Count - 1;
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
            switch (strAction)
            {
                case "rdlExcel":
                    this.RdlToExcel();
                    break;
                case "rdlPDF":
                    this.RdlToPdf();
                    break;
                case "rdlSuppExcel":
                    this.RdlToSuppExcel();
                    break;
                case "rdlSuppPDF":
                    this.RdlToSuppPdf();
                    break;
                case "rdlOrderExcel":
                    this.RdlToOrderExcel();
                    break;
                case "rdlOrderPDF":
                    this.RdlToOrderPdf();
                    break;
            }
        }

        /// <summary>
        /// RDLs to excel.
        /// </summary>
        private void RdlToExcel()
        {
            string[,] param = 
            {
                { "FromOrderNumber", this.cboFrom.Text.Trim() },
                { "ToOrderNumber", this.cboTo.Text.Trim() },
                { "FromDate", this.dtpDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "ToDate", this.dtpDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                { "STKCODE", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() },
                { "Company",RT2008.SystemInfo.CurrentInfo.Default.CompanyName}
             };

            RT2008.Controls.Reporting.RdlExport rdlExport = new RT2008.Controls.Reporting.RdlExport();

            rdlExport.Datasource = this.BindData();
            rdlExport.ReportName = "RT2008.Purchasing.Reports.PurchaseOrder.Reports.WorksheetPOStatusRdl.rdlc";
            rdlExport.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptPurchaseOrder";
            rdlExport.Parameters = param;

            rdlExport.ToExcel();
        }

        /// <summary>
        /// RDLs to PDF.
        /// </summary>
        private void RdlToPdf()
        {
            string[,] param = 
            {
                { "FromOrderNumber", this.cboFrom.Text.Trim() },
                { "ToOrderNumber", this.cboTo.Text.Trim() },
                { "FromDate", this.dtpDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "ToDate", this.dtpDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                { "STKCODE", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() },
                { "Company",RT2008.SystemInfo.CurrentInfo.Default.CompanyName}
            };

            RT2008.Controls.Reporting.RdlExport rdlExport = new RT2008.Controls.Reporting.RdlExport();

            rdlExport.Datasource = this.BindData();
            rdlExport.ReportName = "RT2008.Purchasing.Reports.PurchaseOrder.Reports.WorksheetPOStatusRdl.rdlc";
            rdlExport.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptPurchaseOrder";
            rdlExport.Parameters = param;

            rdlExport.ToPdf();
        }

        /// <summary>
        /// RDLs to Supplier excel.
        /// </summary>
        private void RdlToSuppExcel()
        {
            string[,] param = 
            {
                { "FromSupplier", this.cboSuppFrom.Text.Trim() },
                { "ToSupplier", this.cboSuppTo.Text.Trim() },
                { "FromDate", this.dtpSuppDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "ToDate", this.dtpSuppDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                { "STKCODE", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                { "Company",RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() }
            };

            RT2008.Controls.Reporting.RdlExport rdlExport = new RT2008.Controls.Reporting.RdlExport();

            rdlExport.Datasource = this.BindDataSupp();
            rdlExport.ReportName = "RT2008.Purchasing.Reports.PurchaseOrder.Reports.WorksheetSuppStatusRdl.rdlc";
            rdlExport.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptPurchaseOrder";
            rdlExport.Parameters = param;

            rdlExport.ToExcel();
        }

        /// <summary>
        /// RDLs to Supplier PDF.
        /// </summary>
        private void RdlToSuppPdf()
        {
            string[,] param = 
            {
                { "FromSupplier", this.cboSuppFrom.Text.Trim() },
                { "ToSupplier", this.cboSuppTo.Text.Trim() },
                { "FromDate", this.dtpSuppDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "ToDate", this.dtpSuppDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                { "STKCODE", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                { "Company",RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() }
            };

            RT2008.Controls.Reporting.RdlExport rdlExport = new RT2008.Controls.Reporting.RdlExport();

            rdlExport.Datasource = this.BindDataSupp();
            rdlExport.ReportName = "RT2008.Purchasing.Reports.PurchaseOrder.Reports.WorksheetSuppStatusRdl.rdlc";
            rdlExport.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptPurchaseOrder";
            rdlExport.Parameters = param;

            rdlExport.ToPdf();
        }

        /// <summary>
        /// RDLs to Orderation excel.
        /// </summary>
        private void RdlToOrderExcel()
        {
            string[,] param = 
            {
                { "FromOrderDate", this.dtpOrderDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "ToOrderDate", this.dtpOrderDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                { "STKCODE", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                { "Company",RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() }
            };

            RT2008.Controls.Reporting.RdlExport rdlExport = new RT2008.Controls.Reporting.RdlExport();

            rdlExport.Datasource = this.BindDataOrder();
            rdlExport.ReportName = "RT2008.Purchasing.Reports.PurchaseOrder.Reports.WorksheetOrderDateStatusRdl.rdlc";
            rdlExport.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptPurchaseOrder";
            rdlExport.Parameters = param;

            rdlExport.ToExcel();
        }

        /// <summary>
        /// RDLs to Orderation PDF.
        /// </summary>
        private void RdlToOrderPdf()
        {
            string[,] param = 
            {
                { "FromOrderDate", this.dtpOrderDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "ToOrderDate", this.dtpOrderDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                { "STKCODE", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                { "Company",RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() }
            };

            RT2008.Controls.Reporting.RdlExport rdlExport = new RT2008.Controls.Reporting.RdlExport();

            rdlExport.Datasource = this.BindDataOrder();
            rdlExport.ReportName = "RT2008.Purchasing.Reports.PurchaseOrder.Reports.WorksheetOrderDateStatusRdl.rdlc";
            rdlExport.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptPurchaseOrder";
            rdlExport.Parameters = param;

            rdlExport.ToPdf();
        }
        #endregion

        #region Validate Selections
        /// <summary>
        /// Determines whether [is sel valid] [the specified range name].
        /// </summary>
        /// <param name="rangeName">Name of the range.</param>
        /// <returns>
        /// 	<c>true</c> if [is sel valid] [the specified range name]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsSelValid(string rangeName)
        {
            bool result = true;
        
            if (String.Compare(cboTo.Text.Trim(), cboFrom.Text.Trim()) < 0)     
            {
                // cboTo < cboFrom
                result = false;
                string errorStr = "Range Error: " + rangeName;
                MessageBox.Show(errorStr, "Message");
            }
            else if (dtpDateTo.Value < dtpDateFrom.Value)                  
            { 
                // dtpTxDateTo < dtpDateFrom
                result = false;
                MessageBox.Show("Range Error: Date", "Message");
            }

            return result;
        }
        #endregion

        #region Bind data to report
        /// <summary>
        /// Binds the data.
        /// </summary>
        /// <returns>A DataTable object</returns>
        private DataTable BindData()
        {
            string sql = @"
SELECT TOP 100 PERCENT *
FROM vwRptPurchaseOrder
WHERE	OrderNumber BETWEEN '" + this.cboFrom.Text.Trim() + @"' AND '" + this.cboTo.Text.Trim() + @"' AND
        OrderOn BETWEEN '" + this.dtpDateFrom.Value.ToString("MM/dd/yyyy 00:00:00") + @"' AND '" + this.dtpDateTo.Value.ToString("MM/dd/yyyy") + "'";

            if (rdbtnYes.Checked)
            {
                sql += " AND OrderedQty > TotalQtyReceived ORDER BY OrderNumber, OrderOn";
            }
            else
            {
                sql += " ORDER BY OrderNumber, OrderOn";
            }

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (DataSet dataset = SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }
        }

        /// <summary>
        /// Binds the data Supplier.
        /// </summary>
        /// <returns>A DataTable object</returns>
        private DataTable BindDataSupp()
        {
            string sql = @"
SELECT TOP 100 PERCENT *
FROM vwRptPurchaseOrder
WHERE	SupplierCode BETWEEN '" + this.cboSuppFrom.Text.Trim() + @"' AND '" + this.cboSuppTo.Text.Trim() + @"' AND
        OrderOn BETWEEN '" + this.dtpSuppDateFrom.Value.ToString("MM/dd/yyyy 00:00:00") + @"' AND '" + this.dtpSuppDateTo.Value.ToString("MM/dd/yyyy 23:23:59") + @"'";

            if (rdbtnSuppYes.Checked)
            {
                sql += " AND OrderedQty > TotalQtyReceived ORDER BY OrderNumber, OrderOn";
            }
            else
            {
                sql += " ORDER BY OrderNumber, OrderOn";
            }

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (DataSet dataset = SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }
        }

        /// <summary>
        /// Binds the data Orderation.
        /// </summary>
        /// <returns>A DataTable object</returns>
        private DataTable BindDataOrder()
        {
            string sql = @"
SELECT TOP 100 PERCENT *
FROM vwRptPurchaseOrder
WHERE	OrderOn BETWEEN '" + this.dtpOrderDateFrom.Value.ToString("MM/dd/yyyy 00:00:00") + @"' AND '" + this.dtpOrderDateTo.Value.ToString("MM/dd/yyyy 23:23:59") + "'";

            if (rdbtnOrderYes.Checked)
            {
                sql += " AND OrderedQty > TotalQtyReceived ORDER BY OrderNumber, OrderOn";
            }
            else
            {
                sql += " ORDER BY OrderNumber, OrderOn";
            }

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (DataSet dataset = SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the btnExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnSuppExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdSuppExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the cmdOrderExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdOrderExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Preview
        /// <summary>
        /// Handles the Click event of the cmdPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdPreview_Click(object sender, EventArgs e)
        {
            if (this.IsSelValid("Order Number"))
            {
                string[,] param = 
                {
                    { "FromOrderNumber", this.cboFrom.Text.Trim() },
                    { "ToOrderNumber", this.cboTo.Text.Trim() },
                    { "FromDate", this.dtpDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                    { "ToDate", this.dtpDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                    { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                    { "STKCODE", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                    { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                    { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                    { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                    { "Company",RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                    { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() }
                };

                RT2008.Controls.Reporting.Viewer rptViewer = new RT2008.Controls.Reporting.Viewer();

                rptViewer.Datasource = this.BindData();
                rptViewer.ReportName = "RT2008.Purchasing.Reports.PurchaseOrder.Reports.WorksheetPOStatusRdl.rdlc";
                rptViewer.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptPurchaseOrder";
                rptViewer.Parameters = param;

                rptViewer.Show();
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdSuppPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdSuppPreview_Click(object sender, EventArgs e)
        {
            if (this.IsSelValid("Supplier Number"))
            {
                string[,] param = 
                {
                    { "FromSupplier", this.cboSuppFrom.Text.Trim() },
                    { "ToSupplier", this.cboSuppTo.Text.Trim() },
                    { "FromDate", this.dtpSuppDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                    { "ToDate", this.dtpSuppDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                    { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                    { "STKCODE", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                    { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                    { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                    { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                    { "Company",RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                    { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() }
                };

                RT2008.Controls.Reporting.Viewer rptViewer = new RT2008.Controls.Reporting.Viewer();

                rptViewer.Datasource = this.BindDataSupp();
                rptViewer.ReportName = "RT2008.Purchasing.Reports.PurchaseOrder.Reports.WorksheetSuppStatusRdl.rdlc";
                rptViewer.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptPurchaseOrder";
                rptViewer.Parameters = param;

                rptViewer.Show();
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdOrderPreview control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdOrderPreview_Click(object sender, EventArgs e)
        {
            if (this.IsSelValid("Orderation Number"))
            {
                string[,] param = 
                {
                    { "FromOrderDate", this.dtpOrderDateFrom.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                    { "ToOrderDate", this.dtpOrderDateTo.Value.ToString(RT2008.SystemInfo.Settings.GetDateFormat()) },
                    { "PrintedOn", DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat()) },
                    { "STKCODE", RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE") },
                    { "Appendix1", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1") },
                    { "Appendix2", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2") },
                    { "Appendix3", RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3") },
                    { "Company",RT2008.SystemInfo.CurrentInfo.Default.CompanyName},
                    { "DateFormat", RT2008.SystemInfo.Settings.GetDateFormat() }
                };

                RT2008.Controls.Reporting.Viewer rptViewer = new RT2008.Controls.Reporting.Viewer();

                rptViewer.Datasource = this.BindDataOrder();
                rptViewer.ReportName = "RT2008.Purchasing.Reports.PurchaseOrder.Reports.WorksheetOrderDateStatusRdl.rdlc";
                rptViewer.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_vwRptPurchaseOrder";
                rptViewer.Parameters = param;

                rptViewer.Show();
            }
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the cmdExcel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdExcel_Click(object sender, EventArgs e)
        {
            if (this.IsSelValid("Order Number"))
            {
                Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "rdlExcel"));
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdPDF control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdPDF_Click(object sender, EventArgs e)
        {
            if (this.IsSelValid("Order Number"))
            {
                Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "rdlPDF"));
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdSuppExcel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdSuppExcel_Click(object sender, EventArgs e)
        {
            if (this.IsSelValid("Supplier Code"))
            {
                Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "rdlSuppExcel"));
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdSuppPDF control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdSuppPDF_Click(object sender, EventArgs e)
        {
            if (this.IsSelValid("Supplier Code"))
            {
                Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "rdlSuppPDF"));
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdOrderExcel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdOrderExcel_Click(object sender, EventArgs e)
        {
            if (this.IsSelValid("Order Date"))
            {
                Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "rdlOrderExcel"));
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdOrderPDF control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CmdOrderPDF_Click(object sender, EventArgs e)
        {
            if (this.IsSelValid("Order Date"))
            {
                Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "rdlOrderPDF"));
            }
        }
    }
}