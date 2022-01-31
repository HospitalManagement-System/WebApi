using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels.Procedures
{
    [NotMapped]
    public class NoteData
    {
        public Guid Id { get; set; }
        public string Designation { get; set; }
        public string SenderName { get; set; }
        public string RecieverName { get; set; }
        public string Message { get; set; }
        public string IsSentOrRecieved { get; set; }
        public DateTime NotesDateTime { get; set; }
    }
}

