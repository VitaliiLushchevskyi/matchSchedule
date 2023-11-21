using AutoMapper;
using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.ModelsDTO;
using matchSchedule.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Repositories.Implements
{
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        private readonly IMapper _mapper;
        public MatchRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public override async Task<List<Match>> GetAllAsync()
        {
            return await _context.Matches
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.Tournament)
                .ToListAsync();
        }

        public override async Task<Match> GetByIdAsync(Guid id)
        {
            return await _context.Matches
                  .Where(m => m.MatchId == id)
                  .FirstOrDefaultAsync();
        }
        public async Task<Tournament> GetTournamentByIdAsync(Guid id)
        {
            return await _context.Tournaments
                .Include(t => t.Teams)
                .Include(t => t.Matches)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Team> GetTeamByIdAsync(Guid id)
        {
            return await _context.Teams
                .Include(t => t.Players)
                .Include(t => t.Coaches)
                .Include(t => t.TournamentsWon)
                .Include(t => t.Matches)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<Match> AddMatchAsync(NewMatchDTO model)
        {
            var tournament = await GetTournamentByIdAsync(model.Tournament.Id);
            var homeTeam = await GetTeamByIdAsync(model.HomeTeamId);
            var awayTeam = await GetTeamByIdAsync(model.AwayTeamId);
            var newModel = _mapper.Map<NewMatchDTO, Match>(model);
            newModel.Tournament = tournament;
            newModel.HomeTeam = homeTeam;
            newModel.AwayTeam = awayTeam;
            AddEntityAsync(newModel);
            if (await SaveAllAsync())
                return newModel;
            return null;
        }
    }
}
