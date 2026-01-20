using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.DataAccessLayer.Repositories;
using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.DataAccessLayer.Services;

public class UserExamService : IUserExam
{
    private readonly UserExamRepository _repo;
    private readonly ExamRepository _examRepo;
    private readonly QuestionRepository _questionRepo;
    private readonly UserRepository _userRepo;

    public UserExamService(UserExamRepository repo, ExamRepository examRepo, QuestionRepository questionRepo, UserRepository userRepo)
    {
        _repo = repo;
        _examRepo = examRepo;
        _questionRepo = questionRepo;
        _userRepo = userRepo;
    }

    public async Task<UserExam> GetByIdAsync(string id)
    {
        var userExam = await _repo.GetByIdAsync(id);
        if (userExam == null)
            throw new Exception("Exam not found");
        return userExam;
    }

    public async Task<UserExam> BuyExamAsync(string userId, string examId)
    {
        var exam = await _examRepo.GetByIdAsync(examId);
        if (exam == null)
            throw new Exception("Exam not found");

        var existing = await _repo.GetByUserAndExamAsync(userId, examId);
        if (existing != null)
            throw new Exception("User already bought this exam");

        var userExam = new UserExam
        {
            UserId = userId,
            ExamId = examId,
            Status = "Booked",
            BookedAt = DateTime.UtcNow,
            AmountPaid = exam.Price
        };

        await _repo.CreateAsync(userExam);
        return userExam;
    }

    public async Task<UserExam> StartExamAsync(string userId, string examId)
    {
        var existing = await _repo.GetByUserAndExamAsync(userId, examId);
        if (existing == null)
            throw new Exception("User has not bought this exam");

        if (existing.Status != "Booked")
            throw new Exception("Exam already started or submitted");

        var exam = await _examRepo.GetByIdAsync(examId);
        if (exam == null)
            throw new Exception("Exam not found");

        var today = DateTime.UtcNow.Date;
        if (today != exam.Date.Date)
            throw new Exception($"This exam can only be taken on {exam.Date:dd-MM-yyyy}");

        existing.Status = "Started";
        await _repo.UpdateAsync(existing);
        return existing;
    }

    public async Task<UserExam> SubmitExamAsync(string userId, string examId, List<UserExamAnswer> answers)
    {
        var existing = await _repo.GetByUserAndExamAsync(userId, examId);
        if (existing == null)
            throw new Exception("User has not bought this exam");

        if (existing.Status != "Started")
            throw new Exception("Exam not started or already submitted");

        // Evaluate score
        int score = 0;
        foreach (var ans in answers)
        {
            var q = await _questionRepo.GetByIdAsync(ans.QuestionId);
            ans.CorrectAnswer = q?.CorrectAnswer;
            if (q != null && q.CorrectAnswer == ans.SelectedAnswer)
                ans.IsCorrect = true;
            else
                ans.IsCorrect = false;

            if (ans.IsCorrect == true) score++; 
        }

        existing.Answers = answers;
        existing.Score = score;
        existing.Status = "Submitted";

        await _repo.UpdateAsync(existing);
        return existing;
    }

    public async Task<List<UserExam>> GetByExamIdAsync(string examId)
    {
        var userExams = await _repo.GetByExamIdAsync(examId);
        if (userExams.Count == 0)
            throw new Exception("No user exams found for this exam");
        return userExams;
    }

    public async Task<List<ExamUserDto>> GetExamUsersWithUserAsync(string examId)
    {
        var userExams = await GetByExamIdAsync(examId);
        var result = new List<ExamUserDto>();

        foreach (var ue in userExams)
        {
            var user = await _userRepo.GetByIdAsync(ue.UserId);
            if (user == null) continue;

            result.Add(new ExamUserDto
            {
                UserExamId = ue.Id,
                User = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Mobile = user.Mobile ?? "",
                    Address = user.Address ?? "",
                    Balance = user.Balance,
                    ProfileImagePath = user.ProfileImagePath ?? ""
                },
                BookedAt = ue.BookedAt,
                Status = ue.Status,
                AmountPaid = ue.AmountPaid
            });
        }

        return result;
    }
}
