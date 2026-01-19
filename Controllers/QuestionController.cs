using Microsoft.AspNetCore.Mvc;
using MediatR;
using DotnetExamSystem.Api.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using DotnetExamSystem.Api.Application.Queries;

namespace DotnetExamSystem.Api.Controllers;

[ApiController]
[Route("api/question")]
public class QuestionController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuestionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand command)
    {
        try
        {
            var question = await _mediator.Send(command);
            return Ok(question);
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
    public async Task<IActionResult> UpdateQuestion(string id, [FromBody] UpdateQuestionCommand command)
    {
        try
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(new { message = "Question updated successfully", success = result });
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
    public async Task<IActionResult> DeleteQuestion(string id)
    {
        var result = await _mediator.Send(new DeleteQuestionCommand { Id = id });
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetQuestion(string id)
    {
        var question = await _mediator.Send(new GetQuestionQuery { Id = id });
        return Ok(question);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllQuestions()
    {
        var questions = await _mediator.Send(new GetAllQuestionsQuery());
        return Ok(questions);
    }
}
