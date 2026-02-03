using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.DTO;
using MediatR;


namespace DotnetExamSystem.Api.Application.QueryHandler; 

public class GetAdminDashboardQueryHandler
    : IRequestHandler<GetAdminDashboardQuery, AdminDashboardDto>
{
    private readonly IUser _userRepo;
    private readonly IExam _examRepo;
    private readonly IQuestion _questionRepo;

    public GetAdminDashboardQueryHandler(
        IUser userRepo,
        IExam examRepo,
        IQuestion questionRepo)
    {
        _userRepo = userRepo;
        _examRepo = examRepo;
        _questionRepo = questionRepo;
    }

    public async Task<AdminDashboardDto> Handle(GetAdminDashboardQuery request,CancellationToken cancellationToken)
    {
        return new AdminDashboardDto
        {
            TotalUsers = await _userRepo.CountAsync(x => x.Role == "User"),
            TotalExams = await _examRepo.CountAsync(),
            TotalQuestions = await _questionRepo.CountAsync()
        };
    }
}
