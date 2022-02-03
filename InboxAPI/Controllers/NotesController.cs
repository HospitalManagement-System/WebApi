using DomainLayer.EntityModels;
using DomainLayer.EntityModels.Procedures;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces.IInboxService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InboxAPI.Controllers
{
    [Authorize]
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
        //[Route("GetEmployee")]
        public IActionResult GetEmployees()
        {
            try
            {
                List<EmployeeDetails> lstEmployeeDetails = _notesService.GetEmployeeDetails();
                return Ok(lstEmployeeDetails);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
       
        }

        // GET: api/<NotesController>
        [HttpGet("{id}")]
        ////[Route("GetNotesById/{id}")]
        public  IActionResult GetNotesById(Guid id)
        {
            List<NoteData> lstNotesData =  _notesService.GetNotesData(id);

            return Ok(lstNotesData);
        }

        // POST api
        // /<NotesController>
        [HttpPost]
        //[Route("PostNotes")]
        public void PostNotes([FromBody] Notes notes)
        {
            try
            {
                _notesService.SaveNote(notes);
            }
            catch (Exception ex)
            {

            }
        }

        // DELETE api/<NotesController>/5
        [HttpDelete]
        //[Route("DeleteNotes/{Id}")]
        public void Delete([FromBody]Guid id)
        {
            try
            {
                _notesService.DeleteNote(id);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
