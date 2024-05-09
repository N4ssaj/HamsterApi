namespace HamsterApi.Domain.Common;

public class Error
{
    public (string, IEnumerable<string>) ErrorMessage { get; }

    public Error(string errorType, IEnumerable<string> errors)
        => ErrorMessage = (errorType, errors);

    public static Error None => new Error(string.Empty, []);
}