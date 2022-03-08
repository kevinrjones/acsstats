using System.Data.Objects;
using DomainObjects;

namespace DAL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        protected ObjectContext ctx;

        public EFUnitOfWork(string connectionString)
        {
            ctx = new ObjectContext(connectionString);
        }

        public void Dispose()
        {
          ctx.Dispose();
        }

        private IOwnerRepository ownerRepository;

        public IOwnerRepository Owners
        {
            get { return ownerRepository ?? (ownerRepository = new EFOwnerRepository(ctx)); }
        }

        private IAuditRepository auditRepository;

        public IAuditRepository Audits {
            get { return auditRepository ?? (auditRepository = new EFAuditEntryRepository(ctx)); }
        }

        public void Commit()
        {
            ctx.SaveChanges();
        }
    }
}







