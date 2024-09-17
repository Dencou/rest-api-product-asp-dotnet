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
        Task<ProductModel> AddProductAsync(ProductModel product);
        // TODO: Mejorar el flujo de Update Products
        Task<ProductModel?> UpdateProductAsync(ProductModel product, int id);
        Task<ProductModel?> DeleteProductAsync(int id);
        Task<ProductModel?> GetProductByNameAsync(string name);
    }
}