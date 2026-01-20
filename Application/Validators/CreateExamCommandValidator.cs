using FluentValidation;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.Application.Validators;

public class CreateExamCommandValidator : AbstractValidator<CreateExamCommand>
{
    public CreateExamCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.TimeInMinutes).GreaterThan(0);
        RuleFor(x => x.TotalQuestions).GreaterThan(0);
    }
}
