using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels.Procedures
{
    public class UserInfo
    {
        [Key]   
        public Guid UserId { get; set; }
        public Guid EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Contact { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public string Role { get; set; }


    }



}
