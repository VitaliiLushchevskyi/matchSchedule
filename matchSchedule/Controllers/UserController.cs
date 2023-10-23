using matchSchedule.Context;
using matchSchedule.Helpers;
using matchSchedule.Models;
using matchSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace matchSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        public UserController(AppDbContext dbContext, IAuthService authService, IConfiguration config)
        {
            _dbContext = dbContext;
            _authService = authService;
            _config = config;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _authService.GetUser(userObj);
            if (user == null)
                return NotFound(new { Message = "User not found!" });

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect!" });
            }

            user.Token = CreateJwt(user);
            return Ok(new
            {
                Token = user.Token,
                Message = "Login Success!"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            //Check email 
            if (await CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "Email already exist!" });

            //Check userName
            if (await CheckUserNameExistAsync(userObj.UserName))
                return BadRequest(new { Message = "UserName already exist!" });

            //Check password strength
            var pass = CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass });

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.Role = "user";
            userObj.Token = "";
            _authService.AddEntity(userObj);
            await _authService.SaveAllAsync();
            return Ok(new
            {
                Message = "User Register!"
            });
        }

        private string CreateJwt(User user)
        {

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToLower()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            });

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Tokens:Issuer"],
                Audience = _config["Tokens:Audience"],
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(7),
                SigningCredentials = credentials,
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private async Task<bool> CheckUserNameExistAsync(string username)
            => await _dbContext.Users.AnyAsync(u => u.UserName == username);

        private async Task<bool> CheckEmailExistAsync(string email)
           => await _dbContext.Users.AnyAsync(u => u.Email == email);

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Minimum password length should be 8!" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric" + Environment.NewLine);
            return sb.ToString();

        }

        [HttpGet("getSmth")]
        [Authorize]
        public ActionResult Get(string q)
        {
            return Ok(q);
        }
    }
}
