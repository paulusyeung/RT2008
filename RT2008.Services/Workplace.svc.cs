using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Common;
using System.Configuration;
using RT2008.DAL;
using System.Data.SqlClient;
using RT2008.Services.Contracts;

namespace RT2008.Services
{
    // NOTE: If you change the class name "Workplace" here, you must also update the reference to "Workplace" in Web.config and in the associated .svc file.
    public class Workplace : IWorkplace
    {
        #region IWorkplace Members

        public RT2008.DAL.Workplace[] GetAllWorkplace()
        {
            return RT2008.DAL.Workplace.LoadCollection().ToArray<RT2008.DAL.Workplace>();
        }

        public RT2008.DAL.Workplace GetWorkplaceById(string workplaceId)
        {
            string query = "WorkplaceId = '" + workplaceId + "'";
            return RT2008.DAL.Workplace.LoadWhere(query);
        }

        public RT2008.DAL.Workplace GetWorkplaceByCode(string workplaceCode)
        {
            string query = "WorkplaceCode = '" + workplaceCode + "'";
            return RT2008.DAL.Workplace.LoadWhere(query);
        }

        public RT2008.DAL.Workplace[] GetWorkplaceByWhereClause(string whereClause)
        {
            return RT2008.DAL.Workplace.LoadCollection(whereClause).ToArray<RT2008.DAL.Workplace>();
        }

        #endregion
    }
}
