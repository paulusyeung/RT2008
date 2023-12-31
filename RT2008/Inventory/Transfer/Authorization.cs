using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using RT2008.DAL;
using Gizmox.WebGUI.Common.Resources;
using System.Configuration;

namespace RT2008.Inventory.Transfer
{
    public partial class Authorization : Form
    {
        private String _NewEraBeginsOn = "2013-11-30 00:00:00";     //在這天之前的 Transactions 不會顯示
        private DataTable _ErrorPool;                               //存放有問題的 Tx 和相關的 Error Message
        private RT2008.Controls.InvtUtility.PostingStatus _PostStatus = RT2008.Controls.InvtUtility.PostingStatus.Ready;

        public Authorization()
        {
            InitializeComponent();
            InitComboBox();

            BindingList(Common.Enums.Status.Draft);
            BindingList(Common.Enums.Status.Active);
        }

        #region Init
        private void InitComboBox()
        {
            txtPostedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(DateTime.Now, true);
            txtSysMonth.Text = RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemMonth;
            txtSysYear.Text = RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemYear;

            cboFieldName.SelectedIndex = 0;
            cboOperator.SelectedIndex = 0;
            cboOrdering.SelectedIndex = 0;
        }
        #endregion

        #region Bind Methods
        private SqlDataReader DataSource(string status, bool conditions)
        {
            string sql = BuildSqlQueryString(status, conditions);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = System.Data.CommandType.Text;

            return SqlHelper.Default.ExecuteReader(cmd);
        }

        private void BindingList(Common.Enums.Status status)
        {
            SqlDataReader reader;
            switch (status)
            {
                case Common.Enums.Status.Draft: // Holding
                    reader = DataSource(Common.Enums.Status.Draft.ToString("d"), false);
                    BindingHoldingList(reader);
                    break;
                case Common.Enums.Status.Active: // Posting
                    reader = DataSource(Common.Enums.Status.Active.ToString("d"), true);
                    BindingPostingList(reader);
                    break;
            }
        }

        private void BindingHoldingList(SqlDataReader reader)
        {
            lvHoldingTxList.Items.Clear();

            int iCount = 1;

            while (reader.Read())
            {
                ListViewItem objItem = this.lvHoldingTxList.Items.Add(reader.GetGuid(0).ToString()); // TxId
                objItem.SubItems.Add(string.Empty);
                objItem.SubItems.Add(iCount.ToString()); // Line Number
                objItem.SubItems.Add(reader.GetString(2)); // TxNumber
                objItem.SubItems.Add(reader.GetString(1)); // Type
                objItem.SubItems.Add(reader.GetString(4)); // From Location
                objItem.SubItems.Add(reader.GetString(5)); // To Location
                objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(3), false)); // TxDate
                objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(8), false)); // CreatedOn

                iCount++;
            }
            reader.Close();
        }

        private void BindingPostingList(SqlDataReader reader)
        {
            lvPostTxList.Items.Clear();

            int iCount = 1;

            while (reader.Read())
            {
                ListViewItem objItem = this.lvPostTxList.Items.Add(reader.GetGuid(0).ToString()); // TxId
                objItem.SubItems.Add(new IconResourceHandle("16x16.16_progress.gif").ToString());
                objItem.SubItems.Add(iCount.ToString()); // Line Number
                objItem.SubItems.Add(reader.GetString(2)); // TxNumber
                objItem.SubItems.Add(reader.GetString(1)); // Type
                objItem.SubItems.Add(reader.GetString(4)); // From Location
                objItem.SubItems.Add(reader.GetString(5)); // To Location
                objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(3), false)); // TxDate
                objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(8), false)); // CreatedOn
                objItem.BackColor = CheckTxDate(reader.GetDateTime(3)) ? Color.Transparent : RT2008.SystemInfo.ControlBackColor.DisabledBox;

                iCount++;
            }
            reader.Close();
        }

        private string BuildSqlQueryString(string status, bool withConditions)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT HeaderId, TxType, TxNumber, TxDate, FromLocation, ToLocation, ");
            sql.Append(" CreatedOn, CreatedBy, ModifiedOn, ModifiedBy ");
            sql.Append(" FROM vwDraftTxferList ");
            sql.Append(" WHERE TxDate >= '").Append(_NewEraBeginsOn).Append("' AND STATUS = ").Append(status).Append(" AND ReadOnly = 0");
            sql.Append(" AND TxType = '").Append(Common.Enums.TxType.TXF.ToString()).Append("'");

            if (txtTxNumber.Text.Trim().Length > 0)
            {
                sql.Append(" AND TxNumber LIKE '%").Append(txtTxNumber.Text).Append("%'");
            }
            else if (chkSortAndFilter.Checked && withConditions)
            {
                if (cboFieldName.Text.Length > 0 && cboOperator.Text != "None")
                {
                    sql.Append(" AND ");
                    sql.Append(ColumnName()).Append(" ");

                    if (cboFieldName.Text.Contains("Date"))
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
                }

                sql.Append(" ORDER BY ");
                sql.Append(ColumnName());
                sql.Append((cboOrdering.Text == "Ascending") ? " ASC" : " DESC");
            }

            return sql.ToString();
        }

        private string ColumnName()
        {
            string colName = string.Empty;
            switch (cboFieldName.Text)
            {
                case "Type":
                    colName = "TxType";
                    break;
                case "From Loc#":
                    colName = "FromLocation";
                    break;
                case "To Loc#":
                    colName = "ToLocation";
                    break;
                case "Tx Date":
                    colName = "TxDate";
                    break;
                case "Last Update(dd/MM/yyyy)":
                    colName = "ModifiedOn";
                    break;
                case "Last Update":
                    colName = "ModifiedBy";
                    break;
                default:
                case "Tx#":
                    colName = "TxNumber";
                    break;
            }
            return colName;
        }

        private bool VerifyDate()
        {
            bool isVerified = true;
            if (cboFieldName.Text.Contains("Date") && cboOperator.Text != "None")
            {
                string pattern = @"^\d{1,2}\/\d{1,2}\/\d{4}$";
                Regex rex = new Regex(pattern);
                Match match = rex.Match(txtData.Text);
                if (!match.Success)
                {
                    errorProvider.SetError(lblData, "Incorrect Date Format! (Date Format:dd/MM/yyyy)");
                    isVerified = false;
                }
                else
                {
                    errorProvider.SetError(lblData, string.Empty);
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

        #region Verify

        private DataTable InitErrorPool()
        {
            DataTable oTable = new DataTable();

            oTable.Columns.Add("HeaderId", typeof(string));
            oTable.Columns.Add("TxNumber", typeof(string));
            oTable.Columns.Add("STKCODE", typeof(string));
            oTable.Columns.Add("APPENDIX1", typeof(string));
            oTable.Columns.Add("APPENDIX2", typeof(string));
            oTable.Columns.Add("APPENDIX3", typeof(string));
            oTable.Columns.Add("ErrorReason", typeof(string));
            oTable.Columns.Add("PostDate", typeof(DateTime));

            return oTable;
        }

        #region Count 勾選咗嘅 items，同時 flag 有問題的 item，把問題存放喺 _ErrorPool
        private int ValidateSelectedTx()
        {
            int errorCounts = 0;
            _ErrorPool = InitErrorPool();

            foreach (ListViewItem objItem in this.lvPostTxList.Items)
            {
                if (objItem.Checked)
                {
                    if (!IsPostable(objItem.Text))
                    {
                        objItem.SubItems[1].Text = new IconResourceHandle("16x16.16_error.gif").ToString();
                        _PostStatus = RT2008.Controls.InvtUtility.PostingStatus.Error;
                        errorCounts++;
                    }

                    colPostingStatus.Visible = true;
                    this.Update();
                }
                else
                {
                    objItem.SubItems[1].Text = string.Empty;
                }
            }

            if (_PostStatus == RT2008.Controls.InvtUtility.PostingStatus.Ready)
            {
                _PostStatus = RT2008.Controls.InvtUtility.PostingStatus.Postable;
            }

            return errorCounts;
        }

        private bool IsPostable(string headerId)
        {
            bool isPostable = true;

            if (Common.Utility.IsGUID(headerId))
            {
                InvtBatchTXF_Header oBatchHeader = InvtBatchTXF_Header.Load(new Guid(headerId));
                if (oBatchHeader != null)
                {
                    if (!CheckTxDate(oBatchHeader.TxDate))
                    {
                        DataRow row = _ErrorPool.NewRow();
                        row["HeaderId"] = oBatchHeader.HeaderId.ToString();
                        row["TxNumber"] = oBatchHeader.TxNumber;
                        row["STKCODE"] = string.Empty;
                        row["APPENDIX1"] = string.Empty;
                        row["APPENDIX2"] = string.Empty;
                        row["APPENDIX3"] = string.Empty;
                        row["ErrorReason"] = "Transaction date does not belong to current system month.";
                        row["PostDate"] = DateTime.Now;

                        _ErrorPool.Rows.Add(row);

                        isPostable = isPostable & false;
                    }

                    if (oBatchHeader.ReadOnly && oBatchHeader.Status == (int)Common.Enums.Status.Active)
                    {
                        DataRow row = _ErrorPool.NewRow();
                        row["HeaderId"] = oBatchHeader.HeaderId.ToString();
                        row["TxNumber"] = oBatchHeader.TxNumber;
                        row["STKCODE"] = string.Empty;
                        row["APPENDIX1"] = string.Empty;
                        row["APPENDIX2"] = string.Empty;
                        row["APPENDIX3"] = string.Empty;
                        row["ErrorReason"] = "Transaction already had been posted! Cannot post again!";
                        row["PostDate"] = DateTime.Now;

                        _ErrorPool.Rows.Add(row);

                        isPostable = isPostable & false;
                    }

                    InvtBatchTXF_DetailsCollection detailList = InvtBatchTXF_Details.LoadCollection("HeaderId = '" + oBatchHeader.HeaderId.ToString() + "'");
                    foreach (InvtBatchTXF_Details detail in detailList)
                    {
                        bool retired = false;
                        string stk = string.Empty, a1 = string.Empty, a2 = string.Empty, a3 = string.Empty;

                        DAL.Product oProduct = DAL.Product.Load(detail.ProductId);
                        if (oProduct != null)
                        {
                            stk = oProduct.STKCODE;
                            a1 = oProduct.APPENDIX1;
                            a2 = oProduct.APPENDIX2;
                            a3 = oProduct.APPENDIX3;
                            retired = oProduct.Retired;
                        }

                        if (retired)
                        {
                            DataRow row = _ErrorPool.NewRow();
                            row["HeaderId"] = oBatchHeader.HeaderId.ToString();
                            row["TxNumber"] = oBatchHeader.TxNumber;
                            row["STKCODE"] = stk;
                            row["APPENDIX1"] = a1;
                            row["APPENDIX2"] = a2;
                            row["APPENDIX3"] = a3;
                            row["ErrorReason"] = "Product does not exist or has been removed!";
                            row["PostDate"] = DateTime.Now;

                            _ErrorPool.Rows.Add(row);

                            isPostable = isPostable & false;
                        }

                        if (chkNegative.Checked)
                        {
                            decimal cdqty = GetCDQty(detail.ProductId, oBatchHeader.FromLocation);

                            if ((cdqty - detail.QtyRequested) < 0)
                            {
                                DataRow row = _ErrorPool.NewRow();
                                row["HeaderId"] = oBatchHeader.HeaderId.ToString();
                                row["TxNumber"] = oBatchHeader.TxNumber;
                                row["STKCODE"] = stk;
                                row["APPENDIX1"] = a1;
                                row["APPENDIX2"] = a2;
                                row["APPENDIX3"] = a3;
                                row["ErrorReason"] = "Product does not have enough on-hand qty!";
                                row["PostDate"] = DateTime.Now;

                                _ErrorPool.Rows.Add(row);

                                isPostable = isPostable & false;
                            }
                        }
                    }

                    RT2008.DAL.Staff oStaff = RT2008.DAL.Staff.Load(oBatchHeader.StaffId);
                    if (oStaff != null)
                    {
                        if (oStaff.Retired)
                        {
                            DataRow row = _ErrorPool.NewRow();
                            row["HeaderId"] = oBatchHeader.HeaderId.ToString();
                            row["TxNumber"] = oBatchHeader.TxNumber;
                            row["STKCODE"] = string.Empty;
                            row["APPENDIX1"] = string.Empty;
                            row["APPENDIX2"] = string.Empty;
                            row["APPENDIX3"] = string.Empty;
                            row["ErrorReason"] = "Staff has been removed!";
                            row["PostDate"] = DateTime.Now;

                            _ErrorPool.Rows.Add(row);

                            isPostable = isPostable & false;
                        }
                    }

                    InvtLedgerHeader oInvtLedger = InvtLedgerHeader.LoadWhere("TxNumber = '" + oBatchHeader.TxNumber + "' AND (TxType = 'TXI' OR TxType = 'TXI')");
                    if (oInvtLedger != null)
                    {
                        DataRow row = _ErrorPool.NewRow();
                        row["HeaderId"] = oBatchHeader.HeaderId.ToString();
                        row["TxNumber"] = oBatchHeader.TxNumber;
                        row["STKCODE"] = string.Empty;
                        row["APPENDIX1"] = string.Empty;
                        row["APPENDIX2"] = string.Empty;
                        row["APPENDIX3"] = string.Empty;
                        row["ErrorReason"] = "Transaction existed in Inventory Ledger!";
                        row["PostDate"] = DateTime.Now;

                        _ErrorPool.Rows.Add(row);

                        isPostable = isPostable & false;
                    }

                    string message = string.Empty;
                    RT2008.DAL.Workplace fromLoc = RT2008.DAL.Workplace.Load(oBatchHeader.FromLocation);
                    if (fromLoc == null)
                    {
                        message = "Transfer From location does existe!";
                    }
                    else if (fromLoc.Retired)
                    {
                        message = "Transfer From location has been removed!";
                    }

                    if (message.Length > 0)
                    {
                        DataRow row = _ErrorPool.NewRow();
                        row["HeaderId"] = oBatchHeader.HeaderId.ToString();
                        row["TxNumber"] = oBatchHeader.TxNumber;
                        row["STKCODE"] = string.Empty;
                        row["APPENDIX1"] = string.Empty;
                        row["APPENDIX2"] = string.Empty;
                        row["APPENDIX3"] = string.Empty;
                        row["ErrorReason"] = message;
                        row["PostDate"] = DateTime.Now;

                        _ErrorPool.Rows.Add(row);

                        isPostable = isPostable & false;
                    }

                    message = string.Empty;
                    RT2008.DAL.Workplace toLoc = RT2008.DAL.Workplace.Load(oBatchHeader.ToLocation);
                    if (toLoc == null)
                    {
                        message = "Transfer To location does existe!";
                    }
                    else if (toLoc.Retired)
                    {
                        message = "Transfer To location has been removed!";
                    }

                    if (message.Length > 0)
                    {
                        DataRow row = _ErrorPool.NewRow();
                        row["HeaderId"] = oBatchHeader.HeaderId.ToString();
                        row["TxNumber"] = oBatchHeader.TxNumber;
                        row["STKCODE"] = string.Empty;
                        row["APPENDIX1"] = string.Empty;
                        row["APPENDIX2"] = string.Empty;
                        row["APPENDIX3"] = string.Empty;
                        row["ErrorReason"] = message;
                        row["PostDate"] = DateTime.Now;

                        _ErrorPool.Rows.Add(row);

                        isPostable = isPostable & false;
                    }
                }
                else
                {
                    DataRow row = _ErrorPool.NewRow();
                    row["HeaderId"] = oBatchHeader.HeaderId.ToString();
                    row["TxNumber"] = oBatchHeader.TxNumber;
                    row["STKCODE"] = string.Empty;
                    row["APPENDIX1"] = string.Empty;
                    row["APPENDIX2"] = string.Empty;
                    row["APPENDIX3"] = string.Empty;
                    row["ErrorReason"] = "Transaction does not existe!";
                    row["PostDate"] = DateTime.Now;

                    _ErrorPool.Rows.Add(row);

                    return false;
                }
            }

            return isPostable;
        }
        #endregion

        private decimal GetCDQty(Guid productId, Guid workplaceId)
        {
            decimal cdQty = 0;
            string sql = "ProductId = '" + productId.ToString() + "' AND WorkplaceId = '" + workplaceId.ToString() + "'";
            ProductWorkplace wpProd = ProductWorkplace.LoadWhere(sql);
            if (wpProd != null)
            {
                cdQty = wpProd.CDQTY;
            }
            return cdQty;
        }

        private bool CheckTxDate(DateTime txDate)
        {
            bool isChecked = false;

            isChecked = (txDate.Year.ToString() == RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemYear);
            isChecked = isChecked & (txDate.Month.ToString().PadLeft(2, '0') == RT2008.SystemInfo.CurrentInfo.Default.CurrentSystemMonth);

            return isChecked;
        }

        private bool FoundInErrorPool(string headerId)
        {
            if (_ErrorPool == null)
            {
                return false;
            }

            DataRow[] rows = _ErrorPool.Select("HeaderId = '" + headerId + "'");
            return rows.Length > 0;
        }

        #endregion

        #region Posting Batch

        private int PostSelectedTx()
        {
            int iCount = 0;
            if (lvPostTxList.Items.Count > 0)
            {
                foreach (ListViewItem oItem in lvPostTxList.CheckedItems)
                {
                    if (Common.Utility.IsGUID(oItem.Text) && oItem.Checked)
                    {
                        if (!(FoundInErrorPool(oItem.Text)))
                        {
                            if (PostThisTx(new Guid(oItem.Text)))
                            {
                                oItem.SubItems[1].Text = new IconResourceHandle("16x16.16_succeeded.png").ToString();
                                ++iCount;
                            }
                        }
                    }
                }
            }
            return iCount;
        }

        private bool PostThisTx(Guid headerId)
        {
            bool result = false;

            InvtBatchTXF_Header oBatchHeader = InvtBatchTXF_Header.Load(headerId);
            if (oBatchHeader != null)
            {
                // Update Product Info
                UpdateProduct(oBatchHeader);

                // Create TXF SubLedger
                string txNumber_SubLedger = oBatchHeader.TxNumber;
                System.Guid subLedgerHeaderId = CreateTXFSubLedgerHeader(txNumber_SubLedger, oBatchHeader);
                CreateTXFSubLedgerDetail(txNumber_SubLedger, oBatchHeader.HeaderId, subLedgerHeaderId);

                // Create Ledger
                CreateLedger(subLedgerHeaderId, oBatchHeader, txNumber_SubLedger);

                // Update Batch Header Info
                oBatchHeader.PostedOn = DateTime.Now;
                oBatchHeader.PostedBy = Common.Config.CurrentUserId;
                oBatchHeader.POSTNEG = true;
                oBatchHeader.ReadOnly = true;
                oBatchHeader.ModifiedBy = Common.Config.CurrentUserId;
                oBatchHeader.ModifiedOn = DateTime.Now;
                oBatchHeader.Save();

                result = true;

                ClearBatchTransaction(oBatchHeader);
            }
            return result;
        }

        #region Clear Batch

        /// <summary>
        /// Clears the batch transaction.
        /// </summary>
        private void ClearBatchTransaction(InvtBatchTXF_Header oBatchHeader)
        {
            string query = "HeaderId = '" + oBatchHeader.HeaderId.ToString() + "'";
            InvtBatchTXF_DetailsCollection detailList = InvtBatchTXF_Details.LoadCollection(query);
            for (int i = 0; i < detailList.Count; i++)
            {
                detailList[i].Delete();
            }

            oBatchHeader.Delete();
        }

        #endregion

        #region SubLedger
        private Guid CreateTXFSubLedgerHeader(string txnumber, InvtBatchTXF_Header oBatchHeader)
        {
            InvtSubLedgerTXF_Header oSubTXF = new InvtSubLedgerTXF_Header();
            oSubTXF.TxNumber = txnumber;
            oSubTXF.TxType = oBatchHeader.TxType;
            oSubTXF.TxDate = oBatchHeader.TxDate;
            oSubTXF.StaffId = oBatchHeader.StaffId;
            oSubTXF.FromLocation = oBatchHeader.FromLocation;
            oSubTXF.ToLocation = oBatchHeader.ToLocation;
            oSubTXF.TransferredOn = oBatchHeader.TransferredOn;
            oSubTXF.CompletedOn = oBatchHeader.CompletedOn;
            oSubTXF.Reference = oBatchHeader.Reference + "\t" + oBatchHeader.TxNumber;
            oSubTXF.DeliveryNoteRef = oBatchHeader.DeliveryNoteRef;
            oSubTXF.Remarks = oBatchHeader.Remarks;
            oSubTXF.PostedOn = DateTime.Now;
            oSubTXF.PostedBy = Common.Config.CurrentUserId;
            oSubTXF.POSTNEG = !chkNegative.Checked;
            oSubTXF.ReadOnly = true;
            oSubTXF.PickingNoteRef = oBatchHeader.PickingNoteRef;
            oSubTXF.Picked = oBatchHeader.Picked;
            oSubTXF.Status = (int)Common.Enums.Status.Active;
            oSubTXF.CONFIRM_TRF = oBatchHeader.CONFIRM_TRF;
            oSubTXF.CONFIRM_TRF_LASTUPDATE = oBatchHeader.CONFIRM_TRF_LASTUPDATE;
            oSubTXF.CONFIRM_TRF_LASTUSER = oBatchHeader.CONFIRM_TRF_LASTUSER;

            oSubTXF.CreatedBy = Common.Config.CurrentUserId;
            oSubTXF.CreatedOn = DateTime.Now;
            oSubTXF.ModifiedBy = Common.Config.CurrentUserId;
            oSubTXF.ModifiedOn = DateTime.Now;

            oSubTXF.Save();

            return oSubTXF.HeaderId;
        }

        private void CreateTXFSubLedgerDetail(string txnumber, Guid batchHeaderId, Guid subledgerHeaderId)
        {
            string sql = "HeaderId = '" + batchHeaderId.ToString() + "'";
            string[] orderBy = new string[] { "LineNumber" };
            InvtBatchTXF_DetailsCollection oBatchDetails = InvtBatchTXF_Details.LoadCollection(sql, orderBy, true);
            foreach (InvtBatchTXF_Details oBDetail in oBatchDetails)
            {
                InvtSubLedgerTXF_Details oSubLedgerDetail = new InvtSubLedgerTXF_Details();
                oSubLedgerDetail.HeaderId = subledgerHeaderId;
                oSubLedgerDetail.TxType = oBDetail.TxType;
                oSubLedgerDetail.TxNumber = txnumber;
                oSubLedgerDetail.LineNumber = oBDetail.LineNumber;
                oSubLedgerDetail.ProductId = oBDetail.ProductId;
                oSubLedgerDetail.QtyRequested = oBDetail.QtyRequested;
                oSubLedgerDetail.QtyReceived = oBDetail.QtyReceived;
                oSubLedgerDetail.QtyManualInput = oBDetail.QtyManualInput;
                oSubLedgerDetail.QtyHHT = oBDetail.QtyHHT;
                oSubLedgerDetail.QtyConfirmed = oBDetail.QtyConfirmed;
                oSubLedgerDetail.Remarks = oBDetail.Remarks;

                oSubLedgerDetail.Save();
            }

            InvtSubLedgerTXF_Header oSubLedgerHeader = InvtSubLedgerTXF_Header.Load(subledgerHeaderId);
            if (oSubLedgerHeader != null)
            {
                oSubLedgerHeader.ModifiedOn = DateTime.Now;
                oSubLedgerHeader.ModifiedBy = Common.Config.CurrentUserId;

                oSubLedgerHeader.Save();
            }
        }
        #endregion

        #region Ledger
        private void CreateLedger(Guid subLedgerHeaderId, InvtBatchTXF_Header oBatchHeader, string subLedgerTxNumber)
        {
            // Transfer In
            System.Guid ledgerHeaderId_In = CreateLedgerHeader(subLedgerHeaderId, Common.Enums.TxType.TXI.ToString(), oBatchHeader.ToLocation, oBatchHeader.FromLocation, oBatchHeader.StaffId, oBatchHeader.Reference + "\t" + oBatchHeader.TxNumber, oBatchHeader.Remarks, oBatchHeader);
            CreateLedgerDetails(oBatchHeader, Common.Enums.TxType.TXI.ToString(), subLedgerHeaderId, ledgerHeaderId_In, 1, this.GetWorkplaceCode(oBatchHeader.FromLocation), this.GetStaffCode(oBatchHeader.StaffId));

            // Transfer Out
            System.Guid ledgerHeaderId_Out = CreateLedgerHeader(subLedgerHeaderId, Common.Enums.TxType.TXO.ToString(), oBatchHeader.FromLocation, oBatchHeader.ToLocation, oBatchHeader.StaffId, oBatchHeader.Reference + "\t" + oBatchHeader.TxNumber, oBatchHeader.Remarks, oBatchHeader);
            CreateLedgerDetails(oBatchHeader, Common.Enums.TxType.TXO.ToString(), subLedgerHeaderId, ledgerHeaderId_Out, 1, this.GetWorkplaceCode(oBatchHeader.ToLocation), this.GetStaffCode(oBatchHeader.StaffId));

            // Update LedgerHeader
            UpdateVsLedgerHeader(ledgerHeaderId_In, ledgerHeaderId_Out);
        }

        private Guid CreateLedgerHeader(Guid subLedgerHeaderId, string txType, Guid workplaceId, Guid vsLocationId, Guid staffId, string reference, string remarks, InvtBatchTXF_Header oBatchHeader)
        {
            InvtLedgerHeader oLedgerHeader = new InvtLedgerHeader();
            oLedgerHeader.HeaderId = System.Guid.NewGuid();
            oLedgerHeader.TxNumber = oBatchHeader.TxNumber;
            oLedgerHeader.TxType = txType;
            oLedgerHeader.TxDate = oBatchHeader.TxDate;
            oLedgerHeader.SubLedgerHeaderId = subLedgerHeaderId;
            oLedgerHeader.WorkplaceId = workplaceId;
            oLedgerHeader.VsLocationId = vsLocationId;
            oLedgerHeader.StaffId = staffId;
            oLedgerHeader.Staff1 = Common.Config.CurrentUserId;
            oLedgerHeader.Reference = reference;
            oLedgerHeader.Remarks = remarks;
            oLedgerHeader.Status = (int)Common.Enums.Status.Active;
            oLedgerHeader.CreatedBy = Common.Config.CurrentUserId;
            oLedgerHeader.CreatedOn = DateTime.Now;
            oLedgerHeader.ModifiedBy = Common.Config.CurrentUserId;
            oLedgerHeader.ModifiedOn = DateTime.Now;
            oLedgerHeader.CONFIRM_TRF = oBatchHeader.CONFIRM_TRF;
            oLedgerHeader.CONFIRM_TRF_LASTUPDATE = oBatchHeader.CONFIRM_TRF_LASTUPDATE;
            oLedgerHeader.CONFIRM_TRF_LASTUSER = this.GetStaffCode(oBatchHeader.CONFIRM_TRF_LASTUSER);
            oLedgerHeader.Save();

            return oLedgerHeader.HeaderId;
        }

        private void CreateLedgerDetails(InvtBatchTXF_Header oBatchHeader, string txType, Guid subledgerHeaderId, Guid ledgerHeaderId, decimal Direction, string shop, string staffNumber)
        {
            string sql = "HeaderId = '" + subledgerHeaderId.ToString() + "'";
            string[] orderBy = new string[] { "LineNumber" };
            InvtSubLedgerTXF_DetailsCollection oSubLedgerDetails = InvtSubLedgerTXF_Details.LoadCollection(sql, orderBy, true);
            foreach (InvtSubLedgerTXF_Details oSDetail in oSubLedgerDetails)
            {
                InvtLedgerDetails oLedgerDetail = new InvtLedgerDetails();
                oLedgerDetail.HeaderId = ledgerHeaderId;
                oLedgerDetail.SubLedgerDetailsId = oSDetail.DetailsId;
                oLedgerDetail.LineNumber = oSDetail.LineNumber;
                oLedgerDetail.ProductId = oSDetail.ProductId;
                oLedgerDetail.Qty = oSDetail.QtyRequested * Direction;
                oLedgerDetail.TxNumber = oBatchHeader.TxNumber;
                oLedgerDetail.TxType = txType;
                oLedgerDetail.TxDate = oBatchHeader.TxDate;
                oLedgerDetail.UnitAmount = 0;
                oLedgerDetail.Amount = 0;
                oLedgerDetail.AverageCost = 0;
                oLedgerDetail.Notes = string.Empty;
                oLedgerDetail.SerialNumber = string.Empty;
                oLedgerDetail.SHOP = shop;
                oLedgerDetail.OPERATOR = staffNumber;
                oLedgerDetail.CONFIRM_TRF_QTY = oSDetail.QtyConfirmed;

                // Product Info
                RT2008.DAL.Product oItem = RT2008.DAL.Product.Load(oSDetail.ProductId);
                if (oItem != null)
                {
                    oLedgerDetail.BasicPrice = oItem.RetailPrice;
                    oLedgerDetail.Discount = oItem.NormalDiscount;
                    oLedgerDetail.AverageCost = this.GetAverageCost(oItem.ProductId);

                    sql = "ProductId = '" + oSDetail.ProductId.ToString() + "' AND PriceTypeId = '" + GetPriceType(Common.Enums.ProductPriceType.VPRC.ToString()).ToString() + "'";
                    ProductPrice oPrice = ProductPrice.LoadWhere(sql);
                    if (oPrice != null)
                    {
                        oLedgerDetail.VendorRef = oPrice.CurrencyCode;
                    }
                }

                oLedgerDetail.Save();

                InvtLedgerHeader oLedgerHeader = InvtLedgerHeader.Load(ledgerHeaderId);
                if (oLedgerHeader != null)
                {
                    oLedgerHeader.TotalAmount += oLedgerDetail.Amount;

                    oLedgerHeader.Save();
                }
            }
        }

        private void UpdateVsLedgerHeader(Guid ledgerHeaderId, Guid vsLedgerHeaderId)
        {
            InvtLedgerHeader ledgerHeader = InvtLedgerHeader.Load(ledgerHeaderId);
            if (ledgerHeader != null)
            {
                ledgerHeader.VsLeddgerHeaderId = vsLedgerHeaderId;
                ledgerHeader.Save();
            }

            InvtLedgerHeader vsLedgerHeader = InvtLedgerHeader.Load(vsLedgerHeaderId);
            if (vsLedgerHeader != null)
            {
                vsLedgerHeader.VsLeddgerHeaderId = ledgerHeaderId;
                vsLedgerHeader.Save();
            }
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

        private string GetWorkplaceCode(Guid workplaceId)
        {
            RT2008.DAL.Workplace oWp = RT2008.DAL.Workplace.Load(workplaceId);
            if (oWp != null)
            {
                return oWp.WorkplaceCode;
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetStaffCode(Guid staffId)
        {
            RT2008.DAL.Staff oStaff = RT2008.DAL.Staff.Load(staffId);
            if (oStaff != null)
            {
                return oStaff.StaffNumber;
            }

            return string.Empty;
        }

        private decimal GetAverageCost(Guid productId)
        {
            string sql = "ProductId = '" + productId.ToString() + "'";
            ProductCurrentSummary oSummary = ProductCurrentSummary.LoadWhere(sql);
            if (oSummary != null)
            {
                return oSummary.AverageCost;
            }

            return 0;
        }
        #endregion

        #region Product
        private void UpdateProduct(InvtBatchTXF_Header oBatchHeader)
        {
            string sql = "HeaderId = '" + oBatchHeader.HeaderId.ToString() + "'";
            InvtBatchTXF_DetailsCollection detailsList = InvtBatchTXF_Details.LoadCollection(sql);
            for (int i = 0; i < detailsList.Count; i++)
            {
                InvtBatchTXF_Details detail = detailsList[i];
                //Out
                UpdateProductQty(detail.ProductId, oBatchHeader.FromLocation, detail.QtyRequested * (-1));
                //In
                UpdateProductQty(detail.ProductId, oBatchHeader.ToLocation, detail.QtyRequested);
            }
        }

        private void UpdateProductQty(Guid productId, Guid workplaceId, decimal qty)
        {
            string sql = "ProductId = '" + productId.ToString() + "' AND WorkplaceId = '" + workplaceId.ToString() + "'";
            ProductWorkplace wpProd = ProductWorkplace.LoadWhere(sql);
            if (wpProd == null)
            {
                wpProd = new ProductWorkplace();
                wpProd.ProductId = productId;
                wpProd.WorkplaceId = workplaceId;
            }

            wpProd.CDQTY += qty;

            wpProd.Save();
        }
        #endregion

        #endregion

        #region Finding TxNumber
        private void FindingTxNumber()
        {
            errorProvider.SetError(txtTxNumber, string.Empty);
            if (txtTxNumber.Text.Trim().Length > 0)
            {
                string sql = "TxNumber LIKE '%" + txtTxNumber.Text.Trim() + "%'";
                InvtBatchTXF_Header oHeader = InvtBatchTXF_Header.LoadWhere(sql);
                if (oHeader != null)
                {
                    Common.Enums.Status oStatus = (Common.Enums.Status)Enum.Parse(typeof(Common.Enums.Status), oHeader.Status.ToString());
                    switch (oStatus)
                    {
                        case Common.Enums.Status.Draft: // Holding
                            tabTxfAuthorization.SelectedIndex = 1;
                            break;
                        case Common.Enums.Status.Active: // Posting
                            tabTxfAuthorization.SelectedIndex = 0;
                            break;
                    }

                    BindingList(oStatus);
                }
                else
                {
                    errorProvider.SetError(txtTxNumber, "Transfer Number field does not exist!");
                }
            }
            else
            {
                errorProvider.SetError(txtTxNumber, "Transfer Number cannot be blank!");
            }
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSortAndFilter_CheckedChanged(object sender, EventArgs e)
        {
            cboFieldName.Enabled = chkSortAndFilter.Checked;
            cboOperator.Enabled = chkSortAndFilter.Checked;
            cboOrdering.Enabled = chkSortAndFilter.Checked;
            txtData.Enabled = chkSortAndFilter.Checked;
            btnReload.Enabled = chkSortAndFilter.Checked;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (VerifyDate())
            {
                errorProvider.SetError(txtTxNumber, string.Empty);

                BindingList(Common.Enums.Status.Active);

                if (this.lvPostTxList.Items.Count == 0)
                {
                    MessageBox.Show("No record found!", "ATTENTION");
                }
            }
        }

        private void btnMarkAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem objItem in this.lvPostTxList.Items)
            {
                if (btnMarkAll.Text.Contains("Mark"))
                {
                    objItem.Checked = true;
                }
                else if (btnMarkAll.Text.Contains("Unmark"))
                {
                    objItem.Checked = false;
                }
            }
            this.Update();

            btnMarkAll.Text = (btnMarkAll.Text == "Mark All") ? "Unmark All" : "Mark All";
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (lvPostTxList.CheckedItems.Count > 0)
            {
                int errorCount = ValidateSelectedTx();

                if (errorCount == 0)
                {
                    btnMarkAll.Enabled = false;

                    int result = PostSelectedTx();
                    if (result > 0)
                    {
                        MessageBox.Show(String.Format("Number of record(s) posted = {0}", result.ToString()), "Posting result", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    string errorDetails = string.Empty;
                    if (_ErrorPool != null)
                    {
                        foreach (DataRow errorRow in _ErrorPool.Rows)
                        {
                            errorDetails += errorRow["ErrorReason"].ToString() + " : \n";
                            errorDetails += errorRow["TxNumber"].ToString() + ": " + errorRow["STKCODE"].ToString() + " " + errorRow["APPENDIX1"].ToString() + " " + errorRow["APPENDIX2"].ToString() + "\n";
                        }
                    }
                    MessageBox.Show(String.Format("Number of error(s) found = {0}...Job aborted\n{1}", errorCount.ToString(), errorDetails), "Warning");
                }
            }
            else
            {
                MessageBox.Show("No selected record...", "Warning");
            }
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            chkNegative.Visible = !chkNegative.Visible;
        }

        private void txtTxNumber_TextChanged(object sender, EventArgs e)
        {
            FindingTxNumber();
        }

        private void txtTxNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && e.KeyData == Keys.Return)
            {
                FindingTxNumber();
            }
        }

        private void lvPostTxList_DoubleClick(object sender, EventArgs e)
        {
            if (lvPostTxList.Items != null && lvPostTxList.SelectedIndex >= 0)
            {
                int index = lvPostTxList.SelectedIndex;

                if (_PostStatus != RT2008.Controls.InvtUtility.PostingStatus.Ready)
                {
                    string headerId = this.lvPostTxList.Items[index].Text;
                    if (_ErrorPool != null)
                    {
                        DataRow[] rows = _ErrorPool.Select("HeaderId = '" + headerId + "'");
                        if (rows.Length > 0)
                        {
                            RT2008.Controls.PostingMessageForm frmMessage = new RT2008.Controls.PostingMessageForm();
                            frmMessage.RowList = rows;
                            frmMessage.ShowDialog();
                        }
                        else
                        {
                            if (this.lvPostTxList.Items[index].Checked)
                            {
                                if (_PostStatus == RT2008.Controls.InvtUtility.PostingStatus.Postable)
                                {
                                    MessageBox.Show("Transaction is posted!", "Message");
                                }
                                else
                                {
                                    MessageBox.Show("Transaction is ready to be posted.", "Message");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void lvPostTxList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                lvPostTxList.Items[e.Index].Checked = !(lvPostTxList.Items[e.Index].BackColor == RT2008.SystemInfo.ControlBackColor.DisabledBox);
            }
        }
    }
}