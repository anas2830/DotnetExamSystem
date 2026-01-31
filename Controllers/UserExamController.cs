using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.Application.Queries;
using System.Security.Claims;

[ApiController]
[Route("api/userexam")]
public class UserExamController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserExamController(IMediator mediator) => _mediator = mediator;

    [HttpPost("buy")]
    [Authorize]
    public async Task<IActionResult> BuyExam([FromBody] BuyExamCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("start")]
    [Authorize]
    public async Task<IActionResult> StartExam([FromBody] StartExamCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("submit")]
    [Authorize]
    public async Task<IActionResult> SubmitExam([FromBody] SubmitExamCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{userExamId}/summary")]
    [Authorize]
    public async Task<IActionResult> GetExamSummary(string userExamId)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var summary = await _mediator.Send(new GetUserExamSummaryQuery
            {
                UserExamId = userExamId,
                UserId = userId,
                Role = role
            });
            return Ok(summary);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
