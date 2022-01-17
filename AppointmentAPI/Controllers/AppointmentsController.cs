using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
using RepositoryLayer;
using ServiceLayer.Interfaces;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IEmailSender _iEMailSender;

        public AppointmentsController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _iEMailSender = emailSender;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }


        [HttpGet("GetAllAppointments")]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetAllAppointments()
        {
            var result = await (from c in _context.Appointments
                               .Where(p => p.AppointmentStatus!=("Rejected") && p.AppointmentDateTime>=new DateTime())
                                select new {
                                    publicId = c.Id,
                                    title = c.AppointmentType, 
                                    date=c.AppointmentDateTime,
                                    description="Test",
                                    color =(
                                    c.AppointmentStatus.Equals("Pending") ? "red":
                                    c.AppointmentStatus.Equals("Approved") ? "green" :
                                    c.AppointmentStatus.Equals("Rejected") ? "blue" :"Unknown"
                                    )
                                }
                          ).ToListAsync();

            return Ok(result);
        }


        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointments>> GetAppointments(Guid id)
        {
            var appointments = await _context.Appointments.FindAsync(id);

            if (appointments == null)
            {
                return NotFound();
            }

            return appointments;
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointments(Guid id, Appointments appointments)
        {
            if (id != appointments.Id)
            {
                return BadRequest();
            }

            _context.Entry(appointments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentsExists(id))
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

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostAppointments(Appointments appointments)
        {

           
            _context.Appointments.Add(appointments);
           var SaveResult=  await _context.SaveChangesAsync();

            string Result = (SaveResult == 1) ? "Success" : "Failure";

            return Ok(Result);
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointments(Guid id)
        {
            var appointments = await _context.Appointments.FindAsync(id);
            if (appointments == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointments);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentsExists(Guid id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }

        [HttpGet("GetBookSlots/{Id}")]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetBookSlots(string Id,DateTime appointmentdateTime)
        {

            Guid PhysicianID = new Guid(Id);

            var slot = await (from c in _context.Appointments
                              where (c.PhysicianId == PhysicianID  && c.bookslot != null &&
                              c.AppointmentDateTime.Date == Convert.ToDateTime(appointmentdateTime).Date
                              )
                              select new { c.bookslot }).ToListAsync();


            return Ok(slot);

        }

        [HttpPatch("ApproveReject/{Id}")]
        public IActionResult ApproveReject(string Id,string Status)
        {
            Guid AppointmentId = new Guid(Id);
            var FindAppointment = _context.Appointments.Where(x=>x.Id==AppointmentId).FirstOrDefault();

            if (FindAppointment!=null)
            {
                FindAppointment.AppointmentStatus = Status;
                var Result= _context.SaveChanges();

                return Ok(Result==1?"Success":"Failure");
            }
            else
            {
                return NotFound();
            }

            
        }


    }
}
