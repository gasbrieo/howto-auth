namespace HowToAuth.Presentation.Tests.TestHelpers;

public static class ProblemDetailsAssertions
{
    private static readonly JsonSerializerOptions DefaultJsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public static void ShouldHaveAnyError(this ProblemDetails problemDetails)
    {
        var errors = problemDetails.GetErrors();
        Assert.NotEmpty(errors);
    }

    public static ProblemDetailsValidator ShouldHaveError(this ProblemDetails problemDetails)
    {
        var errors = problemDetails.GetErrors();
        Assert.NotEmpty(errors);
        return new ProblemDetailsValidator(errors);
    }

    public static string[] GetErrors(this ProblemDetails problemDetails)
    {
        if (problemDetails.Extensions.TryGetValue("errors", out var errorsElement) && errorsElement is JsonElement jsonElement)
        {
            return jsonElement.Deserialize<string[]>(DefaultJsonOptions) ?? [];
        }

        return [];
    }

    public class ProblemDetailsValidator(string[] errors)
    {
        private readonly string[] _errors = errors;

        public ProblemDetailsValidator WithMessage(string expectedMessage)
        {
            Assert.Contains(_errors, e => e == expectedMessage);
            return this;
        }
    }
}
