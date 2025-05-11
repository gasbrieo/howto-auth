using HowToAuth.UseCases.User.Login;
using HowToAuth.UseCases.User.Register;

namespace HowToAuth.UseCases;

public static class UseCasesServiceExtensions
{
    public static IServiceCollection AddUseCasesServices(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();

        return services;
    }
}
