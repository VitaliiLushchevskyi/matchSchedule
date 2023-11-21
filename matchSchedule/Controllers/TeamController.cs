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
    public class TeamController : ControllerBase
    {

        private readonly ILogger<TeamController> _logger;
        private readonly IMapper _mapper;
        private readonly ITeamService _service;
        public TeamController(ILogger<TeamController> logger, IMapper mapper, ITeamService service)
        {

            _logger = logger;
            _mapper = mapper;
            _service = service;

        }

        [HttpGet]
        [Route("teams")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _service.GetTeamAsync();
                if (result.IsSuccess)
                    return Ok(result.Value);

                else
                    return BadRequest(result.Error.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get teams!");
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _service.GetTeamAsync(id);
                if (result.IsSuccess)
                    return Ok(result.Value);

                else
                    return BadRequest(result.Error.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get team!");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost("new")]
        public async Task<IActionResult> Post([FromBody] NewTeamDTO team)
        {
            try
            {
                var result = await _service.CreateNewTeamAsync(team);
                if (result.IsSuccess)
                {
                    var newteam = (Team)result.Value;
                    return Created($"/api/teams/{newteam.Id}", newteam);
                }
                else
                    return BadRequest(result.Error.Description);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Failed to post the team!");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Route("{teamId:Guid}/addPlayer/{playerId:Guid}")]
        public async Task<IActionResult> Post(Guid teamId, Guid playerId)
        {
            try
            {
                var result = await _service.AddPlayerAsync(teamId, playerId);
                if (result.IsSuccess)
                    return Ok(result.Value);
                else
                    return BadRequest(result.Error.Description);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to add player to the team!");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost]
        [Route("{teamId:Guid}/addListOfPlayers")]
        public async Task<IActionResult> Post(Guid teamId, [FromBody] List<Guid> playersIds)
        {
            var result = await _service.AddListOfPlayersAsync(teamId, playersIds);

            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error.Description);

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _service.DeleteTeamAsync(id);
                if (result.IsSuccess)
                    return NoContent();
                else
                    return BadRequest(result.Error.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest(new { Message = "Failed to delete the team!" });
        }


    }
}
