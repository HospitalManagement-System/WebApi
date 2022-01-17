using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Mail
    {

        public class Root
        {
            public string from { get; set; }
            public string to { get; set; }
            public string subject { get; set; }
            public string message { get; set; }
        }

    }
}
