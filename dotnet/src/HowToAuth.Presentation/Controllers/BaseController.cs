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
            ResultStatus.Error => Problem(result.Errors),
            _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
        };
    }

    protected BadRequestObjectResult Problem(IEnumerable<string> errors)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest
        };

        problemDetails.Extensions.Add("errors", errors.ToArray());

        return new BadRequestObjectResult(problemDetails);
    }
}
