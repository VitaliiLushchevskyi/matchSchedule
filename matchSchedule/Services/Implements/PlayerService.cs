﻿using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Services.Implements
{
    public class PlayerService: IPlayerService
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
    }
}