using Mango.Services.EmailApi.Models.Dto;

namespace Mango.Services.EmailApi.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);
    }
}
