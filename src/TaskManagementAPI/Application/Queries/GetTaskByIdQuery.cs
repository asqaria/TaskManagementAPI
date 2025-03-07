using MediatR;
using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Services.Queries
{
    public record GetTaskByIdQuery(Guid Id) : IRequest<TaskEntity>;
}