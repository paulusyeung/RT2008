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
using System.IO;
using System.Text.RegularExpressions;
using RT2008.Controls;
using RT2008.DAL;
using FileHelpers;

#endregion

namespace RT2008.Inventory.StockTake.Import
{
    public partial class ImportFromPPC : Form
    {
        string mstrDirectory;
        string tempDirectory;
        string backupDirectory;
        string logFile;

        public ImportFromPPC()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetAttributes();
            LoadPacketList();
            WriteLog();
        }

        #region Set Attributes

        private void SetAttributes()
        {
            selectedDataCtrl.VisibleChanged += new EventHandler(selectedDataCtrl_VisibleChanged);

            mstrDirectory = Path.Combine(Context.Config.GetDirectory("Upload"), "StockTake_From_PPC");
            tempDirectory = Path.Combine(mstrDirectory, "Temp");
            backupDirectory = Path.Combine(mstrDirectory, "Backup");
            logFile = Path.Combine(Path.Combine(mstrDirectory, "Log"), "STK1300M_PPC_" + DateTime.Now.ToString("yyyyMMdd") + ".log");

            lblRecordNotFound.BackColor = SystemInfo.ControlBackColor.DisabledBox;
            lblLedgendOfStockTakeNumber.BackColor = SystemInfo.ControlBackColor.RequiredBox;

            txtUploadOn.BackColor = SystemInfo.ControlBackColor.DisabledBox;
            txtHHTId.BackColor = SystemInfo.ControlBackColor.DisabledBox;
            txtHHTTxNumber.BackColor = SystemInfo.ControlBackColor.DisabledBox;
            txtStockTakeNumber.BackColor = SystemInfo.ControlBackColor.DisabledBox;
        }

        private string GetUploadOn(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            return SystemInfo.Settings.DateTimeToString(fileInfo.LastWriteTime, true);
        }

        #endregion

        #region Verify

        private bool IsHeaderFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                Regex guidRegEx = new Regex(@"^(HD_)+(?=.*\d)(?=.*[a-zA-Z0-9])(?!.*[\W_\x7B-\xFF]).{6,15}$");
                return guidRegEx.IsMatch(fileName);
            }
            else
                return false;
        }

        private bool IsDetailFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                Regex guidRegEx = new Regex(@"^(DT_)+(?=.*\d)(?=.*[a-zA-Z0-9])(?!.*[\W_\x7B-\xFF]).{6,15}$");
                return guidRegEx.IsMatch(fileName);
            }
            else
                return false;
        }

        private void WriteLog()
        {
            if (!Directory.Exists(Path.GetDirectoryName(logFile)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFile));
            }

            string message = "STK1300M_PPC - Import Data from Pocket PC	[RUN AT {0}]";

            Utility.WriteLog(string.Format(message, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")), logFile);
            Utility.WriteLog(string.Empty, logFile);
        }

        private void CheckHHTLog(string HHTID)
        {
            string sql = "HHTID = '" + HHTID + "'";
            SystemHHTLog hhtLog = SystemHHTLog.LoadWhere(sql);
            if (hhtLog == null)
            {
                hhtLog = new SystemHHTLog();
                hhtLog.HHTID = HHTID;
                hhtLog.Module = "RWS_STK3 [Stock Take]";
                hhtLog.Form = "STK1300M_PPC " + this.Text;
                hhtLog.CreatedBy = Common.Config.CurrentUserId;
                hhtLog.CreatedOn = DateTime.Now;
            }

            hhtLog.ModifiedBy = Common.Config.CurrentUserId;
            hhtLog.ModifiedOn = DateTime.Now;
            hhtLog.Save();
        }

        #endregion

        #region Help Message

        private void btnHelp_Click(object sender, EventArgs e)
        {
            #region Header Message

            StringBuilder message = new StringBuilder();
            message.Append(@"File Structure").AppendLine();
            message.Append(@"").AppendLine();
            message.Append(@"The record content is listed as follows:").AppendLine();
            message.Append(@"** Each field in a line is seperated by a <TAB> **").AppendLine();
            message.Append(@"").AppendLine();
            message.Append(@"File name: 'HD_' + HHT TRN# + '.TXT'").AppendLine();
            message.Append(@"").AppendLine();
            message.Append(@"Header (Column)").AppendLine();
            message.Append(@"--------------------------------------------------------------------").AppendLine();
            message.Append(@"1 -    Record Type     - ('HH')").AppendLine();
            message.Append(@"2 -    Location Code").AppendLine();
            message.Append(@"3 -    HHT ID").AppendLine();
            message.Append(@"4 -    HHT TRN#").AppendLine();
            message.Append(@"").AppendLine();
            message.Append(@"Detail (Column)").AppendLine();
            message.Append(@"--------------------------------------------------------------------").AppendLine();
            message.Append(@"1 -    Record Type     - ('DD')").AppendLine();
            message.Append(@"2 -    Shelf").AppendLine();
            message.Append(@"3 -    Shelf (Name)").AppendLine();
            message.Append(@"4 -    Qty").AppendLine();
            message.Append(@"5 -    # of Line(s)").AppendLine();
            message.Append(@"6 -    Last Updated    - (ddMMyyyyHHmmss)").AppendLine();
            message.Append(@"").AppendLine();
            message.Append(@"Footer (Column)").AppendLine();
            message.Append(@"--------------------------------------------------------------------").AppendLine();
            message.Append(@"1 -    Record Type     - ('EE')").AppendLine();
            message.Append(@"2 -    Total Line").AppendLine();
            message.Append(@"").AppendLine();

            #endregion

            MessageBox.Show(message.ToString(), "Attention", new EventHandler(HelpMessage));
        }

        private void HelpMessage(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.OK)
            {
                #region Detail Message

                StringBuilder message = new StringBuilder();
                message.Append(@"File Structure").AppendLine();
                message.Append(@"").AppendLine();
                message.Append(@"The record content is listed as follows:").AppendLine();
                message.Append(@"** Each field in a line is seperated by a <TAB> **").AppendLine();
                message.Append(@"").AppendLine();
                message.Append(@"File name: 'DT_' + HHT TRN# + '_' + Shelf + '.TXT'").AppendLine();
                message.Append(@"").AppendLine();
                message.Append(@"Header (Column)").AppendLine();
                message.Append(@"--------------------------------------------------------------------").AppendLine();
                message.Append(@"1 -    Record Type     - ('HH')").AppendLine();
                message.Append(@"").AppendLine();
                message.Append(@"Detail (Column)").AppendLine();
                message.Append(@"--------------------------------------------------------------------").AppendLine();
                message.Append(@"1 -    Record Type     - ('DD')").AppendLine();
                message.Append(@"2 -    Location Code").AppendLine();
                message.Append(@"3 -    HHT ID").AppendLine();
                message.Append(@"4 -    HHT TRN#").AppendLine();
                message.Append(@"5 -    Shelf").AppendLine();
                message.Append(@"6 -    Seq#").AppendLine();
                message.Append(@"7 -    Barcode").AppendLine();
                message.Append(@"8 -    Qty").AppendLine();
                message.Append(@"9 -    Key in          - ('Y' / 'N')").AppendLine();
                message.Append(@"10 -   Last Updated    - (ddMMyyyyHHmmss)").AppendLine();
                message.Append(@"").AppendLine();
                message.Append(@"Footer (Column)").AppendLine();
                message.Append(@"--------------------------------------------------------------------").AppendLine();
                message.Append(@"1 -    Record Type     - ('EE')").AppendLine();
                message.Append(@"2 -    Total Line").AppendLine();
                message.Append(@"").AppendLine();

                #endregion

                MessageBox.Show(message.ToString(), "Attention");
            }
        }

        #endregion

        #region Load Not finished Packet

        private void LoadPacketList()
        {
            lvPPCPacketList.Items.Clear();

            if (!Directory.Exists(tempDirectory))
            {
                Directory.CreateDirectory(tempDirectory);
            }

            if (!string.IsNullOrEmpty(tempDirectory))
            {
                string[] packetFiles = Directory.GetFiles(tempDirectory, "*.txt", SearchOption.TopDirectoryOnly);

                for (int i = 0; i < packetFiles.Length; i++)
                {
                    string fileName = packetFiles[i];
                    if (IsHeaderFile(Path.GetFileNameWithoutExtension(fileName)) && Path.GetExtension(fileName).ToLower() == ".txt")
                    {
                        lbPacketList.Items.Add(Path.GetFileName(fileName));

                        LoadPacketList(fileName);
                    }
                }
            }
        }

        private void LoadPacketList(string packetName)
        {
            int iCount = 0;
            decimal qty = 0;
            using (StreamReader sr = File.OpenText(packetName))
            {
                if (sr.Peek() != -1)
                {
                    string stocktakeNumber = string.Empty;
                    string loc = string.Empty;

                    ListViewItem lvItem = null;

                    string[] srAllLine = sr.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    for (int i = 0; i < srAllLine.Length; i++)
                    {
                        string srLine = srAllLine[i];

                        if (srLine.Substring(0, 2) == "HH")
                        {
                            lvItem = new ListViewItem();

                            string[] headerInfo = srLine.Split(new char[] { '\t' });

                            lvItem.SubItems.Add(headerInfo[1]); // LOC#
                            lvItem.SubItems.Add(headerInfo[3]); // Stock Take#
                            lvItem.SubItems.Add(headerInfo[3]); // HHT TRN#
                            lvItem.SubItems.Add(GetUploadOn(packetName)); // Upload On
                            lvItem.SubItems.Add(headerInfo[2]); // HHT ID

                            lvItem.UseItemStyleForSubItems = false;
                            lvItem.SubItems[1].BackColor = SystemInfo.ControlBackColor.RequiredBox;

                            stocktakeNumber = headerInfo[3];
                            loc = headerInfo[1];
                        }

                        if (srLine.Substring(0, 2) == "DD")
                        {
                            string[] detailInfo = srLine.Split(new char[] { '\t' });

                            if (Common.Utility.IsNumeric(detailInfo[3]))
                            {
                                qty += Convert.ToDecimal(detailInfo[3]);
                            }

                            iCount++;
                        }

                        if (srLine.Substring(0, 2) == "EE" && lvItem != null)
                        {
                            lvItem.SubItems.Add(iCount.ToString()); // # of shelf
                            lvItem.SubItems.Add(qty.ToString()); // Qty
                            lvItem.SubItems.Add(string.Empty); // Remarks

                            lvItem.SubItems.Add(packetName); // File name
                            lvItem.SubItems.Add(loc); // Origin Location
                            lvItem.SubItems.Add(stocktakeNumber); // Origin StockTake Number

                            lvPPCPacketList.Items.Add(lvItem);
                        }
                    }
                }
            }
        }

        #endregion

        #region Save data to Stock Take

        private void Save()
        {
            int iCount = 1;
            bool isValid = true;
            foreach (ListViewItem lvItem in lvPPCPacketList.Items)
            {
                if (lvItem.Checked)
                {
                    string stktkNumber = string.Empty;
                    System.Guid workplaceId = System.Guid.Empty;
                    DateTime uploadedOn = DateTime.Now;
                    List<ImportDetailsInfo> detailAllList = new List<ImportDetailsInfo>();
                    decimal totalLine = 0, totalQty = 0, missingLine = 0, missingQty = 0;

                    this.CheckHHTLog(lvItem.SubItems[4].Text);

                    // Load Header's detail info
                    FileHelperEngine<ImportHeaderInfo> headerInfoEngine = new FileHelperEngine<ImportHeaderInfo>();

                    headerInfoEngine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

                    ImportHeaderInfo[] headerInfoList = headerInfoEngine.ReadFile(lvItem.SubItems[8].Text);

                    if (headerInfoEngine.ErrorManager.ErrorCount > 0)
                        headerInfoEngine.ErrorManager.SaveErrors(logFile);

                    Utility.WriteLog("Date Create	 : " + uploadedOn.ToString("dd/MM/yyyy HH:mm:ss"), logFile);
                    Utility.WriteLog("Session ID	 : " + uploadedOn.ToString("yyyyMMdd-HHmmss") + "-" + iCount.ToString().PadLeft(3, '0'), logFile);
                    Utility.WriteLog("Upload Time	 : " + uploadedOn.ToString("dd/MM/yyyy HH:mm:ss"), logFile);
                    Utility.WriteLog("HHT TRN#	 : " + lvItem.SubItems[2].Text, logFile);
                    Utility.WriteLog("Location#	 : " + lvItem.SubItems[9].Text + " [Original]; " + lvItem.Text + " [Current]", logFile);
                    Utility.WriteLog("Stock Take#	 : " + lvItem.SubItems[10].Text + " [Suggested]; " + lvItem.SubItems[1].Text + " [Current]", logFile);
                    Utility.WriteLog("Process Detail	 : Import Data", logFile);
                    Utility.WriteLog("Message :- ", logFile);
                    Utility.WriteLog("=> Checking Loc# ", logFile);

                    // Check Workplace (Loc#)
                    RT2008.DAL.Workplace wp = RT2008.DAL.Workplace.LoadWhere("WorkplaceCode = '" + lvItem.Text + "'");
                    if (wp != null)
                    {
                        if (wp.Retired)
                        {
                            Utility.WriteLog("	[ERROR] Loc# was retired ", logFile);
                            isValid = isValid & false;
                        }
                        else
                        {
                            Utility.WriteLog("	[OK] ", logFile);
                            workplaceId = wp.WorkplaceId;
                        }
                    }
                    else
                    {
                        Utility.WriteLog("	[ERROR] Loc# Not Found", logFile);
                        isValid = isValid & false;
                    }

                    Utility.WriteLog("	RESULT : COMPLETED", logFile);
                    Utility.WriteLog("=> Import Packet File ", logFile);

                    // Load details files
                    string[] packetFiles = Directory.GetFiles(tempDirectory, "DT_" + lvItem.SubItems[2].Text + "*", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < packetFiles.Length; i++)
                    {
                        Utility.WriteLog(@"	" + (i + 1).ToString() + @") Packet => " + Path.GetFileNameWithoutExtension(packetFiles[i]) + " [" + packetFiles[i] + "] ", logFile);
                    }

                    Utility.WriteLog("	RESULT : COMPLETED", logFile);
                    Utility.WriteLog("=> Checking (Header) ", logFile);

                    stktkNumber = lvItem.SubItems[1].Text.Trim();

                    // checking Header info
                    StockTakeHeader stktkHeader = StockTakeHeader.LoadWhere("TxNumber = '" + lvItem.SubItems[1].Text.Trim() + "'");
                    if (stktkHeader != null)
                    {
                        if (!string.IsNullOrEmpty(stktkHeader.ADJNUM))
                        {
                            Utility.WriteLog("	[ERROR] The Stock Take Number was posted, cannot be used anymore. ", logFile);
                            isValid = isValid & false;
                        }
                        else if (!GetWorkplaceCode(stktkHeader.WorkplaceId).Equals(lvItem.Text.Trim()))
                        {
                            Utility.WriteLog("	[ERROR] The loc# in Stock Take Header must be as same as the selected one. ", logFile);
                            isValid = isValid & false;
                        }
                        else
                        {
                            string sql = "TxNumber = '" + lvItem.SubItems[1].Text.Trim() + "' AND HHTId = '" + lvItem.SubItems[4].Text + "' AND CONVERT(NVARCHAR(20), UploadedOn, 120) = '" + uploadedOn.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                            StocktakeHeader_HHT hhtHeader = StocktakeHeader_HHT.LoadWhere(sql);
                            if (hhtHeader != null)
                            {
                                if (hhtHeader.PostedOn.Year > 1900)
                                {
                                    Utility.WriteLog("	[ERROR] The Stock Take (HHT) Number was posted, cannot be used anymore. ", logFile);
                                    isValid = isValid & false;
                                }
                                else
                                {
                                    Utility.WriteLog("	[ERROR] The Stock Take (HHT) Number existed. ", logFile);
                                    isValid = isValid & false;
                                }
                            }
                            else
                            {
                                Utility.WriteLog("	[OK]  ", logFile);
                            }
                        }
                    }
                    else
                    {
                        Utility.WriteLog("	[OK]  ", logFile);
                    }

                    Utility.WriteLog("=> Checking (Detail) ", logFile);
                    int iCountBarcode = 0;

                    // checking details info
                    for (int iHeader = 0; iHeader < headerInfoList.Length; iHeader++)
                    {
                        ImportHeaderInfo headerInfo = headerInfoList[iHeader];

                        FileHelperEngine<ImportDetailsInfo> detailInfoEngine = new FileHelperEngine<ImportDetailsInfo>();

                        detailInfoEngine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

                        string detailPacket = Path.Combine(tempDirectory, "DT_" + lvItem.SubItems[2].Text + "_" + headerInfo.ShelfId + ".TXT");
                        ImportDetailsInfo[] detailInfoList = detailInfoEngine.ReadFile(detailPacket);

                        if (headerInfoEngine.ErrorManager.ErrorCount > 0)
                            headerInfoEngine.ErrorManager.SaveErrors(logFile);

                        Utility.WriteLog("	=> Checking Shelf (" + headerInfo.ShelfId + " - " + headerInfo.ShelfName + ")", logFile);

                        for (int iDetail = 0; iDetail < detailInfoList.Length; iDetail++)
                        {
                            ImportDetailsInfo detailInfo = detailInfoList[iDetail];
                            if (string.IsNullOrEmpty(detailInfo.Barcode))
                            {
                                iCountBarcode++;
                                missingQty += detailInfo.Qty;

                                Utility.WriteLog("	[ERROR] Barcode does not exist. ", logFile);
                            }
                            else
                            {
                                Guid productId = GetProductId(detailInfo.Barcode);

                                if (productId == System.Guid.Empty)
                                {
                                    iCountBarcode++;
                                    missingQty += detailInfo.Qty;

                                    Utility.WriteLog("	[ERROR] Barcode (" + detailInfo.Barcode + ") does not exist. ", logFile);
                                }
                                else
                                {
                                    if (detailInfo.Qty <= 0)
                                    {
                                        Utility.WriteLog("	[ERROR] Barcode (" + detailInfo.Barcode + ") QTY <= 0 ", logFile);
                                        isValid = isValid & false;
                                    }
                                    else
                                    {
                                        Utility.WriteLog("	[OK] Barcode (" + detailInfo.Barcode + ") QTY > 0 ", logFile);
                                    }
                                }
                            }

                            totalLine++;
                            totalQty += detailInfo.Qty;

                            detailAllList.Add(detailInfo);
                        }

                        missingLine += iCountBarcode;

                        if (iCountBarcode > 0)
                        {
                            Utility.WriteLog("	[ERROR] Details of Shelf (" + headerInfo.ShelfId + " - " + headerInfo.ShelfName + ") has " + iCountBarcode.ToString() + " empty barcode.", logFile);
                        }
                        else
                        {
                            Utility.WriteLog("	[OK] Details of Shelf (" + headerInfo.ShelfId + " - " + headerInfo.ShelfName + ") has 0 empty barcode.", logFile);
                        }
                    }

                    Utility.WriteLog("	RESULT : COMPLETED", logFile);
                    Utility.WriteLog("=> Save Packet", logFile);

                    if (isValid)
                    {
                        if (stktkNumber.Trim().Length == 0)
                        {
                            stktkNumber = SystemInfo.Settings.QueuingTxNumber(Common.Enums.TxType.STK);
                        }

                        Utility.WriteLog("	[OK] System Queue ", logFile);

                        if (stktkNumber.Length > 0)
                        {
                            // Stock take header
                            System.Guid stktkheaderId = CreateStockTakeHeader(stktkNumber.Trim(), workplaceId);
                            Utility.WriteLog("	[OK] Create Worksheet (Stock Take - Header)", logFile);

                            // Stock take details
                            if (stktkheaderId != System.Guid.Empty)
                            {
                                CreatedStockTakeDetail(stktkheaderId, stktkNumber.Trim(), detailAllList, workplaceId, uploadedOn);
                            }
                            Utility.WriteLog("	[OK] Create Worksheet (Stock Take - Detail)", logFile);

                            // Stock take header (HHT)
                            System.Guid hhtHeaderId = CreateStockTakeHHTHeader(stktkNumber.Trim(), lvItem.SubItems[4].Text, uploadedOn, workplaceId, lvItem.SubItems[2].Text,
                                totalLine, totalQty, missingLine, missingQty);
                            Utility.WriteLog("	[OK] Create Worksheet (HHT Data Review - Header)", logFile);

                            // Stock take details (HHT)
                            if (hhtHeaderId != System.Guid.Empty)
                            {
                                CreateStockTakeHHTDetails(hhtHeaderId, stktkNumber.Trim(), lvItem.SubItems[4].Text, uploadedOn, detailAllList, lvItem.SubItems[2].Text);
                            }

                            Utility.WriteLog("	[OK] Create Worksheet (HHT Data Review - Detail)", logFile);
                            Utility.WriteLog("	[OK] Barcode Matching", logFile);
                            Utility.WriteLog("	[OK] Counting Missing Data", logFile);
                            Utility.WriteLog("	RESULT : COMPLETED", logFile);

                            // Backup text files
                            if (!Directory.Exists(backupDirectory))
                            {
                                Directory.CreateDirectory(backupDirectory);
                            }

                            // Header file
                            File.Move(lvItem.SubItems[8].Text, Path.Combine(backupDirectory, Path.GetFileName(lvItem.SubItems[8].Text)));

                            for (int i = 0; i < packetFiles.Length; i++)
                            {
                                File.Move(packetFiles[i], Path.Combine(backupDirectory, Path.GetFileName(packetFiles[i])));
                            }

                            Utility.WriteLog("=> Backup Data	RESULT : COMPLETED", logFile);
                        }
                    }

                    iCount++;
                }
            }
        }

        #region Stock Take

        private Guid CreateStockTakeHeader(string txNumber, Guid workplaceId)
        {
            string sql = "TxNumber = '" + txNumber.Trim() + "'";

            StockTakeHeader stktkHeader = StockTakeHeader.LoadWhere(sql);
            if (stktkHeader == null)
            {
                stktkHeader = new StockTakeHeader();
                stktkHeader.TxNumber = txNumber;
                stktkHeader.TxDate = DateTime.Now;

                stktkHeader.CreatedBy = Common.Config.CurrentUserId;
                stktkHeader.CreatedOn = DateTime.Now;
            }

            stktkHeader.WorkplaceId = workplaceId;

            stktkHeader.ModifiedBy = Common.Config.CurrentUserId;
            stktkHeader.ModifiedOn = DateTime.Now;
            stktkHeader.Status = (int)Common.Enums.Status.Draft;

            stktkHeader.Save();

            return stktkHeader.HeaderId;
        }

        private void CreatedStockTakeDetail(Guid stktkHeaderId, string txNumber, List<ImportDetailsInfo> detailList, Guid workplaceId, DateTime uploadedOn)
        {
            foreach (ImportDetailsInfo detail in detailList)
            {
                Guid productId = GetProductId(detail.Barcode);

                if (!string.IsNullOrEmpty(detail.Barcode.Trim()) && productId != System.Guid.Empty)
                {
                    string sql = "HeaderId = '" + stktkHeaderId.ToString() + "' AND TxNumber = '" + txNumber + "' AND ProductId = '" + productId.ToString() + "' AND WorkplaceId = '" + workplaceId.ToString() + "'";
                    StockTakeDetails stktkDetail = StockTakeDetails.LoadWhere(sql);
                    if (stktkDetail == null)
                    {
                        stktkDetail = new StockTakeDetails();
                        stktkDetail.HeaderId = stktkHeaderId;
                        stktkDetail.TxNumber = txNumber;
                        stktkDetail.ProductId = productId;
                        stktkDetail.WorkplaceId = workplaceId;

                        stktkDetail.ModifiedOn = uploadedOn;
                        stktkDetail.ModifiedBy = Common.Config.CurrentUserId;
                    }

                    stktkDetail.Save();
                }
            }
        }

        #endregion

        #region HHT Summation Data

        private Guid CreateStockTakeHHTHeader(string txNumber, string hhtId, DateTime uploadedOn, Guid workplaceId, string hhtTxNumber,
            decimal totalLine, decimal totalQty, decimal missingLine, decimal missingQty)
        {
            string sql = "TxNumber = '" + txNumber + "'";
            StocktakeHeader_HHT hhtHeader = StocktakeHeader_HHT.LoadWhere(sql);
            if (hhtHeader == null)
            {
                hhtHeader = new StocktakeHeader_HHT();
                hhtHeader.TxNumber = txNumber;

                hhtHeader.Status = (int)Common.Enums.Status.Draft;

                hhtHeader.CreatedBy = Common.Config.CurrentUserId;
                hhtHeader.CreatedOn = DateTime.Now;
            }

            hhtHeader.HHTId = hhtId;
            hhtHeader.UploadedOn = uploadedOn;
            hhtHeader.WorkplaceId = workplaceId;
            hhtHeader.Remarks = hhtTxNumber;
            hhtHeader.TotalRows = (int)totalLine;
            hhtHeader.TOTALQTY = totalQty;
            hhtHeader.MissingRows = (int)missingLine;
            hhtHeader.MissingQty = missingQty;

            hhtHeader.ModifiedBy = Common.Config.CurrentUserId;
            hhtHeader.ModifiedOn = DateTime.Now;

            hhtHeader.Save();

            return hhtHeader.HeaderId;
        }

        private void CreateStockTakeHHTDetails(Guid hhtHeaderId, string txNumber, string hhtId, DateTime uploadedOn, List<ImportDetailsInfo> detailList, string hhtTxNumber)
        {
            int iCount = 0;
            foreach (ImportDetailsInfo detail in detailList)
            {
                Guid productId = GetProductId(detail.Barcode);

                if (!string.IsNullOrEmpty(detail.Barcode.Trim()) && productId != System.Guid.Empty)
                {
                    string sql = "HeaderId = '" + hhtHeaderId.ToString() + "' AND TxNumber = '" + txNumber + "' AND ProductId = '" + productId.ToString() + "'";
                    StockTakeDetails_HHT hhtDetail = StockTakeDetails_HHT.LoadWhere(sql);
                    if (hhtDetail == null)
                    {
                        hhtDetail = new StockTakeDetails_HHT();
                        hhtDetail.HeaderId = hhtHeaderId;
                        hhtDetail.TxNumber = txNumber;
                        hhtDetail.LineNumber = iCount++;
                        hhtDetail.UploadedOn = uploadedOn;
                        hhtDetail.ProductId = productId;
                        hhtDetail.Barcode = detail.Barcode;
                    }

                    hhtDetail.Qty = detail.Qty;
                    hhtDetail.Remarks = hhtTxNumber;

                    hhtDetail.Save();
                }
            }
        }

        #endregion

        private Guid GetProductId(string barcode)
        {
            string sql = "Barcode = '" + barcode.Trim() + "'";
            RT2008.DAL.ProductBarcode product = RT2008.DAL.ProductBarcode.LoadWhere(sql);
            if (product != null)
            {
                return product.ProductId;
            }
            else
            {
                return System.Guid.Empty;
            }
        }

        private string GetWorkplaceCode(Guid wpId)
        {
            RT2008.DAL.Workplace wp = RT2008.DAL.Workplace.Load(wpId);
            if (wp != null)
            {
                return wp.WorkplaceCode;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoadPacketList_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "PPC Packet files";
            openFileDialog.MaxFileSize = 10240;
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog objFileDialog = sender as OpenFileDialog;
            Utility.UploadFile(openFileDialog, tempDirectory);

            LoadPacketList();
        }

        private void lbPacketList_Click(object sender, EventArgs e)
        {
            string selectedPackt = Path.Combine(tempDirectory, lbPacketList.SelectedItem.ToString());

            using (StreamReader sr = File.OpenText(selectedPackt))
            {
                if (sr.Peek() != -1)
                {
                    string header = sr.ReadLine();
                    if (header.Substring(0, 2) == "HH")
                    {
                        string[] headerInfo = header.Split(new char[] { '\t' });

                        txtUploadOn.Text = GetUploadOn(selectedPackt);
                        txtHHTId.Text = headerInfo[2];
                        txtHHTTxNumber.Text = headerInfo[3];
                        txtStockTakeNumber.Text = headerInfo[3];
                    }
                }
            }
        }

        private void btnMarkAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvItem in lvPPCPacketList.Items)
            {
                lvItem.Checked = (btnMarkAll.Text.Contains("Mark"));
            }

            btnMarkAll.Text = (btnMarkAll.Text.Contains("Mark")) ? "Unmark All" : "Mark All";
        }

        private void lvPPCPacketList_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem lvItem = lvPPCPacketList.SelectedItem;

            lvItem.Checked = lvItem.Checked;
            lvItem.SubItems[9].Text = lvItem.Text;
            lvItem.SubItems[10].Text = lvItem.SubItems[1].Text;

            if (!string.IsNullOrEmpty(lvItem.Text))
            {
                selectedDataCtrl.SelectedIndex = lvItem.Index;
                selectedDataCtrl.Workplace = lvItem.Text.Trim();
                selectedDataCtrl.HHTTxNumber = lvItem.SubItems[2].Text.Trim();
                selectedDataCtrl.StockTakeNumber = lvItem.SubItems[1].Text.Trim();
                selectedDataCtrl.Visible = true;
            }
        }

        void selectedDataCtrl_VisibleChanged(object sender, EventArgs e)
        {
            if (!selectedDataCtrl.Visible && selectedDataCtrl.HasChanged)
            {
                ListViewItem lvItem = lvPPCPacketList.Items[selectedDataCtrl.SelectedIndex];
                lvItem.Selected = true;
                lvItem.Text = selectedDataCtrl.Workplace;
                lvItem.SubItems[1].Text = selectedDataCtrl.StockTakeNumber;

                lvItem.Update();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void ImportFromPPC_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("Log file: " + logFile, "Attention");
        }
    }
}