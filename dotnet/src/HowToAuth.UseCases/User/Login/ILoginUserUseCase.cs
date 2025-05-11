using HowToAuth.UseCases.Common;

namespace HowToAuth.UseCases.User.Login;

public interface ILoginUserUseCase
{
    Task<Result<LoginUserResponse>> ExecuteAsync(LoginUserRequest request);
}
