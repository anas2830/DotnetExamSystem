using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.Application.Commands;
using System.Linq.Expressions;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IExam
{
    Task<Exam> CreateAsync(CreateExamCommand command);
    Task<Exam?> GetByIdAsync(string id);
    Task<List<Exam>> GetAllAsync( string userId, string role, string? search = null, int pageNumber = 1, int pageSize = 10);
    Task<bool> UpdateAsync(UpdateExamCommand command);
    Task<bool> DeleteAsync(string id);
    Task<int> CountAsync(Expression<Func<Exam, bool>>? predicate = null);
}