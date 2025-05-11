using HowToAuth.Core.Interfaces;
using HowToAuth.Infrastructure.Data;
using HowToAuth.Infrastructure.Data.Repositories;
using HowToAuth.Infrastructure.Identity;

namespace HowToAuth.Infrastructure;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
