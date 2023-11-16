using matchSchedule.Models;
using matchSchedule.ModelsDTO;

namespace matchSchedule.Services.Interfaces
{
    public interface IMatchService : IBaseDataService<Match>
    {

        Task<Tournament> GetTournamentByIdAsync(Guid id);
        Task<Team> GetTeamByIdAsync(Guid id);
        Task<Match> AddMatchAsync(NewMatchDTO model);

    }
}
