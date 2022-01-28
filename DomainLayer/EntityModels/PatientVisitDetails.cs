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
    public class PatientVisitDetails
    {
        [Key]
        public Guid Id { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public int BloodPressure { get; set; }
        public int BodyTemprature { get; set; }
        public int RespirationRate { get; set; }
        public string DoctorDescription { get; set; }
        public string ProcedureDesciption { get; set; }
        public string DiagnosisDescription { get; set; }



        public string DrugDescription { get; set; }

        [ForeignKey("Appointments")]
        public Guid AppointmentId { get; set; }
        public Appointments Appointments { get; set; }
        public DateTime Createddate { get; set; }



        [NotMapped]
        public List<string> Diagnosislist { get; set; }
        [NotMapped]
        public List<string> Procedureslist { get; set; }
        [NotMapped]
        public List<string> Druglist { get; set; }



    }
}
