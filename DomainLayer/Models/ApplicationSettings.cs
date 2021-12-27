using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class ApplicationSettings
    {
        public string JWT_SecretKey { get; set; }
        public string Client_URL { get; set; }
    }
}
