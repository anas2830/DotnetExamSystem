using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetExamSystem.Api.Models;

public class Exam
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public int TimeInMinutes { get; set; }
    public int TotalQuestions { get; set; }
}
