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
        //public int AddDemographicsDetails(PatientDemographicsdetails patientDemographicDetails)
        //{
        //    try
        //    {
        //       // var PatientDetails = _context.PatientDetails.Where(x => x.Id == patientDemographicDetails.PatientId)
        //        var PatientDemographicDetails = new PatientDemographicDetails
        //        {

        //            FirstName = patientDemographicDetails.firstname,
        //            LastName = patientDemographicDetails.lastName,
        //            Contact = patientDemographicDetails.contactnumber,
        //            DateOfBirth = patientDemographicDetails.dateOfBirth,
        //           PatientId =patientDemographicDetails.PatientId,
        //           //PreviousAllergies= patientDemographicDetails.allergyname,
        //            PatientRelativeDetails = new PatientRelativeDetails
        //            {
        //                //Title = patientDemographicDetails.
        //                FirstName=patientDemographicDetails.emergancyfirstname,
        //                LastName=patientDemographicDetails.emergancylastname,
        //                Address=patientDemographicDetails.emergancyaddress,
        //                Relation=patientDemographicDetails.emergancyrelationship,
        //                Email=patientDemographicDetails.emergancyemail,
        //                //Contact=patientDemographicDetails.emergancycontactnumber,
                       
        //            }
                    
        //        };

        //        _context.PatientDemographicDetails.Add(PatientDemographicDetails);
        //        int result= _context.SaveChanges();
        //        return result;
        //    }
        //    catch(Exception ex)
        //    {
        //        return 0;

        //    }
        ////}

        public int AddDemographicsDetails(PatientDemographicDetails patientDemographicDetails)
        {
            try
            {
                // var PatientDetails = _context.PatientDetails.Where(x => x.Id == patientDemographicDetails.PatientId)
                var PatientDemographicDetails = new PatientDemographicDetails
                {

                    FirstName = patientDemographicDetails.FirstName,
                    LastName = patientDemographicDetails.LastName,
                    Contact = patientDemographicDetails.Contact,
                    DateOfBirth = patientDemographicDetails.DateOfBirth,
                    PatientId = patientDemographicDetails.PatientId,
                    Age = patientDemographicDetails.Age,
                    Race = patientDemographicDetails.Race,
                    Ethinicity = patientDemographicDetails.Ethinicity,
                    Gender = patientDemographicDetails.Gender,
                    Email = patientDemographicDetails.Email,
                    Address = patientDemographicDetails.Address,
                    Pincode = patientDemographicDetails.Pincode,
                    Country = patientDemographicDetails.Country,
                    State = patientDemographicDetails.State,
                    Createddate = DateTime.Now,
                    AllergyDetails=patientDemographicDetails.AllergyDetails,
                    ClinicalInformation=patientDemographicDetails.ClinicalInformation,
                    AllergytypeList=patientDemographicDetails.AllergytypeList,
                    AllergynameList=patientDemographicDetails.AllergynameList,
                    //PreviousAllergies= patientDemographicDetails.allergyname,
                    PatientRelativeDetails = new PatientRelativeDetails
                    {
                        //Title = patientDemographicDetails.
                        FirstName = patientDemographicDetails.PatientRelativeDetails.FirstName,
                        LastName = patientDemographicDetails.PatientRelativeDetails.LastName,
                        Address = patientDemographicDetails.PatientRelativeDetails.Address,
                        Relation = patientDemographicDetails.PatientRelativeDetails.Relation,
                        Email = patientDemographicDetails.PatientRelativeDetails.Email,
                        Contact=patientDemographicDetails.PatientRelativeDetails.Contact,
                        State=patientDemographicDetails.PatientRelativeDetails.State,
                       Pincode=patientDemographicDetails.Pincode,
                       Country=patientDemographicDetails.Country,


                    }

                };

                _context.PatientDemographicDetails.Add(PatientDemographicDetails);
                int result = _context.SaveChanges();
                return result;
            }
            catch (Exception ex)
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
                patientDemographicDetails.Allergylist = patientDemographicDetails.AllergytypeList.Split(',').ToList();
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

        public string UpdateDemographicsdetails(string demoid,PatientDemographicDetails patientDemographicDetails)
        {
           var id = new Guid(demoid);
            try
            {

                PatientDemographicDetails Existingdetails = _context.PatientDemographicDetails.Include(x => x.PatientRelativeDetails).FirstOrDefault(x => x.Id == id);
                if (Existingdetails != null)
                {

                    {

                        Existingdetails.FirstName = patientDemographicDetails.FirstName;
                        Existingdetails.LastName = patientDemographicDetails.LastName;
                        Existingdetails.Contact = patientDemographicDetails.Contact;
                        Existingdetails.DateOfBirth = patientDemographicDetails.DateOfBirth;
                        Existingdetails.Age = patientDemographicDetails.Age;
                        Existingdetails.Race = patientDemographicDetails.Race;
                        Existingdetails.Ethinicity = patientDemographicDetails.Ethinicity;
                        Existingdetails.Email = patientDemographicDetails.Email;
                        Existingdetails.Address = patientDemographicDetails.Address;
                        Existingdetails.Pincode = patientDemographicDetails.Pincode;
                        Existingdetails.Country = patientDemographicDetails.Country;
                        Existingdetails.State = patientDemographicDetails.State;
                        //Existingdetails.Allergylist = patientDemographicDetails.Allergylist;
                       Existingdetails.AllergytypeList  = string.Join(",", patientDemographicDetails.Allergylist.ToArray());
                        Existingdetails.AllergynameList = patientDemographicDetails.AllergynameList;


                        Existingdetails.AllergyDetails = patientDemographicDetails.AllergyDetails;
                        Existingdetails.ClinicalInformation = patientDemographicDetails.ClinicalInformation;
                        Existingdetails.PatientRelativeDetails.FirstName = patientDemographicDetails.PatientRelativeDetails.FirstName;
                        Existingdetails.PatientRelativeDetails.LastName = patientDemographicDetails.PatientRelativeDetails.LastName;
                        Existingdetails.PatientRelativeDetails.Address = patientDemographicDetails.PatientRelativeDetails.Address;
                        Existingdetails.PatientRelativeDetails.Relation = patientDemographicDetails.PatientRelativeDetails.Relation;
                        Existingdetails.PatientRelativeDetails.Email = patientDemographicDetails.PatientRelativeDetails.Email;
                        Existingdetails.PatientRelativeDetails.Contact = patientDemographicDetails.PatientRelativeDetails.Contact;
                        Existingdetails.PatientRelativeDetails.Pincode = patientDemographicDetails.PatientRelativeDetails.Pincode;
                        Existingdetails.PatientRelativeDetails.Country = patientDemographicDetails.PatientRelativeDetails.Country;
                        Existingdetails.PatientRelativeDetails.State = patientDemographicDetails.PatientRelativeDetails.State;




                    };

                    _context.PatientDemographicDetails.Update(Existingdetails);
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
}
