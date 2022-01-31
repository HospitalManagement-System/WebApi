using DomainLayer.EntityModels;
using RepositoryLayer.Interfaces.ICommonRepository;
using ServiceLayer.Interfaces.ICommonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.CommonService
{
    public class LoggerService : ILoggerService
    {
        ILoggerRepository _repository;
        public LoggerService(ILoggerRepository repository)
        {
            this._repository = repository;
        }
        public async Task WriteLog(Logger logger)
        {
            await _repository.SaveLog(logger);
        }
    }
}
