using Ecommerce.BL.Adapters;
using Ecommerce.BL.Commands;
using Ecommerce.BL.DbServices;
using Ecommerce.BL.Interfaces;
using Ecommerce.BL.Interfaces.Services;
using Ecommerce.BL.Services;
using Ecommerce.Core;
using Ecommerce.Core.Entities;
using Ecommerce.Core.PaymentFactory;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IMyProfileService, MyProfileService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IProductAdapter, ProductAdapter>();
builder.Services.AddScoped<IOrderPrototype, OrderPrototype>();
builder.Services.AddScoped<IOrderServiceProxy, OrderServiceProxy>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentMethodFactory, PaymentMethodFactory>();
builder.Services.AddScoped<IPaymentDetails, CashPaymentDetails>();
builder.Services.AddScoped<IPaymentDetails, CardPaymentDetails>();
builder.Services.AddScoped<ICartBuilder, ShoppingCartBuilder>();

builder.Services.AddScoped<ICartService, CartService>();


builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(typeof(AddProductToCartCommand).Assembly); });

// Get JWT options from configuration
var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

// Configure JWT options
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

// Add custom authentication
builder.Services.AddCustomAuthentication(jwtOptions);


// Добавляем сервис CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Global error handler
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

