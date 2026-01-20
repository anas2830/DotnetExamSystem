namespace DotnetExamSystem.Api.DTO
{
    public class ExamUserDto
    {
        public string UserExamId { get; set; } = null!;
        public UserDto User { get; set; } = null!;
        public DateTime BookedAt { get; set; }
        public string Status { get; set; } = null!;
        public decimal AmountPaid { get; set; }
    }
}
