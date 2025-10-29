using ProvaDev.Domain.Common.Validators;

namespace ProvaDev.Domain.Exceptions;

public class DomainValidationException : Exception
{
    public IReadOnlyCollection<Error> Errors { get; }
    
    public DomainValidationException(string message, IEnumerable<Error> errors) 
        : base(BuildMessage(message, errors))
    {
        Errors = new List<Error>(errors);
    }

    private static string BuildMessage(string message, IEnumerable<Error> errors)
    {
        var errorList = errors.ToList();
        if (!errorList.Any())
            return message;

        var errorMessages = string.Join("; ", errorList.Select(e => e.ToString()));
        return $"{message} {errorMessages}";
    }
}