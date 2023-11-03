using matchSchedule.Models;

namespace matchSchedule.Services.Interfaces
{
    public interface IPlayerService : IBaseDataService<Player>
    {
  
        Task<List<Player>> GetFreePlayersAsync();
    
    }
}
