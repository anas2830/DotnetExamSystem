using FluentValidation;
using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.Application.Validators.Base;

namespace DotnetExamSystem.Api.Application.Validators;

public class UpdateUserCommandValidator : UserBaseValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Name)
        .Cascade(CascadeMode.Stop)
        .MinimumLength(3)
        .When(x => x.Name != null)
        .WithMessage("Name must be at least 3 characters");

        AddCommonRules(x => x.Mobile, x => x.Address, x => x.ProfileImage);
    }
}
