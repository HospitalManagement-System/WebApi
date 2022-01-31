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
            //_context.EmployeeAvailability.Add(employeeAttendance);
        }

        public IEnumerable<EmployeeAvailability> GetAttendanceAvailability(Guid phyid)
        {
            //List<EmployeeAvailability> result = _context.EmployeeAvailability.Where(x => x.DateTime == DateTime.Today && x.PhysicianId.ToString() == phyid.ToString()).ToList();

            //foreach (var item in result)
            //{
            //    item.arrTimeSlot = item.TimeSlot.Split(',');
            //}
            //return result;  
            var result = new EmployeeAvailability();
            yield return result;
        }
    }
}
