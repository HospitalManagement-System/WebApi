using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RepositoryLayer;
using ServiceLayer.Interfaces.IAppointmentService;
using ServiceLayer.Interfaces;
using ServiceLayer.Interfaces.IZoom;
using DomainLayer.Models.Master;
using Microsoft.IdentityModel.Tokens;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IAppointmentService _service;
        //private readonly IEmailSender _iEMailSender;

        private readonly IEmailSender _iEMailSender;

        private readonly IZoom _zoom;

        public AppointmentsController(ApplicationDbContext context, IEmailSender emailSender, IZoom zoom, IAppointmentService service)
        {
            _context = context;
            _iEMailSender = emailSender;
            _zoom = zoom;
            _service = service;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        [HttpGet("GetAppointmentsByEmployeeId")]
        public async Task<IActionResult> GetAppointmentsByEmployeeId(Guid id)
        {
            try
            {
                IEnumerable<InboxAppointment> lstAppointment = await _service.GetAppointmentById(id);
                return Ok(lstAppointment);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            //return await _context.Appointments.Where(x => x.PhysicianId == id || x.NurseId == id).ToListAsync();
        }


        [HttpGet("GetAllAppointments")]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetAllAppointments()
        {
            var result = await (from c in _context.Appointments
                               .Where(p => p.AppointmentStatus != ("Rejected") && p.AppointmentDateTime >= DateTime.Now)
                                select new
                                {
                                    publicId = c.Id,
                                    title = c.AppointmentType,
                                    date = c.AppointmentDateTime,
                                    description = "Test",
                                    color = (
                                    c.AppointmentStatus.Equals("Pending") ? "red" :
                                    c.AppointmentStatus.Equals("Approved") ? "green" :
                                    c.AppointmentStatus.Equals("Rejected") ? "blue" : "Unknown"
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
            catch (Exception ex)
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

            try
            {
                Appointments appointmentsData = new Appointments();
                var Time = appointments.AppointmentDateTime;
                var Hour = appointments.bookslot;
                string[] timeSplit = Hour.Split("to");
                string[] secondSplit = timeSplit[0].Split(":");
                if (secondSplit[1].Contains("30"))
                {
                    int Hours = Convert.ToInt32(secondSplit[0]);
                    int Minutes = Convert.ToInt32(secondSplit[1]);
                    DateTime dt3 = new DateTime(Time.Year, Time.Month, Time.Day, Hours, Minutes, 0);
                    appointmentsData.AppointmentDateTime = dt3;
                }
                else
                {
                    int Hours = Convert.ToInt32(timeSplit[0]);
                    DateTime dt3 = new DateTime(Time.Year, Time.Month, Time.Day, Hours, 0, 0);
                    appointmentsData.AppointmentDateTime = dt3;
                }

                appointmentsData.Diagnosis = appointments.Diagnosis;
                appointmentsData.AppointmentType = appointments.AppointmentType;
                appointmentsData.bookslot = appointments.bookslot;
                appointmentsData.PatientId = appointments.PatientId;
                appointmentsData.PhysicianId = appointments.PhysicianId;
                appointmentsData.Mode = appointments.Mode;
                appointmentsData.AppointmentStatus = appointments.AppointmentStatus;
                appointmentsData.QueueStatus = "Upcoming";
                if (appointments.Mode == "Online")
                {
                    var ZoomLinks = _zoom.Zoom();
                    appointmentsData.PhysicianMeetingLink = ZoomLinks.Item1;
                    appointmentsData.PatientMeetingLink = ZoomLinks.Item2;
                }
                _context.Appointments.Add(appointmentsData);
                var SaveResult = await _context.SaveChangesAsync();
                string Result = (SaveResult == 1) ? "Success" : "Failure";


                return Ok(Result);

            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        [HttpPut("UpdateAppointments/{AppointmentId}")]
        public async Task<ActionResult> UpdateAppointments(string AppointmentId, Appointments appointments)
        {
            try
            {
                var appointmentId = new Guid(AppointmentId);

                var GetAppointment = _context.Appointments.Where(x => x.Id == appointmentId).FirstOrDefault();

                if (GetAppointment != null)
                {
                    GetAppointment.AppointmentDateTime = appointments.AppointmentDateTime;
                    GetAppointment.bookslot = appointments.bookslot;
                    GetAppointment.PhysicianId = appointments.PhysicianId;

                }

                //_context.Appointments.Update(appointments);
                var SaveResult = await _context.SaveChangesAsync();
                string Result = (SaveResult == 1) ? "Success" : "Failure";
                return Ok(Result);


            }
            catch (Exception ex)
            {

                throw;
            }



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
        //public async Task<ActionResult<IEnumerable<Appointments>>> GetBookSlots(string Id, DateTime appointmentdateTime)
        public async Task<IActionResult> GetBookSlots(string Id, DateTime appointmentdateTime)
        {

            Guid PhysicianID = new Guid(Id);

            var slot = _context.Appointments.Where(x => x.PhysicianId == PhysicianID && x.bookslot != null
                          && x.AppointmentDateTime.Date == Convert.ToDateTime(appointmentdateTime).Date)
                            .Select(t => t.bookslot).ToList();
            string empslot = _context.EmployeeAvailability.Where(x => x.PhysicianId == PhysicianID
                                      && x.DateTime.Date == Convert.ToDateTime(appointmentdateTime).Date).Select(x => x.TimeSlot).FirstOrDefault();

            string[] arrempavailable = empslot.Split(',');

            slot.AddRange(arrempavailable);
            return Ok(slot);

            //await (from c in _context.Appointments
            //              where (c.PhysicianId == PhysicianID && c.bookslot != null &&
            //              c.AppointmentDateTime.Date == Convert.ToDateTime(appointmentdateTime).Date
            //              )
            //              select new { c.bookslot }).ToListAsync();



        }

        [HttpPatch("ApproveReject/{Id}")]
        public IActionResult ApproveReject(string Id,string Status, string DeletedReason="")
        {
            Guid AppointmentId = new Guid(Id);

            
            var FindAppointment = _context.Appointments.Where(x => x.Id == AppointmentId).FirstOrDefault();

            if (FindAppointment != null)
            {
                FindAppointment.AppointmentStatus = Status;
                if(string.IsNullOrEmpty(DeletedReason))
                {
                    FindAppointment.DeletedReason = "Canceled Appointment From Calendar";
                }
                else
                {
                    FindAppointment.DeletedReason = DeletedReason;
                }
                var Result= _context.SaveChanges();

                return Ok(Result == 1 ? "Success" : "Failure");
            }
            else
            {
                return NotFound();
            }


        }

        //Get: api/DeclineAppointments
        [HttpGet("DeclineAppointments")]
        public async Task<ActionResult<IEnumerable<Appointments>>> DeclineAppointments()
        {

            var User = (

                 from a in _context.Appointments
                 join e in _context.EmployeeDetails
                 on a.PhysicianId equals e.Id
                 join r in _context.RoleMaster
                 on a.DeletedBy equals r.Id
                 where (a.AppointmentStatus == "Rejected")
                 select new
                 {
                     a.Id,
                     a.AppointmentType,
                     a.AppointmentDateTime,
                     DoctorName = e.Title + e.FirstName + e.LastName,
                     a.Diagnosis,
                     a.AppointmentStatus,
                     a.IsCompleted,
                     a.DeletedReason,
                     DeletedBy = r.UserRole

                 }).ToList();



            return Ok(User);
        }
        //Get: api/UpcomingAppointments
        [HttpGet("UpcomingAppointments")]
        public async Task<ActionResult<IEnumerable<Appointments>>> UpcomingAppointments()
        {
            var User = (
              from a in _context.Appointments
              join e in _context.EmployeeDetails
              on a.PhysicianId equals e.Id
              join p in _context.PatientDetails
              on a.PatientId equals p.Id
              join pd in _context.PatientDemographicDetails
              on p.PatientDemographicId equals pd.Id
              where (a.AppointmentDateTime >= DateTime.Now & a.AppointmentStatus == "Approved")
              select new
              {

                  a.Id,
                  a.AppointmentType,
                  a.AppointmentDateTime,
                  DoctorName = e.Title + e.FirstName + e.LastName,
                  a.Diagnosis,
                  a.AppointmentStatus,
                  a.IsCompleted,
                  a.DeletedReason

              }).ToList();



            return Ok(User);
        }

        //Get: api/PastAppointments
        [HttpGet("PastAppointments")]
        public async Task<ActionResult<IEnumerable<Appointments>>> PastAppointments()
        {
            var User = (
              from a in _context.Appointments
              join e in _context.EmployeeDetails
              on a.PhysicianId equals e.Id
              where (a.AppointmentDateTime < DateTime.Now & a.AppointmentStatus == "Approved")
              select new
              {
                  a.Id,
                  a.AppointmentType,
                  a.AppointmentDateTime,
                  DoctorName = e.Title + e.FirstName + e.LastName,
                  a.Diagnosis,
                  a.AppointmentStatus,
                  a.IsCompleted,
                  a.DeletedReason

              }).ToList();



            return Ok(User);
        }


        [HttpGet("GetAllPhysician")]
        public IActionResult GetAllPhysician()
        {
            var Roles = (_context.RoleMaster.Where(x => x.UserRole == "PHYSICIAN").ToList());
            var Physican = (from e in _context.EmployeeDetails
                            join u in _context.UserDetails
                            on e.UserId equals u.Id
                            join r in _context.RoleMaster
                            on u.RoleId equals r.Id
                            where(u.RoleId == Roles[0].Id)
                            select new
                            {
                                Id = e.Id,
                                PhysicianName = e.FirstName
                            }
                            );

            return Ok(Physican);

        }

        [HttpGet("GetPhysicianByDiagnosics/{Id}")]
        public IActionResult GetPhysicianByDiagnosics(string Id)
        {
            var Diagnosics = new Guid(Id);

            var Physican = (from e in _context.EmployeeDetails
                            join u in _context.UserDetails
                            on e.UserId equals u.Id
                            join r in _context.RoleMaster
                            on u.RoleId equals r.Id
                            where (r.UserRole.ToUpper() == "PHYSICIAN")
                            select new
                            {
                                Id = e.Id,
                                PhysicianName = e.FirstName
                            }
                            );

            return Ok(Physican);

        }

        [HttpGet("GetEditBookAppointmentDetails/{AppointmentId}")]
        public IActionResult GetEditBookAppointmentDetails(string AppointmentId)
        {

            try
            {
                var appointmentID = new Guid(AppointmentId);

                var BookedAppointmentData = (from a in _context.Appointments
                                             join pd in _context.PatientDetails
                                             on a.PatientId equals pd.Id into App
                                             from m in App.DefaultIfEmpty()
                                             join e in _context.EmployeeDetails
                                             on a.PhysicianId equals e.Id into Emp
                                             from e in Emp.DefaultIfEmpty()
                                             where (a.Id == appointmentID)
                                             select new
                                             {
                                                 id = a.Id,
                                                 appointmentType = a.AppointmentType,
                                                 slotBooked = a.bookslot,
                                                 appointmentDateTime = a.AppointmentDateTime,
                                                 patientName = m != null ? m.FirstName : "Unknown",
                                                 physicianName = e != null ? e.FirstName : "Unknown",
                                                 diagnosis = a.Diagnosis,

                                             }
                                );

                return Ok(BookedAppointmentData);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            return NotFound();
        }

        [HttpGet("GetZoomLink/{Id}")]
        public IActionResult GetZoomLink(string Id, string Role)
        {
            var appointmentID = new Guid(Id);
            var Url = (from a in _context.Appointments
                       where (a.Mode == "Online" && a.Id == appointmentID)
                       select new
                       {
                           PatientMeetingLink = a.PatientMeetingLink,
                           PhysicianMeetingLink = a.PhysicianMeetingLink
                       }
                       );


            return Ok(Url);
        }

        //Get: api/GetPrescriptions
        [HttpGet("GetPrescriptions/{Id}")]
        public async Task<ActionResult<IEnumerable<Drug>>> GetPrescriptions(string Id)
        {
            var appointmentID = new Guid(Id);

            var pvDetails = _context.PatientVisitDetails.Where(e => e.AppointmentId == appointmentID)
                            .FirstOrDefault();

            IList<Drug> drugslist = new List<Drug>();

            try
            {
                drugslist = _context.Drug.Where(e => pvDetails.DrugDescription.Contains(
                    e.DrugName.Trim())).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Ok(drugslist);
        }

    }
}





