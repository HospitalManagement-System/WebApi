using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.IVisitDetails
{

    public interface IPatientvisitdetailsService
    {
       string Addpatientvisitdetails(PatientVisitDetails patientVisitDetails);
        PatientVisitDetails GetVisitdetailsfromId(string appointmentid);
        string putvisitdetails(string id, PatientVisitDetails patientDetails);
        IEnumerable<PatientVisitDetails> GetdetailsfrompatientId(string patientid);
        string PostAllocatedPatientDetails(AllocatedPatientDetails allocatedPatient);
    }
}
