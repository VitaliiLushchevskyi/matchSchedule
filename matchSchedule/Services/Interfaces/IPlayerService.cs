using matchSchedule.Models;

namespace matchSchedule.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<List<Player>> GetAllAsync();
        Task<Player> GetPlayerByIdAsync(Guid id);
        void AddEntity(object model);
        Task<bool> SaveAllAsync();
        bool SaveAll();
        void RemoveEntity(object model);
    }
}
