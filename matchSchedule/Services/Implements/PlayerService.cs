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
                .Include(p => p.Team)
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
            return await _appDbContext.Players.Where(p => p.TeamId == null).ToListAsync();
        }

        public Task<Player> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void AddEntity(Player entity)
        {
            _appDbContext.Add(entity);
        }

        public async void AddEntityAsync(Player entity)
        {
            await _appDbContext.AddAsync(entity);
        }

        public void RemoveEntity(Player entity)
        {
            _appDbContext.Remove(entity);
        }
    }
}
