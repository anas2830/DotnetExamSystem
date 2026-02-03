using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetAdminDashboard()
    {
        var adminDashboard = await _mediator.Send(new GetAdminDashboardQuery());
        return Ok(adminDashboard);
    }


    [HttpGet("users")]
    public async Task<IActionResult> GetUsers([FromQuery(Name = "search")] string? query = null)
    {
        var result = await _mediator.Send(new GetUsersFromAdminPanelQuery { Query = query });
        return Ok(result);
    }


    [HttpPut("users/update-balance")]
    public async Task<IActionResult> UpdateUserBalance([FromBody] UpdateUserBalanceAdminPanelCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    } 
}
