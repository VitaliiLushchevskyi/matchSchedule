using matchSchedule.Models;

namespace matchSchedule.Services.Interfaces
{
    public interface IMatchService : IBaseDataService<Match>
    {

        Tournament GetTournamentById(Guid id);
        Team GetTeamById(Guid id);
        
    }
}
