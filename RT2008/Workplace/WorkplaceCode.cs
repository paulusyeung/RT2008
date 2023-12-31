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
using RT2008.Controls;
using System.Configuration;
#endregion

namespace RT2008.Workplace
{
    public partial class WorkplaceCode : Form
    {
        public WorkplaceCode()
        {
            InitializeComponent();
            InitialSmartTags();
        }

        public WorkplaceCode(System.Guid workplaceId)
        {
            InitializeComponent();
            InitialSmartTags();
            this.WorkplaceID = workplaceId;
        }

        private void WorkplaceCode_Load(object sender, EventArgs e)
        {
            SetAttributes();
            FillComo();
            databind(this.WorkplaceID);
        }

        private void SetAttributes()
        {
            //2014.01.05 paulus: 唔明掂解要選 Customer？暫時取消先
            lblCustomerInfo.Visible = false;
            cboSmartTag6.Visible = false;
        }

        private void InitialSmartTags()
        {
            string[] orderBy = new string[] { "Priority" };
            SmartTag4WorkplaceCollection smartTagList = SmartTag4Workplace.LoadCollection(orderBy, true);

            SmartTag oTag = new SmartTag(this.groupBoxMain);
            oTag.WorkplaceSmartTagList = smartTagList;
            oTag.SetSmartTags();
        }

        #region databind
        private void databind(System.Guid workplaceID)
        {
            if (workplaceID != System.Guid.Empty)
            {
                SetData();
            }
            else
            {
                AddNew();
            }
        }
        #endregion

        #region FillComo
        private void FillComo()
        {
            FillCountry();

            SetNature();
            SetZone();
            SetLineOfOperation();

            //2014.01.05 paulus: 唔明掂解要選 Customer？暫時取消先
            //SetCustomerInfo();
        }
        #endregion

        #region SetTextBox
        private void SetTextBox()
        {
            txtLoc.Text = "";
            txtInitial.Text = "";
            txtPassword.Text = "";
            txtName.Text = "";
            txtNameChs.Text = "";
            txtNameCht.Text = "";
            txtAddress.Text = "";
            txtDistrict.Text = "";
            txtPriority.Text = "";
            txtPhoneTag4.Text = "";
            txtPostal.Text = "";
            txtPhoneTag1.Text = "";
            txtSmartTag1.Text = "";
            txtSmartTag2.Text = "";
            txtPhoneTag3.Text = "";
            txtEmail.Text = "";
            txtLastUpdate.Text = "";
            txtCreationDate.Text = "";
            txtModified.Text = "";
            txtStatus_Office.Text = "";
            txtStatus_Counter.Text = "";
        }
        #endregion

        #region SetData
        private void SetData()
        {
            RT2008.DAL.Workplace workplace = RT2008.DAL.Workplace.Load(this.WorkplaceID);
            if (workplace != null)
            {
                txtLoc.Text = workplace.WorkplaceCode;
                txtInitial.Text = workplace.WorkplaceInitial; ;
                txtPassword.Text = workplace.Password;
                txtName.Text = workplace.WorkplaceName;
                txtNameChs.Text = workplace.WorkplaceName_Chs;
                txtNameCht.Text = workplace.WorkplaceName_Cht;
                txtAltWorkplaceNum.Text = workplace.AlternateWorkplaceCode;
                txtPriority.Text = workplace.Priority.ToString();
                txtEmail.Text = workplace.Email;
                txtLastUpdate.Text = RT2008.SystemInfo.Settings.DateTimeToString(workplace.ModifiedOn, false);
                txtCreationDate.Text = RT2008.SystemInfo.Settings.DateTimeToString(workplace.CreatedOn, false);
                txtModified.Text = GetStaffNum(workplace.ModifiedBy);
                txtStatus_Office.Text = "";
                txtStatus_Counter.Text = "";
                cmbLOO.SelectedValue = workplace.LineOfOperationId;
                cmbZone.SelectedValue = workplace.ZoneId;
                for (int j = 0; j < cmbNature.Items.Count; j++)
                {
                    if (((System.Web.UI.WebControls.ListItem)cmbNature.Items[j]).Value == workplace.NatureId.ToString())
                    {
                        cmbNature.SelectedIndex = j;
                        break;
                    }
                }

                if (workplace.DownloadToPOS)
                {
                    if (workplace.ModifiedOn.Date.Equals(workplace.CreatedOn.Date))
                    {
                        txtStatus_Office.Text = "A";
                    }
                    else
                    {
                        txtStatus_Office.Text = "M";
                    }
                }

                if (workplace.DownloadToCounter)
                {
                    if (workplace.ModifiedOn.Date.Equals(workplace.CreatedOn.Date))
                    {
                        txtStatus_Counter.Text = "A";
                    }
                    else
                    {
                        txtStatus_Counter.Text = "M";
                    }
                }

                LoadWorkplaceAddress();
                LoadSmartTag();
            }

        }
        #endregion

        #region LoadWorkplaceAddress
        private void LoadWorkplaceAddress()
        {
            string whrerClause = " WorkplaceId = '" + this.WorkplaceID.ToString() + "'";
            WorkplaceAddress workplaceAddress = WorkplaceAddress.LoadWhere(whrerClause);
            if (workplaceAddress != null)
            {
                txtAddress.Text = workplaceAddress.Address;
                txtDistrict.Text = workplaceAddress.District;
                txtPostal.Text = workplaceAddress.PostalCode;
                cmbCountry.SelectedValue = workplaceAddress.CountryId;
                cmbProvince.SelectedValue = workplaceAddress.ProvinceId;
                cmbCity.SelectedValue = workplaceAddress.CityId;
                txtPhoneTag1.Text = workplaceAddress.PhoneTag1Value;
                txtPhoneTag3.Text = workplaceAddress.Phonetage3Value;
                txtPhoneTag4.Text = workplaceAddress.PhoneTag4Value;
            }
        }
        #endregion

        #region LoadSmartTag
        private void LoadSmartTag()
        {
            string sql = " WorkplaceId = '" + this.WorkplaceID.ToString() + "'  AND TagId = '{0}'";

            WorkplaceSmartTag oTag1 = WorkplaceSmartTag.LoadWhere(string.Format(sql, txtSmartTag1.Tag.ToString()));
            if (oTag1 != null)
            {
                txtSmartTag1.Text = oTag1.SmartTagValue;
            }

            WorkplaceSmartTag oTag2 = WorkplaceSmartTag.LoadWhere(string.Format(sql, txtSmartTag2.Tag.ToString()));
            if (oTag2 != null)
            {
                txtSmartTag2.Text = oTag2.SmartTagValue;
            }
        }
        #endregion

        #region GetStaffNum
        private string GetStaffNum(Guid ModifiedId)
        {
            RT2008.DAL.Staff staff = RT2008.DAL.Staff.Load(ModifiedId);
            if (staff != null)
            {
                return staff.StaffNumber;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region Properties
        private Guid workplaceID = System.Guid.Empty;
        public Guid WorkplaceID
        {
            get
            {
                return workplaceID;
            }
            set
            {
                workplaceID = value;
            }
        }
        #endregion

        #region Save
        private bool Save()
        {
            if (txtLoc.Text.Trim().Length <= 0)
            {
                errorProvider.SetError(txtLoc, "Cannot be blank!");
                return false;
            }
            else if (!Common.Utility.IsNumeric(txtPriority.Text.Trim()))
            {
                errorProvider.SetError(txtPriority, Resources.Common.DigitalNeeded);
                return false;
            }
            else
            {
                errorProvider.SetError(txtLoc, string.Empty);
                errorProvider.SetError(txtPriority, string.Empty);

                if (txtLoc.ReadOnly)
                {
                    System.Guid workplaceId = AddWorkplace(this.WorkplaceID);
                    this.WorkplaceID = workplaceId;
                    if (workplaceId != System.Guid.Empty)
                    {
                        AddWorkplaceAddress(workplaceId);
                        AddWorkplaceSmart(workplaceId);
                    }
                }
                else
                {
                    System.Guid workplaceId = AddWorkplace(System.Guid.Empty);
                    if (workplaceId != System.Guid.Empty)
                    {
                        AddWorkplaceAddress(workplaceId);
                        AddWorkplaceSmart(workplaceId);
                    }
                }
                if (this.WorkplaceID != System.Guid.Empty)
                {
                    txtLoc.ReadOnly = true;
                    txtLoc.BackColor = Color.LightYellow;

                    databind(this.WorkplaceID);
                    MessageBox.Show("Save successfully!", "Save Result");

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        #region AddNew
        private void AddNew()
        {
            txtLoc.BackColor = Color.LightSkyBlue;
            txtLoc.ReadOnly = false;
            toolBarDelete.Enabled = false;
            SetTextBox();
            FillComo();
            this.WorkplaceID = System.Guid.Empty;
        }
        #endregion

        #region Delete
        private void Delete()
        {
            RT2008.DAL.Workplace workplace = RT2008.DAL.Workplace.Load(this.WorkplaceID);
            if (workplace != null)
            {
                switch ((int)workplace.Status)
                {
                    case (int)Common.Enums.Status.Active:
                        workplace.Status = Convert.ToInt32(Common.Enums.Status.Deleted.ToString("d"));
                        workplace.Retired = true;
                        workplace.RetiredOn = DateTime.Now;
                        workplace.RetiredBy = Common.Config.CurrentUserId;
                        workplace.Save();
                        break;
                    case (int)Common.Enums.Status.Draft:
                        WorkplaceAddress workplaceAddress = WorkplaceAddress.LoadWhere(" WorkplaceId = '" + this.WorkplaceID.ToString() + "'");
                        if (workplaceAddress != null)
                        {
                            workplaceAddress.Delete();
                        }

                        WorkplaceSmartTagCollection WorkplaceSmartTagCol = WorkplaceSmartTag.LoadCollection(" WorkplaceId = '" + this.WorkplaceID.ToString() + "'");
                        if (WorkplaceSmartTagCol.Count > 0)
                        {
                            foreach (WorkplaceSmartTag workplaceSmartTag in WorkplaceSmartTagCol)
                            {
                                workplaceSmartTag.Delete();
                            }
                        }

                        workplace.Delete();
                        break;
                }
            }

            MessageBox.Show("Delete succeeded.");
        }
        #endregion

        #region Find
        private void Find()
        {
            WorkplaceFind workplaceFind = new WorkplaceFind();
            workplaceFind.Closed += new EventHandler(workplaceFind_Closed);
            workplaceFind.ShowDialog();
        }

        void workplaceFind_Closed(object sender, EventArgs e)
        {
            WorkplaceFind workplaceFind = sender as WorkplaceFind;
            if (workplaceFind.IsCompleted)
            {
                this.WorkplaceID = workplaceFind.WorkplaceID;
                databind(this.WorkplaceID);
            }
        }
        #endregion

        #region SetRefresh
        private void SetRefresh()
        {
            txtLoc.ReadOnly = true;
            txtLoc.BackColor = Color.LightYellow;
            databind(System.Guid.Empty);
        }
        #endregion

        #region AddWorkplace
        private bool CheckWorkplaceExists()
        {
            RT2008.DAL.Workplace workplace = RT2008.DAL.Workplace.LoadWhere("WorkplaceCode = '" + txtLoc.Text.Trim() + "'");
            if (workplace != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Guid AddWorkplace(Guid workplaceId)
        {
            bool isNew = false;

            RT2008.DAL.Workplace workplace = RT2008.DAL.Workplace.Load(workplaceId);
            if (workplace == null)
            {
                workplace = new RT2008.DAL.Workplace();

                workplace.WorkplaceCode = txtLoc.Text.Trim();
                workplace.CreatedOn = DateTime.Now;
                workplace.CreatedBy = Common.Config.CurrentUserId;
                workplace.Status = Convert.ToInt32(Common.Enums.Status.Active.ToString("d"));
                isNew = true;

                if (CheckWorkplaceExists())
                {
                    errorProvider.SetError(txtLoc, string.Format(Resources.Common.DuplicatedCode, "Workplace Code"));
                    txtLoc.ReadOnly = false;
                    return System.Guid.Empty;
                }
                else
                {
                    errorProvider.SetError(txtLoc, string.Empty);
                }
            }
            workplace.WorkplaceInitial = txtInitial.Text.Trim();
            workplace.WorkplaceName = txtName.Text.Trim();
            workplace.WorkplaceName_Chs = txtNameChs.Text.Trim();
            workplace.WorkplaceName_Cht = txtNameCht.Text.Trim();
            workplace.AlternateWorkplaceCode = txtAltWorkplaceNum.Text;
            workplace.Priority = Convert.ToInt32((txtPriority.Text.Length == 0) ? "0" : txtPriority.Text.Trim());
            workplace.Email = txtEmail.Text.Trim();
            workplace.Password = txtPassword.Text.Trim();
            workplace.ModifiedOn = DateTime.Now;
            workplace.ModifiedBy = Common.Config.CurrentUserId;
            workplace.NatureId = new Guid(((System.Web.UI.WebControls.ListItem)cmbNature.SelectedItem).Value);
            workplace.ZoneId = (System.Guid)cmbZone.SelectedValue;
            workplace.LineOfOperationId = Common.Utility.IsGUID(cmbLOO.SelectedValue.ToString()) ? new System.Guid(cmbLOO.SelectedValue.ToString()) : System.Guid.Empty;
            workplace.DownloadToCounter = true;
            workplace.DownloadToPOS = true;
            workplace.DownloadToShop = true;

            if (!isNew)
            {
                workplace.Status = Convert.ToInt32(Common.Enums.Status.Modified.ToString("d"));
            }

            workplace.Save();
            this.WorkplaceID = workplace.WorkplaceId;

            return workplace.WorkplaceId;
        }
        #endregion

        #region AddWorkplaceAddress
        private void AddWorkplaceAddress(System.Guid workplaceID)
        {
            WorkplaceAddress workplaceAddress = WorkplaceAddress.LoadWhere(" WorkplaceId = '" + workplaceID.ToString() + "'");
            if (workplaceAddress == null)
            {
                workplaceAddress = new WorkplaceAddress();
            }
            workplaceAddress.WorkplaceId = workplaceID;
            workplaceAddress.Address = txtAddress.Text.Trim();
            workplaceAddress.PostalCode = txtPostal.Text.Trim();
            workplaceAddress.District = txtDistrict.Text.Trim();
            workplaceAddress.CountryId = Common.Utility.IsGUID(cmbCountry.SelectedValue.ToString()) ? new Guid(cmbCountry.SelectedValue.ToString()) : System.Guid.Empty;
            workplaceAddress.ProvinceId = Common.Utility.IsGUID(cmbProvince.SelectedValue.ToString()) ? new Guid(cmbProvince.SelectedValue.ToString()) : System.Guid.Empty;
            workplaceAddress.CityId = Common.Utility.IsGUID(cmbCity.SelectedValue.ToString()) ? new Guid(cmbCity.SelectedValue.ToString()) : System.Guid.Empty;

            // Phone Tag 1
            System.Guid phoneTag1Id = System.Guid.Empty;
            RT2008.DAL.PhoneTag tag1 = RT2008.DAL.PhoneTag.LoadWhere("PhoneCode = 'Work' AND Priority = 1");
            if (tag1 != null)
            {
                phoneTag1Id = tag1.PhoneTagId;
            }

            workplaceAddress.PhoneTag1 = phoneTag1Id;
            workplaceAddress.PhoneTag1Value = txtPhoneTag1.Text;

            // Phone Tag 3
            System.Guid phoneTag3Id = System.Guid.Empty;
            RT2008.DAL.PhoneTag tag3 = RT2008.DAL.PhoneTag.LoadWhere("PhoneCode = 'Fax' AND Priority = 3");
            if (tag3 != null)
            {
                phoneTag3Id = tag3.PhoneTagId;
            }

            workplaceAddress.PhoneTag3 = phoneTag3Id;
            workplaceAddress.Phonetage3Value = txtPhoneTag3.Text;

            // Phone Tag 4
            System.Guid phoneTag4Id = System.Guid.Empty;
            RT2008.DAL.PhoneTag tag4 = RT2008.DAL.PhoneTag.LoadWhere("PhoneCode = 'Other' AND Priority = 4");
            if (tag4 != null)
            {
                phoneTag4Id = tag4.PhoneTagId;
            }

            workplaceAddress.PhoneTag4 = phoneTag4Id;
            workplaceAddress.PhoneTag4Value = txtPhoneTag4.Text;
            workplaceAddress.Save();
        }
        #endregion

        #region AddWorkplaceSmart
        private void AddWorkplaceSmart(Guid workplaceId)
        {
            string sql = " WorkplaceId = '{0}'  AND TagId = '{1}'";

            // Smart Tag 1
            WorkplaceSmartTag oTag1 = WorkplaceSmartTag.LoadWhere(string.Format(sql, workplaceId.ToString(), txtSmartTag1.Tag.ToString()));
            if (oTag1 == null)
            {
                oTag1 = new WorkplaceSmartTag();
                oTag1.WorkplaceId = workplaceId;
                oTag1.TagId = (txtSmartTag1.Tag == null) ? System.Guid.Empty : new System.Guid(txtSmartTag1.Tag.ToString());
            }
            oTag1.SmartTagValue = txtSmartTag1.Text;
            oTag1.Save();

            // Smart Tag 2
            WorkplaceSmartTag oTag2 = WorkplaceSmartTag.LoadWhere(string.Format(sql, workplaceId.ToString(), txtSmartTag2.Tag.ToString()));
            if (oTag2 == null)
            {
                oTag2 = new WorkplaceSmartTag();
                oTag2.WorkplaceId = workplaceId;
                oTag2.TagId = (txtSmartTag2.Tag == null) ? System.Guid.Empty : new System.Guid(txtSmartTag2.Tag.ToString());
            }
            oTag2.SmartTagValue = txtSmartTag2.Text;
            oTag2.Save();
        }
        #endregion

        #region FillCountry
        private void FillCountry()
        {
            cmbCountry.DataSource = null;
            cmbCountry.Items.Clear();

            DAL.Country.LoadCombo(ref cmbCountry, "CountryName", false, true, String.Empty, String.Empty);
            cmbCountry.SelectedIndex = 0;

            cmbProvince.DataSource = null;
            cmbProvince.Items.Clear();
            cmbCity.DataSource = null;
            cmbCity.Items.Clear();
        }
        #endregion

        #region FillProvince
        private void FillProvince(System.Guid CountryId)
        {
            cmbProvince.DataSource = null;
            cmbProvince.Items.Clear();

            string sql = " CountryId = '" + CountryId.ToString() + "'";
            DAL.Province.LoadCombo(ref cmbProvince, "ProvinceName", false, true, String.Empty, sql);
            cmbProvince.SelectedIndex = 0;

            cmbCity.DataSource = null;
            cmbCity.Items.Clear();
        }
        #endregion

        #region FillCity
        private void FillCity(System.Guid ProvinceId)
        {
            cmbCity.DataSource = null;
            cmbCity.Items.Clear();

            string sql = " ProvinceId = '" + ProvinceId.ToString() + "'";
            DAL.City.LoadCombo(ref cmbCity, "CityName", false, true, String.Empty, sql);
            cmbCity.SelectedIndex = 0;
        }
        #endregion

        #region SetNature
        private void SetNature()
        {
            cmbNature.DataSource = null;
            cmbNature.Items.Clear();
            WorkplaceNatureCollection NatureCol = WorkplaceNature.LoadCollection();
            foreach (WorkplaceNature nature in NatureCol)
            {
                string text = "   " + nature.NatureCode + "  -" + nature.NatureName;
                string value = nature.NatureId.ToString();
                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(text, value);
                cmbNature.Items.Add(item);
            }
            cmbNature.SelectedIndex = 0;

        }
        #endregion

        #region SetZone
        private void SetZone()
        {
            cmbZone.DataSource = null;
            cmbZone.Items.Clear();

            WorkplaceZone.LoadCombo(ref cmbZone, "ZoneName", false);
            if (cmbZone.Items.Count > 0) cmbZone.SelectedIndex = 0;
        }
        #endregion

        #region SetLineOfOperation
        private void SetLineOfOperation()
        {
            cmbLOO.DataSource = null;
            cmbLOO.Items.Clear();

            LineOfOperation.LoadCombo(ref cmbLOO, "LineOfOperationName", false, true, String.Empty, String.Empty);
            cmbLOO.SelectedIndex = 0;
        }
        #endregion

        #region SetCustomerInfo
        private void SetCustomerInfo()
        {
            cboSmartTag6.DataSource = null;
            MemberCollection memberList = RT2008.DAL.Member.LoadCollection();
            memberList.Add(new RT2008.DAL.Member());
            cboSmartTag6.Items.Clear();
            cboSmartTag6.DataSource = memberList;
            cboSmartTag6.DisplayMember = "FullName";
            cboSmartTag6.ValueMember = "MemberId";
            cboSmartTag6.SelectedIndex = cboSmartTag6.Items.Count - 1;
        }
        #endregion

        #region Get Priority
        private int GetNextPriority()
        {
            int priority = 1;
            string sql = "SELECT MAX(priority) + 1 FROM Workplace";
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = Common.Config.CommandTimeout;
            cmd.CommandType= CommandType.Text;

            using (SqlDataReader reader = SqlHelper.Default.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    priority = reader.GetInt32(0);
                }
            }

            return priority;
        }
        #endregion

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            switch (e.Button.Tag.ToString().ToLower())
            {
                case "save":
                    MessageBox.Show("Save Record?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveMessageHandler));
                    break;
                case "savenew":
                    MessageBox.Show("Save Record?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveNewMessageHandler));
                    break;
                case "saveclose":
                    MessageBox.Show("Save Record And Close?", "Save Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(SaveCloseMessageHandler));
                    break;
                case "delete":
                    MessageBox.Show("Delete Record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(DeleteMessageHandler));
                    break;
            }
        }

        private void SaveMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save() && this.WorkplaceID != System.Guid.Empty)
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<DefaultList>();
                }
            }
        }

        private void SaveNewMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save() && this.WorkplaceID != System.Guid.Empty)
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<DefaultList>();
                    AddNew();
                }
            }
        }

        private void SaveCloseMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                if (Save() && this.WorkplaceID != System.Guid.Empty)
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<DefaultList>();
                    this.Close();
                }
            }
        }

        private void DeleteMessageHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                Delete();
                // 2008-01-21 David: close the dialog window after delete the record
                //AddNew(); 
                this.Close();
            }
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCountry.SelectedValue != null)
            {
                if (Common.Utility.IsGUID(cmbCountry.SelectedValue.ToString()))
                {
                    FillProvince((System.Guid)cmbCountry.SelectedValue);
                }
            }
        }

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvince.SelectedValue != null)
            {
                if (Common.Utility.IsGUID(cmbProvince.SelectedValue.ToString()))
                {
                    FillCity((System.Guid)cmbProvince.SelectedValue);
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.WorkplaceID != System.Guid.Empty)
            {
                if (txtLoc.ReadOnly)
                {
                    ChangePassword changePassword = new ChangePassword(this.WorkplaceID);
                    changePassword.Closed += new EventHandler(changePassword_Closed);
                    changePassword.ShowDialog();
                }
                else
                {
                    ChangePassword changePassword = new ChangePassword(System.Guid.Empty);
                    changePassword.Closed += new EventHandler(changePassword_Closed);
                    changePassword.ShowDialog();
                }
            }
            else
            {
                ChangePassword changePassword = new ChangePassword(System.Guid.Empty);
                changePassword.Closed += new EventHandler(changePassword_Closed);
                changePassword.ShowDialog();
            }
        }

        void changePassword_Closed(object sender, EventArgs e)
        {
            ChangePassword changePassword = sender as ChangePassword;
            if (changePassword.IsCompleted)
            {
                txtPassword.Text = changePassword.Password;
            }
        }
    }
}