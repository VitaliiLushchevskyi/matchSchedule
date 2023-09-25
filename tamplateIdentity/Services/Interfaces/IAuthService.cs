using tamplateIdentity.Models;

namespace tamplateIdentity.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> GetUser(User userObj);
        void AddEntity(object model);
        Task<bool> SaveAllAsync();
    }
}
