using DomainLayer.Models;
using RepositoryLayer.Interfaces.IAppointmentRepository;
using ServiceLayer.Interfaces.IAppointmentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        public IAppointmentRepository _repository;
        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<InboxAppointment>> GetAppointmentById(Guid id)
        {
            IEnumerable<InboxAppointment> lstAppoitment = await _repository.GetAppointmentsById(id);
            return (lstAppoitment);
            
        }
    }
}
