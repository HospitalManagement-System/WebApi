using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Demographicsdetails
    {
        public Guid id { get; set; }
        public string firstname { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string email { get; set; }
        public string gender { get; set; } //enum
        public string race { get; set; }
        public string ethinicity { get; set; }
        public string address { get; set; }

        public int pincode { get; set; }
        public string country { get; set; }

        public string state { get; set; }
        public string contactnumber { get; set; }
        //public string PreviousAllergies { get; set; }
        public bool IsFatal { get; set; }
        public string emergancyfirstname { get; set; }
        public string emergancylastname { get; set; }
        public string emergancyrelationship { get; set; }
        public string emergancyemail { get; set; }
        public string emergancycontactnumber  { get; set; }
        public string emergancyaddress  { get; set; }
       public string  emergancypincode  { get; set; }
       public string emergancycountry { get; set; }
       public bool accessforpatientportal { get; set; }
       public  int allergyid { get; set; }
      public  string allergytype { get; set; }
      public  string allergyname { get; set; }
      public string  allergydetails { get; set; }
      public  string allergydescription { get; set; }
      public string clinicalinformation{ get; set; }
      public Guid PatientId{ get; set; }
     }
}
