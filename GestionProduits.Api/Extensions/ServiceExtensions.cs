using System.Text;
using FluentValidation;
using GestionProduits.Api.Data;
using GestionProduits.Api.Mappings;
using GestionProduits.Api.Repositories.Implementations;
using GestionProduits.Api.Repositories.Interfaces;
using GestionProduits.Api.Services.Implementations;
using GestionProduits.Api.Services.Interfaces;
using GestionProduits.Api.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GestionProduits.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProduitRepository, ProduitRepository>();
        services.AddScoped<IProduitService, ProduitService>();
        services.AddScoped<ITokenService, TokenService>();

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddValidatorsFromAssemblyContaining<CreateProduitDtoValidator>();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey        = new SymmetricSecurityKey(key),
                ValidateIssuer          = true,
                ValidIssuer             = configuration["Jwt:Issuer"],
                ValidateAudience        = true,
                ValidAudience           = configuration["Jwt:Audience"],
                ValidateLifetime        = true,
                ClockSkew               = TimeSpan.Zero
            };
        });

        services.AddAuthorization();
        return services;
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
        return services;
    }
}
