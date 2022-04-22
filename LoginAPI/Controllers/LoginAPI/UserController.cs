using DomainLayer;
using DomainLayer.EntityModels;
using DomainLayer.EntityModels.ListModels;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RepositoryLayer.Interfaces.IMasterRepository;
using ServiceLayer.Interfaces;
using ServiceLayer.Interfaces.ICommonService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoginAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMasterRepository _repository;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private IUserService _userService;
        private IEmailSender _emailSender;
        //private IMessageService _messageservice;
        private ILoggerService _loggerservice;
        public IConfiguration _config { get; }
        string CurrentUserDetails = "";
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings, IConfiguration configuration, IUserService userService, ILoggerService loggerservice, IMessageService messageservice, IEmailSender emailSender, IMasterRepository masterRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _userService = userService;
            _config = configuration;
            this._loggerservice = loggerservice;
            _emailSender = emailSender;
            _repository = masterRepository;
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
                
                await _userService.RegisterUserData(objRegistration);
                //await _loggerservice.WriteLog(new Logger
                //{
                //    ComponentName = "User/RegistrationAction",
                //    Message = "Registration done for" + objRegistration.FirstName + ", Email : " + objRegistration.Email,
                //    LogDateTime = DateTime.Now,
                //    //Logtype = enumLogType.SUCCESS.ToString()
                //});
                return Ok(new string("Registration Success"));
                
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
                UserDetails userDetails = _userService.Login(objLogin);
                if (userDetails!=null)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    var token1 = new JwtSecurityToken(_config["Jwt:Issuer"],
                          _config["Jwt:Issuer"],
                           null,
                           expires: DateTime.Now.AddMinutes(120),
                           signingCredentials: credentials
                    );
                    string roleName = _repository.GetRolefromid(userDetails.Id.ToString());
                    userDetails.Role = roleName;
                    userDetails.RoleMaster = null;
                    token1.Payload["user"] = userDetails;
                    var token = new JwtSecurityTokenHandler().WriteToken(token1);
                    userDetails = new UserDetails();
                    userDetails.Token = token;
                    CurrentUserDetails = JsonConvert.SerializeObject(userDetails);
                    HttpContext.Session.SetString("userDetails",CurrentUserDetails);
                    return Ok(userDetails);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());


            }
        }

        [HttpPost("SendEmail/{email}")]
        //  [Route("SendEmail")]
        public async Task<IActionResult> SendEmail(string email, string username)
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
        [HttpGet]
        [Route("getCurrentUserDetails")]
        public UserDetails getCurrentUserDetails1()
        {
            UserDetails detals = null;
            if (HttpContext.Session.GetString("userDetails") != null)
            {
                detals = JsonConvert.DeserializeObject<UserDetails>(HttpContext.Session.GetString("userDetails"));
            }
            return detals;
        }
    }
}
