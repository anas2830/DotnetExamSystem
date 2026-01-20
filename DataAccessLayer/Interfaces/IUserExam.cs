using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IUserExam
{
    Task<List<UserExam>> GetByExamIdAsync(string examId);
    Task<List<ExamUserDto>> GetExamUsersWithUserAsync(string examId);
    Task<UserExam> GetByIdAsync(string id);
    Task<UserExam> BuyExamAsync(string userId, string examId);
    Task<UserExam> StartExamAsync(string userId, string examId);
    Task<UserExam> SubmitExamAsync(string userId, string examId, List<UserExamAnswer> answers);
}
