using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.DataAccessLayer.Repositories;

namespace DotnetExamSystem.Api.DataAccessLayer.Services;

public class QuestionService : IQuestion
{
    private readonly QuestionRepository _questionRepository;

    public QuestionService(QuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Question> CreateAsync(Question question)
    {
        await _questionRepository.CreateAsync(question);
        return question;
    }

    public async Task<Question?> GetByIdAsync(string id) => await _questionRepository.GetByIdAsync(id);

    public async Task<List<Question>> GetAllAsync() => await _questionRepository.GetAllAsync();

    public async Task<bool> UpdateAsync(Question question) => await _questionRepository.UpdateAsync(question);

    public async Task<bool> DeleteAsync(string id) => await _questionRepository.DeleteAsync(id);
}
