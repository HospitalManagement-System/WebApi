using DomainLayer.EntityModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.ICommonService
{
    public interface IAttendanceService
    {
        void AddAtendance(EmployeeAvailability employeeAttendance);

        List<EmployeeAvailability> GetAttendanceAvailability();
        public List<NurseAppointment> GetNextPatientDetails();
    }
}
