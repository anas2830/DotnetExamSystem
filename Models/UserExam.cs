using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetExamSystem.Api.Models;

public class UserExam
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public string ExamId { get; set; } = null!;
    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Booked"; // Booked | Started | Submitted
    public int? Score { get; set; } = 0;
    public List<UserExamAnswer> Answers { get; set; } = new();
    public decimal AmountPaid { get; set; }
}

public class UserExamAnswer
{
    public string QuestionId { get; set; } = null!;
    public string SelectedAnswer { get; set; } = null!;
    public bool? IsCorrect { get; set; } // Null until submitted
    public string? CorrectAnswer { get; set; } = null!;
}
