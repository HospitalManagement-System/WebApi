using DomainLayer.EntityModels;
using DomainLayer.EntityModels.Procedures;
using DomainLayer.Models;
using RepositoryLayer.Interfaces.IInboxRepository;
using ServiceLayer.Interfaces.IInboxService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.InboxService
{
    public class NotesService : INotesService
    {
        INotesRepository _notesRepository;
        public NotesService(INotesRepository notesRepository)
        {
            this._notesRepository = notesRepository;
        }

        public List<EmployeeDetails> GetEmployeeDetails()
        {
            List<EmployeeDetails> lstEmployeeDetails = _notesRepository.GetEmployees();
            return lstEmployeeDetails;
        }

        public List<NoteData> GetNotesData(Guid id)
        {
            List<NoteData> lstNotesData =  _notesRepository.GetNotes(id);
            return lstNotesData;
        }

        public void SaveNote(Notes note)
        {
            _notesRepository.AddNotes(note);
        }

        public void DeleteNote(Guid id)
        {
            _notesRepository.RemoveNotes(id);
        }

    }
}
