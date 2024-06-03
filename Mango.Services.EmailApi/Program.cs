using Mango.Services.EmailApi.Data;
using Mango.Services.EmailApi.Messaging;
using Mango.Services.EmailApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//1. DbContext registration
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// We need to create a signleton for DbCOntext as we are going to use it in Signleton class AzureServiceBusConsumer
var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSingleton(new EmailService(optionsBuilder.Options));

builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();

void ApplyMigration()
{
    using (var scopedService = app.Services.CreateScope())
    {
        var _dbContext = scopedService.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_dbContext.Database.GetPendingMigrations().Any())
        {
            _dbContext.Database.Migrate();
        }
    }
}
