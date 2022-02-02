using DomainLayer.EntityModels.Master;
using DomainLayer.Models.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonAPI.Controllers.MasterData
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MasterDataController(ApplicationDbContext context)
        {

            _context = context;

        }


        [HttpGet("GetDiagnosisData")]
        public async Task<IActionResult> GetDiagnosisData()
        {
            var result = await (from c in _context.Diagnosis
                                select new { ID = c.Id, Value = c.DiagnosisCode }
                          ).ToListAsync();
            //await _context.Diagnosis.ToListAsync();
            return Ok(result);
        }

        [HttpGet("GetEducation")]
        public async Task<ActionResult<IEnumerable<Education>>> GetEducation()
        {
            return await _context.Education.ToListAsync();
        }


        [HttpGet("GetEducation/{Type}")]
        public async Task<ActionResult<IEnumerable<Education>>> GetEducation(string Type)
        {

            var User = (_context.RoleMaster.Where(x => x.UserRole == Type.ToUpper()).FirstOrDefault());

            var Education = await _context.Education.Where(e => e.RoleId == User.Id).ToListAsync();

            return Ok(Education);
        }


        [HttpGet("GetDesignation/{Type}")]
        public async Task<ActionResult<IEnumerable<Designation>>> GetDesignation(string Type)
        {

            var User = (_context.RoleMaster.Where(x => x.UserRole == Type.ToUpper()).FirstOrDefault());

            var Designation = await _context.Designation.Where(e => e.RoleId == User.Id).ToListAsync();

            return Ok(Designation);
        }
        

        [HttpGet("GetDepartment/{Type}")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment(string Type)
        {

            var User = (_context.RoleMaster.Where(x => x.UserRole == Type.ToUpper()).FirstOrDefault());
            var Department = await _context.Department.Where(e => e.RoleId == User.Id).ToListAsync();
            return Ok(Department);
        }




        [HttpGet("Specialization/{Type}")]
        public async Task<ActionResult<IEnumerable<Specalization>>> Specialization(string Type)
        {

            var User = (_context.RoleMaster.Where(x => x.UserRole == Type.ToUpper()).FirstOrDefault());
            var Specialization = await _context.Specalization.Where(e => e.RoleId == User.Id).ToListAsync();
            return Ok(Specialization);
        }

        [HttpGet("GetGender")]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGender()
        {
            var Gender = await _context.Gender.ToListAsync();
            return Ok(Gender);
        }








    }
}
