using HowToAuth.UseCases.Common;

namespace HowToAuth.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    Task<Result<RegisterUserResponse>> ExecuteAsync(RegisterUserRequest request);
}
