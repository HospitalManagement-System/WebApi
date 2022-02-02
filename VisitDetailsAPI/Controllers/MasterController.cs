using DomainLayer.Models.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public Allergy GetdetailsfromAllergytype(string AllergyType)
        {
            try
            {
                Allergy allergy = _MasterService.GetAllergyfromallergytype(AllergyType);
                return allergy;
            }
            catch(Exception ex)
            {
                return null;
            }
           
        }

        // POST api/<MasterController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MasterController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MasterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
