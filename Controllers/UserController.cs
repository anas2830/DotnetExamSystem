using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}