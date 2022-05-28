using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces.IVisitdetailsRepository
{
    public interface IPatientvisitdetailRepository
    {
        string AddpatientvisitDetails(PatientVisitDetails patientVisitDetails);
        PatientVisitDetails Getdetailsfromid(string appointmentid) ;
        string updatepatientvisitdetails(string id, PatientVisitDetails patientDetails);
        IEnumerable<PatientVisitDetails> GetdetailsfromPatientid(string patientid);
        string PostAllocatedPatientDetails(AllocatedPatientDetails allocatedPatient);
    }
}
