using DomainLayer.Models;
using DomainLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Interfaces;
using DomainLayer.EntityModels.Procedures;
using DomainLayer.EntityModels.ListModels;

using DomainLayer.EntityModels;
using DomainLayer.Enums;
using ServiceLayer.Interfaces.ICommonService;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private IUserService _userService;
        private IEmailSender _emailSender;
        //private IMessageService _messageservice;
        private ILoggerService _loggerservice;
        public IConfiguration Configuration { get; }



        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings, IConfiguration configuration, IUserService userService, ILoggerService loggerservice, IMessageService messageservice, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _userService = userService;
            Configuration = configuration;
            this._loggerservice = loggerservice;
            _emailSender = emailSender;
            //_messageservice = messageservice;
        }

        [HttpGet("GetUser")]
        //[Route("GetUsersData")]
        public List<UserInfoDetails> GetUser()
        {
            List<UserInfoDetails> lstUserDetails = _userService.GetUserData();
            return lstUserDetails;
        }


        [HttpGet("{id}")]
        //[Route("GetUserData")]
        public EmployeeDetails GetUser(Guid id)
        {
            try
            {
                EmployeeDetails emp = _userService.GetUser(id);
                return emp;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        //PatientRegistration
        [HttpPost]
        [Route("Register")] //POST : /api/{ApplicationUser/Patient/Register}
        public async Task<IActionResult> PostUser([FromBody] Registration objRegistration)
        {

            var applicationUser = new ApplicationUser()
            {
                UserName = objRegistration.UserName,
                //Email = objRegistration.Email,
                fullName = objRegistration.FirstName + objRegistration.LastName
            };

            try
            {
                //var result = await _userManager.CreateAsync(applicationUser, objRegistration.Email);
                //if (result.Errors.Count() == 0)
                //{
                _userService.RegisterUserData(objRegistration);
                await _loggerservice.WriteLog(new Logger
                {
                    ComponentName = "User/RegistrationAction",
                    Message = "Registration done for" + objRegistration.FirstName + ", Email : " + objRegistration.Email,
                    LogDateTime = DateTime.Now,
                    //Logtype = enumLogType.SUCCESS.ToString()
                });
                return Ok(new string("Registration Success"));
                //}
                //else
                //{
                //    return Ok(result);
                //}
            }
            catch (Exception ex)
            {
                await _loggerservice.WriteLog(new Logger
                {
                    ComponentName = "User/RegistrationAction",
                    Message = "Registration failed for" + objRegistration.FirstName + ", Email : " + objRegistration.Email,
                    LogDateTime = DateTime.Now,
                    //Logtype = enumLogType.FAILURE.ToString()
                });
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login objLogin)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(objLogin.Username);
                //if (user != null && await _userManager.CheckPasswordAsync(user, objLogin.Email))
                //{
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //Subject = new ClaimsIdentity(new Claim[]
                    //{
                    //new Claim("UserID",user.Id.ToString())
                    //}),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ApplicationSetting:Client_Url"].ToString())), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
                //}
                //else
                //{
                //return BadRequest();
                //}
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("SendEmail/{email}")]
        //  [Route("SendEmail")]
        public async Task<IActionResult> SendEmail(string email,string username)
        {
            try
            {
                await _emailSender.ForgotPassword(email, username);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {

            try
            {
                //var applicationUser = new ApplicationUser()
                //{
                //    UserName = registration.UserName,
                //    Email = registration.Email,
                //    fullName = registration.FirstName + registration.LastName
                //};

                //var result = await _userManager.CreateAsync(applicationUser, registration.Password);
                _userService.UpdatePassword(changePassword);
                await _loggerservice.WriteLog(new Logger
                {
                    //ComponentName = "User/ChangePassword",
                    //Message = "Password changed for" + registration.UserName,
                    //LogDateTime = DateTime.Now,
                    //Logtype = enumLogType.SUCCESS.ToString()
                });
                return Ok();
            }
            catch (Exception ex)
            {
                //await _loggerservice.WriteLog(new Logger
                //{
                //    ComponentName = "User/ChangePassword",
                //    Message = "Password change failed for" + registration.UserName,
                //    LogDateTime = DateTime.Now,
                //    //Logtype = enumLogType.SUCCESS.ToString()
                //});
                return StatusCode(500);
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] UserDetails user)
        {
            try
            {
                _userService.ResetPassword(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("LockAccount")]
        public IActionResult LockAccount([FromBody] UserDetails user)
        {
            try
            {
                _userService.LockedAccount(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetEmployeeUser()
        {
            //_userService.GetEmployee();
            return Ok();
        }
    }
}
