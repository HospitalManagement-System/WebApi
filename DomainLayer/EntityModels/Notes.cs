using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels
{
    public class Notes
    {
        [Key]
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime NotesDateTime { get; set; }
        public bool IsSent { get; set; }

        public Guid SenderEmployeeId { get; set; }
        //[ForeignKey("Id")]
        public EmployeeDetails SenderEmployeeDetails { get; set; }

        public Guid RecieverEmployeeId { get; set; }
        //[ForeignKey("Id")]
        public EmployeeDetails RecieverEmployeeDetails { get; set; }

        //[Key]
        //public Guid Id { get; set; }
        //public string SentMessage { get; set; }
        //public string RecievedMessage { get; set; }
        //public DateTime SentDateTime { get; set; }
        //public DateTime RecievedDateTime { get; set; }
        //[ForeignKey("EmployeeDetails")]
        //public Guid SenderId { get; set; }
        //public virtual EmployeeDetails SenderEmployee { get; set; }
        //[ForeignKey("EmployeeDetails")]
        //public Guid RecieverId { get; set; }
        //public EmployeeDetails RecieverEmployee { get; set; }

    }
}
