using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RT2008.DAL;

namespace RT2008.Services.Contracts
{
    // NOTE: If you change the interface name "IStaff" here, you must also update the reference to "IStaff" in Web.config.
    [ServiceContract]
    public interface IStaff
    {
        [OperationContract]
        RT2008.DAL.Staff[] GetAllStaff();

        [OperationContract]
        RT2008.DAL.Staff GetStaffById(string staffId);

        [OperationContract]
        RT2008.DAL.Staff GetStaffByCode(string staffNumber);

        [OperationContract]
        RT2008.DAL.Staff[] GetStaffByWhereClause(string whereClause);

        [OperationContract]
        Guid IsAuth(string staffNumber, string password);
    }
}
