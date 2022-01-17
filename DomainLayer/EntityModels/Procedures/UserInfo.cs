using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels.Procedures
{
    [NotMapped]
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Contact { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public int IsActive { get; set; }

        public int isDisabled { get; set; }

        public string Role { get; set; }
    }
}
