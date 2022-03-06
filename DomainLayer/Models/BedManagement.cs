using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class BedManagement
    {
        [Key]
        public Guid Id { get; set; }
        public int Floor { get; set; }
        public int Room { get; set; }
        public int Bed { get; set; }
    }
    public class BedRequest
    {
        public BedManagement[] AddBedDetails { get; set; }
        public BedManagement[] RemovedBedDetails { get; set; }
        public BedManagement[] UpdatedBedDetails { get; set; }
    }
}
