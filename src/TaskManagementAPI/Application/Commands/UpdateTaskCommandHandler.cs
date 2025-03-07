using MediatR;
using Serilog;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services.Commands
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly ITaskRepository repository;

        public UpdateTaskCommandHandler(ITaskRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Execute: UpdateTaskCommandHandler.Handle");
            
            if (string.IsNullOrEmpty(request.status))
            {
                throw new ArgumentNullException(nameof(request.status));
            }
            await repository.UpdateAsync(request.id, request.status);
        }
    }
}