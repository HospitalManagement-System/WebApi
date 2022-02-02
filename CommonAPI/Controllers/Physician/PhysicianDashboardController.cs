using DomainLayer.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public PhysicianDashboardController(IAttendanceService service)
        {
            _service = service;
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
    }
}
