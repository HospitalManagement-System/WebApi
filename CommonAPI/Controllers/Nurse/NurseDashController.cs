using DomainLayer;
using DomainLayer.EntityModels;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces.INurseDashboard;
using ServiceLayer.Services.NurseDashBoardServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NurseDashAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NurseDashController : ControllerBase
    {
        private INurseService _BarChartService;

        public NurseDashController(INurseService barChartService)
        {
            _BarChartService = barChartService;


        }
        [HttpGet]
        [Route("GetallBarChartDetails")]
        public IActionResult GetallBarChartDetails()
        {
            List<BarChartDetails> getdetails = _BarChartService.GetBarChartDetails();


            return Ok(getdetails);
        }


        [HttpGet("GetNurseAppointment")]

        public ActionResult GetNurseAppointment()
        {



            List<NurseAppointment> getNurseDetails = _BarChartService.GetnurseDetails();


            return Ok(getNurseDetails);

        }
        [HttpGet("GetNurseUpComingAppointment")]
        public ActionResult GetNurseUpComingAppointment()
        {

            List<NurseAppointment> getUpcomingAppointmentDetails = _BarChartService.GetUpcomingAppointments();
            return Ok(getUpcomingAppointmentDetails);

        }
        [HttpPut("UpdateUpcomingAppoinmets")]
        public string UpdateUpcomingAppoinmets(string Id, Appointments nurse)
        {
            var updateapp = _BarChartService.UpdateUpcomingAppoinmets(Id, nurse);
            return updateapp;
        }
        [HttpPut("UpdateNextPatient")]
        public string UpdateNextPatient(string Id, Appointments nurse)
        {
            var updateapp = _BarChartService.UpdateNextPatient(Id, nurse);
            return updateapp;
        }
    }
}
