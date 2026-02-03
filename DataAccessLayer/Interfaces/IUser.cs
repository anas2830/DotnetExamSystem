using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.Application.Commands;
using System.Linq.Expressions;

namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IUser
{
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateAsync(CreateUserCommand command);
    Task<bool> UpdateAsync(UpdateUserCommand command);
    Task<bool> DeleteAsync(string id);
    Task<User?> GetByIdAsync(string id);
    Task<int> CountAsync( Expression<Func<User, bool>>? predicate = null );
    Task<List<User>> GetAllAsync(string? query = null);
    Task<User> UpdateBalanceAsync(UpdateUserBalanceAdminPanelCommand command);
}
