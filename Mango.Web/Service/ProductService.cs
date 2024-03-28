using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService)
        {   
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> CreateProductAsync(ProductDTO Productdto)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = Productdto,
                Url = StaticDetails.ProductAPIBase + "/api/Product"
            });
        }

        public async Task<ResponseDTO?> DeleteProductAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticDetails.ApiType.DELETE,
                Url = StaticDetails.ProductAPIBase + "/api/Product/" + id
            });
        }

        public async Task<ResponseDTO?> GetAllProductsAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ProductAPIBase+"/api/Product"
            });
        }

        public async Task<ResponseDTO?> GetProductAsync(string ProductCode)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ProductAPIBase + "/api/Product/GetByCode/" + ProductCode
            });
        }

        public async Task<ResponseDTO?> GetProductByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ProductAPIBase + "/api/Product/" +id
            });
        }

        public async Task<ResponseDTO?> UpdateProductAsync(ProductDTO Productdto)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = StaticDetails.ApiType.PUT,
                Data = Productdto,
                Url = StaticDetails.ProductAPIBase + "/api/Product"
            });
        }
    }
}
