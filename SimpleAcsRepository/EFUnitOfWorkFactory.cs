using DomainObjects;

namespace DAL
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new EFUnitOfWork("name=ToDoContext");
        }
    }
}