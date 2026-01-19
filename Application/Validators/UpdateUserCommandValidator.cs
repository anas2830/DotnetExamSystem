using FluentValidation;
using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.Application.Validators.Base;

namespace DotnetExamSystem.Api.Application.Validators;

public class UpdateUserCommandValidator : UserBaseValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3)
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .When(x => !string.IsNullOrEmpty(x.Password));

        AddCommonRules(x => x.Mobile, x => x.Address, x => x.ProfileImage);
    }
}
