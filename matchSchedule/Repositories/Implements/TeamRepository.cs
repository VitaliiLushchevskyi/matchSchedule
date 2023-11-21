using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Repositories.Implements
{

    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext context) : base(context)
        {

        }
        public override async Task<List<Team>> GetAllAsync()
        {
            return await _context.Teams
               .Include(t => t.Players)
               .Include(t => t.Coaches)
               .Include(t => t.TournamentsWon)
               .Include(t => t.Matches)
               .OrderBy(t => t.Name)
               .ToListAsync();
        }

        public override async Task<Team> GetByIdAsync(Guid id)
        {
            return await _context.Teams
                .Include(t => t.Players
                    .OrderBy(p => p.LastName))
                .Include(t => t.Coaches)
                .Include(t => t.TournamentsWon)
                .Include(t => t.Matches)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DoesTeamExistAsync(string teamName)
        {
            return await _context.Teams.AnyAsync(team => team.Name == teamName);
        }


        public async Task<List<Player>> GetPlayersByIdsAsync(ICollection<Guid> ids)
        {
            return await _context.Players.Where(player => ids.Contains(player.PlayerId)).ToListAsync();
        }

        public Task<Player> GetPlayerByIdAsync(Guid playerId)
        {
            return _context.Players.Where(p => p.PlayerId == playerId)
                .FirstOrDefaultAsync();
        }
    }
}
