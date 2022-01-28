using DomainLayer.Models.Master;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces.IMasterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VisitDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        
        private IMasterService _MasterService;
        public MasterController(IMasterService MasterService)
        {
            _MasterService = MasterService;


        }
        [HttpGet]
        [Route("GetallAllergydetails")]
        public List<Allergy> GetallAllergydetails()
        {
            try
            {
                List<Allergy> getdetails = _MasterService.GetAllAllergydetails();
                return getdetails;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        // GET api/<MasterController>/5
        [HttpGet]
        [Route("GetdetailsfromAllergytype")]
        public List<Allergy> GetdetailsfromAllergytype(string AllergyType)
        {
            try
            {
                List<Allergy> allergy = _MasterService.GetAllergyfromallergytype(AllergyType);
                return allergy;
            }
            catch(Exception ex)
            {
                return null;
            }
           
        }
        [HttpGet]
        [Route("Getdetailsfromdiagnosisdes")]
        public Diagnosis Getdetailsfromdiagnosisdes(string Diagnosisisdes)
        {
            try
            {
                Diagnosis diagnosis = _MasterService.Getdetailsfromdiagnosisdes(Diagnosisisdes);
                return diagnosis;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        [Route("Getdiagnosisdetails")]
        public List<Diagnosis> Getdiagnosisdetails()
        {
            try
            {
                List<Diagnosis> procedure = _MasterService.Getdiagnosisdetails();
                return procedure;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        [Route("Getdetailsfromproceduredes")]
        public Procedure Getdetailsfromproceduredes(string Proceduredes)
        {
            try
            {
                Procedure procedure = _MasterService.Getdetailsfromproceduredes(Proceduredes);
                return procedure;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        [Route("Getproceduredetails")]
        public List<Procedure> Getproceduredetails()
        {
            try
            {
                List<Procedure> procedure = _MasterService.Getproceduredetails();
                return procedure;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        [Route("Getdetailsfromdrug")]
        public Drug Getdetailsfromdrug(string drugname)
        {
            try
            {
                Drug drug = _MasterService.Getdetailsfromdrugname(drugname);
                return drug;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        [Route("Getdrugdetails")]
        public List<Drug> Getdrugdetails()
        {
            try
            {
                List<Drug> drug = _MasterService.Getdrugdetails();
                return drug;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


    }
}
