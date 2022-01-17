using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels.Procedures
{
    [Keyless]
    public class ResultStatus
    {
        public string Result { get; set; }
    }
}
