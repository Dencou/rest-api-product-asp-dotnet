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

        public async Task<ProductModel?> GetProductByNameAsync(string name){
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<ProductModel> AddProductAsync(ProductModel product)
        {
            var result = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return result.Entity;

        }

        public async Task<ProductModel?> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
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
        // TODO: Mejorar el flujo de Update Products
        public async Task<ProductModel?> UpdateProductAsync(ProductModel product, int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            
            if (existingProduct == null) return null;

            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}