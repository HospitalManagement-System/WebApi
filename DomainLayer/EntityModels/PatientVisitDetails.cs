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
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public int BloodPressure { get; set; }
        public int BodyTemprature { get; set; }
        public int RespirationRate { get; set; }
        public string DoctorDescription { get; set; }
        public string ProcedureDesciption { get; set; }
        public string Medication { get; set; }
        public string Dosage { get; set; }

        [ForeignKey("Diagnosis")]
        public Guid DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        [ForeignKey("Procedure")]
        public Guid ProcedureId { get; set; }
        public Procedure Procedure { get; set; }
        [ForeignKey("Appointments")]
        public Guid AppointmentId { get; set; }
        public Appointments Appointments { get; set; }
        //[ForeignKey("PatientDetails")]
        //public Guid PatientId { get; set; }
        //public PatientDetails PatientDetails { get; set; }

        //public Guid PreviousVisitId { get; set; }

    }
}
