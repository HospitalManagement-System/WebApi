using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class NurseAppointment
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }   
        public char Gender { get; set; }
        public string Diagnosis { get; set; }
        public int Contact { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PhysicanName { get; set; }
        public string Date { get; set; }
       
    }
}
