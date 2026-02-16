using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;
using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.DTO;
using DotnetExamSystem.Api.Exceptions;
using DotnetExamSystem.Api.Helpers;

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
            throw new ApiException("User not found");
        }
        bool isPasswordValid = PasswordHelper.VerifyPassword(request.Password, user.Password);
        if (!isPasswordValid)
        {
            throw new ApiException("Invalid credentials");
        }
        return new LoginResponse
        {
            Token = _jwtService.GenerateToken(user.Id, user.Name, user.Role),
            UserId = user.Id,
            UserName = user.Name,
            UserRole = user.Role,
            UserAvatar = user.ProfileImagePath,
            UserEmail = user.Email
        };
    }
}