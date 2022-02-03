using DomainLayer.EntityModels;
using Microsoft.AspNetCore.Authorization;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using ServiceLayer.Interfaces.ICommonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonAPI.Controllers.Physician
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhysicianDashboardController : ControllerBase
    {
        public IAttendanceService _service { get; set; }
        private readonly ApplicationDbContext _context;
        public PhysicianDashboardController(IAttendanceService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }
        [HttpPost]
        public void PostAttendance(EmployeeAvailability employeeAttendance)
        {
            try
            {
                _service.AddAtendance(employeeAttendance);
              
            }
            catch (Exception ex)
            {

            }
        }
        [HttpGet("GetAvailablePhysicianDetails")]
        public IActionResult GetAvailablePhysicianDetails()
        {
           var employeeAvailabilities = _service.GetAttendanceAvailability();
            return Ok(employeeAvailabilities);

        }
        [HttpGet("GetNextAppointment")]
        public ActionResult GetNextAppointment()
        {



            List<NurseAppointment> getNurseDetails = _service.GetNextPatientDetails();


            return Ok(getNurseDetails);

        }


        [HttpGet("GetAllSpecialization")]
        public IActionResult GetAllSpecialization()
        {
            var Roles = (_context.RoleMaster.Where(x => x.UserRole == "PHYSICIAN").ToList());
            var Specialization = (from e in _context.EmployeeDetails
                            join u in _context.UserDetails
                            on e.UserId equals u.Id
                            join r in _context.RoleMaster
                            on u.RoleId equals r.Id
                            where (u.RoleId == Roles[0].Id)
                            select new
                            {
                                id = e.Id,
                                specialization = e.Specialization
                            }
                            ).ToList();

            return Ok(Specialization);

        }
    }
}
