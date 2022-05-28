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
        ServiceLayer.Services.Encryption.Encryption _encryption = new ServiceLayer.Services.Encryption.Encryption();
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

        public string PostAllocatedPatientDetails(AllocatedPatientDetails allocatedPatient)
        {
            allocatedPatient.Password = _encryption.EncodePasswordToBase64("Password@123");
            return _repository.PostAllocatedPatientDetails(allocatedPatient);
        }
    }
}


