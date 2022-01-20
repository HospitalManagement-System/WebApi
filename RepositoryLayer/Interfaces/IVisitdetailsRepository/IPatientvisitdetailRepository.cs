using DomainLayer.Models;
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
        PatientVisitDetails Getdetailsfromid(string appointmentid);
    }
}
