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
   public  class NurseDashRepository: INurseRepository
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
                //return patientDemographicDetails
                //List<PatientDemographicDetails> patientDemographicDetails = _context.PatientDemographicDetails
                //.Include(x => x.PatientRelativeDetails).
                //ToList();

                return barChartDetails;
            }
            catch (Exception ex)
            {
                return null;
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
                            where (a.AppointmentDateTime == DateTime.Now)
                            select new
                            {
                                p.FirstName,
                                pd.Gender,
                                a.Diagnosis,
                                pd.Contact,
                                pd.Age,
                                pd.Email,
                                PhysicanName = e.FirstName,

                            });

                foreach (var item in User)
                {
                    NurseAppointment nurse = new NurseAppointment();
                    nurse.FirstName = item.FirstName;
                    nurse.Gender = item.Gender;


                    nurseAppointments.Add(nurse);
                }

                return nurseAppointments;
                
            }
            catch(Exception ex)
            {
                return null;
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
                            where (a.AppointmentDateTime > DateTime.Now)
                            select new
                            {
                                p.FirstName,
                                pd.Gender,
                                a.Diagnosis,
                                pd.Contact,
                                pd.Age,
                                a.AppointmentDateTime,
                                PhysicanName = e.FirstName,

                            });

                foreach (var item in User)
                {
                    NurseAppointment nurse = new NurseAppointment();
                    nurse.FirstName = item.FirstName;
                    nurse.Gender = item.Gender;


                    nurseUpcomingAppointments.Add(nurse);
                }

                return nurseUpcomingAppointments;

            }
            catch (Exception ex)
            {
                return null;
            }




        }

        public string UpdateUpAppointment(string id, Appointments nurse)
        {
            var appid = new Guid(id);
            try {

                Appointments Existingdetails = _context.Appointments.Where(x => x.Id == appid).FirstOrDefault();
                if (Existingdetails != null)
                {
                    {
                        Existingdetails.AppointmentDateTime = nurse.AppointmentDateTime;
                        Existingdetails.PhysicianId = nurse.PhysicianId;
                    };
                    _context.Appointments.Update(Existingdetails);
                    int result = _context.SaveChanges();
                    Result = (SaveResult == 1) ? "Failure" : "Success";
                }
                return Result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       
    }


    //1)Nurse Data ->Nurse id not required
    //2)Based on Dates Current         
    //3)Appointment should be Approved
    //Tables
    //Appointmenyt
    //EmployeeDeatils
    //Patient Details
    //PatientDemographic Details


}

