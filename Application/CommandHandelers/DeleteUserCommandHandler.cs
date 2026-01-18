using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUser _userService;

    public DeleteUserCommandHandler(IUser userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.DeleteAsync(request.Id);
    }
}