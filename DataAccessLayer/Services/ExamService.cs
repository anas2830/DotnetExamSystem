using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.Application.Commands;
using DotnetExamSystem.Api.DataAccessLayer.Repositories;

namespace DotnetExamSystem.Api.DataAccessLayer.Services;

public class ExamService : IExam
{
    private readonly ExamRepository _examRepository;
    private readonly UserExamRepository _userExamRepository;

    public ExamService(ExamRepository examRepository, UserExamRepository userExamRepository)
    {
        _examRepository = examRepository;
        _userExamRepository = userExamRepository;
    }

    public async Task<Exam> CreateAsync(CreateExamCommand command){
        var exam = new Exam{
            Title = command.Title,
            Date = command.Date,
            Price = command.Price,
            TimeInMinutes = command.TimeInMinutes,
            TotalQuestions = command.TotalQuestions
        };
        await _examRepository.CreateAsync(exam);
        return exam;
    }

    public async Task<Exam?> GetByIdAsync(string id){
        return await _examRepository.GetByIdAsync(id);
    }

    public async Task<List<Exam>> GetAllAsync(string userId, string role){
        return await _examRepository.GetAllAsync(userId, role);
    }

    public async Task<bool> UpdateAsync(UpdateExamCommand command){
        var exam = await _examRepository.GetByIdAsync(command.Id);
        if (exam == null) throw new Exception("Exam not found");

        exam.Title = command.Title ?? exam.Title;
        exam.Date = command.Date ?? exam.Date;
        exam.Price = command.Price ?? exam.Price;
        exam.TimeInMinutes = command.TimeInMinutes ?? exam.TimeInMinutes;
        exam.TotalQuestions = command.TotalQuestions ?? exam.TotalQuestions;
        return await _examRepository.UpdateAsync(exam);
    }

    public async Task<bool> DeleteAsync(string id){
        var exam = await _examRepository.GetByIdAsync(id);
        if (exam == null) throw new Exception("Exam not found");
        var anyUserExam = await _userExamRepository.ExistsAsync(ue => ue.ExamId == id);
        if (anyUserExam) throw new Exception("Some users have bought this exam");
        return await _examRepository.DeleteAsync(id);
    }
}