using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        var user = await _mediator.Send(command);
        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(string id, [FromForm] UpdateUserCommand command)
    {
        command.Id = id;
        var user = await _mediator.Send(command);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _mediator.Send(new DeleteUserCommand { Id = id });
        return user ? NoContent() : NotFound();
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _mediator.Send(new GetUserQuery { Id = id });
        return Ok(user);
    }
}