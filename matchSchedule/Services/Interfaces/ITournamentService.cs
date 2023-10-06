using matchSchedule.Models;

namespace matchSchedule.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<List<Tournament>> GetAllAsync();
        Task<Tournament> GetTournamentByIdAsync(Guid id);
        Task<List<Team>> GetTeamsByIdAsync(ICollection<Guid> guids);
        void AddEntity(object model);
        Task<bool> SaveAllAsync();
        bool SaveAll();
        void RemoveEntity(object model);
    }
}
