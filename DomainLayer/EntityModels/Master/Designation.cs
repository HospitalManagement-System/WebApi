using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Models.Master;

namespace DomainLayer.EntityModels.Master
{
    public class Designation
    {
        [Key]
        public Guid Id { get; set; }
        public string Value { get; set; }
        [ForeignKey("Id")]
        public Guid RoleId { get; set; }
        //[ForeignKey("Id")]
        //public RoleMaster RoleMaster { get; set; }
    }
}
