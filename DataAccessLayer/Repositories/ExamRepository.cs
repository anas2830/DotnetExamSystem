using DotnetExamSystem.Api.Models;
using MongoDB.Driver;
using DotnetExamSystem.Api.Application.Commands;
using MongoDB.Bson;

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

    public async Task<List<Exam>> GetAllAsync(string? userId, string? role)
    {
        var exams = await _exams.Find(_ => true).ToListAsync();

        if (role == "User" && !string.IsNullOrEmpty(userId))
        {
            foreach (var exam in exams)
            {
                var userExam = await _userExams.Find(x => x.ExamId == exam.Id && x.UserId == userId).FirstOrDefaultAsync();

                if (userExam != null)
                {
                    exam.AlreadyPurchase = 1;
                    exam.Status = userExam.Status;   // e.g. Booked / Started / Completed
                    exam.UserExamId = userExam.Id;
                }
                else
                {
                    exam.AlreadyPurchase = 0;
                    exam.Status = "Not Purchased";
                    exam.UserExamId = null;
                }
            }
        }

        return exams;
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
}