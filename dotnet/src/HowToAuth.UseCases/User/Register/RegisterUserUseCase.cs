using HowToAuth.Core.Entities;
using HowToAuth.Core.Interfaces;
using HowToAuth.UseCases.Common;

namespace HowToAuth.UseCases.User.Register;

public class RegisterUserUseCase(IUserRepository userRepository, ITokenService tokenService) : IRegisterUserUseCase
{
    public async Task<Result<RegisterUserResponse>> ExecuteAsync(RegisterUserRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email,
        };

        var result = await userRepository.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return Result.Invalid(result.Errors
                .GroupBy(e => e.Code)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.Description).ToList()
                ));

        var token = tokenService.GenerateToken(user);

        return new RegisterUserResponse
        {
            Token = token
        };
    }
}