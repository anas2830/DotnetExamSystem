using FluentValidation;
using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.Application.Validators.Base;

namespace DotnetExamSystem.Api.Application.Validators;

public class UpdateUserCommandValidator : UserBaseValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters");

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .When(x => !string.IsNullOrEmpty(x.Password))
            .WithMessage("Password must be at least 6 characters");

        AddCommonRules(x => x.Mobile, x => x.Address, x => x.ProfileImage);
    }
}
