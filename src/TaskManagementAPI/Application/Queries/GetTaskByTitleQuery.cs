using MediatR;
using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Services.Queries
{
    public record GetTaskByTitleQuery(string Title) : IRequest<TaskEntity>;
}