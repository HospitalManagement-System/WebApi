using DomainLayer.EntityModels.Master;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommonAPI.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentApiController : ControllerBase
    {


        private ApplicationDbContext _context;
        public PaymentApiController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }


        [HttpPost("CreateOrder/{PatientId}")]
        public IActionResult CreateOrder(string PatientId,int Amount)
        {
            var patientId = new Guid(PatientId);
            // Generate random receipt number for order
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_FksGPtneT1LOtK", "Cj68lGSNBPqWcnvKRjVS5Ft8");
            Dictionary<string, object> options = new Dictionary<string, object>();
            if(Amount==0)
            {
                options.Add("amount", 19900);
            }
            else
            {
                options.Add("amount", Amount * 100);
            }
            //options.Add("amount", 19900);
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
                                                 //options.Add("notes", "-- You can put any notes here --");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            string orderId = orderResponse["id"].ToString();


            var Patient = (from m in _context.UserDetails
                           join p in _context.PatientDetails
                           on m.Id equals p.UserId
                           join pd in _context.PatientDemographicDetails
                           on p.Id equals pd.PatientId
                           where(m.Id==patientId)
                           select new
                           {
                               p.FirstName,
                               pd.Email,
                               pd.Contact,
                               Address = pd.Address 
                           }
                           ).FirstOrDefault();


            // Create order model for return on view
            OrderModel orderModel = new OrderModel
            {
                orderId = orderResponse.Attributes["id"],
                razorpayKey = "rzp_test_FksGPtneT1LOtK",
                amount =Amount* 100,
                currency = "INR",
                name = Patient.FirstName,
                email =Patient.Email,
                contactNumber = Patient.Contact,
                address = Patient.Address,
                description = "Testing description"
            };

            

            return Ok(orderModel);
        }


        [HttpPost("Complete/{PatientId}")]
        public IActionResult Complete(string PatientId,string paymentId,string orderId)
        {
            var patientId = new Guid(PatientId);

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("rzp_test_FksGPtneT1LOtK", "Cj68lGSNBPqWcnvKRjVS5Ft8");

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];
            int Ammount = (Convert.ToInt32(amt)/100);
            Subscription subscription = new Subscription();
            subscription.PatientId = patientId;
            subscription.paymentId = paymentId;
            subscription.orderId = orderId;
            subscription.Amount = Ammount;
            if(Ammount==199)
            {
                subscription.Enable = 5;
            }
            else if(Ammount==499)
            {
                subscription.Enable = 10;
            }
            else if (Ammount == 999)
            {
                subscription.Enable = 100;
            }
            subscription.StartDate = DateTime.Now;
            subscription.EndDate = DateTime.Now.AddYears(1);

             _context.Subscription.Add(subscription);
            var Save = _context.SaveChanges();
            var Result = (Save == 1 ? "Success" : "Failure");


            //// Check payment made successfully

            if (paymentCaptured.Attributes["status"] == "captured" && Result=="Success")
            {
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
        }


        [HttpGet("GetSubscribedData/{PatientId}")]
        public IActionResult GetSubscribedData(string PatientId)
         {
            var patientID = new Guid(PatientId);
            var Amount = (from s in _context.Subscription.Where(x=>x.PatientId==patientID) select new { s.Amount});
            return Ok(Amount);
        }




    }
}
