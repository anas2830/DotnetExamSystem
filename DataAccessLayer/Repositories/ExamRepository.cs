using DotnetExamSystem.Api.Models;
using MongoDB.Driver;
using DotnetExamSystem.Api.Application.Commands;
using MongoDB.Bson;
using System.Linq.Expressions;
using DotnetExamSystem.Api.Common;

namespace DotnetExamSystem.Api.DataAccessLayer.Repositories;

public class ExamRepository
{
    private readonly IMongoCollection<Exam> _exams;
    private readonly IMongoCollection<UserExam> _userExams;

    public ExamRepository(MongoDbContext context)
    {
        _exams = context.GetCollection<Exam>("Exams");
        _userExams = context.GetCollection<UserExam>("UserExams");
    }

    public async Task<PagedResult<Exam>> GetAllAsync(string? userId, string? role, string? search = null, int pageNumber = 1, int pageSize = 10)
    {
         var filterBuilder = Builders<Exam>.Filter;

        // base filter
        FilterDefinition<Exam> filter = filterBuilder.Empty;

        // search by title
        if (!string.IsNullOrEmpty(search))
        {
            filter = filterBuilder.Regex(
                e => e.Title,
                new MongoDB.Bson.BsonRegularExpression(search, "i")
            );
        }

        var totalCount = (int)await _exams.CountDocumentsAsync(filter);

        var exams = await _exams.Find(filter).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();

        if (role == "User" && !string.IsNullOrEmpty(userId))
        {
            foreach (var exam in exams)
            {
                var userExam = await _userExams.Find(x => x.ExamId == exam.Id && x.UserId == userId).FirstOrDefaultAsync();
                exam.AlreadyPurchase = userExam != null ? 1 : 0;
            }
        }

        return new PagedResult<Exam>
        {
            Items = exams,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

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
    public async Task<int> CountAsync(Expression<Func<Exam, bool>>? predicate = null)
    {
        if (predicate == null)
            return (int)await _exams.CountDocumentsAsync(_ => true);

        return (int)await _exams.CountDocumentsAsync(predicate);
    }
}