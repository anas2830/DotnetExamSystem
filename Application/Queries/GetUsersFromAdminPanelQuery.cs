using MediatR;
using System.Collections.Generic;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetUsersFromAdminPanelQuery : IRequest<List<UserFromAdminPanelDto>>
{
    public string? Query { get; set; }
}
