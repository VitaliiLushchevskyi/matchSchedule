using AutoMapper;
using matchSchedule.Models;
using matchSchedule.ModelsDTO;
using matchSchedule.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace matchSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ILogger<PlayerController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public PlayerController(IPlayerService playerService, ILogger<PlayerController> logger, IMapper mapper)
        {
            _playerService = playerService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("players")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _playerService.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get players!");
            }
        }

        [HttpGet("players/free")]
        public async Task<IActionResult> GetFreePlayers()
        {
            try
            {
                return Ok(await _playerService.GetFreePlayersAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get players!");
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _playerService.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get player!");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Route("createPlayer")]
        public IActionResult Post([FromBody] NewPlayerDTO player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newPlayer = _mapper.Map<NewPlayerDTO, Player>(player);
                    _playerService.AddEntity(newPlayer);
                    if (_playerService.SaveAll())
                    {
                        return Created($"/api/players/{newPlayer.PlayerId}", _mapper.Map<Player, NewPlayerDTO>(newPlayer));
                    }
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Failed to post the player!");
        }
    }
}
