using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUser _userService;

    public CreateUserCommandHandler(IUser userService)
    {
        _userService = userService;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.CreateAsync(request);
    }
}