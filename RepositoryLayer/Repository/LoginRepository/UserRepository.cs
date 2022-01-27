using DomainLayer.Enums;
using DomainLayer.Models;
using DomainLayer.Models.Master;
using Microsoft.Data.SqlClient;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class UserRepository: IUserRepository
    {
        private ApplicationDbContext _context;
        IInMemoryCache _memorycache;

        public UserRepository(IInMemoryCache memorycache, ApplicationDbContext context)
        {
            _context = context;
            _memorycache = memorycache;
        }

        public void AddUser(Registration registration)
        {
            try
            {
                var roleMaster = GetOrSetRole(registration.Role);
                if (registration.Role.ToUpper() == enumUserType.PATIENT.ToString())
                {
                    var UserDetails = new UserDetails
                    {
                        UserName = registration.UserName,
                        Password = registration.FirstName + "123",
                        Status = true,
                        PatientDetails = new PatientDetails
                        {
                            Title = registration.Title,
                            FirstName = registration.FirstName,
                            LastName = registration.LastName,
                            Contact = registration.Contact,
                            IsActive = true,
                            PatientDemographicDetails = new PatientDemographicDetails 
                            {
                                FirstName = registration.FirstName,
                                LastName = registration.LastName,
                                Contact = registration.Contact.ToString(),
                                DateOfBirth = registration.DateOfBirth,
                                PatientRelativeDetails = new PatientRelativeDetails
                                {

                                }
                            },
                            
                        },
                        RoleId = roleMaster.Id
                    };
                    _context.UserDetails.Add(UserDetails);
                    _context.SaveChanges();
                }

                else if (registration.Role.ToUpper() == enumUserType.PHYSICIAN.ToString())
                {
                    var UserDetails = new UserDetails
                    {
                        UserName = registration.UserName,
                        Password = registration.FirstName + "123",
                        Status = true,
                        EmployeeDetails = new EmployeeDetails
                        {
                            Title = registration.Title,
                            FirstName = registration.FirstName,
                            LastName = registration.LastName,
                            Email = registration.Email,
                            DateOfBirth = registration.DateOfBirth,
                            Contact = registration.Contact,
                            IsActive = true,
                            Specialization = registration.Specilization,
                            Designation = registration.Designation

                        },
                        //PatientDetails = new PatientDetails
                        //{
                        //    Id = Guid.NewGuid(),
                        //    Title = registration.Title,
                        //    PatientDemographicDetails = new PatientDemographicDetails
                        //    {
                        //        PatientRelativeDetails = new PatientRelativeDetails
                        //        {

                        //        }
                        //    }
                        //},
                        RoleId = roleMaster.Id
                    };
                    _context.UserDetails.Add(UserDetails);
                    _context.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                 
            }
        }

        public void ChangePassword(Registration registration)
        {
            try
            {
                var userDetails = _context.UserDetails.Where(x => x.Id == registration.UserId).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.Password = registration.Password;
                    _context.UserDetails.Update(userDetails);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<UserDetails> GetUserData()
        {
            try
            {
                List<UserDetails> userDetails = _context.UserDetails.ToList();
                //List<RoleMaster> lstRoleMaster = (List<RoleMaster>)_memorycache.GetCache<RoleMaster>("Rolemaster");
                var result = from user in userDetails
                             join
                             roles in _context.RoleMaster
                             on
                             user.RoleId equals roles.Id
                             select new
                             {
                                 Id = user.Id,
                                 Username= user.UserName,
                                 Password = user.Password,
                                 role = roles.UserRole
                             };

                return userDetails;
            }
            catch (SqlException ex)
            {
                return null;
            }
            
        }

        private RoleMaster GetOrSetRole(string role)
        {
            List<RoleMaster> lstRoleMaster = (List<RoleMaster>) _memorycache.GetCache<RoleMaster>("Rolemaster"); 
            if(lstRoleMaster == null)
            {
                lstRoleMaster = _context.RoleMaster.ToList();
                _memorycache.SetCache<RoleMaster>("Rolemaster", lstRoleMaster);
            }
            RoleMaster roleMaster = lstRoleMaster.Where(x => x.UserRole == role.ToUpper()).FirstOrDefault();
            return roleMaster;
        }

        public EmployeeDetails GetUser(Guid id)
        {
            try
            {
                EmployeeDetails emp = _context.EmployeeDetails.Where(x => x.UserId == id).FirstOrDefault();
                return emp;
            }
            catch (SqlException ex)
            {
                return null;
            }

        }
        //public List<UserInfo> GetEmployee()
        //{
        //    //var userinfo = _context.UserDetails.ToList();
        //    var roles = 
        //    var employee = from emp in _context.EmployeeDetails
        //                   join
        //                   user in _context.UserDetails
        //                   on emp.UserId equals user.Id
        //                   select new
        //                   {

        //                   };
        //}
    }
}
