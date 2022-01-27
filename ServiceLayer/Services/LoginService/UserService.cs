using DomainLayer.Models;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceLayer
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public void RegisterUserData(Registration registration)
        {
            _repository.AddUser(registration);
          
        }
        public List<UserDetails> GetUserData()
        {
            List<UserDetails> userDetails = _repository.GetUserData();
            return userDetails;
        }

        public EmployeeDetails GetUser(Guid id)
        {
            EmployeeDetails user = _repository.GetUser(id);
            return user;
        }

        public void UpdatePassword(Registration registration)
        {
            _repository.ChangePassword(registration);
        }

        //public List<UserInfo> GetEmployee()
        //{
        //    List<UserInfo> lstUserinfo = _repository.GetEmployee();
        //    return lstUserinfo;
        //}
    }
}
