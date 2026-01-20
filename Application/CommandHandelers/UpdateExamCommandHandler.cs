using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, bool>
{
    private readonly IExam _examService;

    public UpdateExamCommandHandler(IExam examService)
    {
        _examService = examService;
    }

    public async Task<bool> Handle(
        UpdateExamCommand command,
        CancellationToken cancellationToken)
    {
        return await _examService.UpdateAsync(command);
    }
}