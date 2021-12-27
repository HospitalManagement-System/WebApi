using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Notes
    {
        [Key]
        public Guid Id { get; set; }
        public string SenderMessage { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime SentDateTime { get; set; }
        public DateTime RecievedDateTime { get; set; }
        [ForeignKey("EmployeeDetails")]
        public Guid SenderId { get; set; }
        public EmployeeDetails SenderEmployee { get; set; }
        [ForeignKey("EmployeeDetails")]
        public Guid RecieverId { get; set; }
        public EmployeeDetails RecieverEmployee { get; set; }
    }
}
