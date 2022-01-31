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
using DomainLayer.EntityModels.Procedures;

namespace RepositoryLayer.Repository.InboxRepository
{
    public class NotesRepository : INotesRepository
    {
        public ApplicationDbContext _context;
        public NotesRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task AddNotes(Notes notes)
        {
            try
            {
                _context.Notes.Add(notes);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {

            }
          
        }

        public List<EmployeeDetails> GetEmployees()
        {
            try
            {
                List<EmployeeDetails> lstEmployeeDetails = _context.EmployeeDetails.ToList();
                return lstEmployeeDetails;
            }
            catch (SqlException ex)
            {
                return null;
            }
           
        }

        public List<NoteData> GetNotes(Guid loggedinUserId)
        {
            try
            {

                var notes = _context.Notes.Where(item => item.SenderEmployeeId == loggedinUserId
                                                || item.RecieverEmployeeId == loggedinUserId).ToList();

                var users1 = notes.Select(item => item.SenderEmployeeId);
                var users2 = notes.Select(item => item.RecieverEmployeeId);

                var users = new List<Guid>();
                users.AddRange(users1);
                users.AddRange(users2);

                var employees = _context.EmployeeDetails.Where(item => users.Contains(item.Id)).ToList().Distinct()
                    .Select(data => (data.Id, Name: $"{data.FirstName} { data.LastName}")).ToDictionary(item => item.Id, item => item.Name);

                var result = notes.Select(item => new NoteData
                {
                    Id = item.Id,
                    Message = item.Message,
                    NotesDateTime = item.NotesDateTime,
                    Designation = item.Designation,
                    RecieverName = employees[item.RecieverEmployeeId],
                    SenderName = employees[item.SenderEmployeeId],
                    IsSentOrRecieved = item.SenderEmployeeId == loggedinUserId ? "SENT" : "RECIEVED"
                }).ToList();

                return result;
            }
            catch (SqlException ex)
            {
                return null;
            }

        }

        public async Task RemoveNotes(Guid id)
        {
            try
            {
                var notes = new Notes
                {
                    Id = id
                };
                //var notes = _context.Notes.Where(x => x.Id == id).FirstOrDefault();
                _context.Notes.Remove(notes);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {

            }
        }

    }
}
