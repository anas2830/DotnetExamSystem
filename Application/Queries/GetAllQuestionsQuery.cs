using MediatR;
using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetAllQuestionsQuery : IRequest<List<Question>>
{
    public string? Id { get; set; }
}