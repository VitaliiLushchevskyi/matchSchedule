using AutoMapper;
using matchSchedule.Models;
using matchSchedule.Services.Interfaces;
using matchSchedule.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace matchSchedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _service;
        private readonly ILogger<MatchController> _logger;
        private readonly IMapper _mapper;
        public MatchController(IMatchService service, ILogger<MatchController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("matches")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get matches!");
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _service.GetMatchByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get match!");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("createMatch")]
        public IActionResult Post([FromBody] MatchViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tournament = _service.GetTournamentById(model.Tournament.Id);
                    var homeTeam = _service.GetTeamById(model.HomeTeamId);
                    var awayTeam = _service.GetTeamById(model.AwayTeamId);


                    var newModel = _mapper.Map<MatchViewModel, Match>(model);
                    newModel.Tournament = tournament;
                    newModel.HomeTeam = homeTeam;
                    newModel.AwayTeam = awayTeam;

                    _service.AddEntity(newModel);
                    if (_service.SaveAll())
                    {
                        return Created($"/api/match/{newModel.MatchId}", _mapper.Map<Match, MatchViewModel>(newModel));
                    }
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Failed to post the match!");
        }
    }
}
