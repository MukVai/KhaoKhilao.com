namespace Mango.Services.ShoppingCartAPI.Models.DTO
{
    // to ensure all the endpoints of the api return a specific type of result
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
