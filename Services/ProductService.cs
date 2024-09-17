using Microsoft.AspNetCore.Mvc;
using MiApiRestTest.Model;
using MiApiRestTest.Utiles;
using MiApiRestTest.Repository;
namespace MiApiRestTest.Services;
    public interface IProductService
    {
    Task<Result<List<ProductModel>>> GetProducts();
    Task<Result<ProductModel?>> CreateProduct(ProductModel product);
    Task<Result<ProductModel?>> GetProductById(int id);
    Task<Result<ProductModel?>> DeleteProduct(int id);
    Task<Result<ProductModel?>> UpdateProduct(ProductModel product, int id);
    Task<bool> ProductExists(int id);

    }

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductValidator _productValidator;
     public ProductService(IProductRepository productRepository, IProductValidator productValidator)
        {
            _productRepository = productRepository;
            _productValidator = productValidator;
        }

    public async Task<Result<List<ProductModel>>> GetProducts()
    {
        try {
            var products = await _productRepository.GetAllProductsAsync();
            return Result<List<ProductModel>>.Ok(products.ToList());
            
        }catch {
            return Result<List<ProductModel>>.Fail(ErrorMessages.UnexpectedError);

        }
    }
    public async Task<Result<ProductModel?>> GetProductById(int id){
       try{
            var result = await _productRepository.GetProductByIdAsync(id);

            if (!await ProductExists(id))
            {
                return Result<ProductModel?>.Fail(ErrorMessages.ProductNotFound);
            }

            return Result<ProductModel?>.Ok(result);

       }catch {
            return Result<ProductModel?>.Fail(ErrorMessages.UnexpectedError);
       }
    }

    public async Task<Result<ProductModel?>> CreateProduct(ProductModel product)
    {
        var validationResult = _productValidator.ValidateProductIsNotEmpty(product);

        if (!validationResult.Success)
        {
            var validatorError = validationResult.Error ?? ErrorMessages.UnexpectedError;
            return Result<ProductModel?>.Fail(validatorError);
        }

        var existingProduct = await _productRepository.GetProductByNameAsync(product.Name);
        if (existingProduct != null)
        {
            return Result<ProductModel?>.Fail("El producto ya existe.");
        }

        var result = await _productRepository.AddProductAsync(product);

        if (result == null)
        {
            return Result<ProductModel?>.Fail("Error al agregar el producto.");
        }

        return Result<ProductModel?>.Ok(result);
    }


    public async Task<Result<ProductModel?>> DeleteProduct(int id)
    {
        if (!await ProductExists(id))
    {
        return Result<ProductModel?>.Fail(ErrorMessages.ProductNotFound);
    }
        var result = await _productRepository.DeleteProductAsync(id);
        return Result<ProductModel?>.Ok(result);
    }
    // TODO: Mejorar el flujo de Update Products
    public async Task<Result<ProductModel?>> UpdateProduct(ProductModel product, int id){

        if (!await ProductExists(id))
        {
            return Result<ProductModel?>.Fail(ErrorMessages.ProductNotFound);
        }
        
        var validationResult = _productValidator.ValidateProductIsNotEmpty(product);
        if (!validationResult.Success)
        {
            var validationError = validationResult.Error ?? "Revise el nombre y precio y vuelva a intentarlo";
            return Result<ProductModel?>.Fail(validationError);
        }
        var result = await _productRepository.UpdateProductAsync(product, id);

        if (result != null){
            return Result<ProductModel?>.Ok(result);
        }
        return Result<ProductModel?>.Fail(ErrorMessages.UnexpectedError);
    }
    public async Task<bool> ProductExists(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return product != null;
    }

}
