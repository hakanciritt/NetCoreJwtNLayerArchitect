using NLayerProjectForJwt.Core.Entities;
using NLayerProjectForJwt.Core.Repositories;
using NLayerProjectForJwt.Data.EntityFramework;

namespace NLayerProjectForJwt.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
        
    }
}
