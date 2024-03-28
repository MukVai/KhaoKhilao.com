using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDTO?> GetAllProductsAsync();
        Task<ResponseDTO?> GetProductByIdAsync(int id);
        Task<ResponseDTO?> GetProductAsync(string ProductCode);
        Task<ResponseDTO?> CreateProductAsync(ProductDTO Productdto);
        Task<ResponseDTO?> UpdateProductAsync(ProductDTO Productdto);
        Task<ResponseDTO?> DeleteProductAsync(int id);
        
    }
}
