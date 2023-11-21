using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Repositories.Implements
{
    public class TournamentRepository : BaseRepository<Tournament>, ITournamentRepository
    {
        public TournamentRepository(AppDbContext context) : base(context)
        {

        }
        public override async Task<List<Tournament>> GetAllAsync()
        {
            return await _context.Tournaments
                .Include(t => t.Teams)
                .Include(t => t.Matches.OrderBy(m => m.MatchDateTime))
                .Include(t => t.Matches)
                    .ThenInclude(i => i.HomeTeam)
                .Include(t => t.Matches)
                    .ThenInclude(i => i.AwayTeam)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public override async Task<Tournament> GetByIdAsync(Guid id)
        {
            return await _context.Tournaments
                .Include(t => t.Teams)
                .Include(t => t.Matches)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }


    }
}
