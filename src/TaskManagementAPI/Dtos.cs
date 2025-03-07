using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Dtos
{
    public record TaskDto(
        Guid Id,
        string Title,
        string? Description,
        string Status
    );

    public record CreateTaskDto(
        string Title,
        string? Description,
        string Status
    );
}