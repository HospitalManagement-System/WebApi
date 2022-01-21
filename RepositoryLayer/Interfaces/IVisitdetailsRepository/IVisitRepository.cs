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
        int AddDemographicsDetails(Demographicsdetails patientDemographicDetails);
        List<PatientDemographicDetails> GetPatientDemographicDetails();
        PatientDemographicDetails Getpatientdemodetailsfrompatientid(string patientid);
        string UpdateDemographicsdetails(Guid demoghraphicsid, Demographicsdetails patientDemographicDetails);

        //List<UserDetails> GetUser();
    }
}
