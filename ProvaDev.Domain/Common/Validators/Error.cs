namespace ProvaDev.Domain.Common.Validators;

public class Error
{
    public string PropertyName { get; }
    public string ErrorMessage { get;}

    public Error(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }

    public override string ToString()
    {
        return PropertyName + ": " + ErrorMessage;
    }
}