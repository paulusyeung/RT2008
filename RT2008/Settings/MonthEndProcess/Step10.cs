﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RT2008.DAL;
using RT2008.Controls;

namespace RT2008.Settings.MonthEndProcess
{
    /// <summary>
    /// Log
    /// </summary>
    public class Step10 : IStep
    {
        #region IStep Members

        public event ProgressUpdateEventHandler UpdatedProgress;

        public void DoAction()
        {
            UpdatedProgress(this, new ProgressUpdateEventArgs("Step10 Processing ...", 1, 100));

            string message = BuildLog();

            UpdatedProgress(this, new ProgressUpdateEventArgs("Step10 - Write Log", 50, 100));
            // Write Log
            Utility.WriteLog(message);
        }

        #endregion

        public bool ResetSerivceItemsCDQty = false;
        public DateTime StartOn = DateTime.Now;
        public DateTime EndOn = DateTime.Now;
        public string PostedErrorMsg = string.Empty;

        private string BuildLog()
        {
            StringBuilder log = new StringBuilder();
            log.Append("COMPANY: ").Append(SystemInfo.CurrentInfo.Default.CompanyName).AppendLine();
            log.Append("MONTH: ").Append(SystemInfo.CurrentInfo.Default.CurrentSystemDate.ToString("MMMM yyyy")).AppendLine();
            log.Append("Reset Service Item's CDQty = 0: ").Append(ResetSerivceItemsCDQty ? "YES" : "NO").AppendLine();
            log.Append("USER: ").Append(GetStaffNumber()).AppendLine();
            log.Append("START TIME: ").Append(StartOn.ToString("dd/MM/yyyy HH:mm:ss")).AppendLine();
            log.Append("STOP TIME: ").Append(EndOn.ToString("dd/MM/yyyy HH:mm:ss")).AppendLine();
            log.Append("RESULT: ").Append(PostedErrorMsg).AppendLine();

            return log.ToString();
        }

        private string GetStaffNumber()
        {
            string result = string.Empty;

            RT2008.DAL.Staff oStaff = RT2008.DAL.Staff.Load(Common.Config.CurrentUserId);
            if (oStaff != null)
            {
                result = oStaff.StaffNumber;
            }

            return result;
        }
    }
}
