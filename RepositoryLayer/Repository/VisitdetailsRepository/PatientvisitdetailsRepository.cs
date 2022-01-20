using DomainLayer.Models;
using DomainLayer.Models.Master;

using RepositoryLayer.Interfaces.IVisitdetailsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository.VisitdetailsRepository
{
    public class PatientvisitdetailsRepository : IPatientvisitdetailRepository
    {
        private ApplicationDbContext _context;

        public int SaveResult { get; private set; }
        public string Result { get; set; }
        public PatientvisitdetailsRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public string AddpatientvisitDetails(PatientVisitDetails patientVisitDetails)
        {
            try
            {
                var PatientVisitDetails = new PatientVisitDetails
                {

                    Height = patientVisitDetails.Height,
                    Weight = patientVisitDetails.Weight,
                    BloodPressure = patientVisitDetails.BloodPressure,
                    BodyTemprature = patientVisitDetails.BodyTemprature,
                    RespirationRate = patientVisitDetails.RespirationRate,
                    DoctorDescription = patientVisitDetails.DoctorDescription,
                    ProcedureDesciption = patientVisitDetails.ProcedureDesciption,
                    DiagnosisDescription = patientVisitDetails.DiagnosisDescription,
                    AppointmentId=patientVisitDetails.AppointmentId,
                    Createddate = DateTime.Now,


                };
                _context.PatientVisitDetails.Add(PatientVisitDetails);

                int result = _context.SaveChanges();
                Result = (SaveResult == 1) ? "Success" : "Failure";
                return Result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public PatientVisitDetails Getdetailsfromid(string appointmentid)
        {
            try
            {

                var Id = new Guid(appointmentid);
                PatientVisitDetails patientDetailsList = _context.PatientVisitDetails.Where(x => x.AppointmentId == Id).FirstOrDefault();
                patientDetailsList.Diagnosislist = patientDetailsList.DiagnosisDescription.Split(',').ToList();
                patientDetailsList.Druglist = patientDetailsList.DrugDescription.Split(',').ToList();
                patientDetailsList.Procedureslist = patientDetailsList.ProcedureDesciption.Split(',').ToList();
                return patientDetailsList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}