namespace ProvaDev.Domain.Common.Validators.ValidationExtensions;

public static partial class ValidationExtension
{
    public static List<Error> NotNullOrWhiteSpace(
        this List<Error> source,
        string value,
        string propertyName)
    {
        if (!string.IsNullOrWhiteSpace(value))
            return source;

        return source.AddError(propertyName, $"Campo '{propertyName}' deve ser informado.");
    }
    
    public static List<Error> MaximumLength(
        this List<Error> source,
        string value,
        int maximumLength,
        string propertyName)
    {
        if (value == null || value.Length <= maximumLength)
            return source;

        return source.AddError(propertyName, $"O campo '{propertyName}' deve conter no máximo '{maximumLength}' caracteres.");
    }
    
    public static List<Error> ValidEmail(
        this List<Error> source,
        string value,
        string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            return source;

        try
        {
            var validEmail = new System.Net.Mail.MailAddress(value);
            if (validEmail.Address == value)
                return source;
        }
        catch
        {
            return source.AddError(propertyName, $"O campo '{propertyName}' deve conter um email válido.");
        }

        return source.AddError(propertyName, $"O campo '{propertyName}' deve conter um email válido.");
    }

    public static List<Error> ValidTelephone(
        this List<Error> source,
        string value,
        string propertyName)
    {
        if (string.IsNullOrWhiteSpace(value))
            return source;

        var someNumbers = new string(value.Where(char.IsDigit).ToArray());
        
        var validateLength = new[] { 10, 11 };
        bool isValidLength = validateLength.Contains(someNumbers.Length);
        
        if (isValidLength)
            return source;

        return source.AddError(propertyName, $"O campo '{propertyName}' deve conter um telefone válido ((00) 0 0000-0000).");
    }

    public static List<Error> MinimumLength(
        this List<Error> source,
        string value,
        int minimumLength,
        string propertyName)
    {
        if (value?.Length >= minimumLength)
            return source;

        return source.AddError(propertyName, $"O campo '{propertyName}' deve conter no mínimo '{minimumLength}' caracteres.");
    }
}