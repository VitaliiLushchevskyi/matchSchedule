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
        private readonly IPlayerService _service;
        private readonly ILogger<PlayerController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public PlayerController(IPlayerService playerService, ILogger<PlayerController> logger, IMapper mapper)
        {
            _service = playerService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("players")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _service.GetPlayersAsync();
                if (result.IsSuccess)
                    return Ok(result.Value);

                else
                    return BadRequest(result.Error.Description);
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
                var result = await _service.GetPlayerAsync(id);
                if (result.IsSuccess)
                    return Ok(result.Value);

                else
                    return BadRequest(result.Error.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get player!");
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost("createPlayer")]
        public async Task<IActionResult> Post([FromBody] NewPlayerDTO model)
        {
            try
            {
                var result = await _service.CreateNewPlayerAsync(model);
                if (result.IsSuccess)
                {
                    var newPlayer = (Player)result.Value;
                    return Created($"/api/players/{newPlayer.PlayerId}", newPlayer);
                }
                else
                    return BadRequest(result.Error.Description);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Failed to post the player!");

        }



        //[HttpGet("players/free")]
        //public async Task<IActionResult> GetFreePlayers()
        //{
        //    try
        //    {
        //        return Ok(await _playerService.GetFreePlayersAsync());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return BadRequest("Failed to get players!");
        //    }
        //}


    }
}
