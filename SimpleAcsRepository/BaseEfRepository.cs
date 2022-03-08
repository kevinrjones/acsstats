using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace SimpleTodoRepository
{
    public class BaseEfRepository<T> : BaseEfRepository<T, string> where T:BaseEntity<string>, new()
    {
        public BaseEfRepository(DbContext dbContext) : base (dbContext)
        {
            
        }
    }

    public class BaseEfRepository<T, TKey> : IRepository<T> where T:BaseEntity<TKey>, new()
    {
        private readonly DbSet<T> _dbSet;
        protected readonly DbContext _dbContext;

        protected BaseEfRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        
        public IQueryable<T> Entities => _dbSet;

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            Save();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}