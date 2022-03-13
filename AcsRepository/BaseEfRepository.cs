using System;
using System.Linq;
using AcsRepository.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace AcsRepository
{

    public class BaseEfNoKeyRepository<T> : IReadOnlyRepository<T> where T: class
    {
        private readonly DbSet<T> _dbSet;
        protected readonly AcsDbContext DbContext;

        public BaseEfNoKeyRepository(AcsDbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        
        public void Save()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
        
    }
    
    public class BaseEfRepository<T> : BaseEfRepository<T, string> where T:BaseEntity<string>, new()
    {
        public BaseEfRepository(DbContext dbContext) : base (dbContext)
        {
            
        }
    }

    public class BaseEfRepository<T, TKey> : IRepository<T> where T:BaseEntity<TKey>, new()
    {
        private readonly DbSet<T> _dbSet;
        protected readonly DbContext DbContext;

        protected BaseEfRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        
        public IQueryable<T> Entities => _dbSet;

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
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
            DbContext.SaveChanges();
        }

    }
}