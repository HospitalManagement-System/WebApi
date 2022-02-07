using DomainLayer.EntityModels.ListModels;
using DomainLayer.EntityModels.Procedures;
using DomainLayer.Models;
using RepositoryLayer;
using RepositoryLayer.Interfaces;
using ServiceLayer.Interfaces;
using ServiceLayer.Services.Email;
using ServiceLayer.Services.Encryption;
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
        Encryption _encryption = new Encryption();
        EmailSender _iEMailSender = new EmailSender();


        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task RegisterUserData(Registration registration)
        {
            registration.Password = _encryption.EncodePasswordToBase64("Password@123");
            var Save =  _repository.AddUser(registration);
            if (Save == "Success")
            {
                await _iEMailSender.PatientSendLoginSMSAsync(registration.UserName, "Password@123", registration.Email);
            }


        }
        public List<UserInfoDetails> GetUserData()
        {
            //List<UserDetails> userDetails = _repository.GetUserData();
            List<UserInfoDetails> userDetails = _repository.GetUser();
            foreach (var item in userDetails)
            {
                item.Password= _encryption.DecodeFrom64(item.Password);
            }


            return userDetails;
        }

        public EmployeeDetails GetUser(Guid id)
        {
            //user.Password = _encryption.EncodePasswordToBase64("Password@123");
            EmployeeDetails user = _repository.GetUser(id);
            return user;
        }

        public void UpdatePassword(ChangePassword changePassword)
        {
            changePassword.Password = _encryption.EncodePasswordToBase64(changePassword.Password);
            _repository.ChangePassword(changePassword);
        }

        public void ResetPassword(UserDetails user)
        {
            user.Password = _encryption.EncodePasswordToBase64("Password@123");
            _repository.UpdatePassword(user);
        }

        public void LockedAccount(UserDetails user)
        {
            _repository.LockAccount(user);
        }

        //public List<UserInfo> GetEmployee()
        //{
        //    List<UserInfo> lstUserinfo = _repository.GetEmployee();
        //    return lstUserinfo;
        //}
    }
}
