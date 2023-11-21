using matchSchedule.Models.Errors;
using matchSchedule.ModelsDTO;

namespace matchSchedule.Services.Interfaces
{
    public interface ITeamService
    {
        Task<Result> GetTeamAsync();
        Task<Result> GetTeamAsync(Guid id);
        Task<Result> CreateNewTeamAsync(NewTeamDTO model);

    }
}
