using DomainLayer.EntityModels;
using DomainLayer.Models;
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
                if (!_context.EmployeeAvailability.Any(w => w.PhysicianId == employeeAttendance.PhysicianId))
                {
                    _context.EmployeeAvailability.Add(employeeAttendance);
                    _context.SaveChanges();

                }
                   
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


        public List<NurseAppointment> GetNextPatientDetails()
        {
            List<NurseAppointment> nurseAppointments = new List<NurseAppointment>();

            try
            {
                var User = (
                            from a in _context.Appointments
                            join e in _context.EmployeeDetails
                            on a.PhysicianId equals e.Id
                            join p in _context.PatientDetails
                            on a.PatientId equals p.Id
                            join pd in _context.PatientDemographicDetails
                            on p.PatientDemographicId equals pd.Id
                            where a.AppointmentDateTime == DateTime.Today && a.QueueStatus == "Ongoing"
                            select new
                            {
                                a.Id,
                                Name = p.FirstName + "" + p.LastName,
                                pd.Gender,
                                a.Diagnosis,
                                pd.Contact,
                                pd.Age,
                                pd.Email,
                                PhysicanName = e.FirstName + "" + e.LastName

                            });

                foreach (var item in User)
                {
                    NurseAppointment nurse = new NurseAppointment();
                    nurse.Id = item.Id;
                    nurse.Name = item.Name;
                    nurse.Gender = item.Gender;
                    nurse.PhysicanName = item.PhysicanName;
                    nurse.Age = item.Age;
                    nurse.Email = item.Email;
                    nurse.Diagnosis = item.Diagnosis;
                    nurse.Contact = item.Contact;



                    nurseAppointments.Add(nurse);
                }

                return nurseAppointments;

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }
    }
}
