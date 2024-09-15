using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiApiRestTest.Model;

namespace MiApiRestTest.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetAllProductsAsync();
        Task<ProductModel?> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductModel product);
        Task<ProductModel?> UpdateProductAsync(ProductModel product);
        Task<ProductModel?> DeleteProductAsync(int id);
    }
}