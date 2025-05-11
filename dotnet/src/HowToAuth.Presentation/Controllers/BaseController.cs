using HowToAuth.UseCases.Common;

using IUseCaseResult = HowToAuth.UseCases.Common.IResult;

namespace HowToAuth.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected IActionResult ToActionResult(IUseCaseResult result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Ok(result.GetValue()),
            ResultStatus.Created => Created(result.Location, result.GetValue()),
            ResultStatus.NoContent => NoContent(),
            ResultStatus.NotFound => NotFound(),
            ResultStatus.Error => Problem(result.Title),
            ResultStatus.Invalid => ValidationProblem(result.Errors, result.Title),
            _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
        };
    }

    protected BadRequestObjectResult Problem(string title)
    {
        return new BadRequestObjectResult(new ProblemDetails
        {
            Title = title,
            Status = StatusCodes.Status400BadRequest
        });
    }

    protected BadRequestObjectResult ValidationProblem(Dictionary<string, List<string>> errors, string? title = null)
    {
        var formatted = errors.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.ToArray()
        );

        return new BadRequestObjectResult(new ValidationProblemDetails(formatted)
        {
            Title = title ?? "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest
        });
    }
}
