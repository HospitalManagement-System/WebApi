using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
using RepositoryLayer;

namespace HospitalAPI.Controllers.CommonAPI.BedMangement
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedManagementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BedManagementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BedManagements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BedManagement>>> GetBedManagement()
        {
            return await _context.BedManagement.ToListAsync();
        }

        // GET: api/BedManagements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BedManagement>> GetBedManagement(Guid id)
        {
            var bedManagement = await _context.BedManagement.FindAsync(id);

            if (bedManagement == null)
            {
                return NotFound();
            }

            return bedManagement;
        }

        // PUT: api/BedManagements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBedManagement(Guid id, BedManagement bedManagement)
        {
            if (id != bedManagement.Id)
            {
                return BadRequest();
            }

            _context.Entry(bedManagement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BedManagementExists(id))
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

        // POST: api/BedManagements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BedManagement>> PostBedManagement(BedManagement bedManagement)
        {
            _context.BedManagement.Add(bedManagement);
            await _context.SaveChangesAsync();
            return Ok("done");
        }
        [HttpPost("UpdateBedStatus")]
        public async Task<ActionResult<BedManagement>> UpdateBedStatus(BedManagement bedManagement)
        {
            var result = _context.BedManagement.SingleOrDefault(i => i.Floor == bedManagement.Floor
                && i.Room == bedManagement.Room
                && i.Bed == bedManagement.Bed);
            if (result != null)
            {
                result.BedType = bedManagement.BedType;
                result.IsAvilable = bedManagement.IsAvilable;
                result.FullName = bedManagement.FullName;
                result.RoomType = bedManagement.RoomType;
                await _context.SaveChangesAsync();
            }
            return Ok("done");
        }
        [HttpPost("DeleteFloorRoomBed")]
        public async Task<ActionResult<BedManagement>> DeleteBedManagementFromDB(BedManagement bedManagement)
        {
            if (bedManagement.Floor !=-1 && bedManagement.Room == -1 && bedManagement.Bed == -1)
            {
                // Delete Floor
                _context.BedManagement.RemoveRange(_context.BedManagement.Where(i => i.Floor == bedManagement.Floor));
            }
            else if (bedManagement.Floor!=-1 &&  bedManagement.Room != -1 && bedManagement.Bed == -1)
            {
                // Delete Room
                _context.BedManagement.RemoveRange(_context.BedManagement.Where(i => i.Floor == bedManagement.Floor
                && i.Room == bedManagement.Room));

            }
            else
            {
                // Delete Bed
                _context.BedManagement.RemoveRange(_context.BedManagement.Where(i => i.Floor == bedManagement.Floor
                && i.Room == bedManagement.Room
                && i.Bed == bedManagement.Bed
                && i.Id == bedManagement.Id));
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/BedManagements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBedManagement(Guid id)
        {
            var bedManagement = await _context.BedManagement.FindAsync(id);
            if (bedManagement == null)
            {
                return NotFound();
            }

            _context.BedManagement.Remove(bedManagement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BedManagementExists(Guid id)
        {
            return _context.BedManagement.Any(e => e.Id == id);
        }
    }
}
