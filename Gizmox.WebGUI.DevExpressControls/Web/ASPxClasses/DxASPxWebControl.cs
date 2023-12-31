#region Using

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Gizmox.WebGUI.Forms;
using Gizmox.WebGUI.Common.Extensibility;
using Gizmox.WebGUI.Forms.Design;
using DevExpress.Web;
using DevExpress.Web.Design;
using DevExpress.Web.Data;
using DevExpress.Data;
using System.Web.UI;
using System.Collections.ObjectModel;
using System.Web.UI.WebControls;
using DevExpress.Web.Internal;
using DevExpress.Web.Rendering;
using System.Drawing;
using DevExpress.Utils.Design;
//using DevExpress.Web.Design;
using DevExpress.Utils;

#endregion

namespace Gizmox.WebGUI.DevExpressControls.Web.ASPxClasses
{
    public class DxASPxWebControl : DxASPxWebControlBase
    {
        #region C'tor

        public DxASPxWebControl()
        {
        }

        #endregion

        #region Methods

        public string GetCssClassNamePrefix()
        {
            return this.HostedASPxWebControl.GetCssClassNamePrefix();
        }

        public DefaultBoolean GetDefaultBooleanProperty(string key, DefaultBoolean defaultValue)
        {
            return this.HostedASPxWebControl.GetDefaultBooleanProperty(key, defaultValue);
        }

        //public string GetRenderCssFilePath()
        //{
        //    return this.HostedASPxWebControl.GetRenderCssFilePath();
        //}

        //public ISite GetSite()
        //{
        //    return this.HostedASPxWebControl.GetSite();
        //}

        public string HtmlEncode(string text)
        {
            return this.HostedASPxWebControl.HtmlEncode(text);
        }

        //public bool IsAutoFormatPreview()
        //{
        //    return this.HostedASPxWebControl.IsAutoFormatPreview();
        //}

        //public virtual bool IsCallBacksEnabled()
        //{
        //    return this.HostedASPxWebControl.IsCallBacksEnabled();
        //}

        public virtual bool IsClientSideAPIEnabled()
        {
            return this.HostedASPxWebControl.IsClientSideAPIEnabled();
        }

        public bool IsClientSideEventsAssigned()
        {
            return this.HostedASPxWebControl.IsClientSideEventsAssigned();
        }

        //public virtual bool IsServerSideEventsAssigned()
        //{
        //    return this.HostedASPxWebControl.IsServerSideEventsAssigned();
        //}

        public static void RegisterBaseScript(Page page)
        {
            DevExpress.Web.ASPxWebControl.RegisterBaseScript(page);
        }

        public void SetDefaultBooleanProperty(string key, DefaultBoolean defaultValue, DefaultBoolean value)
        {
            this.HostedASPxWebControl.SetDefaultBooleanProperty(key, defaultValue, value);
        }

        #endregion

        #region Events

        #endregion

        #region Properties

        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Gets the web control's background image."), Category("Appearance")]
        public virtual BackgroundImage BackgroundImage
        {
            get
            {
                return (BackgroundImage)this.GetProperty("BackgroundImage");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Appearance"), PersistenceMode(PersistenceMode.InnerProperty), Description("Gets the web control's border settings.")]
        public virtual BorderWrapper Border
        {
            get
            {
                return (BorderWrapper)this.GetProperty("Border");
            }
        }

        [Description("Gets the settings of the web control's bottom border. "), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Appearance")]
        public virtual Border BorderBottom
        {
            get
            {
                return (Border)this.GetProperty("BorderBottom");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public Color BorderColor
        {
            get
            {
                return (Color)this.GetProperty("BorderColor");
            }
            set
            {
                this.SetProperty("BorderColor", value);
            }
        }

        [Description("Gets the settings of the web control's left border. "), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerProperty), Category("Appearance")]
        public virtual Border BorderLeft
        {
            get
            {
                return (Border)this.GetProperty("BorderLeft");
            }
        }

        [Category("Appearance"), PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Description("Gets the settings of the web control's right border. ")]
        public virtual Border BorderRight
        {
            get
            {
                return (Border)this.GetProperty("BorderRight");
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Web.UI.WebControls.BorderStyle BorderStyle
        {
            get
            {
                return (System.Web.UI.WebControls.BorderStyle)this.GetProperty("BorderStyle");
            }
            set
            {
                this.SetProperty("BorderStyle", value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Category("Appearance"), PersistenceMode(PersistenceMode.InnerProperty), Description("Gets the settings of the web control's top border. ")]
        public virtual Border BorderTop
        {
            get
            {
                return (Border)this.GetProperty("BorderTop");
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Unit BorderWidth
        {
            get
            {
                return (Unit)this.GetProperty("BorderWidth");
            }
            set
            {
                this.SetProperty("BorderWidth", value);
            }
        }

        [Category("Styles"), UrlProperty, AutoFormatUrlProperty, DefaultValue(""), AutoFormatCssUrlProperty]
        public virtual string CssFilePath
        {
            get
            {
                return (string)this.GetProperty("CssFilePath");
            }
            set
            {
                this.SetProperty("CssFilePath", value);
            }
        }

        [Category("Styles"), DefaultValue(""), AutoFormatEnable]
        public virtual string CssPostfix
        {
            get
            {
                return (string)this.GetProperty("CssPostfix");
            }
            set
            {
                this.SetProperty("CssPostfix", value);
            }
        }

        //[TypeConverter(typeof(DevExpress.Web.Design.CursorConverter)), Description("Gets or sets the type of cursor to display when the mouse pointer is over the web control. "), Category("Appearance"), DefaultValue(""), AutoFormatEnable]
        //public virtual string Cursor
        //{
        //    get
        //    {
        //        return (string)this.GetProperty("Cursor");
        //    }
        //    set
        //    {
        //        this.SetProperty("Cursor", value);
        //    }
        //}

        [DefaultValue(true), Description("Gets or sets a value that indicates whether a web control is enabled, allowing it to respond to end-user interactions."), AutoFormatDisable, Category("Behavior")]
        public bool Enabled
        {
            get
            {
                return (bool)this.GetProperty("Enabled");
            }
            set
            {
                this.SetProperty("Enabled", value);
            }
        }

        [AutoFormatEnable, Description("Gets or sets a value that specifies whether the control is displayed with a predefined style or the control's appearance has to be completely defined by a developer via either css or the appropriate style properties."), Category("Appearance"), DefaultValue(true)]
        public virtual bool EnableDefaultAppearance
        {
            get
            {
                return (bool)this.GetProperty("EnableDefaultAppearance");
            }
            set
            {
                this.SetProperty("EnableDefaultAppearance", value);
            }
        }

        [Description("Gets or sets a value that specifies whether the control keeps any of its values that are HTML as HTML, or strips out the HTML markers from it instead."), Category("Behavior"), DefaultValue(true), AutoFormatDisable]
        public virtual bool EncodeHtml
        {
            get
            {
                return (bool)this.GetProperty("EncodeHtml");
            }
            set
            {
                this.SetProperty("EncodeHtml", value);
            }
        }

// 2007.12.21 paulus:
//        [AutoFormatCannotBeEmpty]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Unit Height
        {
            get
            {
                return (Unit)this.GetProperty("Height");
            }
            set
            {
                this.SetProperty("Height", value);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override bool ViewStateLoading
        {
            get
            {
                return (bool)this.GetProperty("ViewStateLoading");
            }
        }
// 2007.12.21 paulus
//        [AutoFormatCannotBeEmpty]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Unit Width
        {
            get
            {
                return (Unit)this.GetProperty("Width");
            }
            set
            {
                this.SetProperty("Width", value);
            }
        }

        #endregion

        #region AspControl Box members

        /// <summary>
        /// Return 
        /// </summary>
        protected override Type HostedControlType
        {
            get
            {
                return typeof(DevExpress.Web.ASPxWebControl);
            }
        }

        /// <summary>
        /// Returns the hosted ASPxWebControl control.
        /// </summary>
        protected DevExpress.Web.ASPxWebControl HostedASPxWebControl
        {
            get
            {
                return (DevExpress.Web.ASPxWebControl)this.HostedControl;
            }
        }

        //protected override bool IsStatelessHostedControl
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}

        #endregion
    }
}
