using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IEmailSender
    {
         Task<string> SendLoginSMSAsync(string UserName,string Password, string Email);

        Task<string> SendAppointmentAsync(string Email,string UserName, string Physician, string Diagnosics,DateTime dateTime);
        Task<string> ForgotPassword(string Email, string Username);
    }
}
