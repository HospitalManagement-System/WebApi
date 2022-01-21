using DomainLayer.Models;
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
    }
}
