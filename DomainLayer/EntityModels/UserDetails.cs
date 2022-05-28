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
        //public UserDetails()
        //{
        //    EmployeeDetails = new EmployeeDetails();
        //    PatientDetails = new PatientDetails();
        //}
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }
        public string Designation { get; set; }
        public bool Status { get; set; }   
        public bool IsFirstLogIn { get; set; }
        public int NoOfAttempts { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }


        public Guid RoleId { get; set; }
        [ForeignKey("Id")]
        public RoleMaster RoleMaster { get; set; }

        [NotMapped]
        public string Role { get; set; }
        [NotMapped]
        public string Token { get; set; }

        public PatientDetails PatientDetails { get; set; }
        public EmployeeDetails EmployeeDetails { get; set; }

    }   
}
