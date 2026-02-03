namespace DotnetExamSystem.Api.DTO;

public class UserFromAdminPanelDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? Mobile { get; set; }
    public string? Address { get; set; }
    public decimal Balance { get; set; }
    public string? ProfileImagePath { get; set; }
    public int TotalPurchaseExams { get; set; }
    public int TotalSubmittedExams { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}