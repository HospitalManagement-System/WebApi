using DomainLayer;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces.IVisitDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VisitDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemographicsdetailsController : ControllerBase
    {
        private IVisitService _VisitService;
        private UserManager<ApplicationUser> _userManager;
        public DemographicsdetailsController(IVisitService visitService, UserManager<ApplicationUser> userManager)
        {
            _VisitService = visitService;
            _userManager = userManager;
                        
        }
        // GET: api/<DemographicsdetailsController>
        [HttpGet]
        [Route("Getallpatientdetails")]
        public List<PatientDemographicDetails> Getallpatientdetails()
        {
            List<PatientDemographicDetails> getdetails = _VisitService.GetPatientDemographicDetailsDetails();
            return getdetails;
        }
        [HttpGet]
       
        public PatientDemographicDetails GetPatientdatafrompatientid(string Patientid)
        {
            //return new string[] { "value1", "value2" };
           PatientDemographicDetails odetails = _VisitService.GetPatientDetailsfrompatientid(Patientid);
            return odetails;
        }
        
        // GET api/<DemographicsdetailsController>/5
       // [HttpGet("{id}")]
        
        [HttpPost]
        [Route("PostPatientdemographicsdetails")]
        public async Task<int> PostPatientdemographicsdetails([FromBody] PatientDemographicDetails objpatientDemographicDetails)
        {
            try
            {
                return  _VisitService.DemographicsUserDetails(objpatientDemographicDetails);

                // return Ok(new string("Registration Success"));

            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        // PUT api/<DemographicsdetailsController>/5
        [HttpPut("UpdateDemographic")]
        //[Route("Put")]
        public IActionResult UpdateDemographic(string Demoid, PatientDemographicDetails patientDemographicDetails)
        {
            try
            {

                return Ok(_VisitService.UpdateDemographicsUserDetails(Demoid,patientDemographicDetails));

                // return Ok(new string("Registration Success"));

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // DELETE api/<DemographicsdetailsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
