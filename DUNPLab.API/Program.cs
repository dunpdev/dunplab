using DUNPLab.API.Infrastructure;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using DUNPLab.API.Jobs;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DunpContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("default")));

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
    config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireServer();
app.UseHangfireDashboard();


RecurringJob.AddOrUpdate<ResultsProcessingJob>(x => x.ProcessResults(), Cron.Daily(13));
RecurringJob.AddOrUpdate<ProcessedFilesRemoverJob>(x => x.DeleteProcessedResults(), Cron.Daily(13, 30));

app.Run();
