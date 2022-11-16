using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProjectForJwt.Core.Dtos.Product;
using NLayerProjectForJwt.Core.Services;

namespace NLayerProjectForJwt.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            return ActionResultInstance(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return ActionResultInstance(result);
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var result = await _productService.AddAsync(productDto);

            return ActionResultInstance(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            var result = await _productService.UpdateAsync(productDto);
            return ActionResultInstance(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.RemoveAsync(id);
            return ActionResultInstance(result);
        }
    }
}
