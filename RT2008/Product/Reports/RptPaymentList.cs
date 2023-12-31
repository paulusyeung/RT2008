#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using DevExpress.XtraReports.UI;
using Gizmox.WebGUI.Common.Interfaces;
using System.Web;
using RT2008.DAL;
using System.Data.Common;
using System.Configuration;

#endregion

namespace RT2008.Product.Reports
{
    //public partial class RptPaymentList : Form, IGatewayControl
    public partial class RptPaymentList : Form
    {
        public RptPaymentList()
        {
            InitializeComponent();
        }

        #region FillComboBox
        private void FillComboBox()
        {
            PosTenderTypeCollection collection = RT2008.DAL.PosTenderType.LoadCollection(new string[] { "TypeCode" }, true);
            if (collection.Count > 0)
            {
                foreach (RT2008.DAL.PosTenderType oPosTenderType in collection)
                {
                    System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(oPosTenderType.TypeCode, oPosTenderType.TypeId.ToString());
                    cmbFrom.Items.Add(item);
                    cmbTo.Items.Add(item);
                }
                cmbFrom.SelectedIndex = 0;
                //cmbFrom.DropDownStyle = ComboBoxStyle.DropDownList;

                cmbTo.SelectedIndex = collection.Count - 1;
                //cmbTo.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        #endregion

        #region Data Binds

        private DataTable BindData()
        {
            string frmCode = cmbFrom.Text.Trim();
            string toCode = cmbTo.Text.Trim();

            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@frmTypeCode", frmCode), new SqlParameter("@toTypeCode", toCode) };

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "apPosTenderTypeList";
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameterValues);

            using (DataSet dataset = SqlHelper.Default.ExecuteDataSet(cmd))
            {
                return dataset.Tables[0];
            }

        }
        #endregion

        //#region IGatewayControl Members

        //public IGatewayHandler GetGatewayHandler(IContext objContext, string strAction)
        //{

        //    // Create a report instance.
        //    if (rbnPDF.Checked == true)
        //    {
        //        RT2008.Product.Reports.PaymentListRpt_Pdf report = new PaymentListRpt_Pdf();

        //        report.DataSource = BindData();
        //        report.FrmCode = cmbFrom.Text.Trim();
        //        report.toCode = cmbTo.Text.Trim();
        //        HttpResponse objResponse = this.Context.HttpContext.Response;

        //        System.IO.MemoryStream memStream = new System.IO.MemoryStream();

        //        objResponse.Clear();
        //        objResponse.ClearHeaders();
        //        report.ExportToPdf(memStream);
        //        objResponse.ContentType = "application/pdf";
        //        objResponse.AddHeader("content-disposition", "attachment; filename=PosTenderTypeList.pdf");
        //        objResponse.BinaryWrite(memStream.ToArray());
        //        objResponse.Flush();
        //        objResponse.End();

        //        return null;
        //    }
        //    else if (rbnXLS.Checked)
        //    {
        //        RT2008.Product.Reports.PaymentListRpt_Xls reportc = new PaymentListRpt_Xls();

        //        reportc.DataSource = BindData();
        //        reportc.FrmCode = cmbFrom.Text.Trim();
        //        reportc.toCode = cmbTo.Text.Trim();
        //        HttpResponse objResponse = this.Context.HttpContext.Response;

        //        System.IO.MemoryStream memStream = new System.IO.MemoryStream();

        //        objResponse.Clear();
        //        objResponse.ClearHeaders();
        //        reportc.ExportToXls(memStream);
        //        objResponse.ContentType = "application/xls";
        //        objResponse.AddHeader("content-disposition", "attachment; filename=PosTenderTypeListc.xls");
        //        objResponse.BinaryWrite(memStream.ToArray());
        //        objResponse.Flush();
        //        objResponse.End();

        //        return null;
        //    }
        //    else
        //    {
        //        RT2008.Product.Reports.PaymentListRpt_Pdf rpt = new PaymentListRpt_Pdf();
        //        try
        //        {
        //            rpt.DataSource = BindData();
        //            rpt.FrmCode = cmbFrom.Text.Trim();
        //            rpt.toCode = cmbTo.Text.Trim();
        //            rpt.PrintDialog();
        //            rpt.Print();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }

        //        return null;
        //    }
        //}

        //#endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "open"));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RptPaymentList_Load(object sender, EventArgs e)
        {
            FillComboBox();
        }

        private void btnPriview_Click(object sender, EventArgs e)
        {
            string[,] param = {
            {"FromPayment",this.cmbFrom.Text.Trim()},
            {"ToPayment",this.cmbTo.Text.Trim()},
            {"PrintedOn",DateTime.Now.ToString(RT2008.SystemInfo.Settings.GetDateTimeFormat())},
            {"CompanyName",RT2008.SystemInfo.CurrentInfo.Default.CompanyName}
            };

            RT2008.Controls.Reporting.Viewer view = new RT2008.Controls.Reporting.Viewer();

            view.Datasource = BindData();
            view.ReportDatasourceName = "RT2008_Controls_Reporting_DataSource_apPosTenderTypeList";
            view.ReportName = "RT2008.Product.Reports.PaymentListRdl.rdlc";
            view.Parameters = param;

            view.Show();
        }
    }
}