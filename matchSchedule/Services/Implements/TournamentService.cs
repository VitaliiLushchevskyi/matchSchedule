using AutoMapper;
using matchSchedule.Models;
using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;
using matchSchedule.Repositories.Interfaces;
using matchSchedule.Services.Interfaces;

namespace matchSchedule.Services.Implements
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _repository;
        private readonly IMapper _mapper;
        public TournamentService(ITournamentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> CreateNewTournamentAsync(NewTournamentDTO model)
        {
            var newTournament = _mapper.Map<NewTournamentDTO, Tournament>(model);

            _repository.AddEntityAsync(newTournament);
            if (await _repository.SaveAllAsync())
                return Result.Success(newTournament);

            return Result.Failure(BaseErrors.BadRequest);
        }


        public async Task<Result> GetTournamentsAsync()
        {
            var tournaments = await _repository.GetAllAsync();

            if (tournaments == null)
                return Result.Failure(BaseErrors.BadRequest);

            return Result.Success(tournaments);
        }

        public async Task<Result> GetTournamentAsync(Guid id)
        {
            var tournament = await _repository.GetByIdAsync(id);
            if (tournament != null)
                return Result.Success(tournament);

            return Result.Failure(BaseErrors.BadRequest);
        }

        public async Task<Result> DeleteTournament(Guid id)
        {
            var tournament = await _repository.GetByIdAsync(id);
            if (tournament == null)
                return Result.Failure(TournamentErrors.NotFoundTournament);
            _repository.RemoveEntity(tournament);
            if (await _repository.SaveAllAsync())
                return Result.Success();
            else
                return Result.Failure(BaseErrors.BadRequest);

        }

        public async Task<Result> EditTournamentByIdAsync(Guid id, TournamentEditDto model)
        {
            var tournament = await _repository.GetByIdAsync(id);
            if (tournament == null)
                return Result.Failure(TournamentErrors.NotFoundTournament);

            tournament.Name = model.Name;
            tournament.Location = model.Location;
            tournament.StartDate = model.StartDate;
            tournament.EndDate = model.EndDate;
            tournament.Description = model.Description;

            _repository.Update(tournament);
            if (await _repository.SaveAllAsync())
            {
                return Result.Success(tournament);
            }
            else
                return Result.Failure(BaseErrors.BadRequest);

        }

    }
}
