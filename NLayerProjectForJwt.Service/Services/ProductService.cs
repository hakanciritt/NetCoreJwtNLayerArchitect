using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayerProjectForJwt.Core.Dtos.Product;
using NLayerProjectForJwt.Core.Entities;
using NLayerProjectForJwt.Core.Repositories;
using NLayerProjectForJwt.Core.Services;
using NLayerProjectForJwt.Core.UnitOfWork;

namespace NLayerProjectForJwt.Service.Services
{
    public class ProductService : GenericService<Product, ProductDto> , IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IGenericRepository<Product> repository) : base(unitOfWork, repository)
        {
        }

    }
}
