using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.QueryHandler;

public class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, List<Question>>
{
    private readonly IQuestion _questionService;

    public GetAllQuestionsQueryHandler(IQuestion questionService)
    {
        _questionService = questionService;
    }

    public async Task<List<Question>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
    {
        return await _questionService.GetAllAsync();
    }
}