using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetExamSystem.Api.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = null!;

    [BsonElement("Email")]
    public string Email { get; set; } = null!;

    [BsonElement("PasswordHash")]
    public string PasswordHash { get; set; } = null!;

    [BsonElement("Role")]
    public string Role { get; set; } = null!;
}
