using AutoMapper;
using matchSchedule.Models;
using matchSchedule.Models.Errors;
using matchSchedule.Services.Interfaces;
using matchSchedule.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                return Ok(await _service.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get match!");
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost("createMatch")]
        public async Task<Result> Post([FromBody] MatchViewModel model)
        {
            var tournament = _service.GetTournamentById(model.Tournament.Id);
            var homeTeam = _service.GetTeamById(model.HomeTeamId);
            var awayTeam = _service.GetTeamById(model.AwayTeamId);

            if (tournament == null)
            {
                return Result.Failure(MatchErrors.NotFoundTournament);
            }
            if (homeTeam == awayTeam)
            {
                return Result.Failure(MatchErrors.SameTeams);
            }
            // put it in a separate method 
            var newModel = _mapper.Map<MatchViewModel, Match>(model);
            newModel.Tournament = tournament;
            newModel.HomeTeam = homeTeam;
            newModel.AwayTeam = awayTeam;
            _service.AddEntityAsync(newModel);
            if (await _service.SaveAllAsync())
            {
                return Result.Success();
            }

            return Result.Failure(MatchErrors.BadRequest);

        }

    }

}