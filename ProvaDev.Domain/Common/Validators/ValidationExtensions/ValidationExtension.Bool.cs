namespace ProvaDev.Domain.Common.Validators.ValidationExtensions;

public static partial class ValidationExtension
{
    public static List<Error> MustBeTrue(
        this List<Error> source,
        bool value,
        string propertyName,
        string message)
    {
        if (value)
            return source;

        return source.AddError(propertyName, message);
    }

    public static List<Error> MustBeFalse(
        this List<Error> source,
        bool value,
        string propertyName,
        string message)
    {
        if (!value)
            return source;

        return source.AddError(propertyName, message);
    }

    public static List<Error> MustExist(
        this List<Error> source,
        bool value,
        string propertyName,
        string message)
    {
        if (value)
            return source;

        return source.AddError(propertyName, message);
    }
}