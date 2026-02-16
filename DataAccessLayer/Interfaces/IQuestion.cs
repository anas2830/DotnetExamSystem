using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.Application.Commands;
using System.Linq.Expressions;
using DotnetExamSystem.Api.Common;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IQuestion
{
    Task<Question> CreateAsync(CreateQuestionCommand command);
    Task<Question?> GetByIdAsync(string id);
    Task<PagedResult<Question>> GetAllAsync( string? search = null, int pageNumber = 1, int pageSize = 10);
    Task<bool> UpdateAsync(UpdateQuestionCommand command);
    Task<bool> DeleteAsync(string id);
    Task<int> CountAsync(Expression<Func<Question, bool>>? predicate = null);
}
