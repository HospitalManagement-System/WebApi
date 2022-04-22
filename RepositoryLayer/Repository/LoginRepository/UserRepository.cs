using DomainLayer.EntityModels.ListModels;
using DomainLayer.EntityModels.Procedures;
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
        public UserDetails Login(Login objLogin)
        {
            UserDetails userDetails = _context.UserDetails.Where(x => x.UserName == objLogin.Username && x.Password == objLogin.Password).FirstOrDefault();
            //if (userDetails != null)
            //{
            //    try
            //    {
            //        userDetails.Role = _context.RoleMaster.Where(x => x.Id == userDetails.RoleId).FirstOrDefault().UserRole;
            //    }
            //    catch (Exception ex)
            //    {
            //        userDetails.Role = "";
            //    }
            //}
            return userDetails;
        }

        public string AddUser(Registration registration)
        {
            try
            {
                var roleMaster = GetOrSetRole(registration.Role);
                if (registration.Role.ToUpper() == enumUserType.PATIENT.ToString())
                {
                    var UserDetails = new UserDetails
                    {
                        UserName = registration.UserName,
                        Password = registration.Password,
                        Status = true,
                        IsFirstLogIn = true,
                        IsActive = true,
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
                    var Save = _context.SaveChanges();
                   var Result = (Save > 1 ? "Success" : "Failure");
                    return Result;
                }
                else
                {
                    var UserDetails = new UserDetails
                    {
                        UserName = registration.UserName,
                        Password = registration.FirstName + "123",
                        Status = true,
                        IsFirstLogIn = true,
                        IsActive = false,
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
                        RoleId = roleMaster.Id
                    };
                    _context.UserDetails.Add(UserDetails);
                    var Save = _context.SaveChanges();
                    var Result = (Save == 1 ? "Success" : "Failure");
                    return Result;
                }

                return "Failure";

            }
            catch (SqlException ex)
            {
                return "Failure";
            }
        }

        public void ChangePassword(ChangePassword changepassword)
        {
            try
            {
                var userDetails = _context.UserDetails.Where(x => x.Id == changepassword.Id).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.Password = changepassword.Password;
                    userDetails.IsFirstLogIn = false;                   
                    _context.UserDetails.Update(userDetails);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void UpdatePassword(UserDetails user)
        {
            try
            {
                var userDetails = _context.UserDetails.Where(x => x.Id == user.Id).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.Password = user.Password;
                    _context.UserDetails.Update(userDetails);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void LockAccount(UserDetails user)
        {
            try
            {
                var userDetails = _context.UserDetails.Where(x => x.Id == user.Id).FirstOrDefault();
                if (userDetails != null)
                {
                    userDetails.IsLocked = user.IsLocked;
                    userDetails.NoOfAttempts = user.NoOfAttempts;
                    _context.UserDetails.Update(userDetails);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
        
        public List<UserInfoDetails> GetUser()
        {
            try
            {


                List<UserDetails> userDetails = _context.UserDetails.ToList();
                List<UserInfoDetails> userInfos = new List<UserInfoDetails>();
                var result = from user in userDetails
                             join
                             roles in _context.RoleMaster
                             on
                             user.RoleId equals roles.Id
                             select new
                             {
                                 Id = user.Id,
                                 Username = user.UserName,
                                 Password = user.Password,
                                 role = roles.UserRole,
                                 IsFirsrLogin = user.IsFirstLogIn,
                                 IsLocked = user.IsLocked,
                                 IsActive = user.IsActive
                             };

                foreach (var item in result)
                {

                    UserInfoDetails info = new UserInfoDetails();
                    info.Id = item.Id;
                    info.UserName = item.Username;
                    info.Password = item.Password;
                    info.Role = item.role;
                    info.IsFirstLogin = item.IsFirsrLogin;
                    info.IsLocked = item.IsLocked;
                    info.IsActive = item.IsActive;
                    userInfos.Add(info);

                }
                

                return userInfos;

            }
            catch (Exception ex)
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
            RoleMaster roleMaster = lstRoleMaster.Where(x => x.UserRole.ToUpper() == role.ToUpper()).FirstOrDefault();
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
