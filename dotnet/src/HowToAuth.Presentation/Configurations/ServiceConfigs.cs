using HowToAuth.Infrastructure;
using HowToAuth.UseCases;

namespace HowToAuth.Presentation.Configurations;

public static class ServiceConfigs
{
    public static IServiceCollection AddServiceConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddUseCasesServices()
            .AddInfrastructureServices(configuration);
    }
}
