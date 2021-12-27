using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class PatientRelativeDetails
    {
        public PatientRelativeDetails()
        {
            PatientDemographicDetails = new PatientDemographicDetails();
        }
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Relation { get; set; }
        public string Email { get; set; }
        public double Contact { get; set; }
        public PatientDemographicDetails PatientDemographicDetails { get; set; }
    }
}
