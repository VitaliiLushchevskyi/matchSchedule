using AutoMapper;
using matchSchedule.Models;
using matchSchedule.Services.Interfaces;
using matchSchedule.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace matchSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly ILogger<TeamController> _logger;
        private readonly IMapper _mapper;
        public TeamController(ITeamService teamService, ILogger<TeamController> logger, IMapper mapper)
        {
            _teamService = teamService;
            _logger = logger;
            _mapper = mapper
            ;
        }

        [HttpGet]
        [Route("teams")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _teamService.GetAllAsync());
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
                return Ok(await _teamService.GetTeamByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get team!");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost("new")]
        public async Task<IActionResult> Post([FromBody] TeamViewModel team)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var players = await _teamService.GetPlayersByIdsAsync(team.PlayerIds);
                    var newTeam = _mapper.Map<TeamViewModel, Team>(team);
                    newTeam.Players = players;
                    _teamService.AddEntity(newTeam);
                    if (_teamService.SaveAll())
                    {
                        return Created($"/api/teams/{newTeam.Id}", _mapper.Map<Team, TeamViewModel>(newTeam));
                    }
                }
                else
                    return BadRequest(ModelState);
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
                var result = await _teamService.AddPlayerAsync(teamId, playerId);
                if (result == null)
                {
                    return BadRequest("Invalid team or player!");
                }
                return Ok(result);
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
            var result = await _teamService.AddListOfPlayersAsync(teamId, playersIds);
            if (result)
                return Ok("Successful!");
            else
                return BadRequest("Failed!");

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tournament = await _teamService.GetTeamByIdAsync(id);
                    if (tournament == null)
                        return NotFound($"The team with id: {id} wasn`t found");
                    _teamService.RemoveEntity(tournament);
                    if (_teamService.SaveAll())
                        return NoContent();
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest(new { Message = "Failed to delete the team!" });
        }

    
}
}
