using System.Text.Json;
using StackExchange.Redis;
using Serilog;
using TaskManagementAPI.Entities;
using TaskManagementAPI.Services.Interface;

namespace TaskManagementAPI.Services
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer redis;
        private readonly IDatabase db;

        public RedisService(IConfiguration config)
        {
            this.redis = ConnectionMultiplexer.Connect(config["Redis:ConnectionString"]);
            this.db = redis.GetDatabase();
        }

        public async Task SetAsync(string key, IEnumerable<TaskEntity> value, TimeSpan? expiry = null)
        {
            try
            {
                var json = JsonSerializer.Serialize(value);
                await db.StringSetAsync(key, json, expiry);
                Log.Information("Successfully added key {Key} to cache with expiry {Expiry}", key, expiry);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding key {Key} to cache", key);
            }
        }

        public async Task<IEnumerable<TaskEntity>?> GetAsync(string key)
        {
            try
            {
                Log.Information("Attempting to retrieve key {Key} from cache", key);
                var json = await db.StringGetAsync(key);
                if (json.HasValue)
                {
                    Log.Information("Successfully retrieved key {Key} from cache", key);
                    return JsonSerializer.Deserialize<IEnumerable<TaskEntity>>(json);
                }
                Log.Warning("Key {Key} not found in cache", key);
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error retrieving key {Key} from cache", key);
            }
            return default;
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                await db.KeyDeleteAsync(key);
                Log.Information("Successfully removed key {Key} from cache", key);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error removing key {Key} from cache", key);
            }
        }
    }
}