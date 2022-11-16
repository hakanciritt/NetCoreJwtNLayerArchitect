using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLayerProjectForJwt.Core.Dtos.Product;
using NLayerProjectForJwt.Core.Entities;

namespace NLayerProjectForJwt.Core.Services
{
    public interface IProductService : IGenericService<Product, ProductDto>
    {
    }
}
