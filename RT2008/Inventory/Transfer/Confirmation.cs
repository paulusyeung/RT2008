#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using System.Data.SqlClient;
using RT2008.DAL;
using System.Text.RegularExpressions;
using System.Configuration;

#endregion

namespace RT2008.Inventory.Transfer
{
    public partial class Confirmation : Form
    {
        private String _NewEraBeginsOn = "2013-11-30 00:00:00";     //在這天之前的 Transactions 不會顯示

        public Confirmation()
        {
            InitializeComponent();
            BindingToBeConfirmedList();
        }

        private void Confirmation_Load(object sender, EventArgs e)
        {
            SetAttributes();
            txtTxNumber.Focus();
        }

        private void SetAttributes()
        {
            lvPostTxList.ListViewItemSorter = new ListViewItemSorter(lvPostTxList);
        }

        #region Bind Methods
        private string StaffNumber(Guid staffId)
        {
            string result = string.Empty;
            RT2008.DAL.Staff oStaff = RT2008.DAL.Staff.Load(staffId);
            if (oStaff != null)
            {
                result = oStaff.StaffNumber;
            }
            return result;
        }

        private void BindingToBeConfirmedList()
        {
            lvPostTxList.Items.Clear();

            int iCount = 1;
            string sql = BuildSqlQueryString(Common.Enums.Status.Active.ToString("d"), false);
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType = System.Data.CommandType.Text;

            using (SqlDataReader reader = RT2008.DAL.SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem objItem = this.lvPostTxList.Items.Add(reader.GetGuid(0).ToString()); // TxId
                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetString(1)); // TxNumber
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(2), false)); // TxDate
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(6), false)); // TxferDate
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(7), false)); // CompletedDate
                    objItem.SubItems.Add(reader.GetString(4)); // From Location
                    objItem.SubItems.Add(reader.GetString(5)); // To Location
                    objItem.SubItems.Add(RT2008.SystemInfo.Settings.DateTimeToString(reader.GetDateTime(11), false) + " " + StaffNumber(reader.GetGuid(12))); // Last Update

                    iCount++;
                }
            }
        }

        private string BuildSqlQueryString(string status, bool withConditions)
        {
            //2013.12.25 paulus: 暫時將 vwTxferConfirmationList 改為 vwDraftTxferList
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT HeaderId, TxNumber, TxDate, StaffNumber, ");
            sql.Append(" FromLocation, ToLocation, ISNULL(TransferredOn, '1990-01-01 00:00:00'), ISNULL(CompletedOn, '1990-01-01 00:00:00'), Remarks, ");
            sql.Append(" CreatedOn, CreatedBy, ModifiedOn, ModifiedBy ");
            sql.Append(" FROM vwDraftTxferList WHERE CONFIRM_TRF <> 'Y' AND TxDate >= '" + _NewEraBeginsOn + "' AND TxType = 'TXF' ");

            if (txtTxNumber.Text.Trim().Length > 0)
            {
                sql.Append(" AND TxNumber LIKE '%").Append(txtTxNumber.Text).Append("%'");
            }

            sql.Append(" ORDER BY TxNumber ");

            return sql.ToString();
        }
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
                    BindingToBeConfirmedList();
                }
                else
                {
                    errorProvider.SetError(txtTxNumber, "Transfer Number field does not exist!");
                }
            }
            else
            {
                errorProvider.SetError(txtTxNumber, "Transfer Number field cannot be blank!");
            }
        }
        #endregion

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
            if (lvPostTxList.SelectedItem != null)
            {
                if (Common.Utility.IsGUID(lvPostTxList.SelectedItem.Text))
                {
                    ConfirmationWizard wizConfirmation = new ConfirmationWizard(new System.Guid(lvPostTxList.SelectedItem.Text));
                    wizConfirmation.Closed += new EventHandler(wizConfirmation_Closed);
                    wizConfirmation.ShowDialog();
                }
            }
        }

        private void wizConfirmation_Closed(object sender, EventArgs e)
        {
            ConfirmationWizard wizConfirmation = sender as ConfirmationWizard;
            if (wizConfirmation.IsCompleted)
            {
                BindingToBeConfirmedList();
            }
        }
    }
}