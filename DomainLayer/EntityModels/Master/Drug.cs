using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Master
{
    public class Drug
    {
        [Key]
        public Guid Id { get; set; }
        public string DrugName { get; set; }
        public string DrugGenericName { get; set; }
        public string DrugManufacturerName { get; set; }
        public string DrugForm { get; set; }
        public string DrugStrength { get; set; }

    }
}
