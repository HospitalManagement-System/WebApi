using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels.ListModels
{
    public class UserInfoDetails
    {

        public Guid Id { get; set; }
        public string Role { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public bool IsFirstLogin { get; set; }



      
    }
}
