using CompanyName.MyProjectName.Modules.Users.Application.Users.Commands;
using FluentValidation;

namespace CompanyName.MyProjectName.Modules.Users.Application.Users.Validators;

internal class UserValidator : AbstractValidator<AddUser>
{
    public UserValidator()
    {
        RuleFor(p => p.Name).NotEmpty()
            .WithErrorCode("name_required")
            .WithMessage("Product name cannot be empty");
    }
}
