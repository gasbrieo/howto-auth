using HowToAuth.Core.Entities;
using HowToAuth.Infrastructure.Data;

namespace HowToAuth.Infrastructure.Tests.TestHelpers;

public class InfrastructureTestsFixture : IDisposable
{
    public ApplicationDbContext ApplicationDbContext { get; private set; }
    public UserManager<ApplicationUser> UserManager { get; private set; }

    public InfrastructureTestsFixture()
    {
        var services = new ServiceCollection();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

        services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        var serviceProvider = services.BuildServiceProvider();

        ApplicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    }

    public void ResetDatabase()
    {
        ApplicationDbContext.Database.EnsureDeleted();
        ApplicationDbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        ApplicationDbContext?.Dispose();
        UserManager?.Dispose();
        GC.SuppressFinalize(this);
    }
}
