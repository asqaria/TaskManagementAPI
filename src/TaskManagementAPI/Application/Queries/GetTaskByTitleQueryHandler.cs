using MediatR;
using Serilog;
using TaskManagementAPI.Entities;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services.Queries
{
    public class GetTaskByTitleQueryHandler : IRequestHandler<GetTaskByTitleQuery, TaskEntity>
    {
        private readonly ITaskRepository repository;

        public GetTaskByTitleQueryHandler(ITaskRepository repository)
        {
            this.repository = repository;
        }

        public async Task<TaskEntity> Handle(GetTaskByTitleQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Execute: GetTaskByTitleQueryHandler.Handle");
            return await repository.GetAsync(request.Title);
        }
    }
}