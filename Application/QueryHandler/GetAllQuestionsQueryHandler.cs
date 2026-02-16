using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;
using DotnetExamSystem.Api.Common;

namespace DotnetExamSystem.Api.Application.QueryHandler;

public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, PagedResult<Question>>
{
    private readonly IQuestion _questionService;

    public GetAllQuestionsQueryHandler(IQuestion questionService)
    {
        _questionService = questionService;
    }

    public async Task<PagedResult<Question>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
    {
        return await _questionService.GetAllAsync(request.Search, request.PageNumber, request.PageSize);
    }
}