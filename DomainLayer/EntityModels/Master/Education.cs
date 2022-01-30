using DomainLayer.Models.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels.Master
{
    public class Education
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
