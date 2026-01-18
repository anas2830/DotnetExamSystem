using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;
using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUser _userService;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(IUser userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return new LoginResponse
        {
            Token = _jwtService.GenerateToken(user.Id, user.Name, user.Role),
            UserId = user.Id,
            UserName = user.Name,
            UserRole = user.Role
        };
    }
}