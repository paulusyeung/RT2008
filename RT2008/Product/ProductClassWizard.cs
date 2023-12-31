#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using RT2008.DAL;
using Gizmox.WebGUI.Common.Resources;
using System.Collections;
using RT2008.Controls;

#endregion

namespace RT2008.Product
{
    public partial class ProductClassWizard : Form
    {
        public ProductClassWizard(Type className)
        {
            InitializeComponent();
            InitClass(className);
            SetCtrlEditable();
            SetToolBar();
            FillParentClassList();
            SetCaptions();
        }

        public ProductClassWizard(Guid classId)
        {
            InitializeComponent();
            this.ClassId = classId;
            SetCtrlEditable();
            SetToolBar();
            FillParentClassList();
            LoadClass();
            SetCaptions();
        }

        #region Initialize Class
        private void InitClass(Type className)
        {
            object objClass = Activator.CreateInstance(className);

            if (objClass.GetType().Equals(typeof(ProductClass1)))
            {
                Class1 = objClass as ProductClass1;
            }

            if (objClass.GetType().Equals(typeof(ProductClass2)))
            {
                Class2 = objClass as ProductClass2;
            }

            if (objClass.GetType().Equals(typeof(ProductClass3)))
            {
                Class3 = objClass as ProductClass3;
            }

            if (objClass.GetType().Equals(typeof(ProductClass4)))
            {
                Class4 = objClass as ProductClass4;
            }

            if (objClass.GetType().Equals(typeof(ProductClass5)))
            {
                Class5 = objClass as ProductClass5;
            }

            if (objClass.GetType().Equals(typeof(ProductClass6)))
            {
                Class6 = objClass as ProductClass6;
            }
        }
        #endregion

        #region Properties
        private Guid classId = System.Guid.Empty;
        public Guid ClassId
        {
            get
            {
                return classId;
            }
            set
            {
                classId = value;
            }
        }

        private ProductClass1 class1 = null;
        public ProductClass1 Class1
        {
            get
            {
                return class1;
            }
            set
            {
                class1 = value;
            }
        }

        private ProductClass2 class2 = null;
        public ProductClass2 Class2
        {
            get
            {
                return class2;
            }
            set
            {
                class2 = value;
            }
        }

        private ProductClass3 class3 = null;
        public ProductClass3 Class3
        {
            get
            {
                return class3;
            }
            set
            {
                class3 = value;
            }
        }

        private ProductClass4 class4 = null;
        public ProductClass4 Class4
        {
            get
            {
                return class4;
            }
            set
            {
                class4 = value;
            }
        }

        private ProductClass5 class5 = null;
        public ProductClass5 Class5
        {
            get
            {
                return class5;
            }
            set
            {
                class5 = value;
            }
        }

        private ProductClass6 class6 = null;
        public ProductClass6 Class6
        {
            get
            {
                return class6;
            }
            set
            {
                class6 = value;
            }
        }
        #endregion

        #region ToolBar
        private void SetCtrlEditable()
        {
            txtCode.BackColor = (this.ClassId == System.Guid.Empty) ? Color.LightSkyBlue : Color.LightYellow;
            txtCode.ReadOnly = (this.ClassId != System.Guid.Empty);

            ClearError();
        }

        private void ClearError()
        {
            errorProvider.SetError(txtCode, string.Empty);
        }

        private void SetCaptions()
        {
            #region 配置彈出的 Windows 的 Title 名稱
            if (this.Class1 != null)
            {
                this.Text += " [" + RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class1") + "] ";
            }

            if (this.Class2 != null)
            {
                this.Text += " [" + RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class2") + "] ";
            }

            if (this.Class3 != null)
            {
                this.Text += " [" + RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class3") + "] ";
            }

            if (this.Class4 != null)
            {
                this.Text += " [" + RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class4") + "] ";
            }

            if (this.Class5 != null)
            {
                this.Text += " [" + RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class5") + "] ";
            }

            if (this.Class6 != null)
            {
                this.Text += " [" + RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class6") + "] ";
            }
            #endregion

            lblCode.Text = Utility.Dictionary.GetWordWithColon("Code");
            lblInitial.Text = Utility.Dictionary.GetWordWithColon("description_short");
            lblName.Text = Utility.Dictionary.GetWordWithColon("description");
            lblNameChs.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Chs") + ")" + Utility.Dictionary.GetColon();
            lblNameCht.Text = Utility.Dictionary.GetWord("description") + " (" + Utility.Dictionary.GetWord("Cht") + ")" + Utility.Dictionary.GetColon();
            lblAltClass.Text = Utility.Dictionary.GetWordWithColon("alternate");
            lblParentClass.Text = Utility.Dictionary.GetWordWithColon("parent_class");
            lblLastUpdate.Text = Utility.Dictionary.GetWordWithColon("ModifiedOn");
            lblCreatedOn.Text = Utility.Dictionary.GetWordWithColon("CreatedOn");
        }

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

            if (ClassId == System.Guid.Empty)
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
                        this.Update();
                        break;
                    case "save":
                        Save();
                        LoadClass();
                        this.Update();
                        break;
                    case "refresh":
                        this.Update();
                        break;
                    case "delete":
                        MessageBox.Show("Delete Record?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, new EventHandler(DeleteConfirmationHandler));
                        break;
                }
            }
        }
        #endregion

        #region Fill Combo List
        private void FillParentClassList()
        {
            cboParentClass.DataSource = null;
            cboAltClass.DataSource = null;

            cboParentClass.Items.Clear();
            cboAltClass.Items.Clear();

            if (Class1 != null)
            {
                cboParentClass.DataSource = GetA1List();
                cboParentClass.DisplayMember = "Class1Code";
                cboParentClass.ValueMember = "Class1Id";

                cboAltClass.DataSource = GetA1List();
                cboAltClass.DisplayMember = "Class1Code";
                cboAltClass.ValueMember = "Class1Id";
            }
            else if (Class2 != null)
            {
                cboParentClass.DataSource = GetA2List();
                cboParentClass.DisplayMember = "Class2Code";
                cboParentClass.ValueMember = "Class2Id";

                cboAltClass.DataSource = GetA2List();
                cboAltClass.DisplayMember = "Class2Code";
                cboAltClass.ValueMember = "Class2Id";
            }
            else if (Class3 != null)
            {
                cboParentClass.DataSource = GetA3List();
                cboParentClass.DisplayMember = "Class3Code";
                cboParentClass.ValueMember = "Class3Id";

                cboAltClass.DataSource = GetA3List();
                cboAltClass.DisplayMember = "Class3Code";
                cboAltClass.ValueMember = "Class3Id";
            }
            else if (Class4 != null)
            {
                cboParentClass.DataSource = GetA4List();
                cboParentClass.DisplayMember = "Class4Code";
                cboParentClass.ValueMember = "Class4Id";

                cboAltClass.DataSource = GetA4List();
                cboAltClass.DisplayMember = "Class4Code";
                cboAltClass.ValueMember = "Class4Id";
            }
            else if (Class5 != null)
            {
                cboParentClass.DataSource = GetA5List();
                cboParentClass.DisplayMember = "Class5Code";
                cboParentClass.ValueMember = "Class5Id";

                cboAltClass.DataSource = GetA5List();
                cboAltClass.DisplayMember = "Class5Code";
                cboAltClass.ValueMember = "Class5Id";
            }
            else if (Class6 != null)
            {
                cboParentClass.DataSource = GetA6List();
                cboParentClass.DisplayMember = "Class6Code";
                cboParentClass.ValueMember = "Class6Id";

                cboAltClass.DataSource = GetA6List();
                cboAltClass.DisplayMember = "Class6Code";
                cboAltClass.ValueMember = "Class6Id";
            }

            cboParentClass.SelectedIndex = cboParentClass.Items.Count - 1;
            cboAltClass.SelectedIndex = cboParentClass.Items.Count - 1;
        }

        private ProductClass1Collection GetA1List()
        {
            string sql = "Class1Id NOT IN ('" + this.ClassId.ToString() + "')";
            string[] orderBy = new string[] { "Class1Code" };
            ProductClass1Collection oProductClassList = ProductClass1.LoadCollection(sql, orderBy, true);
            oProductClassList.Add(new ProductClass1());

            return oProductClassList;
        }

        private ProductClass2Collection GetA2List()
        {
            string sql = "Class2Id NOT IN ('" + this.ClassId.ToString() + "')";
            string[] orderBy = new string[] { "Class2Code" };
            ProductClass2Collection oProductClassList = ProductClass2.LoadCollection(sql, orderBy, true);
            oProductClassList.Add(new ProductClass2());

            return oProductClassList;
        }

        private ProductClass3Collection GetA3List()
        {
            string sql = "Class3Id NOT IN ('" + this.ClassId.ToString() + "')";
            string[] orderBy = new string[] { "Class3Code" };
            ProductClass3Collection oProductClassList = ProductClass3.LoadCollection(sql, orderBy, true);
            oProductClassList.Add(new ProductClass3());

            return oProductClassList;
        }

        private ProductClass4Collection GetA4List()
        {
            string sql = "Class4Id NOT IN ('" + this.ClassId.ToString() + "')";
            string[] orderBy = new string[] { "Class4Code" };
            ProductClass4Collection oProductClassList = ProductClass4.LoadCollection(sql, orderBy, true);
            oProductClassList.Add(new ProductClass4());

            return oProductClassList;
        }

        private ProductClass5Collection GetA5List()
        {
            string sql = "Class5Id NOT IN ('" + this.ClassId.ToString() + "')";
            string[] orderBy = new string[] { "Class5Code" };
            ProductClass5Collection oProductClassList = ProductClass5.LoadCollection(sql, orderBy, true);
            oProductClassList.Add(new ProductClass5());

            return oProductClassList;
        }

        private ProductClass6Collection GetA6List()
        {
            string sql = "Class6Id NOT IN ('" + this.ClassId.ToString() + "')";
            string[] orderBy = new string[] { "Class6Code" };
            ProductClass6Collection oProductClassList = ProductClass6.LoadCollection(sql, orderBy, true);
            oProductClassList.Add(new ProductClass6());

            return oProductClassList;
        }
        #endregion

        #region Save

        private void Save()
        {
            if (txtCode.Text.Length == 0)
            {
                errorProvider.SetError(txtCode, "Cannot be blank!");
            }
            else
            {
                errorProvider.SetError(txtCode, string.Empty);

                SaveClass1();
                SaveClass2();
                SaveClass3();
                SaveClass4();
                SaveClass5();
                SaveClass6();

                if (this.ClassId != System.Guid.Empty)
                {
                    RT2008.SystemInfo.Settings.RefreshMainList<DefaultClassList>();
                }
            }
        }

        private bool SaveClass1()
        {
            bool isNew = false;
            if (this.Class1 != null)
            {
                if (this.ClassId != System.Guid.Empty)
                {
                    this.Class1 = ProductClass1.Load(this.ClassId);
                }
                else
                {
                    string sql = "Class1Code = '" + txtCode.Text + "'";
                    ProductClass1Collection oC1 = ProductClass1.LoadCollection(sql);
                    if (oC1.Count > 0)
                    {
                        errorProvider.SetError(txtCode, string.Format(Resources.Common.DuplicatedCode, RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class1")));
                        return false;
                    }
                    else
                    {
                        this.Class1.Class1Code = txtCode.Text;
                        this.Class1.CreatedBy = Common.Config.CurrentUserId;
                        this.Class1.CreatedOn = DateTime.Now;
                        errorProvider.SetError(txtCode, string.Empty);
                    }
                    isNew = true;
                }

                this.class1.Class1Initial = txtInitial.Text;
                this.Class1.Class1Name = txtName.Text;
                this.Class1.Class1Name_Chs = txtNameChs.Text;
                this.Class1.Class1Name_Cht = txtNameCht.Text;
                this.Class1.ParentClass = (cboParentClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboParentClass.SelectedValue.ToString());
                this.Class1.AlternateClass = (cboAltClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboAltClass.SelectedValue.ToString());

                this.Class1.ModifiedBy = Common.Config.CurrentUserId;
                this.Class1.ModifiedOn = DateTime.Now;
                this.Class1.Save();

                if (isNew)
                {
                    // log activity (New Record)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Create, Class1.ToString());
                }
                else
                {
                    // log activity (Update)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Update, Class1.ToString());
                }   

                this.ClassId = this.Class1.Class1Id;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SaveClass2()
        {
            bool isNew = false;
            if (this.Class2 != null)
            {
                if (this.ClassId != System.Guid.Empty)
                {
                    this.Class2 = ProductClass2.Load(this.ClassId);
                }
                else
                {
                    string sql = "Class2Code = '" + txtCode.Text + "'";
                    ProductClass2Collection oC2 = ProductClass2.LoadCollection(sql);
                    if (oC2.Count > 0)
                    {
                        errorProvider.SetError(txtCode, string.Format(Resources.Common.DuplicatedCode, RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class2")));
                        return false;
                    }
                    else
                    {
                        this.Class2.Class2Code = txtCode.Text;
                        this.Class2.CreatedBy = Common.Config.CurrentUserId;
                        this.Class2.CreatedOn = DateTime.Now;
                        errorProvider.SetError(txtCode, string.Empty);
                    }
                    isNew = true;
                }

                this.class2.Class2Initial = txtInitial.Text;
                this.Class2.Class2Name = txtName.Text;
                this.Class2.Class2Name_Chs = txtNameChs.Text;
                this.Class2.Class2Name_Cht = txtNameCht.Text;
                this.Class2.ParentClass = (cboParentClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboParentClass.SelectedValue.ToString());
                this.Class2.AlternateClass = (cboAltClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboAltClass.SelectedValue.ToString());

                this.Class2.ModifiedBy = Common.Config.CurrentUserId;
                this.Class2.ModifiedOn = DateTime.Now;
                this.Class2.Save();

                if (isNew)
                {
                    // log activity (New Record)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Create, Class2.ToString());
                }
                else
                {

                    // log activity (Update)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Update, Class2.ToString());
                }   

                this.ClassId = this.Class2.Class2Id;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SaveClass3()
        {
            bool isNew = false;
            if (this.Class3 != null)
            {
                if (this.ClassId != System.Guid.Empty)
                {
                    this.Class3 = ProductClass3.Load(this.ClassId);
                }
                else
                {
                    string sql = "Class3Code = '" + txtCode.Text + "'";
                    ProductClass3Collection oC3 = ProductClass3.LoadCollection(sql);
                    if (oC3.Count > 0)
                    {
                        errorProvider.SetError(txtCode, string.Format(Resources.Common.DuplicatedCode, RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class3")));
                        return false;
                    }
                    else
                    {
                        this.Class3.Class3Code = txtCode.Text;
                        this.Class3.CreatedBy = Common.Config.CurrentUserId;
                        this.Class3.CreatedOn = DateTime.Now;
                        errorProvider.SetError(txtCode, string.Empty);
                    }
                    isNew = true;
                }

                this.class3.Class3Initial = txtInitial.Text;
                this.Class3.Class3Name = txtName.Text;
                this.Class3.Class3Name_Chs = txtNameChs.Text;
                this.Class3.Class3Name_Cht = txtNameCht.Text;
                this.Class3.ParentClass = (cboParentClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboParentClass.SelectedValue.ToString());
                this.Class3.AlternatedClass = (cboAltClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboAltClass.SelectedValue.ToString());

                this.Class3.ModifiedBy = Common.Config.CurrentUserId;
                this.Class3.ModifiedOn = DateTime.Now;
                this.Class3.Save();

                if (isNew)
                {
                    // log activity (New Record)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Create, Class3.ToString());   
                }
                else
                {                    
                    // log activity (Update)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Update, Class3.ToString());
                }   

                this.ClassId = this.Class3.Class3Id;
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private bool SaveClass4()
        {
            bool isNew = false;
            if (this.Class4 != null)
            {
                if (this.ClassId != System.Guid.Empty)
                {
                    this.Class4 = ProductClass4.Load(this.ClassId);
                }
                else
                {
                    string sql = "Class4Code = '" + txtCode.Text + "'";
                    ProductClass4Collection oC4 = ProductClass4.LoadCollection(sql);
                    if (oC4.Count > 0)
                    {
                        errorProvider.SetError(txtCode, string.Format(Resources.Common.DuplicatedCode, RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class4")));
                        return false;
                    }
                    else
                    {
                        this.Class4.Class4Code = txtCode.Text;
                        this.Class4.CreatedBy = Common.Config.CurrentUserId;
                        this.Class4.CreatedOn = DateTime.Now;
                        errorProvider.SetError(txtCode, string.Empty);
                    }
                    isNew = true;
                }

                this.class4.Class4Initial = txtInitial.Text;
                this.Class4.Class4Name = txtName.Text;
                this.Class4.Class4Name_Chs = txtNameChs.Text;
                this.Class4.Class4Name_Cht = txtNameCht.Text;
                this.Class4.ParentClass = (cboParentClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboParentClass.SelectedValue.ToString());
                this.Class4.AlternateClass = (cboAltClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboAltClass.SelectedValue.ToString());

                this.Class4.ModifiedBy = Common.Config.CurrentUserId;
                this.Class4.ModifiedOn = DateTime.Now;
                this.Class4.Save();

                if (isNew)
                {
                    // log activity (New Record)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Create, Class4.ToString());
                }
                else
                {                    
                    // log activity (Update)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Update, Class4.ToString());
                }   

                this.ClassId = this.Class4.Class4Id;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SaveClass5()
        {
            bool isNew = false;
            if (this.Class5 != null)
            {
                if (this.ClassId != System.Guid.Empty)
                {
                    this.Class5 = ProductClass5.Load(this.ClassId);
                }
                else
                {
                    string sql = "Class5Code = '" + txtCode.Text + "'";
                    ProductClass5Collection oC5 = ProductClass5.LoadCollection(sql);
                    if (oC5.Count > 0)
                    {
                        errorProvider.SetError(txtCode, string.Format(Resources.Common.DuplicatedCode, RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class5")));
                        return false;
                    }
                    else
                    {
                        this.Class5.Class5Code = txtCode.Text;
                        this.Class5.CreatedBy = Common.Config.CurrentUserId;
                        this.Class5.CreatedOn = DateTime.Now;
                        errorProvider.SetError(txtCode, string.Empty);
                    }
                    isNew = true;
                }

                this.class5.Class5Initial = txtInitial.Text;
                this.Class5.Class5Name = txtName.Text;
                this.Class5.Class5Name_Chs = txtNameChs.Text;
                this.Class5.Class5Name_Cht = txtNameCht.Text;
                this.Class5.ParentClass = (cboParentClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboParentClass.SelectedValue.ToString());
                this.Class5.AlternateClass = (cboAltClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboAltClass.SelectedValue.ToString());

                this.Class5.ModifiedBy = Common.Config.CurrentUserId;
                this.Class5.ModifiedOn = DateTime.Now;
                this.Class5.Save();

                if (isNew)
                {
                    // log activity (New Record)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Create, Class5.ToString());
                }
                else
                {
                    // log activity (Update)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Update, Class5.ToString());
                }   

                this.ClassId = this.Class5.Class5Id;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SaveClass6()
        {
            bool isNew = false;
            if (this.Class6 != null)
            {
                if (this.ClassId != System.Guid.Empty)
                {
                    this.Class6 = ProductClass6.Load(this.ClassId);
                }
                else
                {
                    string sql = "Class6Code = '" + txtCode.Text + "'";
                    ProductClass6Collection oC6 = ProductClass6.LoadCollection(sql);
                    if (oC6.Count > 0)
                    {
                        errorProvider.SetError(txtCode, string.Format(Resources.Common.DuplicatedCode, RT2008.SystemInfo.Settings.GetSystemLabelByKey("Class6")));
                        return false;
                    }
                    else
                    {
                        this.Class6.Class6Code = txtCode.Text;
                        this.Class6.CreatedBy = Common.Config.CurrentUserId;
                        this.Class6.CreatedOn = DateTime.Now;
                        errorProvider.SetError(txtCode, string.Empty);
                    }
                    isNew = true;
                }

                this.class6.Class6Initial = txtInitial.Text;
                this.Class6.Class6Name = txtName.Text;
                this.Class6.Class6Name_Chs = txtNameChs.Text;
                this.Class6.Class6Name_Cht = txtNameCht.Text;
                this.Class6.ParentClass = (cboParentClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboParentClass.SelectedValue.ToString());
                this.Class6.AlternateClass = (cboAltClass.SelectedValue == null) ? System.Guid.Empty : new System.Guid(cboAltClass.SelectedValue.ToString());

                this.Class6.ModifiedBy = Common.Config.CurrentUserId;
                this.Class6.ModifiedOn = DateTime.Now;
                this.Class6.Save();

                if (isNew)
                {
                    // log activity (New Record)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Create, Class6.ToString());
                }
                else
                {
                    // log activity (Update)
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Update, Class6.ToString());
                    
                }   

                this.ClassId = this.Class6.Class6Id;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Clear()
        {
            this.Close();
            Type type = null;

            if (this.Class1 != null)
            {
                type = Class1.GetType();
            }

            if (this.Class2 != null)
            {
                type = Class2.GetType();
            }

            if (this.Class3 != null)
            {
                type = Class3.GetType();
            }

            if (this.Class4 != null)
            {
                type = Class4.GetType();
            }

            if (this.Class5 != null)
            {
                type = Class5.GetType();
            }

            if (this.Class6 != null)
            {
                type = Class6.GetType();
            }

            ProductClassWizard wizClass = new ProductClassWizard(type);
            wizClass.ShowDialog();
        }

        #endregion

        #region Load

        private string GetStaffName(Guid staffId)
        {
            RT2008.DAL.Staff oStaff = RT2008.DAL.Staff.Load(staffId);
            if (oStaff != null)
            {
                return oStaff.StaffNumber;
            }
            else
            {
                return string.Empty;
            }
        }

        private void LoadClass()
        {
            LoadClass1();
            LoadClass2();
            LoadClass3();
            LoadClass4();
            LoadClass5();
            LoadClass6();
        }

        private void LoadClass1()
        {
            this.Class1 = ProductClass1.Load(this.ClassId);
            if (this.Class1 != null)
            {
                this.ClassId = this.Class1.Class1Id;

                FillParentClassList();

                txtCode.Text = this.Class1.Class1Code;
                txtInitial.Text = this.Class1.Class1Initial;
                txtName.Text = this.Class1.Class1Name;
                txtNameChs.Text = this.Class1.Class1Name_Chs;
                txtNameCht.Text = this.Class1.Class1Name_Cht;
                cboParentClass.SelectedValue = this.Class1.ParentClass;
                cboAltClass.SelectedValue = this.Class1.AlternateClass;

                txtLastUpdatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class1.ModifiedOn, false);
                txtCreatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class1.CreatedOn, false);
                txtLastUpdatedBy.Text = GetStaffName(this.Class1.ModifiedBy);

                SetCtrlEditable();
            }
        }

        private void LoadClass2()
        {
            this.Class2 = ProductClass2.Load(this.ClassId);
            if (this.Class2 != null)
            {
                this.ClassId = this.Class2.Class2Id;

                FillParentClassList();

                txtCode.Text = this.Class2.Class2Code;
                txtInitial.Text = this.Class2.Class2Initial;
                txtName.Text = this.Class2.Class2Name;
                txtNameChs.Text = this.Class2.Class2Name_Chs;
                txtNameCht.Text = this.Class2.Class2Name_Cht;
                cboParentClass.SelectedValue = this.Class2.ParentClass;
                cboAltClass.SelectedValue = this.Class2.AlternateClass;

                txtLastUpdatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class2.ModifiedOn, false);
                txtCreatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class2.CreatedOn, false);
                txtLastUpdatedBy.Text = GetStaffName(this.Class2.ModifiedBy);

                SetCtrlEditable();
            }
        }

        private void LoadClass3()
        {
            this.Class3 = ProductClass3.Load(this.ClassId);
            if (this.Class3 != null)
            {
                this.ClassId = this.Class3.Class3Id;

                FillParentClassList();

                txtCode.Text = this.Class3.Class3Code;
                txtInitial.Text = this.Class3.Class3Initial;
                txtName.Text = this.Class3.Class3Name;
                txtNameChs.Text = this.Class3.Class3Name_Chs;
                txtNameCht.Text = this.Class3.Class3Name_Cht;
                cboParentClass.SelectedValue = this.Class3.ParentClass;
                cboAltClass.SelectedValue = this.Class3.AlternatedClass;

                txtLastUpdatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class3.ModifiedOn, false);
                txtCreatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class3.CreatedOn, false);
                txtLastUpdatedBy.Text = GetStaffName(this.Class3.ModifiedBy);

                SetCtrlEditable();
            }
        }

        private void LoadClass4()
        {
            this.Class4 = ProductClass4.Load(this.ClassId);
            if (this.Class4 != null)
            {
                this.ClassId = this.Class4.Class4Id;

                FillParentClassList();

                txtCode.Text = this.Class4.Class4Code;
                txtInitial.Text = this.Class4.Class4Initial;
                txtName.Text = this.Class4.Class4Name;
                txtNameChs.Text = this.Class4.Class4Name_Chs;
                txtNameCht.Text = this.Class4.Class4Name_Cht;
                cboParentClass.SelectedValue = this.Class4.ParentClass;
                cboAltClass.SelectedValue = this.Class4.AlternateClass;

                txtLastUpdatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class4.ModifiedOn, false);
                txtCreatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class4.CreatedOn, false);
                txtLastUpdatedBy.Text = GetStaffName(this.Class4.ModifiedBy);

                SetCtrlEditable();
            }
        }

        private void LoadClass5()
        {
            this.Class5 = ProductClass5.Load(this.ClassId);
            if (this.Class5 != null)
            {
                this.ClassId = this.Class5.Class5Id;

                FillParentClassList();

                txtCode.Text = this.Class5.Class5Code;
                txtInitial.Text = this.Class5.Class5Initial;
                txtName.Text = this.Class5.Class5Name;
                txtNameChs.Text = this.Class5.Class5Name_Chs;
                txtNameCht.Text = this.Class5.Class5Name_Cht;
                cboParentClass.SelectedValue = this.Class5.ParentClass;
                cboAltClass.SelectedValue = this.Class5.AlternateClass;

                txtLastUpdatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class5.ModifiedOn, false);
                txtCreatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class5.CreatedOn, false);
                txtLastUpdatedBy.Text = GetStaffName(this.Class5.ModifiedBy);

                SetCtrlEditable();
            }
        }

        private void LoadClass6()
        {
            this.Class6 = ProductClass6.Load(this.ClassId);
            if (this.Class6 != null)
            {
                this.ClassId = this.Class6.Class6Id;

                FillParentClassList();

                txtCode.Text = this.Class6.Class6Code;
                txtInitial.Text = this.Class6.Class6Initial;
                txtName.Text = this.Class6.Class6Name;
                txtNameChs.Text = this.Class6.Class6Name_Chs;
                txtNameCht.Text = this.Class6.Class6Name_Cht;
                cboParentClass.SelectedValue = this.Class6.ParentClass;
                cboAltClass.SelectedValue = this.Class6.AlternateClass;

                txtLastUpdatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class6.ModifiedOn, false);
                txtCreatedOn.Text = RT2008.SystemInfo.Settings.DateTimeToString(this.Class6.CreatedOn, false);
                txtLastUpdatedBy.Text = GetStaffName(this.Class6.ModifiedBy);

                SetCtrlEditable();
            }
        }
        #endregion

        #region Delete
        private void Delete()
        {
            try
            {
                if (this.Class1 != null)
                {
                    this.Class1.Delete();
                    // log activity
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, Class1.ToString());
                }

                if (this.Class2 != null)
                {
                    this.Class2.Delete();
                    // log activity
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, Class2.ToString());
                }

                if (this.Class3 != null)
                {
                    this.Class3.Delete();
                    // log activity
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, Class3.ToString());
                }

                if (this.Class4 != null)
                {
                    this.Class4.Delete();
                    // log activity
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, Class4.ToString());
                }

                if (this.Class5 != null)
                {
                    this.Class5.Delete();
                    // log activity
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, Class5.ToString());
                }

                if (this.Class6 != null)
                {
                    this.Class6.Delete();
                    // log activity
                    RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Delete, Class6.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Cannot delete the record being used by other record!", "Delete Warning");
            }
        }
        #endregion

        private void DeleteConfirmationHandler(object sender, EventArgs e)
        {
            if (((Form)sender).DialogResult == DialogResult.Yes)
            {
                Delete();

                this.Close();
            }
        }
    }
}