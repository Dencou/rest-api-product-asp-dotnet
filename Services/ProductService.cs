using Microsoft.AspNetCore.Mvc;
using MiApiRestTest.Model;
using MiApiRestTest.Utiles;
namespace MiApiRestTest.Services;
    public interface IProductService
    {
    Result<List<ProductModel>> GetProducts();
    Result<ProductModel> CreateProduct(ProductModel product);
    Result<ProductModel?> GetProductById(string id);
    Result<ProductModel?> DeleteProduct(string id);
    Result<ProductModel?> UpdateProduct(string id, ProductModel product);
    }

public class ProductService : IProductService
{

    public List<ProductModel> products = new List<ProductModel>();

    public Result<List<ProductModel>> GetProducts()
    {
        return Result<List<ProductModel>>.Ok(products);
    }
    public Result<ProductModel?> GetProductById(string id){
    var result = products.FirstOrDefault(item => item.Id == id);

    if (result == null)
    {
        return Result<ProductModel?>.Fail("Producto no encontrado");
    }

    return Result<ProductModel?>.Ok(result);
    }

   public Result<ProductModel> CreateProduct(ProductModel product){
       try{
         if (string.IsNullOrEmpty(product.Name) || product.Price <= 0)
        {
            return Result<ProductModel>.Fail("El nombre del producto no puede estar vacio y el precio debe ser positivos");
            }
        if (products.Count == 0 || product.Id == "1"){
            product.Id = "1"; 
        }else{
            var lastId = int.Parse(products.Last().Id!);
            product.Id = (lastId + 1).ToString();
        }
            products.Add(product); 
            return Result<ProductModel>.Ok(product);
       }catch{
            return Result<ProductModel>.Fail("Error desconocido, por favor intente de nuevo");

       }
        }

    public Result<ProductModel?> DeleteProduct(string id)
    {
        var product = products.FirstOrDefault(item => item.Id == id);
        if(product == null){
            return Result<ProductModel?>.Fail("Producto no encontrado");
        }
        products.Remove(product!);
        return Result<ProductModel?>.Ok(product);
    }


    public Result<ProductModel?> UpdateProduct(string id, ProductModel product){
        var currentProduct = products.FirstOrDefault(p => p.Id == id);
        if (currentProduct != null){
            currentProduct.Name = product.Name;
            currentProduct.Price = product.Price;

            return Result<ProductModel?>.Ok(currentProduct);
        }
        return Result<ProductModel?>.Fail("Error desconocido, por favor intente de nuevo");
    }
}
