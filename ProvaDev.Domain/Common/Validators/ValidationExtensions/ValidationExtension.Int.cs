namespace ProvaDev.Domain.Common.Validators.ValidationExtensions;

public static partial class ValidationExtension
{
    public static List<Error> NotZeroOrNegative(
        this List<Error> source,
        int value,
        string property)
    {
        if (value > 0)
        {
            return source;
        }
        return source.AddError(property, $"A quantidade dos itens deve ser maior que zero.");
    }
}