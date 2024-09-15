using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiApiRestTest.db;
using MiApiRestTest.Model;
using Microsoft.EntityFrameworkCore;

namespace MiApiRestTest.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context){
            _context = context;
        }

        public async Task AddProductAsync(ProductModel product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

        }

        public async Task<ProductModel?> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }

        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductModel?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<ProductModel?> UpdateProductAsync(ProductModel product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}