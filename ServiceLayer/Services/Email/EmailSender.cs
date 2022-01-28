using Newtonsoft.Json;
using ServiceLayer.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static DomainLayer.Models.Mail;

namespace ServiceLayer.Services.Email
{
    public class EmailSender : IEmailSender
    {
      

        public async  Task<string> SendLoginSMSAsync(string UserName,string Password, string Email)
        {

            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            var FilePath = $"{currentDirectory.Replace("\\CommonAPI","")}\\ServiceLayer\\EmailTemplates\\EmployeeRegistartion.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            //Replace Email
            //MailText = MailText.Replace("[newusername]", UserName).Replace("[username]",Email).Replace("[passsword]",Password);

            MailText = MailText.Replace("[username]", Email).Replace("[passsword]", Password);

            //Root root = new Root();
            //root.from = "Cosmo Hospital";
            //root.to = Email;
            //root.subject = "Login Details";
            ////root.message = $"<p>Welcome To Cosmos Hospital</p> <p>Dear {UserName} </p> <p>Your Temporary Email and Password Has been Created</p> <p>UserName:{Email}</p> <p>UserName:{Password}</p>";
            //root.message = MailText;
            ////Serialize
            //string output = JsonConvert.SerializeObject(root);
            //var client = new HttpClient();
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Post,
            //    RequestUri = new Uri("https://easymail.p.rapidapi.com/send"),
            //    Headers =
            //    {
            //       { "x-user-name", "test" },
            //       { "x-rapidapi-host", "easymail.p.rapidapi.com" },
            //       { "x-rapidapi-key", "797560b494msha740e3c190fc521p1121f9jsnf1246d569dde" },
            //    },

            //    Content = new StringContent(output)
            //    {
            //        Headers =
            //         {
            //             ContentType = new MediaTypeHeaderValue("application/json")
            //         }
            //    }
            //};
            //using (var response = await client.SendAsync(request))
            //{
            //    response.EnsureSuccessStatusCode();
            //    var body = await response.Content.ReadAsStringAsync();
            //    return body;

            //}

            return "Success";

        }

        public async Task<string> SendAppointmentAsync(string Email,string UserName, string Physician, string Diagnosics, DateTime dateTime)
        {
            //Fetching Email Body Text from EmailTemplate File.  
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            var FilePath = $"{currentDirectory.Replace("\\CommonAPI", "")}\\ServiceLayer\\EmailTemplates\\ApproveAppointment.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            MailText = MailText.Replace("[username]", UserName).Replace("[Physician]", Physician).Replace("[Diagnosics]", Diagnosics).Replace("[Date]", dateTime.ToString());

            Root root = new Root();
            root.from = "Cosmo Hospital";
            root.to = Email;
            root.subject = "Appointment Approved";
           
            root.message = MailText;
            //Serialize
            string output = JsonConvert.SerializeObject(root);
            var client = new HttpClient();
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Post,
            //    RequestUri = new Uri("https://easymail.p.rapidapi.com/send"),
            //    Headers =
            //    {
            //       { "x-user-name", "test" },
            //       { "x-rapidapi-host", "easymail.p.rapidapi.com" },
            //       { "x-rapidapi-key", "797560b494msha740e3c190fc521p1121f9jsnf1246d569dde" },
            //    },

            //    Content = new StringContent(output)
            //    {
            //        Headers =
            //         {
            //             ContentType = new MediaTypeHeaderValue("application/json")
            //         }
            //    }
            //};
            //using (var response = await client.SendAsync(request))
            //{
            //    response.EnsureSuccessStatusCode();
            //    var body = await response.Content.ReadAsStringAsync();
            //    return body;

            //}

            return "Success";
        }





    }
}
