using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Services.Implements
{
    public class MatchService : IMatchService
    {
        private readonly AppDbContext _appDbContext;
        public MatchService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Match>> GetAllAsync()
        {
            return await _appDbContext.Matches     
                .ToListAsync();
        }

        public async Task<Match> GetMatchByIdAsync(Guid id)
        {
            return await _appDbContext.Matches
                .Where(m => m.MatchId == id)
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
    }
}
