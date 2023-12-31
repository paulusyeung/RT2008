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

#endregion

namespace RT2008.Controls
{
    public class PhoneTag
    {
        Control upCtrl = null;
        string key = "PhoneTag{0}";

        public PhoneTag(Control ctrl)
        {
            upCtrl = ctrl;
        }

        public void SetPhoneTag()
        {
            int iCount = 1;
            string[] orderBy = new string[] { "Priority" };
            PhoneTagCollection phoneTagList = RT2008.DAL.PhoneTag.LoadCollection(orderBy, true);
            foreach (RT2008.DAL.PhoneTag oTag in phoneTagList)
            {
                SetPhoneTagLabel(string.Format(key, iCount.ToString()), oTag.PhoneTagId, oTag.PhoneName, oTag.PhoneName_Chs, oTag.PhoneName_Cht);
                iCount++;
            }
        }

        private void SetPhoneTagLabel(string key, object tag, string name, string name_chs, string name_cht)
        {
            switch (Common.Config.CurrentLanguageId)
            {
                case 2: // chs, zh-chs, zh-cn
                    name_chs = name_chs + "��";
                    break;
                case 3: // cht, zh-cht, zh-hk, zh-tw
                    name_cht = name_cht + "��";
                    break;
                case 1: // en, en-us
                default:
                    name = name + ":";
                    break;
            }

            Control Ctrl = null;
            for (int i = 0; i < upCtrl.Controls.Count; i++)
            {
                Ctrl = upCtrl.Controls[i];

                if (Ctrl != null && Ctrl.Name.Contains(key))
                {
                    if (Ctrl.GetType().Equals(typeof(Label)))
                    {
                        Label lblTag = Ctrl as Label;
                        lblTag.Text = name;
                        lblTag.Visible = true;
                    }

                    if (Ctrl.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox txtTag = Ctrl as TextBox;
                        txtTag.Tag = tag;
                        txtTag.Visible = true;
                    }

                    if (Ctrl.GetType().Equals(typeof(MaskedTextBox)))
                    {
                        MaskedTextBox txtTag = Ctrl as MaskedTextBox;
                        txtTag.Tag = tag;
                        txtTag.Visible = true;
                    }

                    if (Ctrl.GetType().Equals(typeof(ComboBox)))
                    {
                        ComboBox cboTag = Ctrl as ComboBox;
                        cboTag.Tag = tag;
                        cboTag.Visible = true;
                    }

                    if (Ctrl.GetType().Equals(typeof(DateTimePicker)))
                    {
                        DateTimePicker dtpTag = Ctrl as DateTimePicker;
                        dtpTag.Tag = tag;
                        dtpTag.Visible = true;
                    }
                }
            }
        }
    }
}