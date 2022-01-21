using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RepositoryLayer.Interfaces.IVisitdetailsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository.VisitdetailsRepository
{
    public class VisitRepository : IVisitRepository
    {
        private ApplicationDbContext _context;


        public VisitRepository(ApplicationDbContext context)
        {
            _context = context;
          
        }

        public int SaveResult { get; private set; }
        public string Result { get;set; }
        public int AddDemographicsDetails(Demographicsdetails patientDemographicDetails)
        {
            try
            {
               // var PatientDetails = _context.PatientDetails.Where(x => x.Id == patientDemographicDetails.PatientId)
                var PatientDemographicDetails = new PatientDemographicDetails
                {

                    FirstName = patientDemographicDetails.firstname,
                    LastName = patientDemographicDetails.lastName,
                    Contact = patientDemographicDetails.contactnumber,
                    DateOfBirth = patientDemographicDetails.dateOfBirth,
                   PatientId =patientDemographicDetails.PatientId,
                   //PreviousAllergies= patientDemographicDetails.allergyname,
                    PatientRelativeDetails = new PatientRelativeDetails
                    {
                        //Title = patientDemographicDetails.
                        FirstName=patientDemographicDetails.emergancyfirstname,
                        LastName=patientDemographicDetails.emergancylastname,
                        Address=patientDemographicDetails.emergancyaddress,
                        Relation=patientDemographicDetails.emergancyrelationship,
                        Email=patientDemographicDetails.emergancyemail,
                        //Contact=patientDemographicDetails.emergancycontactnumber,
                       
                    }
                    
                };

                _context.PatientDemographicDetails.Add(PatientDemographicDetails);
                int result= _context.SaveChanges();
                return result;
            }
            catch(Exception ex)
            {
                return 0;

            }
        }

        public  PatientDemographicDetails Getpatientdemodetailsfrompatientid(string patientid)
        {
            var Id = new Guid(patientid);

            try
            {
                 //PatientDemographicDetails patientDemographicDetails =   _context.PatientDemographicDetails.Where(x => x.PatientId == patientid).FirstOrDefault();
                //PatientDemographicDetails patientDemographicDetails = _context.PatientDemographicDetails.Where(x => x.PatientId == patientid).FirstOrDefault();
                //var x = JsonSerializer.Serialize(patientDemographicDetails);
                //PatientDemographicDetails patientDemographicDetails;
                PatientDemographicDetails patientDemographicDetails = _context.PatientDemographicDetails.Include(x=>x.PatientRelativeDetails).FirstOrDefault(x => x.PatientId == Id);

                return patientDemographicDetails;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PatientDemographicDetails> GetPatientDemographicDetails()
        {
            try
            {
                List<PatientDemographicDetails> patientDemographicDetails = _context.PatientDemographicDetails.ToList();
               
                
                return patientDemographicDetails;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public string UpdateDemographicsdetails(Guid patientid, Demographicsdetails patientDemographicDetails)
        {
            try
            {
               PatientDemographicDetails Existingdetails = _context.PatientDemographicDetails.Where(x => x.Id == patientid).FirstOrDefault();
                if (Existingdetails != null)
                {
                   
                    {

                        Existingdetails.FirstName = patientDemographicDetails.firstname;
                        Existingdetails.LastName = patientDemographicDetails.lastName;
                        Existingdetails.Contact = patientDemographicDetails.contactnumber;
                        Existingdetails.DateOfBirth = patientDemographicDetails.dateOfBirth;
                        Existingdetails.PatientId = patientDemographicDetails.PatientId;
                        //Existingdetails.PatientRelativeDetails.FirstName = patientDemographicDetails.emergancyfirstname;


                        //Existingdetails.PatientRelativeDetails.LastName = patientDemographicDetails.emergancylastname;
                        //Existingdetails.PatientRelativeDetails.Address = patientDemographicDetails.emergancyaddress;
                        //Existingdetails.PatientRelativeDetails.Relation = patientDemographicDetails.emergancyrelationship;
                        //Existingdetails.PatientRelativeDetails.Email = patientDemographicDetails.emergancyemail;
                        //Existingdetails.PatientRelativeDetails.Contact = patientDemographicDetails.emergancycontactnumber;

                        

                    };

                    _context.PatientDemographicDetails.Update(Existingdetails);
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
    }
}
