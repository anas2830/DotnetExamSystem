using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.QueryHandler;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
{
    private readonly IUser _userService; 

    public GetUserQueryHandler(IUser userService)
    {
        _userService = userService;
    }

    public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetByIdAsync(request.Id);
    }
}