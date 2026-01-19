using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetExamSystem.Api.Models;

public class Question
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Title")]
    public string Title { get; set; } = null!;

    [BsonElement("Option1")]
    public string Option1 { get; set; } = null!;

    [BsonElement("Option2")]
    public string Option2 { get; set; } = null!;

    [BsonElement("Option3")]
    public string Option3 { get; set; } = null!;

    [BsonElement("Option4")]
    public string Option4 { get; set; } = null!;

    [BsonElement("CorrectAnswer")]
    public string CorrectAnswer { get; set; } = null!;
}
