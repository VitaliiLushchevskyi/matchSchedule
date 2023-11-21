using matchSchedule.Models;
using matchSchedule.ModelsDTO;

namespace matchSchedule.Repositories.Interfaces
{
    public interface IMatchRepository : IBaseRepository<Match>
    {
        Task<Match> AddMatchAsync(NewMatchDTO model);
        Task<Team> GetTeamByIdAsync(Guid id);
        Task<Tournament> GetTournamentByIdAsync(Guid id);
    }
}
