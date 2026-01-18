using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IUser
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateAsync(CreateUserCommand command);
    Task<bool> UpdateAsync(UpdateUserCommand command);
    Task<bool> DeleteAsync(string id);
    Task<User?> GetByIdAsync(string id);
}
