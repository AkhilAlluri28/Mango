using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IBaseService
    {
        /// <summary>
        /// Makes an http call.
        /// </summary>
        /// <param name="requestDto"> constructs http request from <see cref="RequestDto"/></param>
        /// <param name="useBearerToken"> Use JWT token</param>
        /// <returns>Returns <see cref="ResponseDto"></returns>
        Task<ResponseDto> SendAsync(RequestDto requestDto, bool useBearerToken = true);
    }
}
