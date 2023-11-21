using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Repositories.Implements
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(AppDbContext context) : base(context)
        {

        }

        public override async Task<List<Player>> GetAllAsync()
        {
            return await _context.Players
                .Include(p => p.TeamHistory)
                .Include(p => p.Team)
                .OrderBy(p => p.LastName)
                .ToListAsync();
        }

        public override async Task<Player> GetByIdAsync(Guid id)
        {
            return await _context.Players
                .Include(p => p.TeamHistory)
                .Where(p => p.PlayerId == id)
                .FirstOrDefaultAsync();
        }


        public async Task<List<Player>> GetFreePlayersAsync()
        {
            return await _context.Players.Where(p => p.TeamId == null).ToListAsync();
        }

    }
}
