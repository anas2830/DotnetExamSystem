using Microsoft.AspNetCore.Mvc;
using MediatR;
using DotnetExamSystem.Api.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using DotnetExamSystem.Api.Application.Queries;

namespace DotnetExamSystem.Api.Controllers;

[ApiController]
[Route("api/exam")]
public class ExamController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateExam([FromBody] CreateExamCommand command)
    {
        try
        {
            var exam = await _mediator.Send(command);
            return Ok(exam);
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateExam(string id, [FromBody] UpdateExamCommand command)
    {
        try
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(new { message = "Exam updated successfully", success = result });
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

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteExam(string id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteExamCommand { Id = id });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetExam(string id)
    {
        var exam = await _mediator.Send(new GetExamQuery { Id = id });
        return Ok(exam);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllExams()
    {
        var exams = await _mediator.Send(new GetAllExamsQuery());
        return Ok(exams);
    }

    [HttpGet("{examId}/users")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetExamUsers(string examId)
    {
        try
        {
            var users = await _mediator.Send(new GetExamUsersQuery { ExamId = examId });
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
