using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.IVisitDetails
{
    public interface IVisitService
    {
      
        int DemographicsUserDetails(Demographicsdetails patientDemographicDetails);
        List<PatientDemographicDetails> GetPatientDemographicDetailsDetails();
        PatientDemographicDetails GetPatientDetailsfrompatientid(string patientid);
       string UpdateDemographicsUserDetails(Guid patientidid, Demographicsdetails patientDemographicDetails);
       //Task<string> UpdateDemographicsUsengrDetails(int demoghraphicsid, PatientDemographicDetails patientDemographicDetails);
    }
}
