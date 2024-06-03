using Mango.Services.EmailApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.EmailApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<EmailLogger> EmailLoggers { get; set; }
    }
}
