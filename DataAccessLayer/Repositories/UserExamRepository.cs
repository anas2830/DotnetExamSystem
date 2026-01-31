using DotnetExamSystem.Api.Models;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DotnetExamSystem.Api.DataAccessLayer.Repositories;

public class UserExamRepository
{
    private readonly MongoDbContext _context;
    private readonly IMongoCollection<UserExam> _collection;

    public UserExamRepository(MongoDbContext context)
    {
        _context = context;
        _collection = _context.Database.GetCollection<UserExam>("UserExams");
    }

    public async Task<UserExam?> GetByUserAndExamAsync(string userId, string examId)
        => await _collection.Find(x => x.UserId == userId && x.ExamId == examId).FirstOrDefaultAsync();

    public async Task CreateAsync(UserExam userExam) => await _collection.InsertOneAsync(userExam);

    public async Task UpdateAsync(UserExam userExam)
        => await _collection.ReplaceOneAsync(x => x.Id == userExam.Id, userExam);

    public async Task<UserExam?> GetByIdAsync(string id)
        => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<bool> ExistsAsync(Expression<Func<UserExam, bool>> filter) => await _collection.Find(filter).AnyAsync();

    public async Task<List<UserExam>> GetByExamIdAsync(string examId)
    {
        return await _collection.Find(ue => ue.ExamId == examId).ToListAsync();
    }

    public async Task<List<UserExam>> GetByUserIdAsync(string userId)
    {
        return await _collection.Find(ue => ue.UserId == userId).ToListAsync();
    }
}
