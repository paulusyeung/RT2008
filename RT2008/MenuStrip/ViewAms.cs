using Gizmox.WebGUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RT2008.MenuStrip
{
    public static class ViewAms
    {
        public static MenuItem GetLoyatyAms()
        {
            MenuItem result = new MenuItem();

            result = AmsBase.FillAms("View");

            return result;
        }
    }
}