namespace ProvaDev.Domain.Common.Validators.ValidationExtensions;

public static partial class ValidationExtension
{
    public static List<Error> AddError(
        this List<Error> source,
        string property,
        string message)
    {
        source.Add(new Error(property, message));
        return source;
    }

    public static List<Error> Join(
        this List<Error> source,
        params List<Error>[] errorList)
    {
        foreach (var error in errorList)
        {
            if (error != null)
            source.AddRange(error);
        }

        return source;
    }

    public static List<Error> NotNull<T>(
        this List<Error> source,
        T value,
        string propertyName) where T : class
    {
        if (value != null)
            return source;

        return source.AddError(propertyName, $"O campo '{propertyName}' não pode ser nulo.");
    }
}