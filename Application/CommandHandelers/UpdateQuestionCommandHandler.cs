using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, bool>
{
    private readonly IQuestion _questionService;

    public UpdateQuestionCommandHandler(IQuestion questionService)
    {
        _questionService = questionService;
    }

    public async Task<bool> Handle(
        UpdateQuestionCommand command,
        CancellationToken cancellationToken)
    {
        return await _questionService.UpdateAsync(command);
    }
}