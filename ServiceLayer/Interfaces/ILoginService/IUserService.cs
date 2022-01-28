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
        void RegisterUserData(Registration registration);
        List<UserInfoDetails> GetUserData();

    }
}
