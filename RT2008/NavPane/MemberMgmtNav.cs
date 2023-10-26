#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Common.Resources;
using Gizmox.WebGUI.Forms;

#endregion

namespace RT2008.NavPane
{
    public partial class MemberMgmtNav : UserControl
    {
        public MemberMgmtNav()
        {
            InitializeComponent();

            NavPane.NavMenu.FillNavTree("membermgmt", this.navMemberMgmt.Nodes);
        }

        private void navMemberMgmt_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Control[] controls = this.Form.Controls.Find("wspPane", true);
            if (controls.Length > 0)
            {
                Panel wspPane = (Panel)controls[0];
                wspPane.Text = navMemberMgmt.SelectedNode.Text;
                wspPane.BackColor = Color.FromName("#ACC0E9");
                wspPane.Controls.Clear();
                ShowWorkspace(ref wspPane, (string)navMemberMgmt.SelectedNode.Tag);
            }
        }

        private void ShowWorkspace(ref Panel wspPane, string Tag)
        {
            if (!string.IsNullOrEmpty(Tag))
            {
                switch (Tag.ToLower())
                {
                    case "membership_member":
                        //RT2008.Member.MemberList oMemberList = new RT2008.Member.MemberList();
                        //oMemberList.DockPadding.All = 6;
                        //oMemberList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oMemberList);

                        RT2008.Member.DefaultList oMemberList = new RT2008.Member.DefaultList();
                        oMemberList.DockPadding.All = 6;
                        oMemberList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oMemberList);
                        break;
                    case "membercare_phonebook":
                        //RT2008.Member.PhoneBookList oPhoneBookList = new RT2008.Member.PhoneBookList();
                        //oPhoneBookList.DockPadding.All = 6;
                        //oPhoneBookList.Dock = DockStyle.Fill;
                        //wspPane.Controls.Add(oPhoneBookList);

                        RT2008.Member.DefaultPhoneBookList oPhoneBookList = new RT2008.Member.DefaultPhoneBookList();
                        oPhoneBookList.DockPadding.All = 6;
                        oPhoneBookList.Dock = DockStyle.Fill;
                        wspPane.Controls.Add(oPhoneBookList);
                        break;
                }
            }
        }
    }
}