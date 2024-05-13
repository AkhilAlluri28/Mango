using AutoMapper;
using Mango.Services.CartApi;
using Mango.Services.CartApi.Data;
using Mango.Services.CartApi.Extensions;
using Mango.Services.CartApi.Services;
using Mango.Services.CartApi.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

builder.Services.AddHttpClient("Product", options =>
                    options.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductApi"]));
builder.Services.AddHttpClient("Coupon", options => 
                    options.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponApi"]));

//3. Swagger registration
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//4. Authentication registration using extension method.
builder.AddAppAuthentication();

// 5. Add services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();

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
    using (var scopedService = app.Services.CreateScope())
    {
        var _dbContext = scopedService.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_dbContext.Database.GetPendingMigrations().Any())
        {
            _dbContext.Database.Migrate();
        }
    }
}
