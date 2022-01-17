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
            

            var proc = _context.UserInfos.FromSqlRaw($"Exec GetHospitalUserInfo").ToList().ToArray();
           
            List<UserInfo> userInfos = new List<UserInfo>();

            foreach (var item in proc)
            {
                UserInfo userInfo = new UserInfo();
                userInfo.UserId = item.UserId;
                userInfo.EmployeeId = item.EmployeeId;
                userInfo.FirstName = item.FirstName;
                userInfo.Contact = item.Contact;
                userInfo.Specialization = item.Specialization;
                userInfo.Email = item.Email;
                userInfo.IsActive = item.IsActive;
                userInfo.isDisabled = item.isDisabled;
                userInfo.Role = item.Role;

                userInfos.Add(userInfo);


            }

            return userInfos;
        }

        // PUT api/<AdminUserInfoController>/5
        [HttpPut("HospitalLocked/{id}")]
        public string Put(string id,string Type)
        {
            string Result ;
            var UserId = Convert.ToString(id);
            var Types = Convert.ToString(Type);
            var ProcResult = _context.Result.FromSqlRaw($"Exec UpdateHospitalUserInfo '{UserId}','{Types}'").ToList();
            Result = ProcResult[0].Result;
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
            return Result;

        }


        


    }
}
