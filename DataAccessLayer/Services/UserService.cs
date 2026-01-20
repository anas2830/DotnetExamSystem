using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.DataAccessLayer.Repositories;
using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.Helpers;

namespace DotnetExamSystem.Api.DataAccessLayer.Services;

public class UserService : IUser
{
    private readonly UserRepository _userRepository;
    private readonly IWebHostEnvironment _env;

    public UserService(UserRepository userRepository, IWebHostEnvironment env)
    {
        _userRepository = userRepository;
        _env = env;
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

        string? profileImagePath = null;
        if (command.ProfileImage != null && command.ProfileImage.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(command.ProfileImage.FileName ?? "");
            string filePath = Path.Combine("uploads", uniqueFileName);
            using (var stream = new FileStream(Path.Combine(uploadsFolder, uniqueFileName), FileMode.Create))
            {
                await command.ProfileImage.CopyToAsync(stream);
            }
            profileImagePath = filePath;
        }

        var user = new User
        {
            Name = command.Name,
            Email = command.Email,
            Password = PasswordHelper.HashPassword(command.Password),
            Role = "User",
            Mobile = command.Mobile,
            Address = command.Address,
            ProfileImagePath = profileImagePath,
            Balance = 1000,
        };

        await _userRepository.CreateAsync(user);
        return user;
    }

    public async Task<bool> UpdateAsync(UpdateUserCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.Id);
        if (user == null)
            throw new Exception("User not found");
        
        if (command.ProfileImage != null && command.ProfileImage.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Delete existing image if exists
            if (!string.IsNullOrEmpty(user.ProfileImagePath))
            {
                var existingImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", user.ProfileImagePath);
                if (File.Exists(existingImagePath))
                    File.Delete(existingImagePath);
            }

            // Save new image
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(command.ProfileImage.FileName ?? "");
            var filePath = Path.Combine("uploads", uniqueFileName);
            using (var stream = new FileStream(Path.Combine(uploadsFolder, uniqueFileName), FileMode.Create))
            {
                await command.ProfileImage.CopyToAsync(stream);
            }

            user.ProfileImagePath = filePath;
        }
        
        user.Name = command.Name ?? user.Name;
        
        user.Mobile = command.Mobile ?? user.Mobile;
        user.Address = command.Address ?? user.Address;
        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) throw new Exception("User not found");
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
}
