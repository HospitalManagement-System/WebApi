using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Master
{
    public class RoleMaster
    {
        public RoleMaster()
        {
            lstUserDetails = new List<UserDetails>();
        }
        [Key]
        public Guid Id { get; set; }
        public string UserRole { get; set; }
        public List<UserDetails> lstUserDetails { get; set; }
    }
}
