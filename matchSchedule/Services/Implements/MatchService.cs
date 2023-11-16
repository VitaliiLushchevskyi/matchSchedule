using AutoMapper;
using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.ModelsDTO;
using matchSchedule.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Services.Implements
{
    public class MatchService : IMatchService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public MatchService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<List<Match>> GetAllAsync()
        {
            return await _appDbContext.Matches
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .Include(m => m.Tournament)
                .ToListAsync();
        }

        public async Task<Match> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Matches
                .Where(m => m.MatchId == id)
                .FirstOrDefaultAsync();
        }
        public async Task<Tournament> GetTournamentByIdAsync(Guid id)
        {
            return await _appDbContext.Tournaments
                .Include(t => t.Teams)
                .Include(t => t.Matches)
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Team> GetTeamByIdAsync(Guid id)
        {
            return await _appDbContext.Teams
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
        public async Task<bool> SaveAllAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public bool SaveAll()
        {
            return _appDbContext.SaveChanges() > 0;
        }

        public void AddEntity(Match entity)
        {
            _appDbContext.Add(entity);
        }

        public async void AddEntityAsync(Match entity)
        {
            await _appDbContext.AddAsync(entity);
        }

        public void RemoveEntity(Match entity)
        {
            _appDbContext.Remove(entity);
        }


    }
}
