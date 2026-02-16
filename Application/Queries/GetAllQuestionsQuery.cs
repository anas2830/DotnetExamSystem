using MediatR;
using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.Common;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetAllQuestionsQuery() : IRequest<PagedResult<Question>>
{
    public string? Search { get; set; } = null;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}