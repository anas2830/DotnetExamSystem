using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;
using DotnetExamSystem.Api.Common;

namespace DotnetExamSystem.Api.Application.QueryHandler;

public class GetAllExamsQueryHandler : IRequestHandler<GetAllExamsQuery, PagedResult<Exam>>
{
    private readonly IExam _examService;

    public GetAllExamsQueryHandler(IExam examService)
    {
        _examService = examService;
    }

    public async Task<PagedResult<Exam>> Handle(GetAllExamsQuery request, CancellationToken cancellationToken)
    {
        return await _examService.GetAllAsync(request.UserId, request.Role, request.Search, request.PageNumber, request.PageSize);
    }
}