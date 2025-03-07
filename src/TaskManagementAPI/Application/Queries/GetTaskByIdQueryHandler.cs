using MediatR;
using Serilog;
using TaskManagementAPI.Entities;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services.Queries
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskEntity>
    {
        private readonly ITaskRepository repository;

        public GetTaskByIdQueryHandler(ITaskRepository repository)
        {
            this.repository = repository;
        }

        public async Task<TaskEntity> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Execute: GetTaskByIdQueryHandler.Handle");
            return await repository.GetAsync(request.Id);
        }
    }
}