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
            return await _appDbContext.Teams
                .Include(t => t.Players)
                .Include(t => t.Coaches)
                .Include(t => t.TournamentsWon)
                .Include(t => t.Matches)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(Guid id)
        {
            return await _appDbContext.Teams
                .Include(t => t.Players)
                .Include(t => t.Coaches)
                .Include(t => t.TournamentsWon)
                .Include(t => t.Matches)
                .Where(t => t.TeamId == id)
                .FirstOrDefaultAsync();
        }

        public void AddEntity(object model)
        {
            _appDbContext.Add(model);
            SaveAllAsync();
        }
        public void RemoveEntity(object model)
        {
            _appDbContext.Remove(model);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<Team> AddPlayerAsync(Guid teamId, Guid playerId)
        {
            var team = await GetTeamByIdAsync(teamId);
            var player = await _appDbContext.Players.Where(p => p.PlayerId == playerId).FirstOrDefaultAsync();
            team.Players.Add(player);
            await SaveAllAsync();
            return team;
        }

        public async Task<bool> AddListOfPlayersAsync(Guid teamId, List<Guid> playersIds)
        {
            var team = await GetTeamByIdAsync(teamId);
            if (team == null)
            {
                return false;
            }

            if (playersIds != null && playersIds.Count > 0)
            {
                var playersToAdd = await _appDbContext.Players
                    .Where(p => playersIds.Contains(p.PlayerId))
                    .ToListAsync();

                if (playersToAdd.Count > 0)
                {
                    foreach (var player in playersToAdd)
                    {
                        team.Players.Add(player);
                    }
                    await SaveAllAsync();
                    return true;
                }

            }

            return false;
        }


    }
}
