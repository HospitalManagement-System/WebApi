using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class InboxAppointment
    {
        public Guid AppointmentId { get; set; }
        public string MeetingTitle { get; set; }
        public string Diagnosis { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }
}
