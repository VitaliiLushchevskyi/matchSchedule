using matchSchedule.Context;
using matchSchedule.Models;
using matchSchedule.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        public AuthService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddEntity(object model)
        {
            _appDbContext.Add(model);
        }

        public async Task<User> GetUser(User userObj)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userObj.UserName && u.Password == userObj.Password);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
