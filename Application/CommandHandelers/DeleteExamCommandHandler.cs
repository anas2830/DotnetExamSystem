using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand, bool>
{
    private readonly IExam _examService;

    public DeleteExamCommandHandler(IExam examService)
    {
        _examService = examService;
    }

    public async Task<bool> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
    {
        return await _examService.DeleteAsync(request.Id);
    }
}