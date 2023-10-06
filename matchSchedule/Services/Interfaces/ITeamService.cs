using matchSchedule.Models;

namespace matchSchedule.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllAsync();
        Task<Team> GetTeamByIdAsync(Guid id);
        Task<List<Player>> GetPlayersByIdsAsync(ICollection<Guid> ids);
        void AddEntity(object model);
        Task<bool> SaveAllAsync();
        bool SaveAll();
        void RemoveEntity(object model);
        Task<Team> AddPlayerAsync(Guid teamId, Guid playerId);
        Task<bool> AddListOfPlayersAsync(Guid teamId, List<Guid> playersIds);
    }
}
