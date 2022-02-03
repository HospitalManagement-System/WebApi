
using DomainLayer.Models;
using System;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces.IVisitDetails
{
    public interface IVisitService
    {

        //int DemographicsUserDetails(PatientDemographicsdetails patientDemographicDetails);
        List<PatientDemographicDetails> GetPatientDemographicDetailsDetails();
        PatientDemographicDetails GetPatientDetailsfrompatientid(string patientid);
        
        int DemographicsUserDetails(PatientDemographicDetails objpatientDemographicDetails);
        string UpdateDemographicsUserDetails(string demoid, PatientDemographicDetails patientDemographicDetails);
        //Task<string> UpdateDemographicsUsengrDetails(int demoghraphicsid, PatientDemographicDetails patientDemographicDetails);
    }
}
