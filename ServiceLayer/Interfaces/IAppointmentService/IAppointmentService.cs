using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.IAppointmentService
{
    public interface IAppointmentService
    {
        Task<IEnumerable<InboxAppointment>> GetAppointmentById(Guid id);
    }
}
