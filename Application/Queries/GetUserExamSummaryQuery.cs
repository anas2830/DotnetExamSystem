using MediatR;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetUserExamSummaryQuery : IRequest<UserExamSummaryDto>
{
    public string UserExamId { get; set; } = null!;
}
