using matchSchedule.Models;

namespace matchSchedule.Repositories.Interfaces
{
    public interface ITeamRepository : IBaseRepository<Team>
    {

        Task<List<Player>> GetPlayersByIdsAsync(ICollection<Guid> ids);
        Task<Team> AddPlayerAsync(Guid teamId, Guid playerId);
        Task<bool> AddListOfPlayersAsync(Guid teamId, List<Guid> playersIds);
        Task<bool> DoesTeamExistAsync(string teamName);
    }
}
