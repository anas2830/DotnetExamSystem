using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

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

    [NotMapped]
    public int? AlreadyPurchase { get; set; } = null;

    [NotMapped]
    public string? Status { get; set; } = null;

    [NotMapped]
    public string? UserExamId { get; set; } = null;
}
