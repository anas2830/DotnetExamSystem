using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IQuestion
{
    Task<Question> CreateAsync(Question question);
    Task<Question?> GetByIdAsync(string id);
    Task<List<Question>> GetAllAsync();
    Task<bool> UpdateAsync(Question question);
    Task<bool> DeleteAsync(string id);
}
