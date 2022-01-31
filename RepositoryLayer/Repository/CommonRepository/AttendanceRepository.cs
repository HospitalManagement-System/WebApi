using DomainLayer.EntityModels;
using RepositoryLayer.Interfaces.ICommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository.CommonRepository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        public ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void SaveAttendance(EmployeeAvailability employeeAttendance)
        {
            try
            {
                foreach (var item in employeeAttendance.arrTimeSlot)
                {
                    employeeAttendance.TimeSlot += item + ",";
                }
                _context.EmployeeAvailability.Add(employeeAttendance);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public List<EmployeeAvailability> GetAttendanceAvailability()
        {

            List<EmployeeAvailability> result = _context.EmployeeAvailability.ToList();
            return result;
        }
    }
}
