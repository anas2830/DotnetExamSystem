using DotnetExamSystem.Api.DataAccessLayer.Interfaces;
using DotnetExamSystem.Api.Models;
using MediatR;

public class BuyExamCommandHandler : IRequestHandler<BuyExamCommand, UserExam>
{
    private readonly IUserExam _service;
    public BuyExamCommandHandler(IUserExam service) => _service = service;

    public async Task<UserExam> Handle(BuyExamCommand request, CancellationToken cancellationToken)
        => await _service.BuyExamAsync(request.UserId, request.ExamId);
}

public class StartExamCommandHandler : IRequestHandler<StartExamCommand, UserExam>
{
    private readonly IUserExam _service;
    public StartExamCommandHandler(IUserExam service) => _service = service;

    public async Task<UserExam> Handle(StartExamCommand request, CancellationToken cancellationToken)
        => await _service.StartExamAsync(request.UserId, request.ExamId);
}

public class SubmitExamCommandHandler : IRequestHandler<SubmitExamCommand, UserExam>
{
    private readonly IUserExam _service;
    public SubmitExamCommandHandler(IUserExam service) => _service = service;

    public async Task<UserExam> Handle(SubmitExamCommand request, CancellationToken cancellationToken)
        => await _service.SubmitExamAsync(request.UserId, request.ExamId, request.Answers);
}
