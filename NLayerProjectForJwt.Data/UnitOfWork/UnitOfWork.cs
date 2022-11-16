using NLayerProjectForJwt.Core.Repositories;
using NLayerProjectForJwt.Core.UnitOfWork;
using NLayerProjectForJwt.Data.EntityFramework;
using NLayerProjectForJwt.Data.Repositories;
using System.Threading.Tasks;

namespace NLayerProjectForJwt.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository _productRepository ;

        public IProductRepository Products => _productRepository ?? new ProductRepository(_context);

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
