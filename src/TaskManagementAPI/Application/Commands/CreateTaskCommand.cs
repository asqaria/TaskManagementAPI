using FluentValidation;
using MediatR;
using TaskManagementAPI.Entities;
using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Services.Commands
{
    public record CreateTaskCommand(TaskEntity entity) : IRequest<TaskEntity>;

    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        private readonly ITaskRepository repository;

        public CreateTaskCommandValidator(ITaskRepository repository)
        {
            this.repository = repository;

            RuleFor(x => x.entity.Title)
                .NotEmpty()
                .MustAsync(BeUniqueTitle)
                .WithMessage("Task with the same title already exists")
                .MaximumLength(255);
            RuleFor(x => x.entity.Status)
                .NotEmpty();
        }

        private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            var existingTask = await repository.GetAsync(title);
            return existingTask is null;
        }
    }
}