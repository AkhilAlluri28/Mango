using Mango.Web.Models;

namespace Mango.Web.Services.IServices
{
    public interface IBaseService
    {
        /// <summary>
        /// Makes an http call.
        /// </summary>
        /// <param name="requestDto"> constructs http request from <see cref="RequestDto"/></param>
        /// <returns>Returns <see cref="ResponseDto"></returns>
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
