using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Exceptions;
using DotnetExamSystem.Api.Models;
using MediatR;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.Application.QueryHandler;

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, UserDashboardDto>
{
    private readonly IUser _userService;
    private readonly IUserExam _userExamService;
    private readonly IExam _examService;

    public GetDashboardQueryHandler(IUser userService, IUserExam userExamService, IExam examService)
    {
        _userService = userService;
        _userExamService = userExamService;
        _examService = examService;
    }

    public async Task<UserDashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(request.UserId);
        if (user == null)
            throw new ApiException("User not found");


        List<UserExam> userExams;
        List<Exam> exams;

        userExams = await _userExamService.GetByUserIdAsync(request.UserId) ?? new List<UserExam>();

        if (user.Role == "Admin")
        {
            exams = await _examService.GetAllAsync(request.UserId, "Admin") ?? new List<Exam>();
        }
        else
        {
            exams = await _examService.GetAllAsync(request.UserId, "User") ?? new List<Exam>();
        }

        var userExamsDto = userExams.Select(x =>
        {
            var exam = exams.FirstOrDefault(e => e.Id == x.ExamId);
            return new UserExamDto
            {
                ExamId = x.ExamId,
                UserExamId = x.Id,
                Title = exam?.Title ?? "",
                Status = x.Status,
                Price = exam?.Price ?? 0,
                PaidAmount = x.AmountPaid,
                Date = exam?.Date ?? DateTime.MinValue
            };
        }).ToList();

        // 4️⃣ Calculate stats
        int totalPurchased = userExams.Count(x => x.Status == "Started" || x.Status == "Booked" || x.Status == "Submitted");
        int totalSubmitted = userExams.Count(x => x.Status == "Submitted");
        int remainingExams = exams.Count(e => !userExams.Any(ue => ue.ExamId == e.Id) && e.Status == "Booked");

        return new UserDashboardDto
        {
            TotalPurchased = totalPurchased,
            TotalSubmitted = totalSubmitted,
            RemainingExams = remainingExams,
            Exams = userExamsDto
        };
    }

}