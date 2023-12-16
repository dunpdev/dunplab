using DUNPLab.API.Infrastructure;
using DUNPLab.API.Services;
using DUNPLab.API.Jobs;
using DUNPLab.API.Models;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using DUNPLab.API.Services.Pacijenti;
using DUNPLab.API.Jobs;
using Microsoft.Extensions.Options;
using DUNPLab.API.Services.Mail;
using DUNPLab.API.Jobs.ZahtevZaTestiranjeJobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DunpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddScoped<IEmailJobService, EmailJobService>();


builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer();
    config.UseRecommendedSerializerSettings();
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("default"));
});
builder.Services.AddHangfireServer();
builder.Services.AddTransient<ITransferRezultati, TransferRezultati>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddHangfireServer();
builder.Services.AddScoped<IBackgroundJobsService, BackgroundJobsService>();

builder.Services.AddScoped<IBackgroundJobsService_ZahtevZaTestiranje, BackgroundJobsService_ZahtevZaTestiranje>();

builder.Services.AddTransient<IOdredjivanjeStatusa, OdredjivanjeStatusa>();
builder.Services.AddTransient<IMailService,MailService>(); //registrujemo mail service
builder.Services.AddTransient<IArhivirajPacijenteService, ArhivirajPacijenteService>();
builder.Services.AddTransient<IEmailReportService, EmailReportService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IBackgroundJobsServiceHalida, BackgroundJobsServiceHalida>();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped<IPacijentiService, PacijentiService>();
builder.Services.AddTransient<IReportSupstancaService, ReportSupstancaService>();



builder.Services.Configure<GmailCredentials>(
    builder.Configuration.GetSection("GmailCredentials"));


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
RecurringJob.AddOrUpdate<IEmailService>(x => x.SendEmail(), "*/15 * * * *");

RecurringJob.AddOrUpdate<ITransferRezultati>("transfer-rezultata", service => service.Transfer(), "*/5 * * * *");
RecurringJob.AddOrUpdate<IPacijentiService>("VahidovJob", service => service.Seed(), "0 0 * * 0");

RecurringJob.AddOrUpdate<ResultsProcessingJob>(x => x.ProcessResults(), Cron.Daily(13));
RecurringJob.AddOrUpdate<IArhivirajPacijenteService>("MuhamedovJob", x => x.ArhivirajPacijente(), Cron.Daily(12));
RecurringJob.AddOrUpdate<ProcessedFilesRemoverJob>(x => x.DeleteProcessedResults(), Cron.Daily(13, 30));
RecurringJob.AddOrUpdate<IBackgroundJobsServiceHalida>(x => x.PrepareEmail(), Cron.Daily(16));

RecurringJob.AddOrUpdate<IMailService>("EmailObavestenja",service => service.GetZahteveZaObavestenja(),"*/2 * * * *");

RecurringJob.AddOrUpdate<ProcessedRequestRemover>(x => x.RemoveProcessedRequests(), Cron.Hourly);
RecurringJob.AddOrUpdate<IReportSupstancaService>("generate-reports",
    service => service.GeneratePdfReport(),
    "0 14 * * *");  // Ovde postavljate vreme u formatu (sat, minut)

RecurringJob.AddOrUpdate<IBackgroundJobsService_ZahtevZaTestiranje>("KreiranjeZahtevaZaTestiranje",
    x => x.CreateTestRequestsForTomorrow(), "0 0 * * *"); // Ovo postavlja zadatak da se izvršava svaki dan u ponoć


// Configure Hangfire jobs from the service
//var emailJobService = app.Services.GetRequiredService<IEmailJobService>();emailJobService.EnqueueEmailJob();

app.Run();
