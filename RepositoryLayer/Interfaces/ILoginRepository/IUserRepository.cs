using DomainLayer.EntityModels.ListModels;
using DomainLayer.EntityModels.Procedures;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(Registration registration);
        //List<UserDetails> GetUserData();
        EmployeeDetails GetUser(Guid id);
        void ChangePassword(ChangePassword changePassword);
        void UpdatePassword(UserDetails user);
        void LockAccount(UserDetails user);
        
        //List<UserInfo> GetEmployee();
        List<UserInfoDetails> GetUser();
    }
}
