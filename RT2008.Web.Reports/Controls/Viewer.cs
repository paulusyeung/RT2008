#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Interfaces;
using Microsoft.Reporting.WebForms;
using Gizmox.WebGUI.Common.Resources;
using System.IO;

#endregion

namespace RT2008.Web.Reports.Controls
{
    public partial class Viewer : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Viewer"/> class.
        /// </summary>
        public Viewer()
        {
            InitializeComponent();

            this.RptViewer.Dock = DockStyle.Fill;
            this.RptViewer.MinimumSize = new Size(800, 600);
            this.RptViewer.ExportContentDisposition = Microsoft.Reporting.WebForms.ContentDisposition.AlwaysInline;
        }

        #region Properties
        public enum ReportDestinations { Preview, Printer, ExcelFile, PdfFile }

        private DataTable dataSource = null;
        private Dictionary<string, DataTable> subDataSourceList = new Dictionary<string, DataTable>();
        private DataTable subDataSource;
        private string subDataSourceName;
        private string reportName = string.Empty;
        private string reportDatasourceName = string.Empty;
        private ReportDestinations reportDestination = ReportDestinations.Preview;
        private List<ReportParameter> parameters = new List<ReportParameter>();
        private Microsoft.Reporting.WebForms.ZoomMode _ZoomMode = ZoomMode.PageWidth;
        private int _ZoomPercent;

        /// <summary>
        /// 
        /// </summary>
        public string SubDataSourceName
        {
            get 
            { 
                return subDataSourceName;
            }
            set
            { 
                subDataSourceName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataTable SubDataSource
        {
            get 
            { 
                return subDataSource;
            }
            set 
            { 
                subDataSource = value; 
            }
        }
        /// <summary>
        /// Data Source Name of the Main Report
        /// </summary>
        public DataTable Datasource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                this.dataSource = value;
            }
        }

        /// <summary>
        /// Gets or sets the sub report data source list.
        /// </summary>
        /// <value>The sub report data source list.</value>
        public Dictionary<string, DataTable> SubReportDataSourceList
        {
            get
            {
                return this.subDataSourceList;
            }
            set
            {
                this.subDataSourceList = value;
            }
        }

        /// <summary>
        /// the class name of the report
        /// </summary>
        public string ReportName
        {
            get
            {
                return this.reportName;
            }
            set
            {
                this.reportName = value;
            }
        }

        public string ReportDatasourceName
        {

            get
            {
                return this.reportDatasourceName;
            }
            set
            {
                this.reportDatasourceName = value;
            }
        }

        /// <summary>
        /// Assigned to one of the following:
        /// Preview = screen (default)
        /// Printer = redirect to printer
        /// ExcelFile = generate Excel file
        /// PdfFile = generate PDF file
        /// </summary>
        public ReportDestinations ReportDestination
        {
            get
            {
                return this.reportDestination;
            }
            set
            {
                this.reportDestination = value;
            }
        }

        /// <summary>
        /// parameters used in the report
        /// string array: name, value
        /// e.g. string [,] param = {{"FromDate", "2008-08-01 00:00:00"}, {"ToDate", "2008-08-31 23:59:59"}}
        /// </summary>
        public List<ReportParameter> Parameters
        {
            get
            {
                return this.parameters;
            }
            set
            {
                this.parameters = value;
            }
        }

        public Microsoft.Reporting.WebForms.ZoomMode ZoomMode
        {
            get
            {
                return this._ZoomMode;
            }
            set
            {
                this._ZoomMode = value;
            }
        }

        public int ZoomPercent
        {
            get
            {
                return this._ZoomPercent;
            }
            set
            {
                this._ZoomPercent = value;
            }
        }
        #endregion

        #region Preview Report
        /// <summary>
        /// Previews the report.
        /// </summary>
        public void PreviewReport()
        {
            this.RptViewer.ProcessingMode = ProcessingMode.Local;
            //this.RptViewer.Reset();

            if (reportName != string.Empty)
            {
                this.RptViewer.LocalReport.ReportEmbeddedResource = reportName;
            }
            this.RptViewer.LocalReport.DataSources.Clear();

            ReportDataSource ds = new ReportDataSource(reportDatasourceName, dataSource);
            this.RptViewer.LocalReport.DataSources.Add(ds);
            this.RptViewer.LocalReport.EnableExternalImages = true;
            this.RptViewer.LocalReport.EnableHyperlinks = true;

            if (this.parameters != null)
            {
                if (this.parameters.Count > 0)
                {
                    this.RptViewer.LocalReport.SetParameters(this.parameters);
                }
            }

            //this.RptViewer.ProcessingMode = ProcessingMode.Local;
            this.RptViewer.ZoomMode = this._ZoomMode;
            if (this.ZoomMode == ZoomMode.Percent)
            {
                this.RptViewer.ZoomPercent = this._ZoomPercent;
            }
            this.RptViewer.Update();
        }
        #endregion

        /// <summary>
        /// RPTs the subreport processing event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Reporting.WebForms.SubreportProcessingEventArgs"/> instance containing the event data.</param>
        void RptSubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            if (this.SubReportDataSourceList != null)
            {
                foreach (KeyValuePair<string, DataTable> kvp in this.SubReportDataSourceList)
                {
                    subDataSource = kvp.Value;
                    subDataSourceName = kvp.Key;

                    e.DataSources.Add(new ReportDataSource(subDataSourceName, subDataSource));
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the Viewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Viewer_Load(object sender, EventArgs e)
        {
            PreviewReport();
        }

        /// <summary>
        /// Handles the HostedPageLoadComplete event of the RptViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Gizmox.WebGUI.Forms.Hosts.AspPageEventArgs"/> instance containing the event data.</param>
        private void RptViewer_HostedPageLoadComplete(object sender, Gizmox.WebGUI.Forms.Hosts.AspPageEventArgs e)
        {
            switch (reportDestination)
            {
                case ReportDestinations.Printer:
                    break;
                case ReportDestinations.ExcelFile:
                    break;
                case ReportDestinations.PdfFile:
                    break;
                case ReportDestinations.Preview:
                default:
                    PreviewReport();
                    break;
            }
        }

        /// <summary>
        /// Handles the Drillthrough event of the RptViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Reporting.WebForms.DrillthroughEventArgs"/> instance containing the event data.</param>
        private void RptViewer_Drillthrough(object sender, DrillthroughEventArgs e)
        {
            LocalReport lr = (LocalReport)e.Report;
            string photo = lr.GetParameters()["Photo"].Values[0].ToString();

            //2014.02.17 paulus: 直接用 Photo as image Uri
            //string imageFolder = VWGContext.Current.Config.GetDirectory("RTImages");
            //string imagePath = Path.Combine(Path.Combine(imageFolder, "Product"), photo);
            string imagePath = photo;

            ViewImage viewImage = new ViewImage();
            viewImage.ImageName = imagePath;

            viewImage.ShowDialog();

            e.Cancel = true;

            //LocalReport lr = (LocalReport)e.Report;
            //lr.DataSources.Clear();
            //lr.DataSources.Add(new ReportDataSource(reportDatasourceName, dataSource));
        }

        /// <summary>
        /// Handles the ReportRefresh event of the RptViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void RptViewer_ReportRefresh(object sender, CancelEventArgs e)
        {
            if (this.RptViewer.LocalReport.IsDrillthroughReport)
            {
                this.RptViewer.PerformBack();
            }
            this.RptViewer.Dock = DockStyle.Fill;
            this.RptViewer.ShowBackButton = true;
        }

        /// <summary>
        /// Handles the HostedControlPreRender event of the RptViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Gizmox.WebGUI.Forms.Hosts.AspControlEventArgs"/> instance containing the event data.</param>
        private void RptViewer_HostedControlPreRender(object sender, Gizmox.WebGUI.Forms.Hosts.AspControlEventArgs e)
        {
            this.RptViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(RptSubreportProcessingEventHandler);
        }
    }
}