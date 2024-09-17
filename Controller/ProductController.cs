using MiApiRestTest.Model;
using MiApiRestTest.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MiApiRestTest.Controllers
{
    [ApiController]
    [Route("v1/api/products")]
    public class ProductController(IProductService productService) : ControllerBase {

        private readonly IProductService _productService = productService;

        [HttpGet("all")]
        public async Task<ActionResult> GetTestProducts(){
            return Ok( await _productService.GetProducts());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id){
            var result = await _productService.GetProductById(id);
            return Ok(result);
        
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductModel>> DeleteProduct(int id){
            var product = await _productService.DeleteProduct(id);
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductModel updatedProduct){
            var result = await _productService.UpdateProduct(updatedProduct, id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] ProductModel product){
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.CreateProduct(product);

            return Ok(result);

            }
    }
}
