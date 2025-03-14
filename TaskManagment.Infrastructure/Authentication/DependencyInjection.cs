using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagment.Core.Application.Interfaces;

namespace TaskManagment.Infrastructure.Authentication;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Настройки JWT
        services.Configure<JwtSettings>(
            configuration.GetSection("JwtSettings"));

        // Регистрация сервиса
        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    }
}
