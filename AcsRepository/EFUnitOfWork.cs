using AcsRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcsRepository
{
    public interface IEfUnitOfWork : IUnitOfWork
    {
        public AcsDbContext DbContext { get; set; }
    }

    public class EfUnitOfWork : IEfUnitOfWork
    {
        public AcsDbContext DbContext { get; set; }
        
        
        public EfUnitOfWork(AcsDbContext dbCtx)
        {

            DbContext = dbCtx;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
        
        public void Commit()
        {
            DbContext.SaveChanges();
        }

    }
}
