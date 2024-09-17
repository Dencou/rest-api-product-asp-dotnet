using Xunit;
using MiApiRestTest.Services;
using System.Collections.Generic;
using Moq;
using MiApiRestTest.Repository;
using MiApiRestTest.Model;
using MiApiRestTest.Utiles;

namespace Product.Tests
{
    public class ProductServiceTests
    {
        private readonly IProductService _productService;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IProductValidator> _mockProductValidator;



        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductValidator = new Mock<IProductValidator>(); 
            _productService = new ProductService(_mockProductRepository.Object, _mockProductValidator.Object);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnError_WhenProductNotFound()
        {
            var productId = 1;
            _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((ProductModel?)null);

            
            var result = await _productService.GetProductById(productId);

            Assert.False(result.Success);
            Assert.Equal("Producto no encontrado", result.Error);
        }

        [Fact]
        public async Task GetProductById_ShouldReturnSuccess_WhenProductIsFound(){
           var productId = 1;
           var product = new ProductModel{
                Id = productId,
                Name = "Product Test",
                Price = 10.2m
            };
           _mockProductRepository.Setup(repo => repo.GetProductByIdAsync(productId))
            .ReturnsAsync(product);

            var result = await _productService.GetProductById(productId);

            Assert.True(result.Success);
            Assert.Null(result.Error);
            Assert.NotNull(result.Data);
            Assert.Equal(productId, result.Data.Id);
            Assert.Equal("Product Test", result.Data.Name);
            Assert.Equal(10.2m, result.Data.Price);

        }
        [Fact]
        public async Task GetAllProducts_ShouldReturnSuccess(){
            var products = new List<ProductModel> 
            {
                new ProductModel
                {
                    Id = 1,
                    Name = "Test 1",
                    Price = 10m,
                },
                new ProductModel
                {
                    Id = 2,
                    Name = "Test 2",
                    Price = 10m,
                }
            };
            _mockProductRepository.Setup(repo => repo.GetAllProductsAsync())
                .ReturnsAsync(products);

            var result = await _productService.GetProducts();

            Assert.True(result.Success);
            Assert.Null(result.Error);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
        }
        [Fact]
        public async Task DeleteProduct_ShouldReturnError_WhenProductNotFound(){
            var productId = 1;

            _mockProductRepository.Setup(repo => repo.DeleteProductAsync(productId))
                .ReturnsAsync((ProductModel?) null);

            var result = await _productService.DeleteProduct(productId);

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
            Assert.Null(result.Data);
        }
        [Fact]
        public async Task DeleteProduct_ShouldReturnSuccess(){
            var productId = 1;

            _mockProductRepository.Setup(repo => repo.DeleteProductAsync(productId))
                .ReturnsAsync(new ProductModel {
                        Id = productId,
                        Name = "Product Test",
                        Price = 10.2m
                });

            var result = await _productService.DeleteProduct(productId);

            Assert.True(result.Success);
            Assert.Null(result.Error);
            Assert.NotNull(result.Data);
            Assert.Equal(productId, result.Data.Id);
            Assert.Equal("Product Test", result.Data.Name);
            Assert.Equal(10.2m, result.Data.Price);
        }
        [Fact]
        public async Task CreateProduct_ShouldReturnSuccess()
        {
           var product = new ProductModel
        {
            Id = 1,
            Name = "Product Test",
            Price = 10.2m
        };

        _mockProductValidator.Setup(v => v.ValidateProductIsNotEmpty(It.IsAny<ProductModel>()))
            .Returns(Result<ProductModel>.Ok(product));

        _mockProductRepository.Setup(repo => repo.AddProductAsync(It.IsAny<ProductModel>()))
            .ReturnsAsync(product);

        var result = await _productService.CreateProduct(product);

            Assert.True(result.Success);
            Assert.Null(result.Error);
            Assert.NotNull(result.Data);
        }
        [Fact]
        public async Task UpdateProduct_ShouldReturnError_WhenProductIsInvalid()
        {
            var productId = 1;
            var product = new ProductModel
            {
                Id = 1,
                Name = "", 
                Price = -5 
            };

            _mockProductValidator.Setup(v => v.ValidateProductIsNotEmpty(It.IsAny<ProductModel>()))
                .Returns(Result<ProductModel>.Fail("El nombre no puede estar vacío."));

            var result = await _productService.UpdateProduct(product, productId);

            Assert.False(result.Success);
            Assert.Equal("El nombre no puede estar vacío.", result.Error);
            Assert.Null(result.Data);
        }
        [Fact]
        public async Task CreateProduct_ShouldReturnError_WhenProductAlreadyExists()
        {
            var product = new ProductModel
            {
                Id = 1,
                Name = "Existing Product",
                Price = 10.2m
            };

            _mockProductValidator.Setup(v => v.ValidateProductIsNotEmpty(It.IsAny<ProductModel>()))
                .Returns(Result<ProductModel>.Ok(product));

            _mockProductRepository.Setup(repo => repo.GetProductByNameAsync(product.Name))
                .ReturnsAsync(product);

            var result = await _productService.CreateProduct(product);

            Assert.False(result.Success);
            Assert.Equal("El producto ya existe.", result.Error);
            Assert.Null(result.Data);
        }



    }
}
