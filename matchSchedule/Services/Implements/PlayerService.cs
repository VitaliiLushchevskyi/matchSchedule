using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Services.Implements
{
    public class PlayerService : IPlayerService
    {
        private readonly AppDbContext _appDbContext;
        public PlayerService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Player>> GetAllAsync()
        {
            return await _appDbContext.Players
                .Include(p => p.TeamHistory)
                .OrderBy(p => p.LastName)
                .ToListAsync();
        }

        public async Task<Player> GetPlayerByIdAsync(Guid id)
        {
            return await _appDbContext.Players
                .Include(p => p.TeamHistory)
                .Where(p => p.PlayerId == id)
                .FirstOrDefaultAsync();
        }

        public void AddEntity(object model)
        {
            _appDbContext.Add(model);

        }
        public void RemoveEntity(object model)
        {
            _appDbContext.Remove(model);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public bool SaveAll()
        {
            return _appDbContext.SaveChanges() > 0;
        }

        public async Task<List<Player>> GetFreePlayersAsync()
        {
           return await _appDbContext.Players.Where(p=>p.TeamId == null).ToListAsync();
        }
    }
}
