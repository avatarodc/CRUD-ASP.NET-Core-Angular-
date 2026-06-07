using FluentValidation;
using GestionProduits.Api.Data;
using GestionProduits.Api.Mappings;
using GestionProduits.Api.Repositories.Implementations;
using GestionProduits.Api.Repositories.Interfaces;
using GestionProduits.Api.Services.Implementations;
using GestionProduits.Api.Services.Interfaces;
using GestionProduits.Api.Validators;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProduitRepository, ProduitRepository>();
        services.AddScoped<IProduitService, ProduitService>();

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddValidatorsFromAssemblyContaining<CreateProduitDtoValidator>();

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
