using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Message
    {
        public string Reciever { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        //public List<IFormFile> Attachments { get; set; }
    }
}
