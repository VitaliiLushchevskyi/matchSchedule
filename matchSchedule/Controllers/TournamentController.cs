using AutoMapper;
using matchSchedule.Models;
using matchSchedule.Services.Interfaces;
using matchSchedule.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace matchSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _service;
        private readonly ILogger<TournamentController> _logger;
        private readonly IMapper _mapper;
        public TournamentController(ITournamentService service, ILogger<TournamentController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("tournaments")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get tournaments!");
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _service.GetTournamentByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get tournament!");
            }
        }

  
        [HttpPost("new")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] TournamentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var teams = await _service.GetTeamsByIdAsync(model.TeamIds);
                    var newModel = _mapper.Map<TournamentViewModel, Tournament>(model);
                    newModel.Teams = teams;
                    _service.AddEntity(newModel);
                    if (_service.SaveAll())
                    {
                        return Created($"/api/tournaments/{newModel.Id}", _mapper.Map<Tournament, TournamentViewModel>(newModel));
                    }
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Failed to post the tournament!");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {                 
                    var tournament = await _service.GetTournamentByIdAsync(id);   
                    if (tournament == null)
                        return NotFound($"The tournament with id: {id} wasn`t found");
                    _service.RemoveEntity(tournament);
                    if (_service.SaveAll())
                        return NoContent();
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest(new { Message = "Failed to delete the tournament!" });
        }

    }
}



