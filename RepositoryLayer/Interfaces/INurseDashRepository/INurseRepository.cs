using DomainLayer.EntityModels;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces.INurseDashRepository
{
   public interface INurseRepository
    {
        List<BarChartDetails> GetBarChartDetails();
        List<NurseAppointment> GetnurseDetails();
        List<NurseAppointment> GetUpcomingAppointments();
       
        string UpdateUpAppointment(string id, Appointments nurse);
    }
}
