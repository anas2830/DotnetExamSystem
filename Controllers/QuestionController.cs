using Microsoft.AspNetCore.Mvc;
using MediatR;
using DotnetExamSystem.Api.Application.Commands;
using Microsoft.AspNetCore.Authorization;

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
        var question = await _mediator.Send(command);
        return Ok(question);
    }

    // [HttpPut("{id}")]
    // [Authorize(Roles = "Admin")]
    // public async Task<IActionResult> UpdateQuestion(string id, [FromForm] UpdateQuestionCommand command)
    // {
    //     command.Id = id;
    //     var result = await _mediator.Send(command);
    //     return Ok(result);
    // }

    // [HttpDelete("{id}")]
    // [Authorize(Roles = "Admin")]
    // public async Task<IActionResult> DeleteQuestion(string id)
    // {
    //     var result = await _mediator.Send(new DeleteQuestionCommand { Id = id });
    //     return Ok(result);
    // }

    // [HttpGet]
    // [Authorize]
    // public async Task<IActionResult> GetAllQuestions()
    // {
    //     // later you can add query handler
    //     return Ok("Add query handler for fetching all questions");
    // }
}
