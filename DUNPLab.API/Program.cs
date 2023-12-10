using DUNPLab.API.Infrastructure;
using DUNPLab.API.Services;
using Hangfire;
using Hangfire.Server;
using Microsoft.EntityFrameworkCore;
using DUNPLab.API.Jobs;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DunpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddHangfireServer();
builder.Services.AddTransient<ITransferRezultati, TransferRezultati>();

builder.Services.AddTransient<IOdredjivanjeStatusa, OdredjivanjeStatusa>();
builder.Services.AddTransient<IArhivirajPacijenteService, ArhivirajPacijenteService>();
builder.Services.AddTransient<IEmailReportService, EmailReportService>();

builder.Services.AddTransient<IReportSupstancaService, ReportSupstancaService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IBackgroundJobsService, BackgroundJobsService>();
builder.Services.AddAutoMapper(typeof(MappingProfiles));

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

// Add this line to schedule your job
RecurringJob.AddOrUpdate<IBackgroundJobsService>(x => x.Rezultati(), "0 */10 * * * *");
RecurringJob.AddOrUpdate<IOdredjivanjeStatusa>("odredjivanje-statusa", service => service.Odredi(), "*/5 * * * *");

RecurringJob.AddOrUpdate<FileBackupService>(x => x.BackupFiles(), Cron.MinuteInterval(2));

RecurringJob.AddOrUpdate<ITransferRezultati>("transfer-rezultata", service => service.Transfer(), "*/5 * * * *");
RecurringJob.AddOrUpdate<IPacijentiService>("VahidovJob", service=>service.Seed(), "0 0 * * 0");

RecurringJob.AddOrUpdate<ResultsProcessingJob>(x => x.ProcessResults(), Cron.Daily(13));
RecurringJob.AddOrUpdate<IArhivirajPacijenteService>("MuhamedovJob", x => x.ArhivirajPacijente(), Cron.Daily(12));
RecurringJob.AddOrUpdate<ProcessedFilesRemoverJob>(x => x.DeleteProcessedResults(), Cron.Daily(13, 30));

RecurringJob.AddOrUpdate<DUNPLab.API.Services.BackgroundJobAlmedina>(x => x.SendEmailJob(), Cron.Daily);

app.Run();
