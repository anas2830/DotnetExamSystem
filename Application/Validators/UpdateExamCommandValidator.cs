using FluentValidation;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.Application.Validators;

public class UpdateExamCommandValidator : AbstractValidator<UpdateExamCommand>
{
    public UpdateExamCommandValidator()
    {
        When(x => x.Title != null, () => RuleFor(x => x.Title).MinimumLength(3));
        When(x => x.Price.HasValue, () => RuleFor(x => x.Price).GreaterThanOrEqualTo(0));
        When(x => x.TimeInMinutes.HasValue, () => RuleFor(x => x.TimeInMinutes).GreaterThan(0));
        When(x => x.TotalQuestions.HasValue, () => RuleFor(x => x.TotalQuestions).GreaterThan(0));
    }
}
