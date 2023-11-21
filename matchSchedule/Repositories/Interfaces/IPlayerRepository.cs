using matchSchedule.Models;

namespace matchSchedule.Repositories.Interfaces
{
    public interface IPlayerRepository : IBaseRepository<Player>
    {
        Task<List<Player>> GetFreePlayersAsync();
    }
}
