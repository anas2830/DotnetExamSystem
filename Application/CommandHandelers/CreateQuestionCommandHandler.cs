using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Question>
{
    private readonly IQuestion _questionService;

    public CreateQuestionCommandHandler(IQuestion questionService)
    {
        _questionService = questionService;
    }

    public async Task<Question> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = new Question
        {
            Title = request.Title,
            Option1 = request.Option1,
            Option2 = request.Option2,
            Option3 = request.Option3,
            Option4 = request.Option4,
            CorrectAnswer = request.CorrectAnswer
        };
        return await _questionService.CreateAsync(question);
    }
}
