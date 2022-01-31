using DomainLayer.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.ICommonService
{
    public interface ILoggerService
    {
        Task WriteLog(Logger logger);
    }
}
