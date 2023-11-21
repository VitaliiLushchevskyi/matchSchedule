using AutoMapper;
using matchSchedule.Models;
using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;
using matchSchedule.Repositories.Interfaces;
using matchSchedule.Services.Interfaces;

namespace matchSchedule.Services.Implements
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;
        public PlayerService(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> CreateNewPlayerAsync(NewPlayerDTO model)
        {
            var newPlayer = _mapper.Map<NewPlayerDTO, Player>(model);

            _repository.AddEntityAsync(newPlayer);
            if (await _repository.SaveAllAsync())
                return Result.Success(newPlayer);

            return Result.Failure(BaseErrors.BadRequest);
        }


        public async Task<Result> GetPlayersAsync()
        {
            var players = await _repository.GetAllAsync();

            if (players == null)
                return Result.Failure(BaseErrors.BadRequest);

            return Result.Success(players);
        }

        public async Task<Result> GetPlayerAsync(Guid id)
        {
            var player = await _repository.GetByIdAsync(id);
            if (player != null)
                return Result.Success(player);

            return Result.Failure(BaseErrors.BadRequest);
        }

    }
}
