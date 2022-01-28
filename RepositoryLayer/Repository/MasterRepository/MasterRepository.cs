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
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Allergy> GetAllergyfromallergytype(string AllergyType)
        {


            try
            {

                List<Allergy> allergy = _context.Allergy.Where(x => x.AllergyType == AllergyType).ToList();

                return allergy;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Drug Getdetailsfromdrugname(string drugname)
        {
            try
            {

                Drug drug = _context.Drug.Where(x => x.DrugName == drugname).FirstOrDefault();

                return drug;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public Procedure Getdetailsfromproceduredes(string proceduredes)
        {
            try
            {

                Procedure procedure = _context.Procedure.Where(x => x.Description == proceduredes).FirstOrDefault();

                return procedure;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public List<Diagnosis> Getdignosisdetails()
        {
            try
            {

                List<Diagnosis> diagnoses = _context.Diagnosis.ToList();

                return diagnoses;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public Diagnosis Getdignosisdetailsfromdesc(string diagnosisisdes)
        {
            try
            {

                Diagnosis diagnoses = _context.Diagnosis.Where(x => x.Description == diagnosisisdes).FirstOrDefault();

                return diagnoses;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public List<Drug> Getdrugdetail()
        {
            try
            {

                List<Drug> drug = _context.Drug.ToList();

                return drug;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public List<Procedure> Getproceduredetails()
        {
            try
            {

                List<Procedure> procedures = _context.Procedure.ToList();

                return procedures;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
