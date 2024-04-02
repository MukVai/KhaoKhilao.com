using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        private ResponseDTO _response;
        private IMapper _mapper;
        private readonly ApplicationDBContext _db;

        public CartAPIController(IMapper mapper, ApplicationDBContext db)
        {
            _db = db;
            this._response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDTO> CartUpsert(CartDTO cartDTO)
        {
            try
            {
                var cartHeaderFromDb = await _db.CartHeader.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cartDTO.CartHeader.UserId);
                if (cartHeaderFromDb == null)
                {
                    // create header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDTO.CartHeader);
                    _db.CartHeader.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartDTO.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                    await _db.SaveChangesAsync();
    
                }
                else
                {
                    // if header is not null
                    // check if details have same product
                    var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        u => u.ProductId == cartDTO.CartDetails.First().ProductId && u.CartHeaderId == cartHeaderFromDb.CartHeaderId);      // if the same user has same product

                    if(cartDetailsFromDb == null)
                    {
                        //create cartDetails
                        cartDTO.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                        _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        //update count in cart details
                        cartDTO.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDTO.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDTO.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                        _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                        await _db.SaveChangesAsync();
                    }
                }
                _response.Result = cartDTO;
            }
            catch (Exception ex)             
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }


        [HttpPost("RemoveCart")]
        public async Task<ResponseDTO> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                CartDetails cartDetails = _db.CartDetails.First(u=> u.CartDetailsId == cartDetailsId);

                int totalCountOfCartItem = _db.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _db.CartDetails.Remove(cartDetails);
                if (totalCountOfCartItem == 1)
                {
                    // last item in cart, remove header also
                    var cartHeaderToRemove = await _db.CartHeader.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeader.Remove(cartHeaderToRemove);
                }
                await _db.SaveChangesAsync();
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess = false;
            }
            return _response;
        }
    }
}

