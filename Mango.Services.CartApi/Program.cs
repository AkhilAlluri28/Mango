using AutoMapper;
using Mango.MessageBus;
using Mango.Services.CartApi;
using Mango.Services.CartApi.Data;
using Mango.Services.CartApi.Extensions;
using Mango.Services.CartApi.Services;
using Mango.Services.CartApi.Services.IServices;
using Mango.Services.CartApi.Utility;
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

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ApiToApiHttpCallAuthenticationHandler>();

builder.Services.AddHttpClient("Product", options =>
                    options.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductApi"]))
    .AddHttpMessageHandler<ApiToApiHttpCallAuthenticationHandler>();

builder.Services.AddHttpClient("Coupon", options => 
                    options.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponApi"]))
    .AddHttpMessageHandler<ApiToApiHttpCallAuthenticationHandler>();

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
builder.Services.AddScoped<IMessageBus, MessageBus>();

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
