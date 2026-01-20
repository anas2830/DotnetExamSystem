using MediatR;
using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DTO;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;

namespace DotnetExamSystem.Api.Application.QueryHandlers;

public class GetExamUsersQueryHandler : IRequestHandler<GetExamUsersQuery, List<ExamUserDto>>
{
    private readonly IUserExam _userExamService;

    public GetExamUsersQueryHandler(IUserExam userExamService)
    {
        _userExamService = userExamService;
    }

    public async Task<List<ExamUserDto>> Handle(GetExamUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userExamService.GetExamUsersWithUserAsync(request.ExamId);
    }
}