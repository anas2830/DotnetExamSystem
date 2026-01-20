using MediatR;
using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.DataAccessLayer.Repositories;

namespace DotnetExamSystem.Api.Application.QueryHandlers;

public class GetUserExamSummaryQueryHandler : IRequestHandler<GetUserExamSummaryQuery, UserExam>
{
    private readonly IUserExam _service;

    public GetUserExamSummaryQueryHandler(IUserExam service)
    {
        _service = service;
    }

    public async Task<UserExam> Handle(
        GetUserExamSummaryQuery request,
        CancellationToken cancellationToken)
    {
        var exam = await _service.GetByIdAsync(request.UserExamId);

        if (exam == null)
            throw new Exception("User exam not found");

        if (exam.Status != "Submitted")
            throw new Exception("Exam not submitted yet");

        return exam;
    }
}
