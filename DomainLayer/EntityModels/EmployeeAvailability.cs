using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels
{
    public class EmployeeAvailability
    {
        public Guid Id { get; set; }
        public Guid PhysicianId { get; set; }
        public string TimeSlot { get; set; }
        public string[] arrTimeSlot { get; set; }
        public bool IsAbsent { get; set; }
        public DateTime DateTime { get; set; }

    }
}
