using FluentValidation;
using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.Application.Validators.Base;

namespace DotnetExamSystem.Api.Application.Validators;

public class CreateUserCommandValidator : UserBaseValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
    

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        AddCommonRules(x => x.Mobile, x => x.Address, x => x.ProfileImage);
    }
}
