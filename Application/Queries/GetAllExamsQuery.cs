using MediatR;
using DotnetExamSystem.Api.Common;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetAllExamsQuery() : IRequest<PagedResult<ExamDto>>
{
    public string? UserId { get; set; }
    public string? Role { get; set; }
    public string? Search { get; set; } = null;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}