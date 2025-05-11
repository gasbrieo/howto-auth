using HowToAuth.UseCases.User.Login;
using HowToAuth.UseCases.User.Register;

namespace HowToAuth.Presentation.Controllers.V1;

public class AuthController(
    IRegisterUserUseCase registerUserUseCase,
    ILoginUserUseCase loginUserUseCase) : BaseController
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var result = await registerUserUseCase.ExecuteAsync(request);
        return ToActionResult(result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var result = await loginUserUseCase.ExecuteAsync(request);
        return ToActionResult(result);
    }
}
