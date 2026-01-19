using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IQuestion
{
    Task<Question> CreateAsync(CreateQuestionCommand command);
    Task<Question?> GetByIdAsync(string id);
    Task<List<Question>> GetAllAsync();
    Task<bool> UpdateAsync(Question question);
    Task<bool> DeleteAsync(string id);
}
