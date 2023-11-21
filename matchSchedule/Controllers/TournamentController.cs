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
                var result = await _service.GetTournamentsAsync();
                if (result.IsSuccess)
                    return Ok(result.Value);

                else
                    return BadRequest(result.Error.Description);
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
                var result = await _service.GetTournamentAsync(id);
                if (result.IsSuccess)
                    return Ok(result.Value);

                else
                    return BadRequest(result.Error.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Failed to get tournament!");
            }
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost("createTournament")]
        public async Task<IActionResult> Post([FromBody] NewTournamentDTO model)
        {
            try
            {
                var result = await _service.CreateNewTournamentAsync(model);
                if (result.IsSuccess)
                {
                    var newTournament = (Tournament)result.Value;
                    return Created($"/api/tournamtns/{newTournament.Id}", newTournament);
                }
                else
                    return BadRequest(result.Error.Description);

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
                var result = await _service.DeleteTournament(id);
                if (result.IsSuccess)
                {
                    return NoContent();
                }
                else
                    return BadRequest(result.Error.Description);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return BadRequest(new { Message = "Failed to delete the tournament!" });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut("{id:Guid}/edit")]
        public async Task<ActionResult> Put(Guid id, [FromBody] TournamentEditDto model)
        {
            try
            {
                var result = await _service.EditTournamentByIdAsync(id, model);
                if (result.IsSuccess)
                {
                    var editedTournament = (Tournament)result.Value;
                    return (Ok(editedTournament));
                }
                else
                    return BadRequest(result.Error.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return BadRequest("Couldn't edit the tournament!");
        }

    }
}



