using TaskManagementAPI.Dtos;
using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Extensions
{
    public static class Extensions
    {
        public static TaskDto asDto(this TaskEntity task)
        {
            return new TaskDto
            (
                task.Id,
                task.Title,
                task.Description,
                task.Status
            );
        }
    }
}