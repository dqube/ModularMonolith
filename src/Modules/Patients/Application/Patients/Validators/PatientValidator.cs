using CompanyName.MyProjectName.Modules.Patients.Application.Patients.Commands;
using FluentValidation;

namespace CompanyName.MyProjectName.Modules.Patients.Application.Patients.Validators;

internal class PatientValidator : AbstractValidator<AddPatient>
{
    public PatientValidator()
    {
        RuleFor(p => p.Name).NotEmpty()
            .WithErrorCode("name_required")
            .WithMessage("Product name cannot be empty");
    }
}
