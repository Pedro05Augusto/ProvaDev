using ProvaDev.Domain.Exceptions;

namespace ProvaDev.Domain.Common.Validators;

public class Guard
{
    public static void Enforce(List<Error>? errors)
    {
        if (errors == null || errors.Count == 0)
        {
            return;
        }
        
        throw new DomainValidationException("Domain validation failed: " , errors);
    }
}