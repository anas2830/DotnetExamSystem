using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Exceptions;
using DotnetExamSystem.Api.Helpers;
using MediatR;

namespace DotnetExamSystem.Api.Application.CommandHandelers;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IUser _userService;

    public ChangePasswordCommandHandler(IUser userService)
    {
        _userService = userService;
    }

    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmailAsync(request.Email);
        if (user == null)
            throw new ApiException("User not found");

        bool isPasswordValid = PasswordHelper.VerifyPassword(request.OldPassword, user.Password);
        if (!isPasswordValid)
            throw new ApiException("Old password is incorrect");

        if (request.NewPassword != request.ConfirmPassword)
            throw new ApiException("New password and confirm password do not match");

        var newHash = PasswordHelper.HashPassword(request.NewPassword);

        var result = await _userService.UpdatePasswordAsync(user.Id, newHash);

        if (!result)
            throw new ApiException("Failed to update password");

        return true;
    }
}
