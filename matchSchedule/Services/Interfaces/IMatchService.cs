using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;

namespace matchSchedule.Services.Interfaces
{
    public interface IMatchService
    {

        Task<Result> GetMatchesAsync();
        Task<Result> GetMatchAsync(Guid id);
        Task<Result> CreateNewMatchAsync(NewMatchDTO model);

    }
}
