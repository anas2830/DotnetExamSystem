namespace DotnetExamSystem.Api.DTO;


public class QuestionDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public List<OptionDto> Options { get; set; } = new();
}
