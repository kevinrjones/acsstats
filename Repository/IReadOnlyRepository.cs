using System;
using System.Linq;

namespace Repository
{
    public interface IReadOnlyRepository<T> 
    {
        void Save();
    }
    public interface IRepository<T> : IReadOnlyRepository<T>
    {
        IQueryable<T> Entities { get; }
        void Update(T entity);
        void Create(T entity);
        void Delete(T entity);

    }
}
