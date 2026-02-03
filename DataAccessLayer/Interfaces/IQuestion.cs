using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.Application.Commands;
using System.Linq.Expressions;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IQuestion
{
    Task<Question> CreateAsync(CreateQuestionCommand command);
    Task<Question?> GetByIdAsync(string id);
    Task<List<Question>> GetAllAsync();
    Task<bool> UpdateAsync(UpdateQuestionCommand command);
    Task<bool> DeleteAsync(string id);
    Task<int> CountAsync(Expression<Func<Question, bool>>? predicate = null);
}
