using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces.IAppointmentRepository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<InboxAppointment>> GetAppointmentsById(Guid id);
    }
}
