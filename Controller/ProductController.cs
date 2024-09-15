using MiApiRestTest.Model;
using MiApiRestTest.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MiApiRestTest.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController(IProductService productService) : ControllerBase {

        private readonly IProductService _productService = productService;

        [HttpGet("all")]
        public ActionResult GetTestProducts(){
            return Ok(_productService.GetProducts());
        }
        [HttpGet("{id}")]
        public ActionResult GetProductById(string id){
            var result = _productService.GetProductById(id);
            return Ok(result);
        
        }
        [HttpDelete("{id}")]
        public ActionResult<ProductModel> DeleteProduct(string id){
            var product = _productService.DeleteProduct(id);
            return Ok(product);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(string id, ProductModel updatedProduct){
            var result = _productService.UpdateProduct(id, updatedProduct);
            if(!result.Success){
                return NotFound(new {Message = "Id no se encontro"});
            }
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<ProductModel> CreateProduct([FromBody] ProductModel product){
            try
                {
                    var createdProduct = _productService.CreateProduct(product);
                    return Ok(createdProduct);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Error interno del servidor", Details = ex.Message });
                }        
        }
    }
}
