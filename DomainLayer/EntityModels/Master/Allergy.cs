using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Master
{
    //[Table("Allergy",Schema = "master")]
    public class Allergy
    {
        [Key]
        public Guid Id { get; set; }
        public string AllergyCode { get; set; }
        public string AllergyType { get; set; }
        public string AllergyName { get; set; }
        public string ClinicalInformation { get; set; }
       

    }
}
