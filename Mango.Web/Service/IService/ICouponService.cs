using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDTO?> GetAllCouponsAsync();
        Task<ResponseDTO?> GetCouponByIdAsync(int id);
        Task<ResponseDTO?> GetCouponAsync(string couponCode);
        Task<ResponseDTO?> CreateCouponAsync(CouponDTO coupondto);
        Task<ResponseDTO?> UpdateCouponAsync(CouponDTO coupondto);
        Task<ResponseDTO?> DeleteCouponAsync(int id);
        
    }
}
