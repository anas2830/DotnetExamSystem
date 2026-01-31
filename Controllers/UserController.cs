using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using System.Linq;
using System.Security.Claims;

namespace DotnetExamSystem.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromForm] CreateUserCommand command)
    {
        try
        {
            var user = await _mediator.Send(command);
            return Ok(user);
        }
        catch (FluentValidation.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new
            {
                property = e.PropertyName,
                message = e.ErrorMessage
            });

            return BadRequest(new
            {
                message = "Validation failed",
                errors
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(string id, [FromForm] UpdateUserCommand command)
    {
        try
        {
            command.Id = id;
            var user = await _mediator.Send(command);
            return Ok(user);
        }
        catch (FluentValidation.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new
            {
                property = e.PropertyName,
                message = e.ErrorMessage
            });

            return BadRequest(new
            {
                message = "Validation failed",
                errors
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _mediator.Send(new DeleteUserCommand { Id = id });
        return user ? NoContent() : NotFound();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _mediator.Send(new GetUserQuery { Id = id });
        return Ok(user);
    }

    [HttpGet("dashboard")]
    [Authorize]
    public async Task<IActionResult> GetDashboard([FromQuery] string? targetUserId = null)
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        var userId = userRole == "Admin" ? targetUserId : User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
        if (userId == null)
            return BadRequest(new { message = "User not found" });

        var dashboard = await _mediator.Send(new GetDashboardQuery { UserId = userId });
        return Ok(dashboard);
    }
}