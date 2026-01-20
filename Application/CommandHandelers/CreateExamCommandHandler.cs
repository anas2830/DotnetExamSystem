using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, Exam>
{
    private readonly IExam _examService;

    public CreateExamCommandHandler(IExam examService)
    {
        _examService = examService;
    }

    public async Task<Exam> Handle(
        CreateExamCommand command,
        CancellationToken cancellationToken)
    {
        return await _examService.CreateAsync(command);
    }
}
