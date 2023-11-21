using AutoMapper;
using matchSchedule.Models;
using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;
using matchSchedule.Repositories.Interfaces;
using matchSchedule.Services.Interfaces;

namespace matchSchedule.Services.Implements
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _repository;
        private readonly IMapper _mapper;
        public MatchService(IMatchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> CreateNewMatchAsync(NewMatchDTO model)
        {
            var newMatch = _mapper.Map<NewMatchDTO, Match>(model);

            _repository.AddEntityAsync(newMatch);
            if (await _repository.SaveAllAsync())
                return Result.Success(newMatch);

            return Result.Failure(MatchErrors.BadRequest);
        }


        public async Task<Result> GetMatchesAsync()
        {
            var matches = await _repository.GetAllAsync();

            if (matches == null)
                return Result.Failure(MatchErrors.BadRequest);


            //if (matches.Count == 0)
            //    return Result.Failure(MatchesErrors.NoMatches);

            return Result.Success(matches);
        }

        public async Task<Result> GetMatchAsync(Guid id)
        {
            var match = await _repository.GetByIdAsync(id);
            if (match != null)
                return Result.Success(match);

            return Result.Failure(MatchErrors.BadRequest);
        }
    }
}
