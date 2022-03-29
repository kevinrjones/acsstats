using System;

namespace AcsRepository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
    
}