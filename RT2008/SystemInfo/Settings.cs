﻿using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

using Gizmox.WebGUI.Server;
using Gizmox.WebGUI.Forms;

using RT2008.DAL;
using RT2008.Controls;
using System.Drawing;

namespace RT2008.SystemInfo
{
    /// <summary>
    /// System Settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Queuings the tx number.
        /// </summary>
        /// <param name="txType">Type of the transactions.</param>
        /// <returns></returns>
        public static string QueuingTxNumber(Common.Enums.TxType txType)
        {
            return QueuingTxNumber<Common.Enums.TxType>(txType);
        }

        /// <summary>
        /// Queuings the tx number.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeName">Name of the type. Enums: Common.Enums.TxType, Common.Enums.POType</param>
        /// <returns>TxNumber</returns>
        public static string QueuingTxNumber<T>(T typeName)
        {
            string query = " 1 = 1 ";
            string queuingType = string.Empty;

            switch (typeName.GetType().Name)
            {
                case "TxType":
                    Common.Enums.TxType txType = (Common.Enums.TxType)Convert.ChangeType(typeName, typeof(Common.Enums.TxType));
                    query = "QueuingType = '" + txType.ToString() + "'";
                    queuingType = txType.ToString();
                    break;
                case "POType":
                    Common.Enums.POType poType = (Common.Enums.POType)Convert.ChangeType(typeName, typeof(Common.Enums.POType));
                    query = "QueuingType = '" + poType.ToString() + "'";
                    queuingType = poType.ToString();
                    break;
            }

            long queuedTxNumber = 1;

            SystemQueue oQueue = SystemQueue.LoadWhere(query);
            if (oQueue == null)
            {
                oQueue = new SystemQueue();
                oQueue.QueuingType = queuingType;
                oQueue.LastNumber = "000000000000";
            }

            queuedTxNumber = Convert.ToInt64(oQueue.LastNumber) + 1;

            oQueue.LastNumber = queuedTxNumber.ToString();
            oQueue.Save();

            return queuedTxNumber.ToString().PadLeft(12, '0');
        }

        /// <summary>
        /// Sets the cookies.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetCookies(string key, string value)
        {
            System.Web.HttpCookie oCookie = new System.Web.HttpCookie(key);
            DateTime now = DateTime.Now;

            oCookie.Value = value;
            oCookie.Expires = now.AddYears(1);

            System.Web.HttpContext.Current.Response.Cookies.Add(oCookie);
        }

        /// <summary>
        /// Gets the system label by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetSystemLabelByKey(string key)
        {
            string result = key;
            //string sql = "LanguageCode = '" + VWGContext.Current.CurrentUICulture.ToString() + "'";
            //string sql = "LanguageCode = '" + (string)System.Web.HttpContext.Current.Session["UserLanguage"] + "'";
            string sql = "LanguageCode = '" + Common.Config.CurrentLanguageCode + "'";

            SystemLabel oLabel = SystemLabel.LoadWhere(sql);
            if (oLabel != null)
            {
                PropertyInfo pi = oLabel.GetType().GetProperty(key.Trim().ToUpper());
                if (pi != null)
                {
                    result = pi.GetValue(oLabel, null).ToString();
                }
            }

            //throw new Exception(string.Format("Key: {0}; Result: {1}; Language: {2}", key, result, sql));
            
            //if (result.CompareTo(key) == 0)
            //{
            //    result = RT2008.Controls.Utility.Dictionary.GetWord(key);
            //}

            return result;
        }

        /// <summary>
        /// Convert the datetime value to string with time or without.
        /// If the value is equaled to 1900-01-01, it would return a emty value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="withTime"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime value, bool withTime)
        {
            string formatString = GetDateFormat();
            if (withTime)
            {
                formatString = GetDateTimeFormat();
            }

            if (!value.Equals(new DateTime(1900, 1, 1)))
            {
                return value.ToString(formatString);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the date format.
        /// </summary>
        /// <returns></returns>
        public static string GetDateFormat()
        {
            string result = String.Empty;

            switch (VWGContext.Current.CurrentUICulture.ToString())
            {
                case "zh-CHS":
                    result = "yyyy-MM-dd";
                    break;
                case "zh-CHT":
                    result = "dd/MM/yyyy";
                    break;
                case "en-US":
                default:
                    result = "dd/MM/yyyy";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Gets the date time format.
        /// </summary>
        /// <returns></returns>
        public static string GetDateTimeFormat()
        {
            string result = String.Empty;

            switch (VWGContext.Current.CurrentUICulture.ToString())
            {
                case "zh-CHS":
                    result = "yyyy-MM-dd HH:mm";
                    break;
                case "zh-CHT":
                    result = "dd/MM/yyyy HH:mm";
                    break;
                case "en-US":
                default:
                    result = "dd/MM/yyyy HH:mm";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Gets the qty decimal point.
        /// </summary>
        /// <returns></returns>
        public static int GetQtyDecimalPoint()
        {
            int result = 0;

            return result;
        }

        #region Preference
        /// <summary>
        /// Determines whether the specified page has preference.
        /// </summary>
        /// <param name="pageId">The page id.</param>
        /// <returns>
        /// 	<c>true</c> if the specified page has preference; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPreference(Guid pageId)
        {
            string sql = "StaffId = '" + Common.Config.CurrentUserId.ToString() + "' AND PageId = '" + pageId.ToString() + "'";
            StaffPreference staffPreference = StaffPreference.LoadWhere(sql);
            if (staffPreference != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Refresh Main List View Panel

        /// <summary>
        /// Refreshes the main list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RefreshMainList<T>()
        {
            Desktop desktop = VWGContext.Current.MainForm as Desktop;

            Gizmox.WebGUI.Forms.Control[] ctrlList = desktop.Controls.Find("wspPane", true);
            if (ctrlList.Length > 0)
            {
                for (int i = 0; i < ctrlList[0].Controls.Count; i++)
                {
                    if (ctrlList[0].Controls[i].GetType().Equals(typeof(T)))
                    {
                        T list = (T)Convert.ChangeType(ctrlList[0].Controls[i], typeof(T));
                        IRTList rtList = list as IRTList;
                        rtList.BindRTList(true);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        public static byte[] ReadFully(Stream stream, long initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }
    }

    /// <summary>
    /// Backgroud color for controls
    /// </summary>
    public class ControlBackColor
    {
        /// <summary>
        /// Gets the back color for disabled box.
        /// </summary>
        /// <value>the back color.</value>
        public static Color DisabledBox
        {
            get
            {
                return Color.LightYellow;
            }
        }

        /// <summary>
        /// Gets the back color required box.
        /// </summary>
        /// <value>the back color.</value>
        public static Color RequiredBox
        {
            get
            {
                return Color.PaleTurquoise;
            }
        }
    }
}
