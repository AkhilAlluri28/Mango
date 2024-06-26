using AutoMapper;
using Mango.Services.CouponApi;
using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.Extensions;
using Mango.Services.CouponApi.Interfaces;
using Mango.Services.CouponApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//1. DbContext registration
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//2. AutoMapper registration
IMapper mapper = MappingConfig.RegisterMapping().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//3. Swagger registration
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//4. Authentication registration using extension method.
builder.AddAppAuthentication();

//5. custom services registration
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

ApplyMigration();

app.Run();

void ApplyMigration()
{
    using(var scopedService = app.Services.CreateScope())
    {
        var _dbContext = scopedService.ServiceProvider.GetRequiredService<AppDbContext>();

        if(_dbContext.Database.GetPendingMigrations().Any())
        {
            _dbContext.Database.Migrate();
        }
    }
}
