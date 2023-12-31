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

namespace RT2008.Settings
{
    public partial class CountryWizard : Form
    {
        public CountryWizard()
        {
            InitializeComponent();
            SetToolBar();
            BindCountryList();
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

            if (CountryId == System.Guid.Empty)
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
                            BindCountryList();
                            this.Update();
                        }
                        break;
                    case "refresh":
                        BindCountryList();
                        this.Update();
                        break;
                    case "delete":
                        MessageBox.Show("Delete Record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(DeleteConfirmationHandler));
                        break;
                }
            }
        }
        #endregion

        #region Country Code
        private void SetCtrlEditable()
        {
            txtCountryCode.BackColor = (this.CountryId == System.Guid.Empty) ? Color.LightSkyBlue : Color.LightYellow;
            txtCountryCode.ReadOnly = (this.CountryId != System.Guid.Empty);

            ClearError();
        }

        private void ClearError()
        {
            errorProvider.SetError(txtCountryCode, string.Empty);
        }
        #endregion

        #region Binding
        private void BindCountryList()
        {
            this.lvCountryList.ListViewItemSorter = new ListViewItemSorter(lvCountryList);
            this.lvCountryList.Items.Clear();

            int iCount = 1;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT CountryId,  ROW_NUMBER() OVER (ORDER BY CountryCode) AS rownum, ");
            sql.Append(" CountryCode, CountryName, CountryName_Chs, CountryName_Cht ");
            sql.Append(" FROM Country ");
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql.ToString();
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType= CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem objItem = this.lvCountryList.Items.Add(reader.GetGuid(0).ToString()); // CountryId
                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetString(2)); // CountryCode
                    objItem.SubItems.Add(reader.GetString(3)); // Country Name
                    objItem.SubItems.Add(reader.GetString(4)); // Country Name Chs
                    objItem.SubItems.Add(reader.GetString(5)); // Country Name Cht

                    iCount++;
                }
            }
        }
        #endregion

        #region Save
        private bool CodeExists()
        {
            string sql = "CountryCode = '" + txtCountryCode.Text.Trim() + "'";
            CountryCollection countryList = Country.LoadCollection(sql);
            if (countryList.Count > 0)
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
            if (txtCountryCode.Text.Length == 0)
            {
                errorProvider.SetError(txtCountryCode, "Cannot be blank!");
                return false;
            }
            else
            {
                errorProvider.SetError(txtCountryCode, string.Empty);

                Country oCountry = Country.Load(this.CountryId);
                if (oCountry == null)
                {
                    oCountry = new Country();

                    if (CodeExists())
                    {
                        errorProvider.SetError(txtCountryCode, string.Format(Resources.Common.DuplicatedCode, "Country Code"));
                        return false;
                    }
                    else
                    {
                        oCountry.CountryCode = txtCountryCode.Text;
                        errorProvider.SetError(txtCountryCode, string.Empty);
                    }
                }
                oCountry.CountryName = txtCountryName.Text;
                oCountry.CountryName_Chs = txtCountryNameChs.Text;
                oCountry.CountryName_Cht = txtCountryNameCht.Text;

                oCountry.Save();
                return true;
            }
        }

        private void Clear()
        {
            txtCountryCode.Text = string.Empty;
            txtCountryName.Text = string.Empty;
            txtCountryNameChs.Text = string.Empty;
            txtCountryNameCht.Text = string.Empty;

            this.CountryId = System.Guid.Empty;
        }
        #endregion

        #region Properties
        private Guid countryId = System.Guid.Empty;
        public Guid CountryId
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
            Country oCountry = Country.Load(this.CountryId);
            if (oCountry != null)
            {
                try
                {
                    oCountry.Delete();
                }
                catch
                {
                    MessageBox.Show("Cannot delete the record being used by other record!", "Delete Warning");
                }
            }
        }

        private void lvCountryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvCountryList.SelectedItem != null)
            {
                if (Common.Utility.IsGUID(lvCountryList.SelectedItem.Text))
                {
                    Country oCountry = Country.Load(new System.Guid(lvCountryList.SelectedItem.Text));
                    if (oCountry != null)
                    {
                        txtCountryCode.Text = oCountry.CountryCode;
                        txtCountryName.Text = oCountry.CountryName;
                        txtCountryNameChs.Text = oCountry.CountryName_Chs;
                        txtCountryNameCht.Text = oCountry.CountryName_Cht;

                        this.CountryId = oCountry.CountryId;

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

                BindCountryList();
                Clear();
                SetCtrlEditable();
            }
        }

        private void lnkAddProvince_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProvinceWizard wizProvince = new ProvinceWizard();
            wizProvince.ShowDialog();
        }

        private void lnkAddCity_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CityWizard wizCity = new CityWizard();
            wizCity.ShowDialog();
        }
    }
}