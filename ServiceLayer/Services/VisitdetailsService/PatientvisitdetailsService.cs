using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interfaces.IVisitdetailsRepository;
using RepositoryLayer.Repository.VisitdetailsRepository;
using ServiceLayer.Interfaces.IVisitDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.VisitdetailsService
{
   // private PatientvisitdetailsRepository _repository;
    
    public class PatientvisitdetailsService : IPatientvisitdetailsService
    {
        private IPatientvisitdetailRepository _repository;
        public PatientvisitdetailsService(IPatientvisitdetailRepository repository)
        {
            _repository = repository;
        }
        public string Addpatientvisitdetails(PatientVisitDetails patientVisitDetails)
        {
            return _repository.AddpatientvisitDetails(patientVisitDetails);
        }

        public IEnumerable<PatientVisitDetails> GetdetailsfrompatientId(string patientid)
        {
            return _repository.GetdetailsfromPatientid(patientid);
        }

        public PatientVisitDetails GetVisitdetailsfromId(string appointmentid)
        {
            return _repository.Getdetailsfromid(appointmentid);
        }

        public string putvisitdetails(string id, PatientVisitDetails patientDetails)
        {
            return _repository.updatepatientvisitdetails(id, patientDetails);
        }

        
    }
}


