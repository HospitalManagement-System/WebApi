using DomainLayer.EntityModels;
using RepositoryLayer.Interfaces.ICommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository.CommonRepository
{
    public class LoggerRepository : ILoggerRepository
    {
        ApplicationDbContext _context;
        public LoggerRepository(ApplicationDbContext context)
        {
            this._context = context;

        }
        public async Task SaveLog(Logger logger)
        {
            //await _context.Logger.AddAsync(logger);
        }
    }
}
