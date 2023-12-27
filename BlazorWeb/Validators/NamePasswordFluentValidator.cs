using DAL_Dapper;
using DAL_Dapper.Models;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorWeb.Validators;
public class NamePasswordFluentValidator : AbstractValidator<NamePassword>
{

    private readonly IDapperData _db;

    public NamePasswordFluentValidator(IDapperData db)
    {
        _db = db;

        RuleFor(x => x.EmailAddress)
        .NotEmpty().WithMessage("заполнить")
        .EmailAddress().WithMessage("некорректный адрес")
        .MustAsync(async (e, c) =>
        {
            var result = await _db.UserByEmail(e);
            return result != null;
        }).WithMessage("нет такого пользователя");


        RuleFor(p => p.Password).NotEmpty().WithMessage("заполнить")
                .MinimumLength(8).WithMessage("Используйте пароль от 8 до 16 символов.")
                .MaximumLength(16).WithMessage("Используйте пароль от 8 до 16 символов.")
                .Matches(@"[A-Z\u0400-\u04FF]+").WithMessage("Используйте заглавные и строчные символы.")
                .Matches(@"[a-z\u0400-\u04FF]+").WithMessage("Используйте заглавные и строчные символы.");
    }


    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<NamePassword>.CreateWithOptions((NamePassword)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
