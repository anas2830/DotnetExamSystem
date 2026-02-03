using DotnetExamSystem.Api.Models;
using MongoDB.Driver;
using System.Linq.Expressions;
using DotnetExamSystem.Api.Exceptions;
using MongoDB.Bson;

namespace DotnetExamSystem.Api.DataAccessLayer.Repositories;

public class UserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(MongoDbContext context)
    {
        _users = context.GetCollection<User>("Users");
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var result = await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _users.DeleteOneAsync(u => u.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> CountAsync(Expression<Func<User, bool>>? predicate = null)
    {
        if (predicate == null)
            return (int)await _users.CountDocumentsAsync(_ => true);

        return (int)await _users.CountDocumentsAsync(predicate);
    }

    public async Task<List<User>> GetAllAsync(string? query = null)
    {
        var filterBuilder = Builders<User>.Filter;
        var filter = filterBuilder.Eq(u => u.Role, "User"); // only users, not admin

        if (!string.IsNullOrEmpty(query))
        {
            var searchFilter = filterBuilder.Or(
                filterBuilder.Regex(u => u.Name, new MongoDB.Bson.BsonRegularExpression(query, "i")),
                filterBuilder.Regex(u => u.Email, new MongoDB.Bson.BsonRegularExpression(query, "i"))
            );

            filter = filterBuilder.And(filter, searchFilter);
        }

        return await _users.Find(filter).ToListAsync();
    }

    public async Task<User> UpdateBalanceAsync(User user)
    {
        var update = Builders<User>.Update
            .Set(u => u.Balance, user.Balance)
            .Set(u => u.UpdatedAt, user.UpdatedAt);

        var result = await _users.UpdateOneAsync(u => u.Id == user.Id, update);

        if (!result.IsAcknowledged || result.ModifiedCount == 0)
            throw new ApiException("Failed to update user balance");

        return user; // Update successful, return updated user
    }
}
