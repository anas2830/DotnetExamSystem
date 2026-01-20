using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IExam
{
    Task<Exam> CreateAsync(CreateExamCommand command);
    Task<Exam?> GetByIdAsync(string id);
    Task<List<Exam>> GetAllAsync();
    Task<bool> UpdateAsync(UpdateExamCommand command);
    Task<bool> DeleteAsync(string id);
}