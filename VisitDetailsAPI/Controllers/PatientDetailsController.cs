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

        // PUT: api/PatientDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatientDetails(Guid id, PatientDetails patientDetails)
        {
            if (id != patientDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(patientDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PatientDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public  string PostPatientDetails(PatientVisitDetails patientDetails)
        {
            try
            {
                return _VisitService.Addpatientvisitdetails(patientDetails);

                // return Ok(new string("Registration Success"));

            }
            catch (Exception ex)
            {
                return null;
            }
            //_context.PatientDetails.Add(patientDetails);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPatientDetails", new { id = patientDetails.Id }, patientDetails);
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

        private bool PatientDetailsExists(Guid id)
        {
            return _context.PatientDetails.Any(e => e.Id == id);
        }
    }
}
