#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Forms.Authentication;
using System.Web.Security;
using Gizmox.WebGUI.Common.Interfaces;
using RT2008.DAL;
using System.Reflection;
using RT2008.Controls;
using System.Configuration;

#endregion

namespace RT2008.Public
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Logon : LogonForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Logon"/> class.
        /// </summary>
        public Logon()
        {
            InitializeComponent();

#if (DEBUG)
            txtStaffNumber.Text = "9999";
            txtPassword.Text = "9999";
#endif
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Context.CurrentTheme = RT2008.Controls.Utility.Default.CurrentTheme;
            this.Context.Session.IsLoggedOn = false;

            Common.Config.CurrentUserId = System.Guid.Empty;

            SetAttributes();
            FillZoneList();
            VersionNumber();
        }

        private void SetAttributes()
        {
            nxStudio.BaseClass.WordDict oDict = new nxStudio.BaseClass.WordDict(Common.Config.CurrentWordDict, Common.Config.CurrentLanguageId);

            lblStaffNumber.Text = oDict.GetWordWithColon("staff_number");
            lblPassword.Text = oDict.GetWordWithColon("password");
            lblZone.Text = oDict.GetWordWithColon("zone");
            btnLogon.Text = oDict.GetWord("logon");
        }

        /// <summary>
        /// the number Version.
        /// </summary>
        private void VersionNumber()
        {
            System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            this.lblVersionNumber.Text = version.ToString();    // + " (" + Gizmox.WebGUI.WGConst.Version.ToString() + ")";
        }

        /// <summary>
        /// Fills the zone list.
        /// </summary>
        public void FillZoneList()
        {
            cboZone.Items.Clear();

            WorkplaceZone.LoadCombo(ref cboZone, "ZoneName", false, false, string.Empty, "ZoneId = '" + SystemInfo.CurrentInfo.Default.SysInfo.ZoneId.ToString() + "'");

            if (Common.Config.CurrentZoneId != System.Guid.Empty)
            {
                cboZone.SelectedValue = Common.Config.CurrentZoneId;
            }
            else
                cboZone.SelectedIndex = 0;
        }

        /// <summary>
        /// Verifies the input string.
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        {
            bool result = true;
            if (txtStaffNumber.Text.Length == 0)
            {
                errorProvider.SetError(txtStaffNumber, RT2008.Controls.Utility.Dictionary.GetWord("err_cannot_blank"));
            }
            else
            {
                errorProvider.SetError(txtStaffNumber, string.Empty);
                result = result & true;
            }

            if (txtPassword.Text.Length == 0)
            {
                errorProvider.SetError(txtPassword, RT2008.Controls.Utility.Dictionary.GetWord("err_cannot_blank"));
            }
            else
            {
                errorProvider.SetError(txtPassword, string.Empty);
                result = result & true;
            }

            if (cboZone.Text.Length == 0)
            {
                errorProvider.SetError(cboZone, RT2008.Controls.Utility.Dictionary.GetWord("err_must_select"));
            }
            else
            {
                errorProvider.SetError(cboZone, string.Empty);
                result = result & true;
            }
            return result;
        }

        /// <summary>
        /// Authes the logon.
        /// </summary>
        /// <returns></returns>
        private bool AuthLogon()
        {
            if (Verify())
            {
                string sql = "LoginName = N'" + txtStaffNumber.Text.Trim().Replace("'", "") + "' AND LoginPassword = N'" + txtPassword.Text.Trim().Replace("'", "") + "'";
                RT2008.DAL.UserProfile oUser = RT2008.DAL.UserProfile.LoadWhere(sql);
                if (oUser != null)
                {
                    RT2008.DAL.Staff oStaff = RT2008.DAL.Staff.Load(oUser.UserSid);
                    if (oStaff != null)
                    {
                        if (oStaff.Status > Convert.ToInt32(Common.Enums.Status.Inactive.ToString("d")))
                        {
                            if (!oStaff.Retired)
                            {
                                this.Context.Session.IsLoggedOn = true;

                                Common.Config.CurrentUserId = oStaff.StaffId;
                                Common.Config.CurrentZoneId = new Guid(cboZone.SelectedValue.ToString());
                                Common.Config.CurrentUserType = oUser.UserType;

                                // The below code will logout the loggedin user when idle for the time specified
                                if (ConfigurationManager.AppSettings["sessionTimeout"] != null)
                                {
                                    this.Context.HttpContext.Session.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["sessionTimeout"]);
                                }

                                RT2008.Controls.Log4net.LogInfo(RT2008.Controls.Log4net.LogAction.Login, this.ToString());
                            }
                            else
                            {
                                this.lblErrorMessage.Text = RT2008.Controls.Utility.Dictionary.GetWord("msg_retired_staff");
                                this.Context.Session.IsLoggedOn = false;
                            }
                        }
                        else
                        {
                            this.lblErrorMessage.Text = RT2008.Controls.Utility.Dictionary.GetWord("msg_inactive_staff");
                            this.Context.Session.IsLoggedOn = false;
                        }
                    }
                }
                else
                {
                    // When user inputs incorrect staff number or password, prompt user the error message.
                    // To Do: We can try to limited the times of attempt to 5 or less.
                    this.lblErrorMessage.Text = RT2008.Controls.Utility.Dictionary.GetWord("err_incorrect_staff");
                    this.Context.Session.IsLoggedOn = false;
                }
            }
            else
            {
                this.Context.Session.IsLoggedOn = false;
            }

            return this.Context.Session.IsLoggedOn;
        }

        /// <summary>
        /// Handles the Click event of the btnLogon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnLogon_Click(object sender, EventArgs e)
        {
            if (AuthLogon())
            {
                // Close the Logon form
                this.Close();
            }
        }

        /// <summary>
        /// Handles the Load event of the Logon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Logon_Load(object sender, EventArgs e)
        {
            txtStaffNumber.Focus();
        }
    }
}