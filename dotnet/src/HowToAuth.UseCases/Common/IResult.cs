namespace HowToAuth.UseCases.Common;

public interface IResult
{
    ResultStatus Status { get; }
    string Title { get; }
    Dictionary<string, List<string>> Errors { get; }
    object? GetValue();
    string Location { get; }
}
