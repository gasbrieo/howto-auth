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
            return Result.Error([.. result.Errors.Select(e => e.Description)]);

        var token = tokenService.GenerateToken(user);

        return new RegisterUserResponse
        {
            Token = token
        };
    }
}