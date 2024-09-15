using Microsoft.AspNetCore.Mvc;
using MiApiRestTest.Model;
using MiApiRestTest.Utiles;
using MiApiRestTest.Repository;
namespace MiApiRestTest.Services;
    public interface IProductService
    {
    Task<Result<List<ProductModel>>> GetProducts();
    Task<Result<ProductModel>> CreateProduct(ProductModel product);
    Task<Result<ProductModel?>> GetProductById(int id);
    Task<Result<ProductModel?>> DeleteProduct(int id);
    Task<Result<ProductModel?>> UpdateProduct(ProductModel product);
    }

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
     public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

    public async Task<Result<List<ProductModel>>> GetProducts()
    {
        var products = await _productRepository.GetAllProductsAsync();
        return Result<List<ProductModel>>.Ok(products.ToList());
    }
    public async Task<Result<ProductModel?>> GetProductById(int id){
        var result = await _productRepository.GetProductByIdAsync(id);

        if (result == null)
        {
            return Result<ProductModel?>.Fail("Producto no encontrado");
        }

        return Result<ProductModel?>.Ok(result);
    }

   public async Task<Result<ProductModel>> CreateProduct(ProductModel product){
       try{
         if (string.IsNullOrEmpty(product.Name) || product.Price <= 0)
        {
            return Result<ProductModel>.Fail("El nombre del producto no puede estar vacio y el precio debe ser positivos");
            }
            await _productRepository.AddProductAsync(product);
            return Result<ProductModel>.Ok(product);
       }catch{
            return Result<ProductModel>.Fail("Error desconocido, por favor intente de nuevo");

       }
        }

    public async Task<Result<ProductModel?>> DeleteProduct(int id)
    {
        var result = await _productRepository.DeleteProductAsync(id);

        if(result == null){
            return Result<ProductModel?>.Fail("Producto no encontrado");
        }
        return Result<ProductModel?>.Ok(result);
    }


    public async Task<Result<ProductModel?>> UpdateProduct(ProductModel product){
        
        var result = await _productRepository.UpdateProductAsync(product);

        if (result != null){
            return Result<ProductModel?>.Ok(result);
        }
        return Result<ProductModel?>.Fail("Error desconocido, por favor intente de nuevo");
    }
}
