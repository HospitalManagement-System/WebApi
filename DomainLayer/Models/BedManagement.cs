using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool IsAvilable { get; set; }
        public int BedType { get; set; }
        public int RoomType { get; set; }
        public string FullName { get; set; }
        public string BedCost { get; set; }
        [ForeignKey("Id")]
        public Guid PatientId { get; set; }
    }
}
