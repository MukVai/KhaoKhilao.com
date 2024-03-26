using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface IBaseService   // responsible for making all other api calls
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true);  // async method

    }
}  
