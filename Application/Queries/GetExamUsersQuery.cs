using MediatR;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetExamUsersQuery : IRequest<List<ExamUserDto>>
{
    public string ExamId { get; set; } = null!;
}
