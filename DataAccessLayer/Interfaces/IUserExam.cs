using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.DTO;
using System.Linq.Expressions;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IUserExam
{
    Task<List<UserExam>> GetByExamIdAsync(string examId);
    Task<List<ExamUserDto>> GetExamUsersWithUserAsync(string examId);
    Task<UserExam> GetByIdAsync(string id);
    Task<UserExam> BuyExamAsync(string userId, string examId);
    Task<StartExamResponse> StartExamAsync(string userId, string examId);
    Task<UserExam> SubmitExamAsync(string userId, string examId, List<UserExamAnswer> answers);
    Task<List<UserExam>> GetByUserIdAsync(string userId);
    Task<int> CountAsync(Expression<Func<UserExam, bool>>? predicate = null);
}
