#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using System.Data.SqlClient;
using RT2008.DAL;
using Gizmox.WebGUI.Common.Interfaces;
using System.Web;
using System.Configuration;

#endregion

namespace RT2008.Product
{
    public partial class ProductWizard_Authorization : Form, IGatewayComponent
    {
        public ProductWizard_Authorization()
        {
            InitializeComponent();
            BindingBatchList();

            SetSystemLabels();
        }

        #region Set System label
        private void SetSystemLabels()
        {
            colPLU.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("STKCODE");
            colSeason.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX1");
            colColor.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX2");
            colSize.Text = RT2008.SystemInfo.Settings.GetSystemLabelByKey("APPENDIX3");
        }
        #endregion

        #region Bind Methods
        private void BindingBatchList()
        {
            lvHoldBatchList.Items.Clear();

            int iCount = 0;
            string sql = BuildSqlQueryString();
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = System.Data.CommandType.Text;

            using (SqlDataReader reader = RT2008.DAL.SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem objItem = this.lvHoldBatchList.Items.Add(reader.GetGuid(0).ToString()); // ProductId
                    objItem.SubItems.Add(string.Empty);
                    objItem.SubItems.Add(reader.GetValue(1).ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetString(2)); // ProductCode
                    objItem.SubItems.Add(reader.GetString(3)); // Product Name
                    objItem.SubItems.Add(reader.GetString(4)); // Product Name Chs
                    objItem.SubItems.Add(reader.GetString(5)); // Product name Cht
                    objItem.SubItems.Add(reader.GetString(6)); // Nature
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(7), false)); // CreatedOn

                    iCount++;
                }
            }

            lblRecords.Text = string.Format("Rec: {0}", iCount.ToString());
        }

        private string BuildSqlQueryString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT BatchId,  ROW_NUMBER() OVER (ORDER BY STKCODE) AS rownum, ");
            sql.Append(" STKCODE, APP1_COMBIN, APP2_COMBIN, APP3_COMBIN, Description, DATECREATE");
            sql.Append(" FROM ProductBatch ");
            sql.Append(" WHERE STATUS = 'POST' ");

            if (chkSortAndFilter.Checked)
            {
                if (cboSortColumn.Text.Length > 0 && cboOperator.Text != "None")
                {
                    sql.Append(" AND ");
                    sql.Append(ColumnName()).Append(" ");

                    if (cboSortColumn.Text.Contains("Date"))
                    {
                        sql.Append("BETWEEN '");
                        sql.Append(txtData.Tag.ToString()).Append(" 00:00:00'");
                        sql.Append(" AND '");
                        sql.Append(txtData.Tag.ToString()).Append(" 23:59:59'");
                    }
                    else
                    {
                        sql.Append((cboOperator.Text == "=") ? "= '" : "LIKE '%");
                        sql.Append(txtData.Text).Append((cboOperator.Text == "=") ? "'" : "%'");
                    }

                    sql.Append(" ORDER BY ");
                    sql.Append(ColumnName());
                    sql.Append((cboOrdering.Text == "Ascending") ? " ASC" : " DESC");
                }
            }

            return sql.ToString();
        }

        private string ColumnName()
        {
            string colName = string.Empty;
            switch (cboSortColumn.Text)
            {
                case "Season":
                    colName = "APP1_COMBIN";
                    break;
                case "Color":
                    colName = "APP2_COMBIN";
                    break;
                case "Size":
                    colName = "APP3_COMBIN";
                    break;
                case "Description":
                    colName = "Description";
                    break;
                case "Date Create(dd/MM/yyyy)":
                    colName = "DATECREATE";
                    break;
                default:
                case "PLU":
                    colName = "STKCODE";
                    break;
            }
            return colName;
        }

        private bool VerifyDate()
        {
            bool isVerified = true;
            if (cboSortColumn.Text.Contains("Date"))
            {
                string pattern = @"^\d{1,2}\/\d{1,2}\/\d{4}$";
                Regex rex = new Regex(pattern);
                Match match = rex.Match(txtData.Text);
                if (!match.Success)
                {
                    errorProvider.SetError(txtData, "Incorrect Date Format! (Date Format:dd/MM/yyyy)");
                    isVerified = false;
                }
                else
                {
                    errorProvider.SetError(txtData, string.Empty);
                    FormatDate();
                }
            }
            return isVerified;
        }

        private void FormatDate()
        {
            string[] dateValue = txtData.Text.Split('/');
            txtData.Tag = dateValue[2] + "-" + dateValue[1] + "-" + dateValue[0];
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
            RT2008.Product.Reports.ProductBatchList_Pdf report = new RT2008.Product.Reports.ProductBatchList_Pdf();

            report.DataSource = BindData();
            HttpResponse objResponse = this.Context.HttpContext.Response;

            System.IO.MemoryStream memStream = new System.IO.MemoryStream();

            objResponse.Clear();
            objResponse.ClearHeaders();

            // Export the report to PDF.
            report.ExportToPdf(memStream);
            objResponse.ContentType = "application/pdf";
            objResponse.AddHeader("content-disposition", "attachment; filename=ProductBatchList.pdf");

            objResponse.BinaryWrite(memStream.ToArray());
            objResponse.Flush();
            objResponse.End();
        }

        private DataTable BindData()
        {
            string sql = @"SELECT  
                               STKCODE, APP1_COMBIN, APP2_COMBIN, APP3_COMBIN, CLASS1,  
                               CLASS2, CLASS3, CLASS4, CLASS5, CLASS6, [Description], MAINUNIT,  
                               ALTITEM, REMARKS, MARKUP, BASPRC, WHLPRC, VCURR, VPRC, ORIPRC,  
                               NRDISC, REORDLVL, REORDQTY, SERIALFLAG, NATURE, REMARK1,  
                               REMARK2, REMARK3, REMARK4, PHOTO, STK_MEMO, [STATUS] AS STATUSDESC, CONVERT(VARCHAR(10), DATEPOST, 103) AS DATEPOST,  
                               RETAILITEM, BINX, BINY, BINZ, DESC_MEMO, DESC_POLE,  
                               CONVERT(VARCHAR(10), DATECREATE, 103) AS DATECREATE, CONVERT(VARCHAR(10), DATELCHG, 103) AS DATELCHG, USERLCHG  
                           FROM ProductBatch
                           WHERE STATUS = 'POST' ";

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

        #region Posting Batch

        #region Combination
        private ListView Combin()
        {
            ListView oList = null;

            oList = new ListView();

            foreach (ListViewItem oItem in this.lvHoldBatchList.Items)
            {
                if (oItem.SubItems[1].Text == "*")
                {
                    CombinA1(oItem, ref oList);
                }
            }

            return oList;
        }

        private void CombinA1(ListViewItem oItem, ref ListView oList)
        {
            string sql = "DimCode = '" + oItem.SubItems[4].Text + "'";
            ProductDim oDim = ProductDim.LoadWhere(sql);
            if (oDim != null)
            {
                sql = "DimensionId = '" + oDim.DimensionId.ToString() + "'";
                ProductDim_DetailsCollection detailList = ProductDim_Details.LoadCollection(sql);
                foreach (ProductDim_Details detail in detailList)
                {
                    if (!string.IsNullOrEmpty(detail.APPENDIX1))
                    {
                        CombinA2(oItem, ref oList, oItem.SubItems[0].Text, oItem.SubItems[3].Text, detail.APPENDIX1);
                    }
                }
            }
        }

        private void CombinA2(ListViewItem oItem, ref ListView oList, string batchId, string stk, string a1)
        {
            string sql = "DimCode = '" + oItem.SubItems[5].Text + "'";
            ProductDim oDim = ProductDim.LoadWhere(sql);
            if (oDim != null)
            {
                sql = "DimensionId = '" + oDim.DimensionId.ToString() + "'";
                ProductDim_DetailsCollection detailList = ProductDim_Details.LoadCollection(sql);
                foreach (ProductDim_Details detail in detailList)
                {
                    if (!string.IsNullOrEmpty(detail.APPENDIX2))
                    {
                        CombinA3(oItem, ref oList, batchId, stk, a1, detail.APPENDIX2);
                    }
                }

                if (detailList.Count == 0)
                {
                    AddToList(ref oList, batchId, stk, a1, string.Empty, string.Empty);
                }
            }
            else
            {
                AddToList(ref oList, batchId, stk, a1, string.Empty, string.Empty);
            }
        }

        private void CombinA3(ListViewItem oItem, ref ListView oList, string batchId, string stk, string a1, string a2)
        {
            string sql = "DimCode = '" + oItem.SubItems[6].Text + "'";
            ProductDim oDim = ProductDim.LoadWhere(sql);
            if (oDim != null)
            {
                sql = "DimensionId = '" + oDim.DimensionId.ToString() + "'";
                ProductDim_DetailsCollection detailList = ProductDim_Details.LoadCollection(sql);
                foreach (ProductDim_Details detail in detailList)
                {
                    if (!string.IsNullOrEmpty(detail.APPENDIX3))
                    {
                        AddToList(ref oList, batchId, stk, a1, a2, detail.APPENDIX3);
                    }
                }

                if (detailList.Count == 0)
                {
                    AddToList(ref oList, batchId, stk, a1, a2, string.Empty);
                }
            }
            else
            {
                AddToList(ref oList, batchId, stk, a1, a2, string.Empty);
            }
        }

        private void AddToList(ref ListView oList, string batchId, string stk, string a1, string a2, string a3)
        {
            ListViewItem objItem = oList.Items.Add((oList.Items.Count + 1).ToString());
            objItem.SubItems.Add(batchId);
            objItem.SubItems.Add(stk);
            objItem.SubItems.Add(a1);
            objItem.SubItems.Add(a2);
            objItem.SubItems.Add(a3);
            objItem.SubItems.Add(GetAppendix1Id(a1).ToString());
            objItem.SubItems.Add(GetAppendix2Id(a2).ToString());
            objItem.SubItems.Add(GetAppendix3Id(a3).ToString());
        }

        private Guid GetAppendix1Id(string a1Code)
        {
            string sql = "Appendix1Initial = '" + a1Code + "'";
            ProductAppendix1 oA1 = ProductAppendix1.LoadWhere(sql);
            if (oA1 != null)
            {
                return oA1.Appendix1Id;
            }
            else
            {
                return System.Guid.Empty;
            }
        }

        private Guid GetAppendix2Id(string a2Code)
        {
            string sql = "Appendix2Initial = '" + a2Code + "'";
            ProductAppendix2 oA2 = ProductAppendix2.LoadWhere(sql);
            if (oA2 != null)
            {
                return oA2.Appendix2Id;
            }
            else
            {
                return System.Guid.Empty;
            }
        }

        private Guid GetAppendix3Id(string a3Code)
        {
            string sql = "Appendix3Initial = '" + a3Code + "'";
            ProductAppendix3 oA3 = ProductAppendix3.LoadWhere(sql);
            if (oA3 != null)
            {
                return oA3.Appendix3Id;
            }
            else
            {
                return System.Guid.Empty;
            }
        }
        #endregion

        private int SelectedColumnsCounting()
        {
            int iCount = 0;

            foreach (ListViewItem objItem in this.lvHoldBatchList.Items)
            {
                if (objItem.SubItems[1].Text == "*")
                {
                    iCount++;
                }
            }

            return iCount;
        }

        private bool IsValid()
        {
            return true;
        }

        #region Get IDs
        private Guid GetNatureId(string nature)
        {
            System.Guid natureId = System.Guid.Empty;

            string sql = "NatureCode = '" + nature + "'";
            ProductNature oNature = ProductNature.LoadWhere(sql);
            if (oNature != null)
            {
                natureId = oNature.NatureId;
            }

            return natureId;
        }

        private Guid GetClass1Id(string c1Code)
        {
            System.Guid c1Id = System.Guid.Empty;

            string sql = "Class1Code = '" + c1Code + "'";
            ProductClass1 oClass1 = ProductClass1.LoadWhere(sql);
            if (oClass1 != null)
            {
                c1Id = oClass1.Class1Id;
            }

            return c1Id;
        }

        private Guid GetClass2Id(string c2Code)
        {
            System.Guid c2Id = System.Guid.Empty;

            string sql = "Class2Code = '" + c2Code + "'";
            ProductClass2 oClass2 = ProductClass2.LoadWhere(sql);
            if (oClass2 != null)
            {
                c2Id = oClass2.Class2Id;
            }

            return c2Id;
        }

        private Guid GetClass3Id(string c3Code)
        {
            System.Guid c3Id = System.Guid.Empty;

            string sql = "Class3Code = '" + c3Code + "'";
            ProductClass3 oClass3 = ProductClass3.LoadWhere(sql);
            if (oClass3 != null)
            {
                c3Id = oClass3.Class3Id;
            }

            return c3Id;
        }

        private Guid GetClass4Id(string c4Code)
        {
            System.Guid c4Id = System.Guid.Empty;

            string sql = "Class4Code = '" + c4Code + "'";
            ProductClass4 oClass4 = ProductClass4.LoadWhere(sql);
            if (oClass4 != null)
            {
                c4Id = oClass4.Class4Id;
            }

            return c4Id;
        }

        private Guid GetClass5Id(string c5Code)
        {
            System.Guid c5Id = System.Guid.Empty;

            string sql = "Class5Code = '" + c5Code + "'";
            ProductClass5 oClass5 = ProductClass5.LoadWhere(sql);
            if (oClass5 != null)
            {
                c5Id = oClass5.Class5Id;
            }

            return c5Id;
        }

        private Guid GetClass6Id(string c6Code)
        {
            System.Guid c6Id = System.Guid.Empty;

            string sql = "Class6Code = '" + c6Code + "'";
            ProductClass6 oClass6 = ProductClass6.LoadWhere(sql);
            if (oClass6 != null)
            {
                c6Id = oClass6.Class6Id;
            }

            return c6Id;
        }
        #endregion

        private int CreateProducts()
        {
            int iCount = 0;
            ListView combinList = Combin();
            if (combinList.Items.Count > 0)
            {
                foreach (ListViewItem oItem in combinList.Items)
                {
                    CreateProducts(oItem);
                    iCount++;
                }
            }
            return iCount;
        }

        private void CreateProducts(ListViewItem listItem)
        {
            // Check BatchId(listItem.SubItems[1].Text) and Stock Code(listItem.SubItems[2].Text)
            if (IsValid() && Common.Utility.IsGUID(listItem.SubItems[1].Text) && listItem.SubItems[2].Text.Length > 0)
            {
                ProductBatch oBatch = ProductBatch.Load(new System.Guid(listItem.SubItems[1].Text));
                if (oBatch != null)
                {
                    string a1 = listItem.SubItems[3].Text;
                    string a2 = listItem.SubItems[4].Text;
                    string a3 = listItem.SubItems[5].Text;

                    System.Guid a1Id = (Common.Utility.IsGUID(listItem.SubItems[6].Text)) ? new Guid(listItem.SubItems[6].Text) : System.Guid.Empty;
                    System.Guid a2Id = (Common.Utility.IsGUID(listItem.SubItems[7].Text)) ? new Guid(listItem.SubItems[7].Text) : System.Guid.Empty;
                    System.Guid a3Id = (Common.Utility.IsGUID(listItem.SubItems[8].Text)) ? new Guid(listItem.SubItems[8].Text) : System.Guid.Empty;

                    string prodCode = listItem.SubItems[2].Text + a1 + a2 + a3;
                    if (prodCode.Length <= 22)
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.Append(" STKCODE = '").Append(listItem.SubItems[2].Text.Trim()).Append("' ");
                        sql.Append(" AND APPENDIX1 = '").Append(a1.Trim()).Append("' ");
                        sql.Append(" AND APPENDIX2 = '").Append(a2.Trim()).Append("' ");
                        sql.Append(" AND APPENDIX3 = '").Append(a3.Trim()).Append("' ");

                        RT2008.DAL.Product oItem = RT2008.DAL.Product.LoadWhere(sql.ToString());
                        if (oItem == null)
                        {
                            oItem = new RT2008.DAL.Product();

                            oItem.STKCODE = listItem.SubItems[2].Text;
                            oItem.APPENDIX1 = a1;
                            oItem.APPENDIX2 = a2;
                            oItem.APPENDIX3 = a3;

                            oItem.Status = Convert.ToInt32(Common.Enums.Status.Active.ToString("d"));

                            oItem.CLASS1 = oBatch.CLASS1;
                            oItem.CLASS2 = oBatch.CLASS2;
                            oItem.CLASS3 = oBatch.CLASS3;
                            oItem.CLASS4 = oBatch.CLASS4;
                            oItem.CLASS5 = oBatch.CLASS5;
                            oItem.CLASS6 = oBatch.CLASS6;

                            oItem.ProductName = oBatch.Description;
                            oItem.ProductName_Chs = oBatch.Description;
                            oItem.ProductName_Cht = oBatch.Description;
                            oItem.Remarks = oBatch.REMARKS;

                            oItem.NormalDiscount = oBatch.NRDISC;
                            oItem.UOM = oBatch.MAINUNIT;
                            oItem.NatureId = GetNatureId(oBatch.NATURE);

                            oItem.FixedPriceItem = false;

                            oItem.CreatedBy = Common.Config.CurrentUserId;
                            oItem.CreatedOn = DateTime.Now;
                            oItem.ModifiedBy = Common.Config.CurrentUserId;
                            oItem.ModifiedOn = DateTime.Now;

                            oItem.Save();

                            SaveProductBarcode(oBatch, oItem.ProductId, prodCode);

                            // Appendix / Class
                            System.Guid c1Id = GetClass1Id(oBatch.CLASS1);
                            System.Guid c2Id = GetClass2Id(oBatch.CLASS2);
                            System.Guid c3Id = GetClass3Id(oBatch.CLASS3);
                            System.Guid c4Id = GetClass4Id(oBatch.CLASS4);
                            System.Guid c5Id = GetClass5Id(oBatch.CLASS5);
                            System.Guid c6Id = GetClass6Id(oBatch.CLASS6);
                            SaveProductCode(oItem.ProductId, a1Id, a2Id, a3Id, c1Id, c2Id, c3Id, c4Id, c5Id, c6Id);

                            // Product Price
                            SaveProductSupplement(oBatch, oItem.ProductId);
                            SaveProductPrice(oBatch, oItem.ProductId);

                            // Remarks
                            SaveProductRemarks(oBatch, oItem.ProductId);

                            oBatch.STATUS = "OK";
                            oBatch.Save();
                        }
                    }
                }
            }
        }

        private void SaveProductBarcode(ProductBatch oBatch, Guid productid, string barcode)
        {
            string sql = "ProductId = '" + productid.ToString() + "' AND Barcode = '" + barcode + "'";
            ProductBarcode oBarcode = ProductBarcode.LoadWhere(sql);
            if (oBarcode == null)
            {
                oBarcode = new ProductBarcode();

                oBarcode.ProductId = productid;
                oBarcode.Barcode = barcode;
                oBarcode.BarcodeType = "INTER";
                oBarcode.PrimaryBarcode = true;
                oBarcode.DownloadToPOS = (oBatch.RETAILITEM == "F") ? false : true;
                oBarcode.DownloadToCounter = (oBatch.COUNTER_ITEM == "F") ? false : true;

                oBarcode.Save();
            }
        }

        private void SaveProductCode(Guid productId, Guid a1Id, Guid a2Id, Guid a3Id, Guid c1Id, Guid c2Id, Guid c3Id, Guid c4Id, Guid c5Id, Guid c6Id)
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductCode oCode = ProductCode.LoadWhere(sql);
            if (oCode == null)
            {
                oCode = new ProductCode();

                oCode.ProductId = productId;
                oCode.Appendix1Id = a1Id;
                oCode.Appendix2Id = a2Id;
                oCode.Appendix3Id = a3Id;
            }
            oCode.Class1Id = c1Id;
            oCode.Class2Id = c2Id;
            oCode.Class3Id = c3Id;
            oCode.Class4Id = c4Id;
            oCode.Class5Id = c5Id;
            oCode.Class6Id = c6Id;
            oCode.Save();
        }

        private void SaveProductSupplement(ProductBatch oBatch, Guid productId)
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductSupplement oProdSupp = ProductSupplement.LoadWhere(sql);
            if (oProdSupp == null)
            {
                oProdSupp = new ProductSupplement();

                oProdSupp.ProductId = productId;
            }
            oProdSupp.VendorCurrencyCode = oBatch.VCURR;
            oProdSupp.VendorPrice = oBatch.VPRC;
            oProdSupp.ProductName_Memo = oBatch.DESC_MEMO;
            oProdSupp.ProductName_Pole = oBatch.DESC_POLE;

            oProdSupp.Save();
        }

        private void SaveProductPrice(ProductBatch oBatch, Guid productId)
        {
            SaveProductPrice(productId, Common.Enums.ProductPriceType.BASPRC.ToString(), "HKD", oBatch.BASPRC.ToString());
            SaveProductPrice(productId, Common.Enums.ProductPriceType.ORIPRC.ToString(), "HKD", oBatch.ORIPRC.ToString());
            SaveProductPrice(productId, Common.Enums.ProductPriceType.VPRC.ToString(), oBatch.VCURR, oBatch.VPRC.ToString());
            SaveProductPrice(productId, Common.Enums.ProductPriceType.WHLPRC.ToString(), "HKD", oBatch.WHLPRC.ToString());
        }

        private void SaveProductPrice(Guid productId, string priceType, string currencyCode, string price)
        {
            string sql = "ProductId = '" + productId.ToString() + "' AND PriceTypeId = '" + GetPriceType(priceType).ToString() + "'";
            ProductPrice oPrice = ProductPrice.LoadWhere(sql);
            if (oPrice == null)
            {
                oPrice = new ProductPrice();

                oPrice.ProductId = productId;
            }
            oPrice.PriceTypeId = GetPriceType(priceType);
            oPrice.CurrencyCode = currencyCode;
            oPrice.Price = Convert.ToDecimal((price == string.Empty) ? "0" : price);
            oPrice.Save();
        }

        private Guid GetPriceType(string priceType)
        {
            string sql = "PriceType = '" + priceType + "'";
            ProductPriceType oType = ProductPriceType.LoadWhere(sql);
            if (oType == null)
            {
                oType = new ProductPriceType();

                oType.PriceType = priceType;
                oType.CurrencyCode = "HKD";
                oType.CoreSystemPrice = false;

                oType.Save();
            }
            return oType.PriceTypeId;
        }

        private void SaveProductRemarks(ProductBatch oBatch, Guid productId)
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductRemarks oRemarks = ProductRemarks.LoadWhere(sql);
            if (oRemarks == null)
            {
                oRemarks = new ProductRemarks();

                oRemarks.ProductId = productId;
            }
            oRemarks.Photo = oBatch.PHOTO;

            oRemarks.BinX = oBatch.BINX;
            oRemarks.BinY = oBatch.BINY;
            oRemarks.BinZ = oBatch.BINZ;

            oRemarks.DownloadToShop = (oBatch.RETAILITEM == "F") ? false : true;
            oRemarks.OffDisplayItem = (oBatch.OFF_DISPLAY_ITEM == "F") ? false : true;
            oRemarks.DownloadToCounter = (oBatch.COUNTER_ITEM == "F") ? false : true;

            oRemarks.REMARK1 = oBatch.REMARK1;
            oRemarks.REMARK2 = oBatch.REMARK2;
            oRemarks.REMARK3 = oBatch.REMARK3;
            oRemarks.REMARK4 = oBatch.REMARK4;
            oRemarks.REMARK5 = oBatch.REMARK5;
            oRemarks.REMARK6 = oBatch.REMARK6;

            oRemarks.Save();
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (VerifyDate())
            {
                BindingBatchList();
            }
        }

        private void chkSortAndFilter_Click(object sender, EventArgs e)
        {
            cboSortColumn.Enabled = chkSortAndFilter.Checked;
            cboSortColumn.SelectedIndex = 0;

            cboOrdering.Enabled = chkSortAndFilter.Checked;
            cboOrdering.SelectedIndex = 0;

            cboOperator.Enabled = chkSortAndFilter.Checked;
            cboOperator.SelectedIndex = 0;

            txtData.Enabled = chkSortAndFilter.Checked;
            btnReload.Enabled = chkSortAndFilter.Checked;
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            Link.Open(new Gizmox.WebGUI.Common.Gateways.GatewayReference(this, "open"));
        }

        private void btnMarkAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem objItem in this.lvHoldBatchList.Items)
            {
                if (btnMarkAll.Text.Contains("Mark") && !objItem.SubItems[1].Text.Contains("*"))
                {
                    objItem.SubItems[1].Text = "*";
                }
                else if (btnMarkAll.Text.Contains("Unmark"))
                {
                    objItem.SubItems[1].Text = string.Empty;
                }
            }
            this.Update();

            btnMarkAll.Text = (btnMarkAll.Text == "Mark All") ? "Unmark All" : "Mark All";
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (SelectedColumnsCounting() > 0)
            {
                int result = CreateProducts();
                if (result > 0)
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<DefaultList>();
                    MessageBox.Show(result.ToString() + " succeed!", "Posting result", MessageBoxButtons.OK, new EventHandler(PostingMessageHandler));
                }
                else
                {
                    MessageBox.Show(result.ToString() + " succeed!", "Posting result", MessageBoxButtons.OK, MessageBoxIcon.Warning, new EventHandler(PostingMessageHandler));
                }
            }
            else
            {
                MessageBox.Show("No Record Selected!");
            }
        }

        private void PostingMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.OK)
            {
                BindingBatchList();
            }
        }

        private void lvHoldBatchList_Click(object sender, EventArgs e)
        {
            if (lvHoldBatchList.Items != null && lvHoldBatchList.SelectedIndex >= 0)
            {
                int index = lvHoldBatchList.SelectedIndex;
                this.lvHoldBatchList.Items[index].SubItems[1].Text = (this.lvHoldBatchList.Items[index].SubItems[1].Text.Length == 0) ? "*" : string.Empty;
                //this.Update();
            }
        }
    }
}