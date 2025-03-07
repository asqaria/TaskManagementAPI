using MediatR;
using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Services.Commands
{
    public record UpdateTaskCommand(Guid id, string status) : IRequest;
}