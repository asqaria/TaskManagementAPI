using System.Linq.Expressions;
using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync(string? statusFilter);
        Task<TaskEntity> GetAsync(Guid id);
        Task<TaskEntity> GetAsync(string title);
        Task<TaskEntity> CreateAsync(TaskEntity entity);
        Task UpdateAsync(Guid id, string status);
        Task RemoveAsync(Guid id);
    }
}