using DomainLayer.EntityModels;
using DomainLayer.Models;
using RepositoryLayer.Interfaces.INurseDashRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository.NurseDashRepository
{
    public class NurseDashRepository : INurseRepository
    {
        private ApplicationDbContext _context;


        public NurseDashRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public int SaveResult { get; private set; }
        public string Result { get; set; }


        public List<BarChartDetails> GetBarChartDetails()
        {
            try
            {
                List<BarChartDetails> barChartDetails = _context.BarChartDetails.ToList();
                return barChartDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<NurseAppointment> GetnurseDetails()
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
                            where a.AppointmentDateTime == DateTime.Today && a.QueueStatus == "Upcoming" && a.AppointmentStatus=="Approved"
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
        public List<NurseAppointment> GetUpcomingAppointments()
        {
            List<NurseAppointment> nurseUpcomingAppointments = new List<NurseAppointment>();

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
                            where (a.AppointmentDateTime > DateTime.Today && a.QueueStatus == "Upcoming" && a.AppointmentStatus == "Approved")
                            select new
                            {

                                a.Id,
                                Name = p.FirstName + "" + p.LastName,
                                pd.Gender,
                                a.Diagnosis,
                                pd.Contact,
                                a.bookslot,
                                a.AppointmentDateTime,
                                PhysicanName = e.FirstName + "" + e.LastName,

                            });

                foreach (var item in User)
                {
                    NurseAppointment nurse = new NurseAppointment();
                    nurse.Id = item.Id;
                    nurse.Name = item.Name;
                    nurse.Gender = item.Gender;
                    nurse.PhysicanName = item.PhysicanName;
                    nurse.BookSlot = item.bookslot;
                    nurse.Diagnosis = item.Diagnosis;
                    nurse.Contact = item.Contact;
                    nurse.AppointmentDateTime = item.AppointmentDateTime;


                    nurseUpcomingAppointments.Add(nurse);
                }

                return nurseUpcomingAppointments;

            }
            catch (Exception ex)
            {
                throw ex;
            }




        }

        public string UpdateUpAppointment(string id, Appointments nurse)
        {
            var appid = new Guid(id);
            try
            {

                Appointments Existingdetails = _context.Appointments.Where(x => x.Id == appid).FirstOrDefault();
                if (Existingdetails != null)
                {
                    {
                        Existingdetails.AppointmentDateTime = nurse.AppointmentDateTime;
                        Existingdetails.bookslot = nurse.bookslot;
                    };
                    _context.Appointments.Update(Existingdetails);
                    int result = _context.SaveChanges();
                    Result = (SaveResult == 1) ? "Failure" : "Success";
                }
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UpdateNextPatient(string id, Appointments nurse)
        {
            var appid = new Guid(id);
            try
            {
                Appointments Existingdetails = _context.Appointments.Where(x => x.Id == appid).FirstOrDefault();
                if (Existingdetails != null)
                {
                    Existingdetails.QueueStatus = "Ongoing";
                    _context.Appointments.Update(Existingdetails);
                    int result = _context.SaveChanges();
                    Result = (SaveResult == 1) ? "Failure" : "Success";
                }
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

