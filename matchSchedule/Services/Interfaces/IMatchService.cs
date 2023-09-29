using matchSchedule.Models;

namespace matchSchedule.Services.Interfaces
{
    public interface IMatchService
    {
        Task<List<Match>> GetAllAsync();
        Task<Match> GetMatchByIdAsync(Guid id);
        void AddEntity(object model);
        Task<bool> SaveAllAsync();
        bool SaveAll();
        void RemoveEntity(object model);
    }
}
