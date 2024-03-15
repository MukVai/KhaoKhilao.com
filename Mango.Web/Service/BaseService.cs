using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Mango.Web.Utility.StaticDetails;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory; // to handle any HTTP requests
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO)
        {
            try
            {
                //sending request
                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                // token

                message.RequestUri = new Uri(requestDTO.Url);
                // serialize data for post/put request
                if (requestDTO.Data != null)
                {   
                    // serialize obj into json string
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }

                // for repsonse
                HttpResponseMessage? apiResponse = null;

                switch (requestDTO.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                // getting response
                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Forbidden" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "InternalServerError" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync(); // recieve content from reponse
                        var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);    // deserialize recieved json response into responseDTO
                        return apiResponseDTO;

                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDTO()
                {
                    IsSuccess = false,
                    Message = ex.Message.ToString()
                };
                return dto;
            }
        }
    }
}
