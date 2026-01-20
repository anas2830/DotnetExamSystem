using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.DTO;

public class UserExamSummaryDto
{
    public string Id { get; set; } = null!;
    public DateTime BookedAt { get; set; }
    public string Status { get; set; } = null!;
    public int Score { get; set; }
    public decimal AmountPaid { get; set; }

    public UserDto User { get; set; } = null!;
    public ExamDto Exam { get; set; } = null!;
    public List<UserExamQuestionDto> Questions { get; set; } = new();
}

public class UserDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Mobile { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal Balance { get; set; }
    public string? ProfileImagePath { get; set; }
}

public class ExamDto
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime Date { get; set; }
    public int TotalQuestions { get; set; }
    public int TimeInMinutes { get; set; }
    public decimal Price { get; set; }
}

public class UserExamQuestionDto
{
    public Question Question { get; set; } = null!;
    public UserExamAnswer Answer { get; set; } = null!;
}
