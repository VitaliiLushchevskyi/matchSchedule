using matchSchedule.Models;

namespace matchSchedule.Services.Interfaces
{
    public interface ITeamService : IBaseDataService<Team>
    {
        Task<List<Player>> GetPlayersByIdsAsync(ICollection<Guid> ids);
        Task<Team> AddPlayerAsync(Guid teamId, Guid playerId);
        Task<bool> AddListOfPlayersAsync(Guid teamId, List<Guid> playersIds);
    }
}
