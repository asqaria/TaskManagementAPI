using System.Text;
using Serilog;
using TaskManagementAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TaskManagementAPI.Services;
using Hangfire;
using Hangfire.PostgreSql;
using TaskManagementAPI.Services.Interface;
using TaskManagementAPI.Hubs;
using TaskManagementAPI.Settings;
using TaskManagementAPI;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

builder.Services.AddHangfire(config => config
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(connectionString.HangfireConnection));
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IJobService, JobService>();

builder.Services.AddControllers(
    options =>
    {
        options.SuppressAsyncSuffixInActionNames = false;
    }
);

var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        }
    );
});

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRedisService, RedisService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddSingleton<RabbitMQService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHangfireDashboard();
var bgJobs = builder.Configuration.GetSection(nameof(BackgroundJobSettings)).Get<List<BackgroundJobSettings>>();
foreach (var bgJob in bgJobs){
    RecurringJob.AddOrUpdate<IJobService>(
        bgJob.Name,
        job => job.RunJob(),
        bgJob.Time
    );
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var hubSettings = builder.Configuration.GetSection(nameof(HubSettings)).Get<HubSettings>();

app.UseCors();
app.MapHub<TaskHub>(hubSettings.TaskHub);

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
