using Mango.Services.EmailApi.Data;
using Mango.Services.EmailApi.Models;
using Mango.Services.EmailApi.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.EmailApi.Services
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        public EmailService(DbContextOptions<AppDbContext> options)
        {
            this._dbOptions = options;
        }

        public async Task EmailCartAndLog(CartDto cartDto)
        {
            try
            {
                EmailLogger emailLogger = new EmailLogger()
                {
                    Email = cartDto.CartHeader.Email,
                    EmailSentDate = DateTime.UtcNow,
                    Id = cartDto.CartHeader.UserId,
                    Message = $"Email processed for user - {cartDto.CartHeader.Email}"
                };

                await using var _db = new AppDbContext(_dbOptions);
                await _db.EmailLoggers.AddAsync(emailLogger);
                await _db.SaveChangesAsync();

                // TODO: - Email logic is complex based on emailProviders. Will be not implementing that logic for now.
            }catch(Exception ex)
            {
                // Log exception here.
            }

        }
    }
}
