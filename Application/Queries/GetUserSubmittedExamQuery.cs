using MediatR;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetUserSubmittedExamQuery : IRequest<List<UserExamDto>>
{
    public string? UserId { get; set; } 
}