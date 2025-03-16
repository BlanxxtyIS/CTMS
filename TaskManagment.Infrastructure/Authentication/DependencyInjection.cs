using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagment.Core.Interfaces;

namespace TaskManagment.Infrastructure.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {   
        services.Configure<JwtSettings>(
                configuration.GetSection("JwtSettings"));

        
        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    }
}
