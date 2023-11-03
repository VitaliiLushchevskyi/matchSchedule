using matchSchedule.Models;
using matchSchedule.ModelsDTO;

namespace matchSchedule.Services.Interfaces
{
    public interface ITournamentService : IBaseDataService<Tournament>
    {
        Task<List<Team>> GetTeamsByIdAsync(ICollection<Guid> guids);
        Task<Tournament> EditTournamentByIdAsync(Guid id, TournamentEditDto model);
    }
}
