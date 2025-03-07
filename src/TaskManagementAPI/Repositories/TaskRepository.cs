using Npgsql;
using Dapper;
using TaskManagementAPI.Entities;

namespace TaskManagementAPI.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        private string connectionString;
        private IConfiguration config;

        public TaskRepository(IConfiguration config)
        {
            this.config = config;
            this.connectionString = this.config.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public async Task<TaskEntity> CreateAsync(TaskEntity entity)
        {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = null;
                var query = @"
                            INSERT INTO Task (Title, Description, Status, CreatedAt, UpdatedAt) 
                            VALUES (@Title, @Description, @Status, @CreatedAt, @UpdatedAt) RETURNING Id";

                var createdId = await connection.ExecuteScalarAsync<Guid>(query, entity);
                entity.Id = createdId;
            }

            return entity;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync(string? statusFilter)
        {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                IEnumerable<TaskEntity> result;
                if (string.IsNullOrEmpty(statusFilter))
                {
                    var query = @"SELECT * FROM Task";
                    result = await connection.QueryAsync<TaskEntity>(query);
                } else {
                    var query = @"SELECT * FROM Task WHERE Status = @Status";
                    result = await connection.QueryAsync<TaskEntity>(query, new { Status = statusFilter});
                }
                return result ?? Enumerable.Empty<TaskEntity>();
            }
        }

        public async Task<TaskEntity> GetAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString)){
                connection.Open();
                string query = @"SELECT * FROM Task where Id = @Id";
                var result = await connection.QueryFirstOrDefaultAsync<TaskEntity>(query, new { Id = id });
                return result;
            }
        }

        public async Task<TaskEntity> GetAsync(string title)
        {
            using (var connection = new NpgsqlConnection(connectionString)){
                connection.Open();
                string query = @"SELECT * FROM Task where Title = @Title";
                var result = await connection.QueryFirstOrDefaultAsync<TaskEntity>(query, new { Title = title });
                return result;
            }
        }


        public async Task RemoveAsync(Guid id)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Task WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task UpdateAsync(Guid id, string status)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var updatedAt = DateTime.UtcNow;
                var query = "UPDATE Task SET Status = @Status, UpdatedAt = @UpdatedAt WHERE Id = @Id";
                await connection.ExecuteAsync(query, new { Status = status, Id = id, UpdatedAt = updatedAt });
            }
        }
    }
}