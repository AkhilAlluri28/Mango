using Mango.Web.Models;

namespace Mango.Web.Services.Interfaces
{
    /// <summary>
    /// Provides api service for CartApi.
    /// </summary>
    public interface ICartService
    {
        Task<ResponseDto> GetCartByUserIdAsync(string userId);
        Task<ResponseDto> UpsertCartAsync(CartDto cartDto);
        Task<ResponseDto> RemoveFromCartAsync(int cartDetailsId);
        Task<ResponseDto> ApplyCouponAsync(CartDto cartDto);
        Task<ResponseDto> EmailCartAsync(CartDto cartDto);
    }
}
