using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;

namespace matchSchedule.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Result> GetTeamAsync();
        Task<Result> GetTeamAsync(Guid id);
        Task<Result> CreateNewTeamAsync(NewTeamDTO model);
        Task<Result> DeleteTeamAsync(Guid id);
        Task<Result> AddListOfPlayersAsync(Guid teamId, List<Guid> playersIds);
        Task<Result> AddPlayerAsync(Guid teamId, Guid playerId);
    }
}
