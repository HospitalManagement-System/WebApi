using DomainLayer.Models.Master;
using RepositoryLayer.Interfaces.IMasterRepository;
using ServiceLayer.Interfaces.IMasterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.MasterService
{
    public class MasterService : IMasterService
    {
        private IMasterRepository _repository;
        public MasterService(IMasterRepository repository)
        {
            _repository = repository;
        }
        public List<Allergy> GetAllAllergydetails()
        {
            List<Allergy> allergy = _repository.GetAllergydetails();
            return allergy;
        }

        public List<Allergy> GetAllergyfromallergytype(string AllergyType)
        {
            List<Allergy> allergy = _repository.GetAllergyfromallergytype(AllergyType);
            return allergy;
        }

        public Diagnosis Getdetailsfromdiagnosisdes(string diagnosisisdes)
        {
            Diagnosis diagnosis = _repository.Getdignosisdetailsfromdesc(diagnosisisdes);
            return diagnosis;
        }

        public Drug Getdetailsfromdrugname(string drugname)
        {
            Drug drug = _repository.Getdetailsfromdrugname(drugname);
           return drug;
        }

        public Procedure Getdetailsfromproceduredes(string diagnosisisdes)
        {
            Procedure procedure = _repository.Getdetailsfromproceduredes(diagnosisisdes);
            return procedure;
        }

        public List<Diagnosis> Getdiagnosisdetails()
        {
            List<Diagnosis> diagnosis = _repository.Getdignosisdetails();
            return diagnosis;
        }

        public List<Drug> Getdrugdetails()
        {
            List<Drug> drug = _repository.Getdrugdetail();
                return drug;


        }

        public List<Procedure> Getproceduredetails()
        {
            List<Procedure> procedures = _repository.Getproceduredetails();

            return procedures;
        }
    }
}
