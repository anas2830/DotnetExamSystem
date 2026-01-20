using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IUserExam
{
    Task<UserExam> GetByIdAsync(string id);
    Task<UserExam> BuyExamAsync(string userId, string examId);
    Task<UserExam> StartExamAsync(string userId, string examId);
    Task<UserExam> SubmitExamAsync(string userId, string examId, List<UserExamAnswer> answers);
}
