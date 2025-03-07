using TaskManagementAPI.Repositories;

namespace TaskManagementAPI.Entities
{
    public class TaskEntity : IEntity
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Status { get; set; } // ToDo, InProgress, Done
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}