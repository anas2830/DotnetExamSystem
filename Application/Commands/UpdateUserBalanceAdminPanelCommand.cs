 using MediatR;
 using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Commands;

public class UpdateUserBalanceAdminPanelCommand : IRequest<User>
{
    public string? Id { get; set; }
    public decimal Balance { get; set; }
}