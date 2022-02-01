using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Interfaces.IVisitDetails;
using Microsoft.AspNetCore.Identity;
using DomainLayer;

namespace VisitDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IPatientvisitdetailsService _VisitService;
        private UserManager<ApplicationUser> _userManager;
        public PatientDetailsController(IPatientvisitdetailsService visitService, UserManager<ApplicationUser> userManager)
        {
            _VisitService = visitService;
            _userManager = userManager;
        }

        

        // GET: api/PatientDetails/5
        [HttpGet]
        public  PatientVisitDetails GetPatientDetails(string Appointmentid)
        {
            try
            {
                PatientVisitDetails patientVisitDetails = _VisitService.GetVisitdetailsfromId(Appointmentid);
                return patientVisitDetails;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

       
        [HttpPut("PutPatientDetails")]
        public string PutPatientDetails(string id, PatientVisitDetails patientDetails)
        {
            try
            {
                return _VisitService.putvisitdetails(id,patientDetails);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
        [HttpPost]
        public  string PostPatientDetails(PatientVisitDetails patientDetails)
        {
            try
            {
                return _VisitService.Addpatientvisitdetails(patientDetails);

               

            }
            catch (Exception ex)
            {
                return null;
            }
           
        }

        // DELETE: api/PatientDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatientDetails(Guid id)
        {
            var patientDetails = await _context.PatientDetails.FindAsync(id);
            if (patientDetails == null)
            {
                return NotFound();
            }

            _context.PatientDetails.Remove(patientDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //[HttpGet]
        //public List<PatientVisitDetails> GetPatientDetailsfrompatientid(string patientid)
        //{
        //    try
        //    {
        //        List<PatientVisitDetails> patientVisitDetails = _VisitService.GetdetailsfrompatientId(patientid);
        //        return patientVisitDetails;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //    private bool PatientDetailsExists(Guid id)
        //    {
        //        return _context.PatientDetails.Any(e => e.Id == id);
        //    }
        }
    }
