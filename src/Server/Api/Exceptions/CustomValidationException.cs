namespace Api.Exceptions;

public class CustomValidationException
{
    public List<ValidationProperty> Errors { get; set; }

    public static CustomValidationException Create(List<FluentValidation.Results.ValidationFailure> errors)
    {
        return new CustomValidationException
        {
            Errors = errors
                .Select(e => new ValidationProperty
                {
                    PropertyName = e.PropertyName,
                    Message = e.ErrorMessage
                })
                .ToList()
        };
    }
}
public class ValidationProperty{
    public string PropertyName { get; set; }
    public string Message { get; set; }
}
