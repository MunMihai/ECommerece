using Ecommerce.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class AuthenticationExtension
{
    public static void AddCustomAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
    {
        services.Configure<JwtOptions>(options =>
        {
            options.SecretKey = jwtOptions.SecretKey;
            options.ExpiresHours = jwtOptions.ExpiresHours;
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["JwtToken"];
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("UserPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
            });
        });
    }
}