using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;

namespace matchSchedule.Services.Interfaces
{
    public interface IPlayerService
    {

        Task<Result> GetPlayersAsync();
        Task<Result> GetPlayerAsync(Guid id);
        Task<Result> CreateNewPlayerAsync(NewPlayerDTO model);
        Task<Result> GetFreePlayersAsync();
    }
}
