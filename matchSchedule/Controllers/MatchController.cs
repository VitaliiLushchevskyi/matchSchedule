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
                var result = await _service.GetMatchesAsync();
                if (result.IsSuccess)
                    return Ok(result.Value);

                else
                    return BadRequest(result.Error.Description);
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
                var result = await _service.GetMatchAsync(id);
                if (result.IsSuccess)
                    return Ok(result.Value);

                else
                    return BadRequest(result.Error.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get match!");
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost("createMatch")]
        public async Task<IActionResult> Post([FromBody] NewMatchDTO model)
        {
            try
            {
                var result = await _service.CreateNewMatchAsync(model);
                if (result.IsSuccess)
                {
                    var newMatch = (Match)result.Value;
                    return Created($"/api/matches/{newMatch.MatchId}", newMatch);
                }
                else
                    return BadRequest(result.Error.Description);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Failed to post the match!");

        }

    }

}