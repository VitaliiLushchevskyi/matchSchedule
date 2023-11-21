using matchSchedule.Models;

namespace matchSchedule.Repositories.Interfaces
{
    public interface ITeamRepository : IBaseRepository<Team>
    {

        Task<List<Player>> GetPlayersByIdsAsync(ICollection<Guid> ids);
        Task<Player> GetPlayerByIdAsync(Guid playerId);
        Task<bool> DoesTeamExistAsync(string teamName);
    }
}
