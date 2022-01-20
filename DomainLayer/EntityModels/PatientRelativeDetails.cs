using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class PatientRelativeDetails
    {
        public PatientRelativeDetails()
        {
           // PatientDemographicDetails = new PatientDemographicDetails();
        }
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        //added by khushabu

        public int Pincode { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string Relation { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        
        
        [ForeignKey("PatientDemographicDetails")]
        public Guid PatientDemographicsId { get; set; }
        public PatientDemographicDetails PatientDemographicDetails { get; set; }
    }
}
