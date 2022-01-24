using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Models;
using RepositoryLayer;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ServiceLayer.Interfaces;
using System.IO;
using static DomainLayer.Models.Mail;
using ServiceLayer.Interfaces.IEncription;
//using  CommonAPI.Model.Message;

namespace CommonAPI.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeRegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IEmailSender _iEMailSender;


        private readonly IEncryption _encryption;

        public EmployeRegisterController(ApplicationDbContext context, IEmailSender emailSender, IEncryption encryption)
        {
            _context = context;
            _iEMailSender = emailSender;
            _encryption = encryption;

        }

        // GET: api/EmployeRegister
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetails>>> GetUserDetails()
        {
            return await _context.UserDetails.ToListAsync();
        }

        // GET: api/EmployeRegister/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUserDetails(Guid id)
        {
            var userDetails = await _context.UserDetails.FindAsync(id);

            if (userDetails == null)
            {
                return NotFound();
            }

            return userDetails;
        }

        // PUT: api/EmployeRegister/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetails(Guid id, UserDetails userDetails)
        {
            if (id != userDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(userDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

       
        [HttpPost]
        public async Task<ActionResult<UserDetails>> PostUserDetails(UserDetails userDetails)
        {
            var Role = _context.RoleMaster.Where(x => x.UserRole ==userDetails.Role).FirstOrDefault();
            var Password = _encryption.EncodePasswordToBase64("Password@123");
            
            var User = new UserDetails
            {
                UserName = userDetails.EmployeeDetails.Email,
                Password = Password,
                Status = true,
                IsFirstLogIn = true,
                NoOfAttempts = 0,
                IsActive = true,
                IsLocked = false,
                RoleId = new Guid(Role.Id.ToByteArray()),
                EmployeeDetails = new EmployeeDetails
                {
                    Title = userDetails.EmployeeDetails.Title,
                    FirstName = userDetails.EmployeeDetails.FirstName,
                    LastName = userDetails.EmployeeDetails.LastName,
                    DateOfBirth = userDetails.EmployeeDetails.DateOfBirth,
                    Contact = userDetails.EmployeeDetails.Contact,
                    Specialization = userDetails.EmployeeDetails.Specialization,
                    Email = userDetails.EmployeeDetails.Email,
                    CreatedOn = new DateTime(),

                },

            };

            
            _context.UserDetails.Add(User);
            var Save = await _context.SaveChangesAsync();
            var Result = (Save == 2 ? "Success" : "Failure");
            if (Result == "Success")
            {
                
                var Email = userDetails.EmployeeDetails.Email;
                var UserName = userDetails.EmployeeDetails.FirstName;
                var Decrypt = _encryption.DecodeFrom64(Password);
                var EmailResult = await _iEMailSender.SendLoginSMSAsync(UserName, Decrypt, Email);

            }

            return Ok(Result);

            
        }


        private bool UserDetailsExists(Guid id)
        {
            return _context.UserDetails.Any(e => e.Id == id);
        }


    }
}
