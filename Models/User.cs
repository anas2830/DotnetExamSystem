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

    [BsonElement("Password")]
    public string Password { get; set; } = null!;

    [BsonElement("Role")]
    public string Role { get; set; } = null!;

    [BsonElement("Mobile")]
    public string? Mobile { get; set; }

    [BsonElement("Address")]
    public string? Address { get; set; }

    [BsonElement("ProfileImagePath")]
    public string? ProfileImagePath { get; set; }
}
