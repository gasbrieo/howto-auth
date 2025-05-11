using HowToAuth.Core.Entities;

namespace HowToAuth.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options);
