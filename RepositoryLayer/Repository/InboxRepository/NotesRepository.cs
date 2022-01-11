using DomainLayer.EntityModels;
using DomainLayer.Models;
using RepositoryLayer.Interfaces.IInboxRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Repository.InboxRepository
{
    public class NotesRepository : INotesRepository
    {
        ApplicationDbContext _context;
        public NotesRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void AddNotes(Notes notes)
        {
            _context.Notes.Add(notes);
            _context.SaveChanges();
        }

        public List<EmployeeDetails> GetEmployees()
        {
            List<EmployeeDetails> lstEmployeeDetails = _context.EmployeeDetails.ToList();
            return lstEmployeeDetails;
        }

        public List<NotesData> GetNotes(Guid id)
        {
            //List<Notes> lstNotes = null;
            //var notes = _context.Notes
            //               .Where(s => s.SenderEmployeeId == id &&
            //                            s.RecieverEmployeeId == id)
            //               .Include(s => s.RecieverEmployeeDetails)
            //               .Include(s => s.SenderEmployeeDetails)
            //               .ToList();


            var notes = from t1 in _context.Notes
                        where t1.RecieverEmployeeId == id || t1.SenderEmployeeId == id
                        join t2 in _context.EmployeeDetails
                        on t1.RecieverEmployeeId equals t2.Id
                        select new
                        {
                            t1.Id,
                            t1.Message,
                            t1.NotesDateTime,
                            t1.IsSent,
                            t2.FirstName,
                            t2.LastName
                        };

            //var notes = _context.Database.("EXEC dbo.sp_GetNotes {0}" , id);
            return (List<NotesData>) notes;
        }

        public void RemoveNotes(Guid id)
        {
            var notes = _context.Notes.Where(x => x.Id == id).FirstOrDefault();
            _context.Notes.Remove(notes);
            _context.SaveChanges();
        }

    }
}
