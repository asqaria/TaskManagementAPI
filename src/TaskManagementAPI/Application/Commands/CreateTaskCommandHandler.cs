using FluentValidation;
using MediatR;
using Serilog;
using TaskManagementAPI.Entities;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services.Commands
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskEntity>
    {
        private readonly ITaskRepository repository;

        public CreateTaskCommandHandler(ITaskRepository repository)
        {
            this.repository = repository;
        }

        public async Task<TaskEntity> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Execute: CreateTaskCommandHandler.Handle");

            CreateTaskCommandValidator validator = new(repository);
            await validator.ValidateAndThrowAsync(request);
            return await repository.CreateAsync(request.entity);
        }
    }
}