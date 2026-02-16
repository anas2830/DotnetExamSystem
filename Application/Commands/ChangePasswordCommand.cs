 using MediatR;

 namespace DotnetExamSystem.Api.Application.Commands;

 public class ChangePasswordCommand : IRequest<bool>
 {
    public string? Email { get; set; } = default!;
    public string? OldPassword { get; set; } = default!;
    public string? NewPassword { get; set; } = default!;
    public string? ConfirmPassword { get; set; } = default!;
 }