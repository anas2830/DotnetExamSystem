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
        try
        {
            var result = await _mediator.Send(request);
            return Ok(result);
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
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return result ? Ok(new { message = "Password changed successfully" }) : BadRequest(new { message = "Failed to change password" });
        }
        catch (FluentValidation.ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new
            {
                property = e.PropertyName,
                message = e.ErrorMessage
            });
            return BadRequest(new { message = "Validation failed", errors });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}