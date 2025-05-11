using HowToAuth.Core.Interfaces;
using HowToAuth.UseCases.Common;

namespace HowToAuth.UseCases.User.Login;

public class LoginUserUseCase(IUserRepository userRepository, ITokenService tokenService) : ILoginUserUseCase
{
    public async Task<Result<LoginUserResponse>> ExecuteAsync(LoginUserRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            return Result.Error("Invalid email or password.");

        var isValid = await userRepository.CheckPasswordAsync(user, request.Password);

        if (!isValid)
            return Result.Error("Invalid email or password.");

        var token = tokenService.GenerateToken(user);

        return new LoginUserResponse
        {
            Token = token
        };
    }
}