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

namespace RT2008.Product
{
    public partial class ProductNatureWizard : Form
    {
        public ProductNatureWizard()
        {
            InitializeComponent();
            SetToolBar();
            FillParentNatureList();
            BindProductNatureList();
            SetCtrlEditable();
        }

        #region ToolBar
        private void SetToolBar()
        {
            this.tbWizardAction.MenuHandle = false;
            this.tbWizardAction.DragHandle = false;
            this.tbWizardAction.TextAlign = ToolBarTextAlign.Right;

            ToolBarButton sep = new ToolBarButton();
            sep.Style = ToolBarButtonStyle.Separator;

            // cmdSave
            ToolBarButton cmdNew = new ToolBarButton("New", "New");
            cmdNew.Tag = "New";
            cmdNew.Image = new IconResourceHandle("16x16.ico_16_3.gif");
            cmdNew.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);

            this.tbWizardAction.Buttons.Add(cmdNew);

            // cmdSave
            ToolBarButton cmdSave = new ToolBarButton("Save", "Save");
            cmdSave.Tag = "Save";
            cmdSave.Image = new IconResourceHandle("16x16.16_L_save.gif");
            cmdSave.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Write);

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

            if (ProductNatureId == System.Guid.Empty)
            {
                cmdDelete.Enabled = false;
            }
            else
            {
                cmdDelete.Enabled = RT2008.Controls.UserUtility.IsAccessAllowed(Common.Enums.Permission.Delete);
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
                            BindProductNatureList();
                            this.Update();
                        }
                        break;
                    case "refresh":
                        BindProductNatureList();
                        this.Update();
                        break;
                    case "delete":
                        MessageBox.Show("Delete Record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(DeleteConfirmationHandler));
                        break;
                }
            }
        }
        #endregion

        #region ProductNature Code
        private void SetCtrlEditable()
        {
            txtProductNatureCode.BackColor = (this.ProductNatureId == System.Guid.Empty) ? Color.LightSkyBlue : Color.LightYellow;
            txtProductNatureCode.ReadOnly = (this.ProductNatureId != System.Guid.Empty);
        }
        #endregion

        #region Fill Combo List
        private void FillParentNatureList()
        {
            cboParentNature.Items.Clear();

            string sql = "NatureId NOT IN ('" + this.ProductNatureId.ToString() + "')";
            string[] orderBy = new string[] { "NatureCode" };
            ProductNatureCollection oProductNatureList = ProductNature.LoadCollection(sql, orderBy, true);
            oProductNatureList.Add(new ProductNature());
            cboParentNature.DataSource = oProductNatureList;
            cboParentNature.DisplayMember = "NatureCode";
            cboParentNature.ValueMember = "NatureId";

            cboParentNature.SelectedIndex = cboParentNature.Items.Count - 1;
        }
        #endregion

        #region Binding
        private void BindProductNatureList()
        {
            this.lvProductNatureList.ListViewItemSorter = new ListViewItemSorter(lvProductNatureList);
            this.lvProductNatureList.Items.Clear();

            int iCount = 1;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT NatureId,  ROW_NUMBER() OVER (ORDER BY NatureCode) AS rownum, ");
            sql.Append(" NatureCode, NatureName, NatureName_Chs, NatureName_Cht ");
            sql.Append(" FROM ProductNature ");
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql.ToString();
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType= CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ListViewItem objItem = this.lvProductNatureList.Items.Add(reader.GetGuid(0).ToString()); // ProductNatureId
                    objItem.SubItems.Add(iCount.ToString()); // Line Number
                    objItem.SubItems.Add(reader.GetString(2)); // ProductNatureCode
                    objItem.SubItems.Add(reader.GetString(3)); // ProductNature Name
                    objItem.SubItems.Add(reader.GetString(4)); // ProductNature Name Chs
                    objItem.SubItems.Add(reader.GetString(5)); // ProductNature Name Cht

                    iCount++;
                }
            }
        }
        #endregion

        #region Save
        private bool CodeExists()
        {
            string sql = "NatureCode = '" + txtProductNatureCode.Text.Trim() + "'";
            ProductNatureCollection natureList = ProductNature.LoadCollection(sql);
            if (natureList.Count > 0)
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
            if (txtProductNatureCode.Text.Length == 0)
            {
                errorProvider.SetError(txtProductNatureCode, "Cannot be blank!");
                return false;
            }
            else
            {
                ProductNature oProductNature = ProductNature.Load(this.ProductNatureId);
                if (oProductNature == null)
                {
                    oProductNature = new ProductNature();

                    if (CodeExists())
                    {
                        errorProvider.SetError(txtProductNatureCode, string.Format(Resources.Common.DuplicatedCode, "Product Nature Code"));
                        return false;
                    }
                    else
                    {
                        oProductNature.NatureCode = txtProductNatureCode.Text;
                        errorProvider.SetError(txtProductNatureCode, string.Empty);
                    }
                }
                oProductNature.NatureName = txtProductNatureName.Text;
                oProductNature.NatureName_Chs = txtProductNatureNameChs.Text;
                oProductNature.NatureName_Cht = txtProductNatureNameCht.Text;
                oProductNature.ParentNature = (cboParentNature.SelectedValue == null)? System.Guid.Empty:new System.Guid(cboParentNature.SelectedValue.ToString());

                oProductNature.Save();
                return true;
            }
        }

        private void Clear()
        {
            this.Close();

            ProductNatureWizard wizNature = new ProductNatureWizard();
            wizNature.ShowDialog();
        }
        #endregion

        #region Properties
        private Guid countryId = System.Guid.Empty;
        public Guid ProductNatureId
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

        #region Delete
        private void Delete()
        {
            ProductNature oNature = ProductNature.Load(this.ProductNatureId);
            if (oNature != null)
            {
                try
                {
                    oNature.Delete();
                }
                catch
                {
                    MessageBox.Show("Cannot delete the record being used by other record!", "Delete Warning");
                }
            }
        }
        #endregion

        private void DeleteConfirmationHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                Delete();
                Clear();
            }
        }

        private void lvProductNatureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvProductNatureList.SelectedItem != null)
            {
                if (Common.Utility.IsGUID(lvProductNatureList.SelectedItem.Text))
                {
                    ProductNature oProductNature = ProductNature.Load(new System.Guid(lvProductNatureList.SelectedItem.Text));
                    if (oProductNature != null)
                    {
                        this.ProductNatureId = oProductNature.NatureId;

                        FillParentNatureList();

                        txtProductNatureCode.Text = oProductNature.NatureCode;
                        txtProductNatureName.Text = oProductNature.NatureName;
                        txtProductNatureNameChs.Text = oProductNature.NatureName_Chs;
                        txtProductNatureNameCht.Text = oProductNature.NatureName_Cht;
                        cboParentNature.SelectedValue = oProductNature.ParentNature;

                        SetCtrlEditable();
                    }
                }
            }
        }
    }
}