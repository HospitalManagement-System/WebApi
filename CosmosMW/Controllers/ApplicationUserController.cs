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

namespace CosmosMW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private IUserService _userService;
        public IConfiguration Configuration { get; }


        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings, IConfiguration configuration, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _userService = userService;
            Configuration = configuration;
        }

        //PatientRegistration
        [HttpPost]
        [Route("Patient/Register")] //POST : /api/{ApplicationUser/Patient/Register}
        public async Task<IActionResult> PostPatientUser([FromBody] Registration objRegistration)
        {
            
            var applicationUser = new ApplicationUser()
            {
                UserName = objRegistration.UserName,
                Email = objRegistration.Email,
                fullName = objRegistration.FirstName + objRegistration.LastName
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, objRegistration.Password);
                if (result.Errors.Count() == 0)
                {
                    _userService.RegisterUserData(objRegistration);
                    return Ok(new string("Registration Success"));
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(Login objLogin)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(objLogin.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, objLogin.Password))
                {
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
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUser")]
        public List<UserDetails> GetUser()
        {
            List<UserDetails> lstUserDetails = _userService.GetUserData();
            return lstUserDetails;
        }

    }
}
