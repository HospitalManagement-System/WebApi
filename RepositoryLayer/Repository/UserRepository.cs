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
        List<RoleMaster> lstRoleMaster;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddUser(Registration registration)
        {
            try
            {
                var roleMaster = GetRole(registration.Role);
                if (registration.Role.ToUpper() == enumUserType.PATIENT.ToString())
                {
                    var UserDetails = new UserDetails
                    {
                        UserName = registration.UserName,
                        Password = registration.Password,
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
                                Contact = registration.Contact,
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


        private RoleMaster GetRole(string role)
        {
            if(lstRoleMaster == null)
            {
                lstRoleMaster = new List<RoleMaster>();
                lstRoleMaster = _context.RoleMaster.ToList();
            }
            RoleMaster roleMaster = lstRoleMaster.Where(x => x.UserRole.ToLower() == role.ToLower()).FirstOrDefault();
            return roleMaster;
        }
    }
}
