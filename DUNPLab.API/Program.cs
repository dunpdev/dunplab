using DUNPLab.API.Infrastructure;
using DUNPLab.API.Jobs.PacijentiJobs;
using DUNPLab.API.Services.Pacijenti;
using DUNPLab.API.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
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
builder.Services.AddScoped<IPacijentiService, PacijentiService>();
/*builder.Services.AddScoped<IPacijentiJobRegistrator, PacijentiJobRegistrator>(); Comment out job registrator  */
builder.Services.AddHangfireServer();
builder.Services.AddTransient<ITransferRezultati, TransferRezultati>();

builder.Services.AddTransient<IOdredjivanjeStatusa, OdredjivanjeStatusa>();
builder.Services.AddTransient<IArhivirajPacijenteService, ArhivirajPacijenteService>();

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

RecurringJob.AddOrUpdate<IOdredjivanjeStatusa>("odredjivanje-statusa", service => service.Odredi(), "*/5 * * * *");

RecurringJob.AddOrUpdate<FileBackupService>(x => x.BackupFiles(), Cron.MinuteInterval(2));

RecurringJob.AddOrUpdate<ITransferRezultati>("transfer-rezultata", service => service.Transfer(), "*/5 * * * *");
RecurringJob.AddOrUpdate<IPacijentiService>("VahidovJob", service=>service.Seed(), "0 0 * * 0");

RecurringJob.AddOrUpdate<ResultsProcessingJob>(x => x.ProcessResults(), Cron.Daily(13));
RecurringJob.AddOrUpdate<IArhivirajPacijenteService>("MuhamedovJob", x => x.ArhivirajPacijente(), Cron.Daily(12));

app.Run();
