using FluentValidation;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.Application.Validators;

public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        When(x => x.Title != null, () =>
        {
            RuleFor(x => x.Title).MinimumLength(3);
        });

        When(x => x.Options != null, () =>
        {
            RuleFor(x => x.Options)
                .NotEmpty().WithMessage("Options cannot be empty")
                .Must(o => o.Count >= 2).WithMessage("At least 2 options required");

            RuleFor(x => x.CorrectAnswer)
                .Must((command, correct) => correct != null && command.Options.ContainsKey(correct))
                .WithMessage("CorrectAnswer must be one of the option keys");
        });
    }
}