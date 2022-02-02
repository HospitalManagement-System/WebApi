using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces.IVisitdetailsRepository
{
    public interface IVisitRepository
    {
       // int AddDemographicsDetails(PatientDemographicsdetails patientDemographicDetails);
        List<PatientDemographicDetails> GetPatientDemographicDetails();
        PatientDemographicDetails Getpatientdemodetailsfrompatientid(string patientid);
        //string UpdateDemographicsdetails(PatientDemographicDetails patientDemographicDetails);
        //int AddDemographicsDetails(PatientDemographicsdetails patientDemographicDetails);
        int AddDemographicsDetails(PatientDemographicDetails patientDemographicDetails);
        string UpdateDemographicsdetails(string demoid, PatientDemographicDetails patientDemographicDetails);

        //List<UserDetails> GetUser();
    }
}
