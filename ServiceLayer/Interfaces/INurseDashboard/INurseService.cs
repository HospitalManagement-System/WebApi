using DomainLayer.EntityModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.INurseDashboard
{
    public interface INurseService
    {
        List<BarChartDetails> GetBarChartDetails();
        List<NurseAppointment> GetnurseDetails();
        List<NurseAppointment> GetUpcomingAppointments();
        string UpdateUpcomingAppoinmets(string id, Appointments nurse);
        string UpdateNextPatient(string id, Appointments nurse);
    }
}
