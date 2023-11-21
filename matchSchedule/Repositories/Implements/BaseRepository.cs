using matchSchedule.Context;
using matchSchedule.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace matchSchedule.Repositories.Implements
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public virtual async void AddEntityAsync(T entity)
        {
            await _context.AddAsync(entity);
        }
        public virtual void RemoveEntity(T entity)
        {
            _context.Remove(entity);
        }
        public virtual void AddEntity(T entity)
        {
            _context.Add(entity);
        }
        public virtual bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
        public virtual async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
