#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using System.Collections;
using RT2008.DAL;
using System.Data.SqlClient;

#endregion

namespace RT2008.Inventory.Olap
{
    public partial class OlapViewForm : UserControl
    {
        private int _TriggerAlertPoint = 50000;

        public OlapViewForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.LoadCriteria();
        }

        #region Loading

        private void LoadCriteria()
        {
            splitContainer.Dock = DockStyle.Fill;

            switch (this.ViewerType)
            {
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS:
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS_WithCutOffDate:
                    this.LoadQoH_ATS();
                    this.splitContainer.SplitterDistance = 96;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockReorder:
                    this.LoadStockReorder();
                    this.splitContainer.SplitterDistance = 96;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockIOHistory:
                    this.LoadStockIOHistory();
                    this.splitContainer.SplitterDistance = 96;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.OCInventory:
                    this.LoadOCInventory();
                    this.splitContainer.SplitterDistance = 96;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.DiscrepancyAudit:
                    this.LoadDiscrepancyAudit();
                    this.splitContainer.SplitterDistance = 96;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockTransfer:
                    this.LoadStockTransfer();
                    this.splitContainer.SplitterDistance = 96;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.CAP_Summary:
                    this.LoadCapSummary();
                    this.splitContainer.SplitterDistance = 120;
                    break;
            }

            this.btnShow.Location = new Point(747, 60);
        }

        private void LoadQoH_ATS()
        {
            this.GetProductCode();

            this.dtpFromDate.Value = this.FromDate;
            this.dtpToDate.Value = this.ToDate;
            this.dtpToDate.Enabled = (this.ViewerType == RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS_WithCutOffDate);

            this.chkShowRemarks.Visible = !(this.ViewerType == RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS_WithCutOffDate);

            this.normalQoH_ATS.Location = this.headerPanel.Location;

            this.normalQoH_ATS.Visible = true;
        }

        private void LoadStockReorder()
        {
            this.GetProductCode();

            this.dtpFromDate_Reorder.Value = this.FromDate;
            this.dtpToDate_Reorder.Value = this.ToDate;

            this.stockReorder.Location = this.headerPanel.Location;

            this.stockReorder.Visible = true;
        }

        private void LoadStockIOHistory()
        {
            this.GetProductCode();
            this.GetShopCode();

            this.dtpFromDate_IOHistory.Value = this.FromDate;
            this.dtpFromDate_IOHistory.Enabled = true;
            this.dtpToDate_IOHistory.Value = this.ToDate;
            this.dtpToDate_IOHistory.Enabled = true;

            this.stockInOutHistory.Location = this.headerPanel.Location;

            this.stockInOutHistory.Visible = true;
        }

        private void LoadOCInventory()
        {
            this.GetProductCode();

            this.txtFromMonth_OCInventory.Text = RT2008.SystemInfo.CurrentInfo.Default.LastMonthEnd.ToString();
            this.txtToMonth_OCInventory.Text = RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemDate.ToString("yyyyMM");

            this.openingClosingInvt.Location = this.headerPanel.Location;

            this.openingClosingInvt.Visible = true;
        }

        private void LoadDiscrepancyAudit()
        {
            this.GetProductCode();

            this.txtMonth_DiscrepancyAudit.Text = RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemDate.ToString("yyyyMM");

            this.dtpFromDate_DiscrepancyAudit.Value = this.FromDate;
            this.dtpToDate_DiscrepancyAudit.Value = this.ToDate;

            this.discrepancyAudit.Location = this.headerPanel.Location;

            this.discrepancyAudit.Visible = true;
        }

        private void LoadStockTransfer()
        {
            this.GetProductCode();

            this.dtpFromDate_Transfer.Value = this.FromDate;
            this.dtpToDate_Transfer.Value = this.ToDate;

            this.stockTransfer.Location = this.headerPanel.Location;

            this.stockTransfer.Visible = true;
        }

        private void LoadCapSummary()
        {
            this.dtpFromDate_CAPSummary.Value = this.FromDate;
            this.dtpFromDate_CAPSummary.Enabled = true;
            this.dtpToDate_CAPSummary.Value = this.ToDate;
            this.dtpToDate_CAPSummary.Enabled = true;

            this.chkPostedRecord_CAPSummary.Enabled = true;
            this.chkUnPostRecord_CAPSummary.Enabled = true;

            this.chkPostedRecord_CAPSummary.Checked = true;
            this.chkUnPostRecord_CAPSummary.Checked = true;

            this.capSummary.Location = this.headerPanel.Location;

            this.capSummary.Visible = true;
        }

        /// <summary>
        /// Gets the product code.
        /// </summary>
        private void GetProductCode()
        {
            RT2008.DAL.ProductCollection oProdList = RT2008.DAL.Product.LoadCollection("STKCODE IN (SELECT MIN(STKCODE) FROM Product)", new string[] { "STKCODE" }, true);
            if (oProdList.Count > 0)
            {
                switch (this.ViewerType)
                {
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS:
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS_WithCutOffDate:
                        this.txtFromStkCode.Text = oProdList[0].STKCODE;
                        break;
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockReorder:
                        this.txtFromStkCode_Reorder.Text = oProdList[0].STKCODE;
                        break;
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockIOHistory:
                        txtFromStkCode_IOHistory.Text = oProdList[0].STKCODE;
                        break;
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.OCInventory:
                        txtFromStkCode_OCInventory.Text = oProdList[0].STKCODE;
                        break;
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.DiscrepancyAudit:
                        txtFromStkCode_DiscrepancyAudit.Text = oProdList[0].STKCODE;
                        break;
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockTransfer:
                        txtFromStkCode_Transfer.Text = oProdList[0].STKCODE;
                        break;
                }
            }

            oProdList = RT2008.DAL.Product.LoadCollection("STKCODE IN (SELECT MAX(STKCODE) FROM Product)", new string[] { "STKCODE" }, true);
            if (oProdList.Count > 0)
            {
                switch (this.ViewerType)
                {
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS:
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS_WithCutOffDate:
                        this.txtToStkCode.Text = oProdList[0].STKCODE;
                        break;
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockReorder:
                        this.txtToStkCode_Reorder.Text = oProdList[0].STKCODE;
                        break;
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockIOHistory:
                        txtToStkCode_IOHistory.Text = oProdList[0].STKCODE;
                        break;
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.OCInventory:
                        txtToStkCode_OCInventory.Text = oProdList[0].STKCODE;
                        break;
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.DiscrepancyAudit:
                        txtToStkCode_DiscrepancyAudit.Text = oProdList[0].STKCODE;
                        break;
                    case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockTransfer:
                        txtToStkCode_Transfer.Text = oProdList[0].STKCODE;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the Shop code
        /// </summary>
        private void GetShopCode()
        {
            RT2008.DAL.WorkplaceCollection oShopList = RT2008.DAL.Workplace.LoadCollection(new string[] { "WorkplaceCode" }, true);
            if (oShopList.Count > 0)
            {
                this.txtFromWorkplace_IOHistory.Text = oShopList[0].WorkplaceCode;
                this.txtToWorkplace_IOHistory.Text = oShopList[oShopList.Count - 1].WorkplaceCode;
            }
        }

        #endregion

        #region 2014.01.02 paulus: 如果所選的 Olap 資料過多，Alert user 有可能會出現 Timeout Error
        private void AlertDataSourceSize()
        {
            int DS_RecordCounts = 0;
            bool TriggerAlert = false;

            switch (this.ViewerType)
            {
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS:
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS_WithCutOffDate:
                    DS_RecordCounts = AlertDataSourceSize_ATS();
                    if (DS_RecordCounts > _TriggerAlertPoint) TriggerAlert = true;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockReorder:
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockIOHistory:
                    DS_RecordCounts = AlertDataSourceSize_IOHistory();
                    if (DS_RecordCounts > _TriggerAlertPoint) TriggerAlert = true;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.OCInventory:
                    DS_RecordCounts = AlertDataSourceSize_OCInventory();
                    if (DS_RecordCounts > _TriggerAlertPoint) TriggerAlert = true;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.DiscrepancyAudit:
                    DS_RecordCounts = AlertDataSourceSize_DiscrepancyAudit();
                    if (DS_RecordCounts > _TriggerAlertPoint) TriggerAlert = true;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockTransfer:
                    DS_RecordCounts = AlertDataSourceSize_StockTransfer();
                    if (DS_RecordCounts > _TriggerAlertPoint) TriggerAlert = true;
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.CAP_Summary:
                    break;
            }

            if (TriggerAlert)
            {
                MessageBox.Show(String.Format("You have selected {0} possible records to evaluate!\n\nYou might experience time-out error.\n\nContinue to show the report?", DS_RecordCounts.ToString("#,##0")),
                    "Proceed Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, new EventHandler(AlertDataSourceSize_OnMessageBoxClose));
            }
            else
            {
                ShowOlapPage();
            }
        }

        private int AlertDataSourceSize_ATS()
        {
            int result = 0;

            String sql = String.Format(@"
SELECT     COUNT(*) AS rows
FROM         dbo.Product AS p INNER JOIN
                      dbo.ProductWorkplace AS pw ON p.ProductId = pw.ProductId
WHERE     (p.STKCODE >= '{0}') AND (p.STKCODE <= '{1}')
", txtFromStkCode.Text, txtToStkCode.Text);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
            }

            return result;
        }

        private int AlertDataSourceSize_IOHistory()
        {
            int result = 0;

            String sql = String.Format(@"
SELECT COUNT(*) AS rows
FROM InvtLedgerDetails AS D 
	INNER JOIN InvtLedgerHeader AS H ON D.HeaderId = H.HeaderId 
	INNER JOIN Product AS P ON P.ProductId = D.ProductId
	RIGHT OUTER JOIN Workplace AS W ON W.WorkplaceId = H.WorkplaceId 
WHERE D.TxNumber IS NOT NULL AND H.TxDate BETWEEN '{0} 00:00:00' AND '{1} 23:59:59'
	AND W.WorkplaceCode BETWEEN '{2}' AND '{3}' 
	AND P.STKCODE BETWEEN '{4}' AND '{5}'
", dtpFromDate_IOHistory.Value.ToString("yyyy-MM-dd"), dtpToDate_IOHistory.Value.ToString("yyyy-MM-dd")
 , txtFromWorkplace_IOHistory.Text, txtToWorkplace_IOHistory.Text, txtFromStkCode_IOHistory.Text, txtToStkCode_IOHistory.Text);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
            }

            return result;
        }

        private int AlertDataSourceSize_OCInventory()
        {
            int result = 0;

            String sql = String.Format(@"
SELECT COUNT(*) AS rows
FROM  ProductCurrentSummary AS PCS LEFT OUTER JOIN Product AS P
    ON P.ProductId = PCS.ProductId
WHERE P.STKCODE BETWEEN '{0}' AND '{1}'
", txtFromStkCode_OCInventory.Text, txtToStkCode_OCInventory.Text);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
            }

            return result;
        }

        private int AlertDataSourceSize_DiscrepancyAudit()
        {
            int result = 0;

            String sql = String.Format(@"
SELECT COUNT(*) AS rows
FROM  ProductCurrentSummary AS PCS LEFT OUTER JOIN Product AS P
    ON P.ProductId = PCS.ProductId
WHERE P.STKCODE BETWEEN '{0}' AND '{1}'
", txtFromStkCode_OCInventory.Text, txtToStkCode_OCInventory.Text);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
            }

            return result;
        }

        private int AlertDataSourceSize_StockTransfer()
        {
            int result = 0;

            String sql = String.Format(@"
SELECT COUNT(*) AS rows
FROM  InvtLedgerDetails AS D LEFT OUTER JOIN Product AS P
    ON P.ProductId = D.ProductId
WHERE D.TxType IN ('TXI', 'TXO') AND
    P.STKCODE BETWEEN '{0}' AND '{1}' AND
    D.TxDate BETWEEN '{2} 00:00:00' AND '{3} 23:59:59'
", txtFromTxNumber_Transfer.Text, txtToTxNumber_Transfer.Text, dtpFromDate_Transfer.Value.ToString("yyyy-MM-dd"), dtpToDate_Transfer.Value.ToString("yyyy-MM-dd"));

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
            }

            return result;
        }

        private void AlertDataSourceSize_OnMessageBoxClose(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                ShowOlapPage();
            }
        }
        #endregion

        #region Show OLAP Page
        private void ShowOlapPage()
        {
            RT2008.Controls.Reporting.OlapViewer olapViewer = new RT2008.Controls.Reporting.OlapViewer();
            olapViewer.Anchor = Gizmox.WebGUI.Forms.AnchorStyles.None;
            olapViewer.Dock = Gizmox.WebGUI.Forms.DockStyle.Fill;
            olapViewer.Location = new System.Drawing.Point(0, 0);
            olapViewer.Name = "olapViewer";
            olapViewer.Size = new System.Drawing.Size(150, 445);
            olapViewer.TabIndex = 0;
            olapViewer.Text = "OlapViewer";

            switch (this.ViewerType)
            {
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS:
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS_WithCutOffDate:
                    this.BuildParametersForQoH_ATS(ref olapViewer);
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockReorder:
                    this.BuildParametersForStockReorder(ref olapViewer);
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockIOHistory:
                    this.BuildParametersForStockIOHistory(ref olapViewer);
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.OCInventory:
                    this.BuildParametersForOCInventory(ref olapViewer);
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.DiscrepancyAudit:
                    this.BuildParametersForDiscrepancyAudit(ref olapViewer);
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.StockTransfer:
                    this.BuildParametersForStockTransfer(ref olapViewer);
                    break;
                case RT2008.Controls.InvtUtility.InvtOlapViewerType.CAP_Summary:
                    this.BuildParametersForCapSummary(ref olapViewer);
                    break;
            }

            this.splitContainer.Panel2.Controls.Add(olapViewer);
        }

        private void BuildParametersForQoH_ATS(ref RT2008.Controls.Reporting.OlapViewer olapViewer)
        {
            Hashtable parameterTable = new Hashtable();
            parameterTable.Add("FromStockCode", this.txtFromStkCode.Text);
            parameterTable.Add("ToStockCode", this.txtToStkCode.Text);
            parameterTable.Add("FromDate", this.dtpFromDate.Value.ToString("yyyy-MM-dd"));
            parameterTable.Add("ToDate", this.dtpToDate.Value.ToString("yyyy-MM-dd"));
            parameterTable.Add("ShowPOQty", this.chkShowPOQty.Checked.ToString());
            parameterTable.Add("ShowATSQty", this.chkShowATSQty.Checked.ToString());
            parameterTable.Add("ShowOSTQty", this.chkShowOSTOnLoanQty.Checked.ToString());
            parameterTable.Add("ShowRetailAmount", this.chkShowRetailAmount.Checked.ToString());
            parameterTable.Add("ShowRemarks", this.chkShowRemarks.Checked.ToString());
            parameterTable.Add("SkipZeroQty", this.chkSkipZeroQty.Checked.ToString());

            System.Web.HttpContext.Current.Session["Qoh_Ats"] = parameterTable;

            if (this.ViewerType == RT2008.Controls.InvtUtility.InvtOlapViewerType.QoH_ATS_WithCutOffDate)
            {
                olapViewer.AspxPagePath = @"Inventory\Olap\QohAtsCutOff.aspx";
            }
            else
            {
                olapViewer.AspxPagePath = @"Inventory\Olap\QohAtsNormal.aspx";
            }
        }

        private void BuildParametersForStockReorder(ref RT2008.Controls.Reporting.OlapViewer olapViewer)
        {
            Hashtable parameterTable = new Hashtable();
            parameterTable.Add("FromStockCode", this.txtFromStkCode_Reorder.Text);
            parameterTable.Add("ToStockCode", this.txtToStkCode_Reorder.Text);
            parameterTable.Add("FromDate", this.dtpFromDate_Reorder.Value.ToString("yyyy-MM-dd"));
            parameterTable.Add("ToDate", this.dtpToDate_Reorder.Value.ToString("yyyy-MM-dd"));
            parameterTable.Add("ShowBFQty", this.chkShowBFQty_Reorder.Checked.ToString());
            parameterTable.Add("ShowCDQty", this.chkShowCDQty_Reorder.Checked.ToString());
            parameterTable.Add("ShowATSQty", this.chkShowATSQty_Reorder.Checked.ToString());
            parameterTable.Add("ShowPOQty", this.chkShowPOQty_Reorder.Checked.ToString());

            System.Web.HttpContext.Current.Session["StockReorder"] = parameterTable;

            olapViewer.AspxPagePath = @"Inventory\Olap\StockReorder.aspx";
        }

        private void BuildParametersForStockIOHistory(ref RT2008.Controls.Reporting.OlapViewer olapViewer)
        {
            if (!(string.Compare(txtFromWorkplace_IOHistory.Text, txtToWorkplace_IOHistory.Text) > 0))
            {
                errorProvider.SetError(txtFromWorkplace_IOHistory, string.Empty);

                Hashtable parameterTable = new Hashtable();
                parameterTable.Add("FromStockCode", this.txtFromStkCode_IOHistory.Text);
                parameterTable.Add("ToStockCode", this.txtToStkCode_IOHistory.Text);
                parameterTable.Add("FromWorkplace", this.txtFromWorkplace_IOHistory.Text);
                parameterTable.Add("ToWorkplace", this.txtToWorkplace_IOHistory.Text);
                parameterTable.Add("FromDate", this.dtpFromDate_IOHistory.Value.ToString("yyyy-MM-dd"));
                parameterTable.Add("ToDate", this.dtpToDate_IOHistory.Value.ToString("yyyy-MM-dd"));

                System.Web.HttpContext.Current.Session["StockIOHistory"] = parameterTable;

                olapViewer.AspxPagePath = @"Inventory\Olap\StockInOut.aspx";
            }
            else
            {
                errorProvider.SetError(txtFromWorkplace_IOHistory, "*Shop range error!");
            }
        }

        private void BuildParametersForOCInventory(ref RT2008.Controls.Reporting.OlapViewer olapViewer)
        {
            if (!(string.Compare(txtFromMonth_OCInventory.Text, txtToMonth_OCInventory.Text) > 0))
            {
                errorProvider.SetError(txtFromMonth_OCInventory, string.Empty);

                Hashtable parameterTable = new Hashtable();
                parameterTable.Add("FromStockCode", this.txtFromStkCode_OCInventory.Text);
                parameterTable.Add("ToStockCode", this.txtToStkCode_OCInventory.Text);
                parameterTable.Add("FromMonth", this.txtFromMonth_OCInventory.Text);
                parameterTable.Add("ToMonth", this.txtToMonth_OCInventory.Text);

                System.Web.HttpContext.Current.Session["OCInventory"] = parameterTable;

                olapViewer.AspxPagePath = @"Inventory\Olap\InvtOpeningClosing.aspx";
            }
            else
            {
                errorProvider.SetError(txtFromMonth_OCInventory, "*Shop range error!");
            }
        }

        private void BuildParametersForDiscrepancyAudit(ref RT2008.Controls.Reporting.OlapViewer olapViewer)
        {
            Hashtable parameterTable = new Hashtable();
            parameterTable.Add("FromStockCode", this.txtFromStkCode_DiscrepancyAudit.Text);
            parameterTable.Add("ToStockCode", this.txtToStkCode_DiscrepancyAudit.Text);
            parameterTable.Add("Month", this.txtMonth_DiscrepancyAudit.Text);
            parameterTable.Add("FromDate", this.dtpFromDate_DiscrepancyAudit.Value.ToString("yyyy-MM-dd"));
            parameterTable.Add("ToDate", this.dtpToDate_DiscrepancyAudit.Value.ToString("yyyy-MM-dd"));
            parameterTable.Add("ShowDiff", this.chkShowDiff_DiscrepancyAudit.Checked.ToString());

            System.Web.HttpContext.Current.Session["DiscrepancyAudit"] = parameterTable;

            olapViewer.AspxPagePath = @"Inventory\Olap\StockValueDiscrepancyAudit.aspx";
        }

        private void BuildParametersForStockTransfer(ref RT2008.Controls.Reporting.OlapViewer olapViewer)
        {
            Hashtable parameterTable = new Hashtable();
            parameterTable.Add("FromTxNumber", this.txtFromStkCode_DiscrepancyAudit.Text);
            parameterTable.Add("ToTxNumber", this.txtToStkCode_DiscrepancyAudit.Text);
            parameterTable.Add("FromStockCode", this.txtFromStkCode_DiscrepancyAudit.Text);
            parameterTable.Add("ToStockCode", this.txtToStkCode_DiscrepancyAudit.Text);
            parameterTable.Add("FromDate", this.dtpFromDate_DiscrepancyAudit.Value.ToString("yyyy-MM-dd"));
            parameterTable.Add("ToDate", this.dtpToDate_DiscrepancyAudit.Value.ToString("yyyy-MM-dd"));
            parameterTable.Add("CompletedStatus", this.chkCompleted_Transfer.Checked.ToString());
            parameterTable.Add("InCompletedStatus", this.chkInCompleted_Transfer.Checked.ToString());
            parameterTable.Add("UnprocessedStatus", this.chkUnprocessed_Transfer.Checked.ToString());
            parameterTable.Add("PostedRecord", this.chkPostedRecords_Transfer.Checked.ToString());
            parameterTable.Add("UnPostRecord", this.chkUnPostRecord_Transfer.Checked.ToString());

            System.Web.HttpContext.Current.Session["StockTransfer"] = parameterTable;

            olapViewer.AspxPagePath = @"Inventory\Olap\StockTransfer.aspx";
        }

        private void BuildParametersForCapSummary(ref RT2008.Controls.Reporting.OlapViewer olapViewer)
        {
            Hashtable parameterTable = new Hashtable();
            parameterTable.Add("FromTxNumber", this.txtFromTxNumber_CAPSummary.Text);
            parameterTable.Add("ToTxNumber", this.txtToTxNumber_CAPSummary.Text);
            parameterTable.Add("FromSupplierCode", this.txtFromSupplierNumber_CAPSummary.Text);
            parameterTable.Add("ToSupplierCode", this.txtToSupplierNumber_CAPSummary.Text);
            parameterTable.Add("FromWorkplaceCode", this.txtFromWorkplace_CAPSummary.Text);
            parameterTable.Add("ToWorkplaceCode", this.txtToWorkplace_CAPSummary.Text);
            parameterTable.Add("FromDate", this.dtpFromDate_CAPSummary.Value.ToString("yyyy-MM-dd"));
            parameterTable.Add("ToDate", this.dtpToDate_CAPSummary.Value.ToString("yyyy-MM-dd"));
            parameterTable.Add("PostedRecord", this.chkPostedRecord_CAPSummary.Checked.ToString());
            parameterTable.Add("UnPostRecord", this.chkUnPostRecord_CAPSummary.Checked.ToString());
            parameterTable.Add("ShowRemarks", this.chkShowRemarks_CAPSummary.Checked.ToString());

            System.Web.HttpContext.Current.Session["CapSummary"] = parameterTable;

            olapViewer.AspxPagePath = @"Inventory\Olap\CapSummary.aspx";
        }

        #endregion

        private void btnShow_Click(object sender, EventArgs e)
        {
            AlertDataSourceSize();
            //this.ShowOlapPage();
        }

        #region Properties

        public RT2008.Controls.InvtUtility.InvtOlapViewerType ViewerType { get; set; }

        /// <summary>
        /// Gets from date.
        /// </summary>
        /// <value>From date.</value>
        private DateTime FromDate
        {
            get
            {
                return new DateTime(Convert.ToInt32(RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemYear),
                    Convert.ToInt32(RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemMonth), 1);
            }
        }

        /// <summary>
        /// Gets to date.
        /// </summary>
        /// <value>To date.</value>
        private DateTime ToDate
        {
            get
            {
                return new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            }
        }
        #endregion

        private void txtMonth_DiscrepancyAudit_TextChanged(object sender, EventArgs e)
        {
            if (txtMonth_DiscrepancyAudit.Text.Trim().Length == 6)
            {
                int year = Convert.ToInt32(txtMonth_DiscrepancyAudit.Text.Substring(0, 4));
                int month = Convert.ToInt32(txtMonth_DiscrepancyAudit.Text.Substring(4, 2));

                dtpFromDate_DiscrepancyAudit.Value = new DateTime(year, month, 1);
                dtpToDate_DiscrepancyAudit.Value = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            }
        }

        private void btnFindFromTxNumber_CAPSummary_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT * FROM (
SELECT HeaderId, TxNumber, TxType, TxDate FROM vwRptBatchCAP
UNION ALL
SELECT HeaderId, TxNumber, TxType, TxDate FROM vwRptSubLedgerCAP ) lst ";

            RT2008.Controls.InvtTxSearcher findFromTxNumber = new RT2008.Controls.InvtTxSearcher();
            findFromTxNumber.SqlQuery = sql;
            findFromTxNumber.TxType = Common.Enums.TxType.CAP;
            findFromTxNumber.Closed += new EventHandler(findFromTxNumber_CAPSummary_Closed);
            findFromTxNumber.ShowDialog();
        }

        void findFromTxNumber_CAPSummary_Closed(object sender, EventArgs e)
        {
            RT2008.Controls.InvtTxSearcher findFromTxNumber = sender as RT2008.Controls.InvtTxSearcher;
            if (findFromTxNumber.SelectedTxNumber.Trim().Length > 0)
            {
                txtFromTxNumber_CAPSummary.Text = findFromTxNumber.SelectedTxNumber;
            }
        }

        private void btnFindToTxNumber_CAPSummary_Click(object sender, EventArgs e)
        {
            string sql = @"SELECT * FROM (
SELECT HeaderId, TxNumber, TxType, TxDate FROM vwRptBatchCAP
UNION ALL
SELECT HeaderId, TxNumber, TxType, TxDate FROM vwRptSubLedgerCAP ) lst ";

            RT2008.Controls.InvtTxSearcher findToTxNumber = new RT2008.Controls.InvtTxSearcher();
            findToTxNumber.SqlQuery = sql;
            findToTxNumber.TxType = Common.Enums.TxType.CAP;
            findToTxNumber.Closed += new EventHandler(findToTxNumber_CAPSummary_Closed);
            findToTxNumber.ShowDialog();
        }

        void findToTxNumber_CAPSummary_Closed(object sender, EventArgs e)
        {
            RT2008.Controls.InvtTxSearcher findToTxNumber = sender as RT2008.Controls.InvtTxSearcher;
            if (findToTxNumber.SelectedTxNumber.Trim().Length > 0)
            {
                txtToTxNumber_CAPSummary.Text = findToTxNumber.SelectedTxNumber;
            }
        }
    }
}