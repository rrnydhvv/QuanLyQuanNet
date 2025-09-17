using Microsoft.EntityFrameworkCore;

namespace QuanLyQuanNet.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDbContextFactory<QuanNetDbContext> _contextFactory;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public Repository(IDbContextFactory<QuanNetDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.Set<T>().ToListAsync();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                return await context.Set<T>().FindAsync(id);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var entity = await context.Set<T>().FindAsync(id);
                if (entity == null)
                    return false;

                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            await _semaphore.WaitAsync();
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var entity = await context.Set<T>().FindAsync(id);
                return entity != null;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}