using DomainLayer.Models.Master;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class UserDetails
    {
        public UserDetails()
        {
            EmployeeDetails = new EmployeeDetails();
            PatientDetails = new PatientDetails();
        }
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        //public string RoleId { get; set; }
        public bool Status { get; set; }   
        public bool IsFirstLogIn { get; set; }
        public int NoOfAttempts { get; set; }
        [ForeignKey("RoleMaster")]
        public Guid RoleId { get; set; }
        public RoleMaster RoleMaster { get; set; }

        public PatientDetails PatientDetails { get; set; }
        public EmployeeDetails EmployeeDetails { get; set; }

    }
}
