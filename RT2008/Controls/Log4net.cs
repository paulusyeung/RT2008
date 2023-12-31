﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using log4net;

namespace RT2008.Controls
{
    public class Log4net
    {
        // private static readonly ILog _log = LogManager.GetLogger("MyLoggerName");
        // 使用 System.Reflection.MethodBase.GetCurrentMethod().DeclaringType => logger 命名 = xPort3.Controls.Log4net
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void LogError(String message, Exception e)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(message, e);
            }
        }

        public static void LogWarn(String message, Exception e)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(message, e);
            }
        }

        public static void LogInfo(String message)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info(message);
            }
        }
        public static void LogInfo(LogAction action, String message)
        {
            String sql = String.Format("UserSid = '{0}'", RT2008.DAL.Common.Config.CurrentUserId.ToString());
            RT2008.DAL.UserProfile user = RT2008.DAL.UserProfile.LoadWhere(sql);
            if (user != null)
                LogInfo(String.Format("[{0}] [{1}] {2}", user.Alias, action.ToString("g").ToUpper().PadRight(7), message));
        }

        public static void LogDebug(String message)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(message);
            }
        }

        public enum LogAction
        {
            Login,
            Logout,
            Create,
            Read,
            Update,
            Delete,
            Print,
            Approve
        }
    }
}
