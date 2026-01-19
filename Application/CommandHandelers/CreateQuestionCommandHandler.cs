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

    public async Task<Question> Handle(
        CreateQuestionCommand command,
        CancellationToken cancellationToken)
    {
        return await _questionService.CreateAsync(command);
    }
}
