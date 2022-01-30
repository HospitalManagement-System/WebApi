using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityModels.Master
{
    public class Subscription
    {

        [Key]
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }

        public string paymentId { get; set; }

        public string orderId { get; set; }

        public int Amount { get; set; }

        public int Enable { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }



    }
}
