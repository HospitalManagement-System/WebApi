using DomainLayer.EntityModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces.IInboxRepository
{
    public interface INotesRepository
    {
        List<NotesData> GetNotes(Guid id);
        List<EmployeeDetails> GetEmployees();
        void AddNotes(Notes notes);
        void RemoveNotes(Guid id);
    }
}
