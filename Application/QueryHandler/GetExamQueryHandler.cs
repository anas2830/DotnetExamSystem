using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.QueryHandler;

public class GetExamQueryHandler : IRequestHandler<GetExamQuery, Exam>
{
    private readonly IExam _examService; 

    public GetExamQueryHandler(IExam examService)
    {
        _examService = examService;
    }

    public async Task<Exam> Handle(GetExamQuery request, CancellationToken cancellationToken)
    {
        return await _examService.GetByIdAsync(request.Id);
    }
}