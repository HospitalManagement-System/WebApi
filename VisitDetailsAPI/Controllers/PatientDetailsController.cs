using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
using RepositoryLayer;

namespace VisitDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PatientDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDetails>>> GetPatientDetails()
        {
            return await _context.PatientDetails.ToListAsync();
        }

        // GET: api/PatientDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDetails>> GetPatientDetails(Guid id)
        {
            var patientDetails = await _context.PatientDetails.FindAsync(id);

            if (patientDetails == null)
            {
                return NotFound();
            }

            return patientDetails;
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
        public async Task<ActionResult<PatientDetails>> PostPatientDetails(PatientDetails patientDetails)
        {
            _context.PatientDetails.Add(patientDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatientDetails", new { id = patientDetails.Id }, patientDetails);
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
