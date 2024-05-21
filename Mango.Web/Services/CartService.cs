using Mango.Web.Models;
using Mango.Web.Services.Interfaces;
using Mango.Web.Utilities;

namespace Mango.Web.Services
{
    /// <inherit/>
    public class CartService(IBaseService baseService) : ICartService
    {
        private readonly IBaseService _baseService = baseService;

        public async Task<ResponseDto> ApplyCouponAsync(CartDto cartDto)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Post,
                Url = StaticDetails.CartApiBaseUrl + "/api/cart/apply-coupon",
                Body = cartDto
            };
            return await _baseService.SendAsync(requestDto);
        }

        public async Task<ResponseDto> GetCartByUserIdAsync(string userId)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Get,
                Url = StaticDetails.CartApiBaseUrl + $"/api/cart/by-userId/{userId}"
            };
            return await _baseService.SendAsync(requestDto);
        }

        public async Task<ResponseDto> RemoveFromCartAsync(int cartDetailsId)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Delete,
                Url = StaticDetails.CartApiBaseUrl + $"/api/cart/remove-cart/{cartDetailsId}",
            };
            return await _baseService.SendAsync(requestDto);
        }

        public async Task<ResponseDto> UpsertCartAsync(CartDto cartDto)
        {
            RequestDto requestDto = new RequestDto()
            {
                Method = HttpMethod.Post,
                Url = StaticDetails.CartApiBaseUrl + "/api/cart/cart-upsert",
                Body = cartDto
            };
            return await _baseService.SendAsync(requestDto);
        }
    }
}
