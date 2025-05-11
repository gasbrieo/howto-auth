using HowToAuth.Core.Entities;

namespace HowToAuth.Core.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user);
}
