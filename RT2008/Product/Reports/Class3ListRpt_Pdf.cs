using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace RT2008.Product.Reports
{
    public partial class Class3ListRpt_Pdf : DevExpress.XtraReports.UI.XtraReport
    {
        private string _frmCode = string.Empty;
        private string _toCode = string.Empty;

        public Class3ListRpt_Pdf()
        {
            InitializeComponent();

            this.lblCaption.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("CLASS3") + " List";
        }     

        private void CollectionListRpt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.txtFm.Text = _frmCode;
            this.txtTo.Text = _toCode;
            this.txtID.DataBindings.Add("Text", DataSource, "Class3Code");
            this.txtDESC.DataBindings.Add("Text", DataSource, "Class3Initial");
            this.txtDESC_LONG.DataBindings.Add("Text", DataSource, "Class3Name");
            this.txtDateCreate.DataBindings.Add("Text", DataSource, "CreatedOn", "{0:" + RT2008.SystemInfo.Settings.GetDateFormat() + "}");
            this.txtDateLChg.DataBindings.Add("Text", DataSource, "ModifiedOn", "{0:" + RT2008.SystemInfo.Settings.GetDateFormat() + "}");
            this.txtUserLChg.DataBindings.Add("Text", DataSource, "StaffName");
            this.txtPrint.Text = RT2008.SystemInfo.Settings.DateTimeToString(System.DateTime.Now, true);
        }

        #region Attribute
        public string FrmCode
        {
            get
            {
                return _frmCode;
            }
            set
            {
                _frmCode = value;
            }
        }

        public string toCode
        {
            get
            {
                return _toCode;
            }
            set
            {
                _toCode = value;
            }
        }

        #endregion

    }
}
