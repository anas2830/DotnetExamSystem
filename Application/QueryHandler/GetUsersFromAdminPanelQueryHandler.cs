using MediatR;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.DTO;
using DotnetExamSystem.Api.Application.Queries;

public class GetUsersFromAdminPanelQueryHandler : IRequestHandler<GetUsersFromAdminPanelQuery, List<UserFromAdminPanelDto>>
{
    private readonly IUser _userService;
    private readonly IUserExam _userExamService;

    public GetUsersFromAdminPanelQueryHandler(IUser userService, IUserExam userExamService)
    {
        _userService = userService;
        _userExamService = userExamService;
    }

    public async Task<List<UserFromAdminPanelDto>> Handle(GetUsersFromAdminPanelQuery request, CancellationToken cancellationToken)
    {
        // Step 1: All users with role "User" and optional query
        var users = await _userService.GetAllAsync(request.Query);

        var result = new List<UserFromAdminPanelDto>();

        foreach (var user in users)
        {
            // Step 2: Count purchased exams
            var purchasedCount = await _userExamService.CountAsync(x => x.UserId == user.Id);

            // Step 3: Count submitted exams
            var submittedCount = await _userExamService.CountAsync(x => x.UserId == user.Id && x.Status == "Submitted");

            result.Add(new UserFromAdminPanelDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Mobile = user.Mobile,
                Address = user.Address,
                Balance = user.Balance,
                ProfileImagePath = user.ProfileImagePath,
                TotalPurchaseExams = purchasedCount,
                TotalSubmittedExams = submittedCount,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            });
        }

        return result;
    }
}
