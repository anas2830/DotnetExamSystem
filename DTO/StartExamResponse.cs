namespace DotnetExamSystem.Api.DTO;

public class StartExamResponse
{
    public string ExamId { get; set; }
    public string ExamTitle { get; set; }
    public int DurationMinutes { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}
