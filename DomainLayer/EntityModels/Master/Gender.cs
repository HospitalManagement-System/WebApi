using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels.Master
{
    public class Gender
    {
        [Key]
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
