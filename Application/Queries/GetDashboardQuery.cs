using MediatR;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetDashboardQuery : IRequest<UserDashboardDto>
{
    public string UserId { get; set; } = null!;
}
