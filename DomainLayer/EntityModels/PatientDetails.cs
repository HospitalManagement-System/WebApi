using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class PatientDetails
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Contact { get; set; }
        public bool IsActive { get; set; }
        
        //public Guid PatientDemographicId { get; set; }
        //  [ForeignKey("Id")]
        public PatientDemographicDetails PatientDemographicDetails { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("Id")]
        public UserDetails UserDetails { get; set; }

        public List<Appointments> lstAppointments { get; set; }
        

    }
    public class AllocatedPatientDetails
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Contact { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; } 
        public string Email { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public bool IsFatal { get; set; }
        public Guid PatientId { get; set; }
        public string AllergyDetails { get; set; }
        public string physicianId { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime Createddate { get; set; }
        public string Password { get; set; }
        public BedManagement BedDetails { get; set; }
    }
}
