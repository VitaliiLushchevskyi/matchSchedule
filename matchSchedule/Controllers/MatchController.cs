using AutoMapper;
using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;
using matchSchedule.Services.Interfaces;
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
        public async Task<Result> Post([FromBody] NewMatchDTO model)
        {
            if (model.AwayTeamId == model.HomeTeamId)
                return Result.Failure(MatchErrors.SameTeams);

            if (model.Tournament == null)
                return Result.Failure(MatchErrors.NotFoundTournament);

            var createdMatch = await _service.AddMatchAsync(model);

            if (createdMatch != null)
                return Result.Success();

            return Result.Failure(MatchErrors.BadRequest);
        }

    }

}