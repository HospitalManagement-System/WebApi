using DomainLayer.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces.IMasterRepository
{
    public interface IMasterRepository
    {
        List<Allergy> GetAllergydetails();
        Allergy GetAllergyfromallergytype(string AllergyType);
    }
}
