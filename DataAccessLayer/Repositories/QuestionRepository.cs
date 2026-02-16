using DotnetExamSystem.Api.Models;
using MongoDB.Driver;
using DotnetExamSystem.Api.Application.Commands;
using System.Linq.Expressions;
using DotnetExamSystem.Api.Common;

namespace DotnetExamSystem.Api.DataAccessLayer.Repositories;

public class QuestionRepository
{
    private readonly IMongoCollection<Question> _questions;

    public QuestionRepository(MongoDbContext context)
    {
        _questions = context.GetCollection<Question>("Questions");
    }

    public async Task<PagedResult<Question>> GetAllAsync( string? search = null, int pageNumber = 1, int pageSize = 10){
        var filterBuilder = Builders<Question>.Filter;
        FilterDefinition<Question> filter = filterBuilder.Empty;
        if (!string.IsNullOrEmpty(search))
        {
            filter = filterBuilder.Regex(
                q => q.Title,
                new MongoDB.Bson.BsonRegularExpression(search, "i")
            );
        }
        var totalCount = (int)await _questions.CountDocumentsAsync(filter);
        var questions = await _questions.Find(filter).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        return new PagedResult<Question>
        {
            Items = questions,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
    public async Task<Question?> GetByIdAsync(string id) => await _questions.Find(q => q.Id == id).FirstOrDefaultAsync();
    public async Task CreateAsync(Question question) => await _questions.InsertOneAsync(question);
    public async Task<bool> UpdateAsync(Question question)
    {
        var result = await _questions.ReplaceOneAsync(q => q.Id == question.Id, question);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _questions.DeleteOneAsync(q => q.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<List<Question>> GetRandomAsync(int total)
    {
        return await _questions
            .Aggregate()
            .Sample(total)
            .ToListAsync();
    }

    public async Task<int> CountAsync(Expression<Func<Question, bool>>? predicate = null)
    {
        if (predicate == null)
            return (int)await _questions.CountDocumentsAsync(_ => true);

        return (int)await _questions.CountDocumentsAsync(predicate);
    }
}
