using HowToAuth.Infrastructure.Data;

namespace HowToAuth.Presentation.Tests.TestHelpers;

public class PresentationFactory : WebApplicationFactory<IPresentationMarker>
{
    private readonly string TestId = Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable("JWT_KEY", StaticTokenService.SecretKey);

        builder.ConfigureServices(services =>
        {
            FakeAuthentication(services);
            FakeDbContext(services);
        });
    }

    private static void FakeAuthentication(IServiceCollection services)
    {
        var authDescriptors = services
            .Where(s => s.ServiceType == typeof(IConfigureOptions<AuthenticationOptions>))
            .ToList();

        foreach (var descriptor in authDescriptors)
        {
            services.Remove(descriptor);
        }

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "test-issuer",
                    ValidAudience = "test-audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(StaticTokenService.SecretKey))
                };
            });

        services.AddAuthorization();
    }

    private void FakeDbContext(IServiceCollection services)
    {
        var descriptorsToRemove = services
            .Where(s => s.ServiceType == typeof(ApplicationDbContext) ||
                        s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>) ||
                        s.ServiceType.FullName!.Contains("EntityFrameworkCore"))
            .ToList();

        foreach (var descriptor in descriptorsToRemove)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(TestId));
    }
}
