using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Services.Implements
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _appDbContext;
        public TeamService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Team>> GetAllAsync()
        {
            return await  _appDbContext.Teams
                .Include(t=>t.Players)
                .Include(t=>t.Coaches)
                .Include(t=>t.TournamentsWon)
                .Include(t=>t.Matches)
                .OrderBy(t=>t.Name)
                .ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(Guid id)
        {
            return await _appDbContext.Teams
                .Include(t=>t.Players)
                .Include(t=>t.Coaches)
                .Include(t=>t.TournamentsWon)
                .Include(t=>t.Matches)
                .Where(t => t.TeamId == id)
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

        public bool SaveAll()
        {
            return _appDbContext.SaveChanges() > 0;
        }
    }
}
