using HowToAuth.Core.Entities;
using HowToAuth.Infrastructure.Data;
using HowToAuth.Infrastructure.Identity;

namespace HowToAuth.Presentation.Configurations;

public static class IdentityConfigs
{
    public static IServiceCollection AddIdentityConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddErrorDescriber<CustomIdentityErrorDescriber>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var key = Environment.GetEnvironmentVariable("JWT_KEY")!;
                var issuer = configuration["Jwt:Issuer"]!;
                var audience = configuration["Jwt:Audience"]!;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
