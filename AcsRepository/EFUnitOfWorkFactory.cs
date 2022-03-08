// using Microsoft.EntityFrameworkCore;
// using Repository;
//
// namespace AcsRepository
// {
//     public class EFUnitOfWorkFactory : IUnitOfWorkFactory
//     {
//         private readonly DbContext _ctx;
//
//         public EFUnitOfWorkFactory(DbContext ctx)
//         {
//             _ctx = ctx;
//         }
//         public IUnitOfWork Create()
//         {
//             return new EFUnitOfWork(_ctx);
//         }
//     }
// }