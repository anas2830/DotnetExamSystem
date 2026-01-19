using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetExamSystem.Api.Models;

public class Question
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    [BsonElement("Title")]
    public string Title { get; set; } = null!;

    public Dictionary<string, string> Options { get; set; } = new();

    [BsonElement("CorrectAnswer")]
    public string CorrectAnswer { get; set; } = null!;
}
