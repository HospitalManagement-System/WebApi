using DomainLayer.EntityModels.Procedures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommonAPI.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserInfoController : ControllerBase
    {
        private ApplicationDbContext _context;
        public AdminUserInfoController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }


        // GET api/<AdminUserInfoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public IEnumerable<UserInfo> GetHospitalUsers()
        {


            var UserData = (from u in _context.UserDetails
                            join e in _context.EmployeeDetails 
                            on u.Id equals e.UserId
                            join r in _context.RoleMaster
                            on  u.RoleId equals r.Id
                            select new
                            {
                                UserId = u.Id,
                                EmployeeId = e.Id,
                                firstName = e.FirstName,
                                contact = e.Contact,
                                specialization = e.Specialization,
                                email = e.Email,
                                isActive = u.IsActive,
                                IsLocked = u.IsLocked,
                                Role = r.UserRole,
                            }
                            ).ToList();

            

            List<UserInfo> userInfos = new List<UserInfo>();

            foreach (var item in UserData)
            {
                UserInfo userInfo = new UserInfo();
                userInfo.UserId = item.UserId;
                userInfo.EmployeeId = item.EmployeeId;
                userInfo.FirstName = item.firstName;
                userInfo.Contact = item.contact;
                userInfo.Specialization = item.specialization;
                userInfo.Email = item.email;
                userInfo.IsActive = item.isActive;
                userInfo.IsLocked = item.IsLocked;
                userInfo.Role = item.Role;
                userInfos.Add(userInfo);
            }

            return userInfos;

            //  return (IEnumerable<UserInfo>)USerInfo;
            //return Ok(UserData);
        }

        // PUT api/<AdminUserInfoController>/5
        [HttpPut("HospitalLocked/{id}")]
        public IActionResult Put(string id,bool Status,string Type)
        {
            var User = new Guid(id);

            var CheckUser = _context.UserDetails.Where(x => x.Id == User).FirstOrDefault();

            if (CheckUser !=null && Type=="Locked")
            {
                if (Status)
                {
                    CheckUser.IsLocked = true;
                    
                }
                else
                {
                    CheckUser.IsLocked = false;
                }

                 var Result = _context.SaveChanges();

                return Ok((Result==1?"Success":"Failure"));
            }
            else if(CheckUser != null && Type == "Active")
            {
                CheckUser.IsActive = Status;

                var Result = _context.SaveChanges();

                return Ok((Result == 1 ? "Success" : "Failure"));
            }


            return Ok("Failure");
            //var UserId = Convert.ToString(id);
            //var Types = Convert.ToString(Type);
            //var ProcResult = _context.Result.FromSqlRaw($"Exec UpdateHospitalUserInfo '{UserId}','{Types}'").ToList();
            //Result = ProcResult[0].Result;
            ////var ProcResult = _context.Database.ExecuteSqlRaw($"Exec UpdateHospitalUserInfo '{UserId}','{Types}'");
            //if (ProcResult == "Success")
            //{
            //    Result = "Success";
            //    return Result;
            //}
            //else
            //{
            //    Result = "Failure";
            //    return Result;
            //}
            //return Result;

        }


        


    }
}
