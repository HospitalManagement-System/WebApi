using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class PatientDemographicDetails
    {
        public PatientDemographicDetails()
        {
            //PatientDetails = new PatientDetails();
        }
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; } //enum
        public string Race { get; set; }
        public string Ethinicity { get; set; }
        public string Address { get; set; }
        ////added by khushabu

        public int Pincode { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        [NotMapped]
       // public string Allergylist { get; set; }
        public List<string> Allergylist { get; set; }
        public bool IsFatal { get; set; }
        
        
        [ForeignKey("PatientDetails")]
        public Guid PatientId { get; set; }
        public PatientDetails PatientDetails { get; set; }

        public string AllergynameList { get; set; }
        public string AllergytypeList { get; set; }
         public string ClinicalInformation { get; set; }
       
        public PatientRelativeDetails PatientRelativeDetails { get; set; }

        public DateTime Createddate { get; set; }

        public string AllergyDetails { get; set; }

        [NotMapped]
        public List<string> AllergyListname { get; set; }

    }
}
