using DotnetExamSystem.Api.Models;
using MongoDB.Driver;

namespace DotnetExamSystem.Api.DataAccessLayer.Repositories;

public class QuestionRepository
{
    private readonly IMongoCollection<Question> _questions;

    public QuestionRepository(MongoDbContext context)
    {
        _questions = context.GetCollection<Question>("Questions");
    }

    public async Task<List<Question>> GetAllAsync() => await _questions.Find(_ => true).ToListAsync();
    public async Task<Question?> GetByIdAsync(string id) => await _questions.Find(q => q.Id == id).FirstOrDefaultAsync();
    public async Task CreateAsync(Question question) => await _questions.InsertOneAsync(question);
    public async Task<bool> UpdateAsync(Question question)
    {
        var result = await _questions.ReplaceOneAsync(q => q.Id == question.Id, question);
        return result.ModifiedCount > 0;
    }
    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _questions.DeleteOneAsync(q => q.Id == id);
        return result.DeletedCount > 0;
    }
}
