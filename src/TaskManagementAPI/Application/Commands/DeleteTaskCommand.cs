using MediatR;
using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Services.Commands
{
    public record DeleteTaskCommand(Guid id) : IRequest;
}