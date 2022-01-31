using DomainLayer.EntityModels;
using DomainLayer.EntityModels.Procedures;
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
        List<NoteData> GetNotes(Guid id);
        List<EmployeeDetails> GetEmployees();
        Task AddNotes(Notes notes);
        Task RemoveNotes(Guid id);
    }
}
