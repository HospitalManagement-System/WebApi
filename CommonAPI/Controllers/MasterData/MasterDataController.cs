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


        //Diagnosics

        [HttpPost("PostDiagnosis")]
        public async Task<ActionResult<Diagnosis>> PostDiagnosis(Diagnosis diagnosis)
        {
            _context.Diagnosis.Add(diagnosis);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiagnosis", new { id = diagnosis.Id }, diagnosis);
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





    }
}
