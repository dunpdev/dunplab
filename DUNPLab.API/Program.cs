using DUNPLab.API.Infrastructure;
using DUNPLab.API.Jobs.PacijentiJobs;
using DUNPLab.API.Services.Pacijenti;
using Hangfire;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddScoped<IPacijentiJobRegistrator, PacijentiJobRegistrator>();
builder.Services.AddHangfireServer((provider, serverOptions) =>
{
    using (var serviceScope = provider.CreateScope())
    {

        var pacijentiRegistrator = serviceScope.ServiceProvider.GetRequiredService<IPacijentiJobRegistrator>();
        pacijentiRegistrator.Register();
    }
});
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



app.Run();
