using matchSchedule.Models;

namespace matchSchedule.Services.Interfaces
{
    public interface ITeamService
    {
        Task<List<Team>> GetAllAsync();
        Task<Team> GetTeamByIdAsync(Guid id);
        void AddEntity(object model);
        bool SaveAll();
        void RemoveEntity(object model);
    }
}
