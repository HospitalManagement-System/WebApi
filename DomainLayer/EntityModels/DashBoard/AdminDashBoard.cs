using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels.DashBoard
{
    [NotMapped]
    public class AdminDashBoard: BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int Appointments { get; set; }

        public int LoackedAccount { get; set; }

        public int Patient { get; set; }
    }
}
