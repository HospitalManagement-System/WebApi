using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Master
{
    public class Procedure
    {
        public Procedure()
        {
            lstPatientVisitDetails = new List<PatientVisitDetails>();
        }
        [Key]
        public Guid Id { get; set; }
        public string ProcedureCode { get; set; }
        public string Description { get; set; }
        public bool IsDepricated { get; set; }
        public List<PatientVisitDetails> lstPatientVisitDetails { get; set; }

    }
}
