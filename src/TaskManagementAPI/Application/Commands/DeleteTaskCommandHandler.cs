using MediatR;
using Serilog;
using TaskManagementAPI.Entities;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services.Commands
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository repository;

        public DeleteTaskCommandHandler(ITaskRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Execute: DeleteTaskCommandHandler.Handle");
            await repository.RemoveAsync(request.id);
        }
    }
}