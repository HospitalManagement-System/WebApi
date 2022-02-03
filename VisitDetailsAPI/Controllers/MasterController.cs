using DomainLayer.Models;
﻿using DomainLayer.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using ServiceLayer.Interfaces.IMasterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VisitDetailsAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        
        private IMasterService _MasterService;
        private ApplicationDbContext _context;
        public MasterController(IMasterService MasterService,ApplicationDbContext context)
        {
            _MasterService = MasterService;
            _context = context;

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
                if (AllergyType != null)
                {
                    if (AllergyType.Contains(','))
                    {
                        List<Allergy> getdetails = _MasterService.GetAllAllergydetails();
                        return getdetails;
                    }
                    else
                    {
                        List<Allergy> allergy = _MasterService.GetAllergyfromallergytype(AllergyType);
                        return allergy;
                    }
                    
                }
                return null;
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
        [HttpGet("GetRole")]
        public string GetRole(string id)
        {
            try
            {
               string user  = _MasterService.GetRole(id);
                return user;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("GetPatientId")]
        public string GetPatientId(string userid)
        {
            try
            {
                var id = new Guid(userid);
                var result = _context.PatientDetails.Where(x => x.UserId == id).Select(x => x.Id.ToString()).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("UploadMasters")]
        public IActionResult ImportMasters()
        {
            try
            {
                var file = Request.Form.Files[0];
                string message = "";
                if (file.Length > 0)
                {

                    //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    ////var fullPath = Path.Combine(pathToSave, fileName);
                    //////var dbPath = Path.Combine(folderName, fileName);
                    /////

                    //Stream stream = file;
                    if (file.FileName.EndsWith(".xls"))
                    {

                    }
                    else if (file.FileName.EndsWith(".xlsx"))
                    {

                    }
                    else
                    {
                        message = "This file format is not supported";
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
