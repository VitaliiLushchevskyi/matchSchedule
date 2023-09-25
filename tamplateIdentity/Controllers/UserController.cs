using Microsoft.AspNetCore.Mvc;
using tamplateIdentity.Context;
using tamplateIdentity.Models;
using tamplateIdentity.Services.Interfaces;

namespace tamplateIdentity.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IAuthService _authService;
        public UserController(AppDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _authService.GetUser(userObj);
            if (user == null)
                return NotFound(new {Message = "User not found!"});
            return Ok(new
            {
                Message = "Login Success!"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]User userObj)
        {
            if (userObj == null)
                return BadRequest();

            _authService.AddEntity(userObj);
            await _authService.SaveAllAsync();
            return Ok(new
            {
                Message = "User Register!"
            });
        }
    }
}
