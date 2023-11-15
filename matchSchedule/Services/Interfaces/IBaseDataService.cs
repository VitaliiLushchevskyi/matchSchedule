namespace matchSchedule.Services.Interfaces
{
    public interface IBaseDataService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        void AddEntity(T entity);
        void AddEntityAsync(T entity);
        Task<bool> SaveAllAsync();
        bool SaveAll();
        void RemoveEntity(T entity);

    }
}
