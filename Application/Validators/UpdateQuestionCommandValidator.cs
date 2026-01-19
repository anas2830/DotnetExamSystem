using FluentValidation;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.Application.Validators;

public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Option1).NotEmpty();
        RuleFor(x => x.Option2).NotEmpty();
        RuleFor(x => x.Option3).NotEmpty();
        RuleFor(x => x.Option4).NotEmpty();
        RuleFor(x => x.CorrectAnswer).NotEmpty().Must(x => 
            new[] {"option1","option2","option3","option4"}.Contains(x))
            .WithMessage("CorrectAnswer must be one of option1, option2, option3, option4");
    }
}