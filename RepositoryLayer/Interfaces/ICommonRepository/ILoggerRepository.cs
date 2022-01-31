using DomainLayer.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces.ICommonRepository
{
    public interface ILoggerRepository
    {
        Task SaveLog(Logger logger);
    }
}
