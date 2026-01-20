using MediatR;
using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetUserExamSummaryQuery : IRequest<UserExam>
{
    public string UserExamId { get; set; } = null!;
}
