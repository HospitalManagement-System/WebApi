using DomainLayer.EntityModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces.ICommonRepository
{
    public interface IAttendanceRepository
    {
        void SaveAttendance(EmployeeAvailability employeeAttendance);
        public List<EmployeeAvailability> GetAttendanceAvailability();
        public List<NurseAppointment> GetNextPatientDetails();
    }
}
