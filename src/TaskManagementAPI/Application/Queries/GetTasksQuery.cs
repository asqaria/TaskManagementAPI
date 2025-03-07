using MediatR;
using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Services.Queries
{
    public record GetTasksQuery(string? statusFilter) : IRequest<IEnumerable<TaskEntity>>;
}