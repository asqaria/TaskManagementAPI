using MediatR;
using Serilog;
using TaskManagementAPI.Entities;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services.Queries
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskEntity>>
    {
        private readonly ITaskRepository repository;

        public GetTasksQueryHandler(ITaskRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<TaskEntity>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Execute: GetTasksQueryHandler.Handle");
            return await repository.GetAllAsync(request.statusFilter);
        }
    }
}