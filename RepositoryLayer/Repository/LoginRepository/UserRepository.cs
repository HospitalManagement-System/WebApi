using DomainLayer.Enums;
using DomainLayer.Models;
using DomainLayer.Models.Master;
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
        List<RoleMaster> lstRoleMaster;

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
            }
            catch (Exception ex)
            {

            }
        }

        public List<UserDetails> GetUser()
        {
            List<UserDetails> userDetails = _context.UserDetails.ToList();
            return userDetails;
        }

        private RoleMaster GetOrSetRole(string role)
        {
            List<RoleMaster> lstRoleMaster = (List<RoleMaster>) _memorycache.GetCache<RoleMaster>("Rolemaster"); 
            if(lstRoleMaster == null)
            {
                lstRoleMaster = _context.RoleMaster.ToList();
                _memorycache.SetCache<RoleMaster>("Rolemaster", lstRoleMaster);
            }
            RoleMaster roleMaster = lstRoleMaster.Where(x => x.UserRole == role).FirstOrDefault();
            return roleMaster;
        }
    }
}
