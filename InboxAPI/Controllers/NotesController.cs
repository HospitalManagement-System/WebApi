using DomainLayer.EntityModels;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces.IInboxService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        public INotesService _notesService;
        public NotesController(INotesService notesService)
        {
            this._notesService = notesService;
        }

        [HttpGet]
        [Route("GetEmployee")]
        public IActionResult GetEmployees()
        {
            List<EmployeeDetails> lstEmployeeDetails = _notesService.GetEmployeeDetails();
            return Ok(lstEmployeeDetails);
        }

        // GET: api/<NotesController>
        [HttpGet]
        [Route("GetNotesById/{id}")]
        public IActionResult GetNotesById([FromQuery] Guid id)
        {
            List<Notes> lstNotes = _notesService.GetNotesData(id);
            return Ok(lstNotes);
        }

        // POST api/<NotesController>
        [HttpPost]
        public void Post([FromBody] Notes notes)
        {
            _notesService.SaveNote(notes);
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        [Route("DeleteNotes/{Id}")]
        public void Delete([FromQuery] Guid id)
        {
            _notesService.DeleteNote(id);
        }
    }
}
