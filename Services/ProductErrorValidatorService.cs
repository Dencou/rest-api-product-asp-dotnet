using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiApiRestTest.Model;
using MiApiRestTest.Utiles;

namespace MiApiRestTest.Services


{
    public interface IProductValidator
    {
        Result<ProductModel> ValidateProductIsNotEmpty(ProductModel product);
    }
    
    public class ProductErrorValidatorService : IProductValidator
    {
        public Result<ProductModel> ValidateProductIsNotEmpty(ProductModel product)
    {
        if (string.IsNullOrEmpty(product.Name))
        {
            return Result<ProductModel>.Fail("El nombre no puede estar vacío.");
        }

        if (product.Price <= 0)
        {
            return Result<ProductModel>.Fail("El precio debe ser positivo.");
        }

        return Result<ProductModel>.Ok(product);
    }
        
    }
}