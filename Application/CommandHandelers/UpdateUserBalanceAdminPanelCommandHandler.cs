using MediatR;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.Application.CommandHandelers
{
    public class UpdateUserBalanceAdminPanelCommandHandler 
        : IRequestHandler<UpdateUserBalanceAdminPanelCommand, User>
    {
        private readonly IUser _userService;

        public UpdateUserBalanceAdminPanelCommandHandler(IUser userService)
        {
            _userService = userService;
        }

        public async Task<User> Handle(UpdateUserBalanceAdminPanelCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateBalanceAsync(request);
        }
    }
}
