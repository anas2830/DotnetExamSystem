using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUser _userService;

    public UpdateUserCommandHandler(IUser userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    
    {
        return await _userService.UpdateAsync(request);
    }
}