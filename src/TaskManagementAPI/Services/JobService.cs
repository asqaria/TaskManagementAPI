using Serilog;
using TaskManagementAPI.Repositories;
using TaskManagementAPI.Services.Interface;

namespace TaskManagementAPI.Services {
    public class JobService : IJobService
    {
        private readonly ITaskRepository repository;

        public JobService(ITaskRepository repository){
            this.repository = repository;
        }
        public async Task RunJob()
        {
            Log.Information("Background job: START");
            try {
                int num = await repository.RemoveOldAsync(1);
                Log.Information("Removed " + num + " record(s)");
            } catch (Exception ex) {
                Log.Fatal(ex.Message);
            }

            Log.Information("Background job: END");
        }
    }
}