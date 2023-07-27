using BMB.API.Extensions;
using BMB.Data.Abstractions;
using BMB.Entities.DTO;
using BMB.Entities.Models;
using BMB.Services;
using BMB.Services.Abstractions;
using DnsClient;
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
    [Authorize]
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

        [HttpPost("AddUser")]
        public IActionResult AddUser(User user)
        {
            _userService.CreateUser(user);
            return Ok(new
            {
                Message = $"User  {user.Username} has been added."
            });
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin data)
        {
            IActionResult response = Unauthorized();
            if (_userService.ValidateUser(data.Username, data.Password, out User? user))
            {
                var tokenString = GenerateJSONWebToken(user);
                var cookieOptions = new CookieOptions()
                {
                    IsEssential = true,
                    Expires = DateTime.Now.AddDays(1),
                    Secure = true,
                    HttpOnly = true,
                    SameSite = SameSiteMode.None
                };
                Response.Cookies.Append("AuthCookie", tokenString, cookieOptions);
                response = Ok(new { Token = tokenString, Message = "Success" });
            }
            return response;
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", user.Id),
                new Claim(ClaimTypes.Name,user.Username)
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

        [HttpGet]
        public IActionResult Get()
        {
            if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userService.GetById(userId);
                return Ok(new
                {
                    userId = user.Id,
                    user.Username,
                    user.Email
                });
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthCookie");
            return Ok();
        }


    }
}
