using BMB.Data.Abstractions;
using BMB.Entities.DTO;
using BMB.Entities.Models;
using BMB.Services;
using BMB.Services.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BMB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUserService _userService;
        public AccountController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }
    
        [AllowAnonymous]
        [HttpPost(nameof(AddUser))]
        public IActionResult AddUser(User user)
        {
            _userService.CreateUser(user);
            return Ok(new
            {
                Message = $"User  {user.Username} has been added."
            });
        }
        
        [HttpGet(nameof(GetAllUsers))]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
        private string GenerateJSONWebToken(UserLogin userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Issuer"],
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }       
        private async Task<UserLogin> AuthenticateUser(UserLogin login)
        {
            UserLogin user = null;
            //Validate the User Credentials 
            bool isExist = _userService.ValidateUser(login.UserName,login.Password);
            if (isExist == true)
            {
                user = new UserLogin { UserName = login.UserName, Password = login.Password};
            }
            return user;
        }
        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] UserLogin data)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(data);
            if (data != null && user!=null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { Token = tokenString, Message = "Success" });
            }
            return response;
        }
        [HttpGet]
        [Route("GetToken")]
        public async Task<IEnumerable<string>> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            return new string[] { accessToken };
        }


    }
}
