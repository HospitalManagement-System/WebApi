using DomainLayer.EntityModels;
using DomainLayer.Models;
using RepositoryLayer.Interfaces.INurseDashRepository;
using RepositoryLayer.Repository.NurseDashRepository;
using ServiceLayer.Interfaces.INurseDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.NurseDashBoardServices
{
  public  class NurseDashService:INurseService
    {
        private INurseRepository _repository;
        public NurseDashService(INurseRepository repository)
        {
            _repository = repository;
        }
        public List<BarChartDetails> GetBarChartDetails()
        {
            List<BarChartDetails> barChartDetails = _repository.GetBarChartDetails();
            return barChartDetails;
        }

        public List<NurseAppointment> GetnurseDetails()
        {
            List<NurseAppointment> nurseAppointmentDetails = _repository.GetnurseDetails();
            return nurseAppointmentDetails;
        }

        public List<NurseAppointment> GetUpcomingAppointments()
        {
            List<NurseAppointment> nurseUpcomingAppointments = _repository.GetUpcomingAppointments();
            return nurseUpcomingAppointments;
        }

        public string UpdateUpcomingAppoinmets(string id, Appointments nurse)
        {
            var updateappo = _repository.UpdateUpAppointment(id,nurse);
            return updateappo;
        }
    }
}
