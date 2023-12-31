#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Resources;
using RT2008.DAL;
using System.Data.SqlClient;
using System.Configuration;

#endregion

namespace RT2008.Staff
{
    public partial class StaffJobTitleWizard : Form
    {
        public StaffJobTitleWizard()
        {
            InitializeComponent();
            SetToolBar();
            BindJobTitleList();
            SetCtrlEditable();
        }

        #region ToolBar
        private void SetToolBar()
        {
            this.tbWizardAction.MenuHandle = false;
            this.tbWizardAction.DragHandle = false;
            this.tbWizardAction.TextAlign = ToolBarTextAlign.Right;
            this.tbWizardAction.Buttons.Clear();
            this.tbWizardAction.ButtonClick -= new ToolBarButtonClickEventHandler(tbWizardAction_ButtonClick);

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            // cmdSave
            ToolBarButton cmdNew = new ToolBarButton("New", "New");
            cmdNew.Tag = "New";
            cmdNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");

            this.tbWizardAction.Buttons.Add(cmdNew);

            // cmdSave
            ToolBarButton cmdSave = new ToolBarButton("Save", "Save");
            cmdSave.Tag = "Save";
            cmdSave.Image = new IconResourceHandle("16x16.16_L_save.gif");

            this.tbWizardAction.Buttons.Add(cmdSave);

            // cmdSaveNew
            ToolBarButton cmdRefresh = new ToolBarButton("Refresh", "Refresh");
            cmdRefresh.Tag = "refresh";
            cmdRefresh.Image = new IconResourceHandle("16x16.16_L_refresh.gif");

            this.tbWizardAction.Buttons.Add(cmdRefresh);
            this.tbWizardAction.Buttons.Add(sep);

            // cmdDelete
            ToolBarButton cmdDelete = new ToolBarButton("Delete", "Delete");
            cmdDelete.Tag = "Delete";
            cmdDelete.Image = new IconResourceHandle("16x16.16_L_remove.gif");

            if (JobTitleId == System.Guid.Empty)
            {
                cmdDelete.Enabled = false;
            }
            else
            {
                cmdDelete.Enabled = true;
            }

            this.tbWizardAction.Buttons.Add(cmdDelete);

            this.tbWizardAction.ButtonClick += new ToolBarButtonClickEventHandler(tbWizardAction_ButtonClick);
        }

        void tbWizardAction_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button.Tag != null)
            {
                switch (e.Button.Tag.ToString().ToLower())
                {
                    case "new":
                        Clear();
                        SetCtrlEditable();
                        break;
                    case "save":
                        if (Save())
                        {
                            Clear();
                            BindJobTitleList();
                            this.Update();
                        }
                        break;
                    case "refresh":
                        BindJobTitleList();
                        this.Update();
                        break;
                    case "delete":
                        MessageBox.Show("Delete Record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(DeleteConfirmationHandler));
                        break;
                }
            }
        }
        #endregion

        #region StaffJobTitle Code
        private void SetCtrlEditable()
        {
            txtJobTitleCode.BackColor = (this.JobTitleId == System.Guid.Empty) ? Color.LightSkyBlue : Color.LightYellow;
            txtJobTitleCode.ReadOnly = (this.JobTitleId != System.Guid.Empty);

            ClearError();
        }

        private void ClearError()
        {
            errorProvider.SetError(txtJobTitleCode, string.Empty);
        }
        #endregion

        #region Binding
        private void BindJobTitleList()
        {
            this.lvJobTitleList.ListViewItemSorter = new ListViewItemSorter(lvJobTitleList);
            this.lvJobTitleList.Items.Clear();

            int iCount = 1;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT JobTitleId,  ROW_NUMBER() OVER (ORDER BY JobTitleCode) AS rownum, ");
            sql.Append(" JobTitleCode, JobTitleName, JobTitleName_Chs, JobTitleName_Cht ");
            sql.Append(" FROM StaffJobTitle ");
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql.ToString();
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType= CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem objItem = this.lvJobTitleList.Items.Add(reader.GetGuid(0).ToString()); // JobTitleId
                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetString(2)); // JobTitleCode
                    objItem.SubItems.Add(reader.GetString(3)); // StaffJobTitle Name
                    objItem.SubItems.Add(reader.GetString(4)); // StaffJobTitle Name Chs
                    objItem.SubItems.Add(reader.GetString(5)); // StaffJobTitle Name Cht

                    iCount++;
                }
            }
        }
        #endregion

        #region Save
        private bool CodeExists()
        {
            string sql = "JobTitleCode = '" + txtJobTitleCode.Text.Trim() + "'";
            StaffJobTitleCollection jtList = StaffJobTitle.LoadCollection(sql);
            if (jtList.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool Save()
        {
            if (txtJobTitleCode.Text.Length == 0)
            {
                errorProvider.SetError(txtJobTitleCode, "Cannot be blank!");
                return false;
            }
            else
            {
                errorProvider.SetError(txtJobTitleCode, string.Empty);

                StaffJobTitle oJobTitle = StaffJobTitle.Load(this.JobTitleId);
                if (oJobTitle == null)
                {
                    oJobTitle = new StaffJobTitle();

                    if (CodeExists())
                    {
                        errorProvider.SetError(txtJobTitleCode, string.Format(Resources.Common.DuplicatedCode, "Job Title Code"));
                        return false;
                    }
                    else
                    {
                        oJobTitle.JobTitleCode = txtJobTitleCode.Text;
                        errorProvider.SetError(txtJobTitleCode, string.Empty);
                    }
                }
                oJobTitle.JobTitleName = txtJobTitleName.Text;
                oJobTitle.JobTitleName_Chs = txtJobTitleNameChs.Text;
                oJobTitle.JobTitleName_Cht = txtJobTitleNameCht.Text;

                oJobTitle.Save();
                return true;
            }
        }

        private void Clear()
        {
            this.Close();
            StaffJobTitleWizard wizJobTitle = new StaffJobTitleWizard();
            wizJobTitle.ShowDialog();
        }
        #endregion

        #region Properties
        private Guid countryId = System.Guid.Empty;
        public Guid JobTitleId
        {
            get
            {
                return countryId;
            }
            set
            {
                countryId = value;
            }
        }
        #endregion

        private void Delete()
        {
            StaffJobTitle oStaffJobTitle = StaffJobTitle.Load(this.JobTitleId);
            if (oStaffJobTitle != null)
            {
                try
                {
                    oStaffJobTitle.Delete();
                }
                catch
                {
                    MessageBox.Show("Cannot delete the record being used by other record!", "Delete Warning");
                }
            }
        }

        private void lvJobTitleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvJobTitleList.SelectedItem != null)
            {
                if (Common.Utility.IsGUID(lvJobTitleList.SelectedItem.Text))
                {
                    StaffJobTitle oJobTitle = StaffJobTitle.Load(new System.Guid(lvJobTitleList.SelectedItem.Text));
                    if (oJobTitle != null)
                    {
                        txtJobTitleCode.Text = oJobTitle.JobTitleCode;
                        txtJobTitleName.Text = oJobTitle.JobTitleName;
                        txtJobTitleNameChs.Text = oJobTitle.JobTitleName_Chs;
                        txtJobTitleNameCht.Text = oJobTitle.JobTitleName_Cht;

                        this.JobTitleId = oJobTitle.JobTitleId;

                        SetCtrlEditable();
                        SetToolBar();
                    }
                }
            }
        }

        private void DeleteConfirmationHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                Delete();

                BindJobTitleList();
                Clear();
                SetCtrlEditable();
            }
        }
    }
}