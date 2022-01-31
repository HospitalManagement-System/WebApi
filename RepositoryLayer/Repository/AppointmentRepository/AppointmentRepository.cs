using DomainLayer.Models;
using RepositoryLayer.Interfaces.IAppointmentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository.AppointmentRepository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<InboxAppointment>> GetAppointmentsById(Guid id)
        {
            try
            {
                List<Appointments> lstAppoitntment = _context.Appointments.Where(x => x.PhysicianId == id || x.NurseId == id).ToList();
                var result = from appointment in lstAppoitntment
                             join
                             patient in _context.PatientDetails
                             on appointment.PatientId equals patient.Id
                             select new InboxAppointment
                             {
                                 AppointmentId = appointment.Id,
                                 PatientName = patient.FirstName + " " + patient.LastName,
                                 AppointmentDateTime = appointment.AppointmentDateTime,
                                 MeetingTitle = appointment.AppointmentType,
                                 Diagnosis = appointment.Diagnosis
                             };
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
