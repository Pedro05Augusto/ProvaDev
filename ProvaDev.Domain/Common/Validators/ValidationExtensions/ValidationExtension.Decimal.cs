namespace ProvaDev.Domain.Common.Validators.ValidationExtensions;

public static partial class ValidationExtension
{
    public static List<Error> NotZeroOrNegative(
        this List<Error> source,
        decimal value,
        string property)
    {
        if (value > 0)
        {
            return source;
        }
        return source.AddError(property,$"O campo '{property}' não deve ter valor menor ou igual a zero.");
    }
}