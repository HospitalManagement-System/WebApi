using DomainLayer.EntityModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.IInboxService
{
    public interface INotesService
    {
        List<Notes> GetNotesData(Guid id);
        List<EmployeeDetails> GetEmployeeDetails();
        void SaveNote(Notes note);
        void DeleteNote(Guid id);
    }
}
