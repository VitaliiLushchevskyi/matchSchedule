using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;

namespace matchSchedule.Services.Interfaces
{
    public interface ITournamentService
    {
        Task<Result> GetTournamentsAsync();
        Task<Result> GetTournamentAsync(Guid id);
        Task<Result> CreateNewTournamentAsync(NewTournamentDTO model);
        Task<Result> DeleteTournament(Guid id);
        Task<Result> EditTournamentByIdAsync(Guid id, TournamentEditDto model);
    }
}
