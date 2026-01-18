using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.DataAccessLayer.Repositories;
using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.Helpers;

namespace DotnetExamSystem.Api.DataAccessLayer.Services;

public class UserService : IUser
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<User> CreateAsync(CreateUserCommand command)
    {
        var existingUser = await _userRepository.GetByEmailAsync(command.Email);
        if (existingUser != null)
            throw new Exception("User already exists");

        var user = new User
        {
            Name = command.Name,
            Email = command.Email,
            PasswordHash = PasswordHelper.HashPassword(command.Password),
            Role = "User"
        };

        await _userRepository.CreateAsync(user);
        return user;
    }
}
