using DotnetExamSystem.Api.Models;
using MongoDB.Driver;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.DataAccessLayer.Repositories;

public class ExamRepository
{
    private readonly IMongoCollection<Exam> _exams;

    public ExamRepository(MongoDbContext context)
    {
        _exams = context.GetCollection<Exam>("Exams");
    }

    public async Task<List<Exam>> GetAllAsync() => await _exams.Find(_ => true).ToListAsync();
    public async Task<Exam?> GetByIdAsync(string id) => await _exams.Find(e => e.Id == id).FirstOrDefaultAsync();
    public async Task CreateAsync(Exam exam) => await _exams.InsertOneAsync(exam);
    public async Task<bool> UpdateAsync(Exam exam)
    {
        var result = await _exams.ReplaceOneAsync(e => e.Id == exam.Id, exam);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _exams.DeleteOneAsync(e => e.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}