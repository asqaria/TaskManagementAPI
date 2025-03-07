using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Services.Interface
{
    public interface IRedisService
    {
        Task SetAsync(string key, IEnumerable<TaskEntity> value, TimeSpan? expiry = null);
        Task<IEnumerable<TaskEntity>?> GetAsync(string key);
    }
}