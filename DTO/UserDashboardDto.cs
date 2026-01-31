using System;
using System.Collections.Generic;

namespace DotnetExamSystem.Api.DTO
{
    public class UserDashboardDto
    {
        public int TotalPurchased { get; set; }
        public int TotalSubmitted { get; set; }
        public int RemainingExams { get; set; }
        public List<UserExamDto> Exams { get; set; } = new List<UserExamDto>();
    }

    public class UserExamDto
    {
        public string ExamId { get; set; } = null!;
        public string UserExamId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Status { get; set; } = "Booked";
        public decimal Price { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime Date { get; set; }
    }
}

