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

        public Allergy GetAllergyfromallergytype(string AllergyType)
        {
            Allergy allergy = _repository.GetAllergyfromallergytype(AllergyType);
            return allergy;
        }
    }
}
