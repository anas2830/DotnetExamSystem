using MediatR;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetAdminDashboardQuery : IRequest<AdminDashboardDto> { }
