using MediatR;
using DotnetExamSystem.Api.Application.Queries;
using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.DTO;
using DotnetExamSystem.Api.Exceptions;

namespace DotnetExamSystem.Api.Application.QueryHandlers;

public class GetUserExamSummaryQueryHandler : IRequestHandler<GetUserExamSummaryQuery, UserExamSummaryDto>
{
    private readonly IUserExam _userExamService;
    private readonly IUser _userService;
    private readonly IExam _examService;
    private readonly IQuestion _questionService;

    public GetUserExamSummaryQueryHandler(
        IUserExam userExamService,
        IUser userService,
        IExam examService,
        IQuestion questionService)
    {
        _userExamService = userExamService;
        _userService = userService;
        _examService = examService;
        _questionService = questionService;
    }

    public async Task<UserExamSummaryDto> Handle(
        GetUserExamSummaryQuery request,
        CancellationToken cancellationToken)
    {
        var userExam = await _userExamService.GetByIdAsync(request.UserExamId);

        if (userExam == null)
            throw new ApiException("User exam not found");

        if (userExam.Status != "Submitted")
            throw new ApiException("Exam not submitted yet");

        var user = await _userService.GetByIdAsync(userExam.UserId);
        var exam = await _examService.GetByIdAsync(userExam.ExamId);

        // Load questions
        var questionIds = userExam.Answers.Select(a => a.QuestionId).ToList();
        var questions = new List<Question>();
        foreach (var qid in questionIds)
        {
            var q = await _questionService.GetByIdAsync(qid);
            if (q != null) questions.Add(q);
        }

        // Map questions + answers
        var questionsWithAnswers = questions.Select(q =>
        {
            var answer = userExam.Answers.First(a => a.QuestionId == q.Id);
            return new UserExamQuestionDto
            {
                Question = q,
                Answer = answer
            };
        }).ToList();

        return new UserExamSummaryDto
        {
            Id = userExam.Id,
            BookedAt = userExam.BookedAt,
            Status = userExam.Status,
            Score = userExam.Score ?? 0,
            AmountPaid = userExam.AmountPaid,
            User = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            },
            Exam = new ExamDto
            {
                Id = exam.Id,
                Title = exam.Title,
                Date = exam.Date,
                TotalQuestions = exam.TotalQuestions,
                TimeInMinutes = exam.TimeInMinutes,
                Price = exam.Price
            },
            Questions = questionsWithAnswers
        };
    }
}
