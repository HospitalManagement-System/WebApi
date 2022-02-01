using DomainLayer.Models;
using DomainLayer.Models.Master;
using Microsoft.AspNetCore.Mvc;
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
                Result = (SaveResult == 1) ? "Failure" : "Success";
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
                if (patientDetailsList.Equals(null))
                {

                    patientDetailsList.Diagnosislist = patientDetailsList.DiagnosisDescription.Split(',').ToList();
                    patientDetailsList.Druglist = patientDetailsList.DrugDescription.Split(',').ToList();

                    patientDetailsList.Procedureslist = patientDetailsList.ProcedureDesciption.Split(',').ToList();
                }
                return patientDetailsList;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        
        public string updatepatientvisitdetails(string id, PatientVisitDetails patientVisitDetails)
        { 
            var visitid = new Guid(id);
            try
            {
                PatientVisitDetails Existingdetails = _context.PatientVisitDetails.Where(x => x.Id == visitid).FirstOrDefault();

                if (Existingdetails != null)
                {

                    {
                        Existingdetails.Height = patientVisitDetails.Height;
                        Existingdetails.Weight = patientVisitDetails.Weight;
                        Existingdetails.BloodPressure = patientVisitDetails.BloodPressure;
                        Existingdetails.BodyTemprature = patientVisitDetails.BodyTemprature;
                        Existingdetails.RespirationRate = patientVisitDetails.RespirationRate;
                        Existingdetails.DiagnosisDescription =string.Join(",", patientVisitDetails.Diagnosislist.ToArray());
                       Existingdetails.ProcedureDesciption = string.Join(",", patientVisitDetails.Procedureslist.ToArray());
                        Existingdetails.DrugDescription = string.Join(",", patientVisitDetails.Druglist.ToArray());

                    };

                    _context.PatientVisitDetails.Update(Existingdetails);
                    int result = _context.SaveChanges();
                    Result = (SaveResult == 1) ? "Success" : "Failure";
                }





                return Result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public IEnumerable<PatientVisitDetails> GetdetailsfromPatientid(string patientid)
        {
            try
            {
                var Id = new Guid(patientid);
                List<Appointments> lst = _context.Appointments.Where(x => x.PatientId == Id).ToList();
                var result = from visit in _context.PatientVisitDetails
                             join app in lst
                             on visit.AppointmentId equals app.Id
                             select new PatientVisitDetails
                             {
                                 Id = visit.Id
                             };

                //List < PatientVisitDetails > patientDetailsList = _context.PatientVisitDetails.Where(x => x.Id == Id).ToList();
                return result;
            }
            catch(Exception ex)
            {
                return null;

            }
        }
    }
    
}