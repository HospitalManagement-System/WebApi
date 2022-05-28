using DomainLayer.EntityModels.ListModels;
using DomainLayer.EntityModels.Procedures;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IUserService
    {
        Task RegisterUserData(Registration registration);
        //List<UserDetails> GetUserData();
        EmployeeDetails GetUser(Guid id);
        void UpdatePassword(ChangePassword changePassword);
        void ResetPassword(UserDetails user);
        void LockedAccount(UserDetails user);
        UserDetails Login(Login objLogin);
        //List<UserInfo> GetEmployee();
        List<UserInfoDetails> GetUserData();

    }
}
