using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.QueryHandler;

public class GetAllExamsQueryHandler : IRequestHandler<GetAllExamsQuery, List<Exam>>
{
    private readonly IExam _examService;

    public GetAllExamsQueryHandler(IExam examService)
    {
        _examService = examService;
    }

    public async Task<List<Exam>> Handle(GetAllExamsQuery request, CancellationToken cancellationToken)
    {
        return await _examService.GetAllAsync();
    }
}