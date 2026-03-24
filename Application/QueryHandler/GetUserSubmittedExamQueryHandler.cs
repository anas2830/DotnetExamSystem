using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using MediatR;
using DotnetExamSystem.Api.Exceptions;
using DotnetExamSystem.Api.DTO;


namespace DotnetExamSystem.Api.Application.QueryHandler;

public class GetUserSubmittedExamQueryHandler : IRequestHandler<GetUserSubmittedExamQuery, List<UserExamDto>>
{
    private readonly IUser _userService; 
    private readonly IUserExam _userExamService;
    private readonly IExam _examService;
    
    public GetUserSubmittedExamQueryHandler(IUser userService, IUserExam userExamService, IExam examService)
    {
        _userService = userService;
        _userExamService = userExamService;
        _examService = examService;
    }

    public async Task<List<UserExamDto>> Handle(GetUserSubmittedExamQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(request.UserId ?? "");
        if (user == null)
            throw new ApiException("User not found");
        var userExams = await _userExamService.GetByUserIdAsync(request.UserId);
        var result = new List<UserExamDto>();

        foreach (var userExam in userExams)
        {
            var exam = await _examService.GetByIdAsync(userExam.ExamId);
            result.Add(new UserExamDto
            {
                ExamId = userExam.ExamId,
                UserExamId = userExam.Id,
                Title = exam?.Title ?? "",
                Status = userExam.Status,
                Price = exam?.Price ?? 0,
                PaidAmount = userExam.AmountPaid,
                Duration = exam?.TimeInMinutes ?? 0,
                Score = userExam.Score ?? 0
            });
        }
        return result;

    }

}