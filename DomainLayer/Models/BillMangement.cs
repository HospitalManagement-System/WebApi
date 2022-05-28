using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class BillInfo
    {
        [Key]
        public Guid Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double BillPaid { get; set; }
        public double Balance { get; set; }
        public bool IsPaid { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("Id")]
        public Guid PatientId { get; set; }
    }
    public class PatientInOut
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Id")]
        public Guid ProductId { get; set; }
        public Products Product { get; set; }
        public DateTime DateOfProductAdded { get; set; }
        public double Amount { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("Id")]
        public Guid PatientId { get; set; }
        [ForeignKey("Id")]
        public Guid EmployeeId { get; set; }
    }
    public class Products
    {
        [Key]
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public double Cost { get; set; }
    }
    public class PatientAllDetails
    {
        public List<PatientInOut> PatientInOut { get; set; }
        public BillInfo BillInfo { get; set; }
        public PatientDemographicDetails PatientDetails { get; set; }
    }
}
