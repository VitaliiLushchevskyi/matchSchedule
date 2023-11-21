using AutoMapper;
using matchSchedule.Models;
using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;
using matchSchedule.Repositories.Interfaces;
using matchSchedule.Services.Interfaces;

namespace matchSchedule.Services.Implements
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repository;
        private readonly IMapper _mapper;
        public TeamService(ITeamRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> CreateNewTeamAsync(NewTeamDTO model)
        {
            if (await _repository.DoesTeamExistAsync(model.Name))
            {
                return Result.Failure(TeamErrors.AlreadyExist);
            }
            var newTeam = _mapper.Map<NewTeamDTO, Team>(model);

            if (model.PlayerIds.Count > 0)
            {
                var players = await _repository.GetPlayersByIdsAsync(model.PlayerIds);
                newTeam.Players = players;
            }

            _repository.AddEntityAsync(newTeam);
            if (await _repository.SaveAllAsync())
                return Result.Success(newTeam);

            return Result.Failure(TeamErrors.BadRequest);
        }


        public async Task<Result> GetTeamAsync()
        {
            var teams = await _repository.GetAllAsync();

            if (teams == null)
                return Result.Failure(TeamErrors.BadRequest);


            if (teams.Count == 0)
                return Result.Failure(TeamErrors.NoTeams);

            return Result.Success(teams);
        }

        public async Task<Result> GetTeamAsync(Guid id)
        {
            var team = await _repository.GetByIdAsync(id);
            if (team != null)
                return Result.Success(team);

            return Result.Failure(TeamErrors.BadRequest);
        }
    }
}
