using DomainLayer.Models.Master;
using RepositoryLayer.Interfaces.IMasterRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository.MasterRepository
{
    public class MasterRepository : IMasterRepository
    {
        private ApplicationDbContext _context;


        public MasterRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public List<Allergy> GetAllergydetails()
        {
            try
            {
                List<Allergy> allergy = _context.Allergy.ToList();


                return allergy;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public Allergy GetAllergyfromallergytype(string AllergyType)
        {
            

            try
            {
               
                Allergy allergy = _context.Allergy.Where(x => x.AllergyType == AllergyType).FirstOrDefault();

                return allergy;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
