using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosMW.Models
{
    public class Registration
    {
      public string  Title    {get;set;} 
      public string FirstName {get;set;}
      public string LastName  {get;set;}
      public string UserName  {get;set;}
      public double  Contact  {get;set;}
      public string Email     {get;set;}
      public string Password  {get;set;}
      public string ConfirmPassword  {get;set;}
      public DateTime DateOfBirth {get;set;}
      public string Role { get; set; }
    }
}
