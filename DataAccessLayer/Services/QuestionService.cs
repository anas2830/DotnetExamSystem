using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using DotnetExamSystem.Api.DataAccessLayer.Repositories;
using DotnetExamSystem.Api.Application.Commands;

namespace DotnetExamSystem.Api.DataAccessLayer.Services;

public class QuestionService : IQuestion
{
    private readonly QuestionRepository _questionRepository;

    public QuestionService(QuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Question> CreateAsync(CreateQuestionCommand command)
    {
        var question = new Question
        {
            Title = command.Title,
            Option1 = command.Option1,
            Option2 = command.Option2,
            Option3 = command.Option3,
            Option4 = command.Option4,
            CorrectAnswer = command.CorrectAnswer
        };
        await _questionRepository.CreateAsync(question);
        return question;
    }

    public async Task<Question?> GetByIdAsync(string id) => await _questionRepository.GetByIdAsync(id);

    public async Task<List<Question>> GetAllAsync() => await _questionRepository.GetAllAsync();

    public async Task<bool> UpdateAsync(UpdateQuestionCommand command)
    {
        var question = await _questionRepository.GetByIdAsync(command.Id);
        if (question == null)
            throw new Exception("Question not found");
            
        question.Title = command.Title;
        question.Option1 = command.Option1;
        question.Option2 = command.Option2;
        question.Option3 = command.Option3;
        question.Option4 = command.Option4;
        question.CorrectAnswer = command.CorrectAnswer;
        return await _questionRepository.UpdateAsync(question);
    }

    public async Task<bool> DeleteAsync(string id) => await _questionRepository.DeleteAsync(id);
}
