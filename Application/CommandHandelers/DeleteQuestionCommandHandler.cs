using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, bool>
{
    private readonly IQuestion _questionService;

    public DeleteQuestionCommandHandler(IQuestion questionService)
    {
        _questionService = questionService;
    }

    public async Task<bool> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        return await _questionService.DeleteAsync(request.Id);
    }
}