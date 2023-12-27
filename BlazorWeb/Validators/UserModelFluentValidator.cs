using DAL_Dapper.Models;
using FluentValidation;

namespace BlazorWeb.Validators;
public class UserModelFluentValidator : AbstractValidator<User>
{

    public UserModelFluentValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty()
        .WithMessage("заполнить")
        .Length(3, 50);

        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("заполнить")
            .EmailAddress().WithMessage("некорректный адрес");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<User>.CreateWithOptions((User)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}