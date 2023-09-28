using matchSchedule.Models;

namespace matchSchedule.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<List<Tournament>> GetAllAsync();
        Task<Tournament> GetTournamentByIdAsync(Guid id);
        void AddEntity(object model);
        Task<bool> SaveAllAsync();
        bool SaveAll();
        void RemoveEntity(object model);
    }
}
