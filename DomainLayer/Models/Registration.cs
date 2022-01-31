using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Registration
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public double Contact { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Designation { get; set; }
        public string Specilization { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }
    }
}
