namespace matchSchedule.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        void AddEntity(T entity);
        void AddEntityAsync(T entity);
        void RemoveEntity(T entity);
        bool SaveAll();
        Task<bool> SaveAllAsync();
        void Update(T entity);
    }
}