using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Services.Implements
{
    public class TournamentService : ITournamentService
    {
        private readonly AppDbContext _appDbContext;
        public TournamentService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Tournament>> GetAllAsync()
        {
            return await _appDbContext.Tournaments
                .Include(t => t.Teams)
                .Include(t => t.Matches)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Tournament> GetTournamentByIdAsync(Guid id)
        {
            return await _appDbContext.Tournaments
                .Include(t => t.Teams)                
                .Include(t => t.Matches)
                .Where(t => t.Id == id)
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
