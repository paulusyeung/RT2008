using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

using DevExpress.XtraReports.UI;

using RT2008.DAL;

namespace RT2008.Inventory.Replenishment.Reports
{
    public partial class HistoryRpt : DevExpress.XtraReports.UI.XtraReport
    {
        private string _TxNumberFrom = string.Empty;
        private string _TxNumberTo = string.Empty;
        private DateTime _TxDateFrom = DateTime.Now;
        private DateTime _TxDateTo = DateTime.Now;

        public HistoryRpt()
        {
            InitializeComponent();
        }

        #region Attribute
        public string TxNumberFrom
        {
            get
            {
                return TxNumberFrom;
            }
            set
            {
                _TxNumberFrom = value;
            }
        }

        public string TxNumberTo
        {
            get
            {
                return _TxNumberTo;
            }
            set
            {
                _TxNumberTo = value;
            }
        }

        public DateTime TxDateFrom
        {
            get
            {
                return _TxDateFrom;
            }
            set
            {
                _TxDateFrom = value;
            }
        }

        public DateTime TxDateTo
        {
            get
            {
                return _TxDateTo;
            }
            set
            {
                _TxDateTo = value;
            }
        }
        #endregion

        private void HistoryRpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // ph
            this.txtTxNumberFrom.Text = _TxNumberFrom;
            this.txtTxNumberTo.Text = _TxNumberTo;
            this.txtTxDateFrom.Text = _TxDateFrom.ToString(RT2008.SystemInfo.Settings.GetDateFormat());
            this.txtTxDateTo.Text = _TxDateTo.ToString(RT2008.SystemInfo.Settings.GetDateFormat());

            this.phStkCode.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE");
            this.phAppendix1.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1");
            this.phAppendix2.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2");
            this.phAppendix3.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3");

            this.phDateTime.Format = "{0:" + RT2008.SystemInfo.Settings.GetDateTimeFormat() + "}";

            this.phWarehouse.Visible = false;   // HACK: Do know what is this.
            // gh1
            this.txtHeaderId.DataBindings.Add("Text", DataSource, "HeaderId");
            this.txtTxNumber.DataBindings.Add("Text", DataSource, "TxNumber");
            this.txtTxDate.DataBindings.Add("Text", DataSource, "TxDate");
            this.txtFmLocationCode.DataBindings.Add("Text", DataSource, "FmLocationCode");
            this.txtFmLocationName.DataBindings.Add("Text", DataSource, "FmLocationName");
            this.txtToLocationCode.DataBindings.Add("Text", DataSource, "ToLocationCode");
            this.txtToLocationName.DataBindings.Add("Text", DataSource, "ToLocationName");
            this.txtTransferredOn.DataBindings.Add("Text", DataSource, "TransferredOn", "{0:" + RT2008.SystemInfo.Settings.GetDateFormat() + "}");
            this.txtCompletedOn.DataBindings.Add("Text", DataSource, "CompletedOn", "{0:" + RT2008.SystemInfo.Settings.GetDateFormat() + "}");
            this.txtOperatorCode.DataBindings.Add("Text", DataSource, "OperatorCode");
            this.txtOperatorName.DataBindings.Add("Text", DataSource, "OperatorName");
            this.txtRemarks.DataBindings.Add("Text", DataSource, "Remarks");
            this.txtStatus.DataBindings.Add("Text", DataSource, "Status");
            this.txtModifiedBy.DataBindings.Add("Text", DataSource, "ModifiedBy");
            this.txtModifiedOn.DataBindings.Add("Text", DataSource, "ModifiedOn", "{0:" + RT2008.SystemInfo.Settings.GetDateFormat() + "}");

            this.GroupHeader1.GroupFields.Add(new GroupField("TxNumber"));      // HACK: 唔知掂解, 如果用 HeaderId 會影響 順序.
            // gf1
            //this.gfTotalQty.DataBindings.Add("Text", DataSource, "TotalQty", "{0:n" + RT2008.SystemInfo.Settings.GetQtyDecimalPoint().ToString() + "}");
            //this.gfTotalAmount.DataBindings.Add("Text", DataSource, "TotalAmount", "{0:n2}");
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.GetCurrentColumnValue("HeaderId") != null)
            {
                DataTable dt = Reports.DataSource.HistoryDetails(this.GetCurrentColumnValue("HeaderId").ToString());

                if (dt.Rows.Count > 0)
                {
                    HistoryRptDetails subReport = new HistoryRptDetails();
                    subReport.DataSource = dt;
                    rptDetails.ReportSource = subReport;
                }
                else
                {
                    rptDetails.ReportSource = null;
                }
            }
        }

    }
}
