using DomainLayer.Models;
using RepositoryLayer.Interfaces.IVisitdetailsRepository;
using ServiceLayer.Interfaces.IVisitDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.VisitdetailsService
{
    public class PatientvisitService : IVisitService
    {
        private IVisitRepository _repository;
        public PatientvisitService(IVisitRepository repository)
        {
            _repository = repository;
        }
        public int DemographicsUserDetails(Demographicsdetails patientDemographicDetails)
        {
            return _repository.AddDemographicsDetails(patientDemographicDetails);
        }

        public List<PatientDemographicDetails> GetPatientDemographicDetailsDetails()
        {
            List<PatientDemographicDetails> patientDemographicDetails = _repository.GetPatientDemographicDetails();
            return patientDemographicDetails;
        }

        public PatientDemographicDetails GetPatientDetailsfrompatientid(string patientid)
        {
            PatientDemographicDetails odetails = _repository.Getpatientdemodetailsfrompatientid(patientid);
            return odetails;
        }

        public string UpdateDemographicsUserDetails(Guid patientid, Demographicsdetails patientDemographicDetails)
        {
            return _repository.UpdateDemographicsdetails(patientid, patientDemographicDetails);
        }
    }
}
