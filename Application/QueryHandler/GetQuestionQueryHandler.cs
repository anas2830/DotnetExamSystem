using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.QueryHandler;

public class GetQuestionQueryHandler : IRequestHandler<GetQuestionQuery, Question>
{
    private readonly IQuestion _questionService; 

    public GetQuestionQueryHandler(IQuestion questionService)
    {
        _questionService = questionService;
    }

    public async Task<Question> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
    {
        return await _questionService.GetByIdAsync(request.Id);
    }
}