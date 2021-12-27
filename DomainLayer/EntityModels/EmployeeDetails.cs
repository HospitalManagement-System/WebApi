using DomainLayer.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class EmployeeDetails
    {
        public EmployeeDetails()
        {
            lstSenderNotes = new List<EmployeeDetails>();
            lstRecieverNotes = new List<EmployeeDetails>();
        }
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Contact { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("UserDetails")]
        public Guid UserId { get; set; }
        public UserDetails UserDetails { get; set; }
        //public List<EmployeeDetails> lstPhysicianEmployee { get; set; }
        //public List<EmployeeDetails> lstNurseEmployee { get; set; }
        public List<EmployeeDetails> lstSenderNotes { get; set; }
        public List<EmployeeDetails> lstRecieverNotes { get; set; }

    }
}
