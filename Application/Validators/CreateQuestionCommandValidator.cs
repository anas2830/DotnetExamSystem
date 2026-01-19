using FluentValidation;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.Application.Validators;

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3);

        RuleFor(x => x.Options)
            .NotEmpty().WithMessage("Options are required")
            .Must(o => o.Count >= 2).WithMessage("At least 2 options required");

        RuleFor(x => x.CorrectAnswer)
            .NotEmpty()
            .Must((command, correct) => command.Options.ContainsKey(correct))
            .WithMessage("CorrectAnswer must be one of the option keys");
    }
}
