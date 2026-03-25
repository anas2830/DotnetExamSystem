using DotnetExamSystem.Api.Models;
using MongoDB.Driver;
using DotnetExamSystem.Api.Application.Commands;
using MongoDB.Bson;
using System.Linq.Expressions;
using DotnetExamSystem.Api.Common;
using DotnetExamSystem.Api.DTO;

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

    public async Task<PagedResult<ExamDto>> GetAllAsync(string? userId, string? role, string? search = null, int pageNumber = 1, int pageSize = 10)
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

        List<UserExam> userExams = new();

        if (!string.IsNullOrEmpty(userId))
        {
            var examIds = exams.Select(x => x.Id).ToList();

            userExams = await _userExams.Find(x =>
                x.UserId == userId && examIds.Contains(x.ExamId)
            ).ToListAsync();
        }

        var items = exams.Select(exam =>
        {
            var userExam = userExams.FirstOrDefault(x => x.ExamId == exam.Id);

            return new ExamDto
            {
                Id = exam.Id,
                Title = exam.Title,
                Price = exam.Price,
                TimeInMinutes = exam.TimeInMinutes,
                TotalQuestions = exam.TotalQuestions,
                AlreadyPurchase = userExam != null,
                ExamDate = userExam?.ExamDate,
                Status = userExam?.Status ?? "Booked"
            };
        }).ToList();

        return new PagedResult<ExamDto>
        {
            Items = items,
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