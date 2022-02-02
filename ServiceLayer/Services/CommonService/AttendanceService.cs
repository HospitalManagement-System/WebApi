using DomainLayer.EntityModels;
using DomainLayer.Models;
using RepositoryLayer.Interfaces.ICommonRepository;
using ServiceLayer.Interfaces.ICommonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.CommonService
{
    public class AttendanceService : IAttendanceService
    {
        IAttendanceRepository _repository;
        public AttendanceService(IAttendanceRepository repository)
        {
            _repository = repository;
        }
        public void AddAtendance(EmployeeAvailability employeeAttendance)
        {
            _repository.SaveAttendance(employeeAttendance);
        }

        public List<EmployeeAvailability> GetAttendanceAvailability()
        {
            List<EmployeeAvailability> lstEmployeeDetails = _repository.GetAttendanceAvailability();
            return lstEmployeeDetails;
        }
        public List<NurseAppointment> GetNextPatientDetails()
        {
            List<NurseAppointment> nurseAppointmentDetails = _repository.GetNextPatientDetails();
            return nurseAppointmentDetails;
        }
    }
}
