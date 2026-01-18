using Microsoft.AspNetCore.Mvc;
using DotnetExamSystem.Api.Application.Commands;
using MediatR;

namespace DotnetExamSystem.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
