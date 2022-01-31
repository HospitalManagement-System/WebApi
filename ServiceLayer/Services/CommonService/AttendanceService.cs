using DomainLayer.EntityModels;
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
    }
}
