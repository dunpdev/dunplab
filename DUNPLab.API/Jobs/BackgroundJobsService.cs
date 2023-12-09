
using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace DUNPLab.API.Jobs;

public class BackgroundJobsService : IBackgroundJobsService
{
    private readonly DunpContext context;
    private readonly GmailCredentials configuration;

    public BackgroundJobsService(DunpContext context, IOptions<GmailCredentials> configuration)
    {
        this.context = context;
        this.configuration = configuration.Value;
    }

    public async Task PrepareEmail()
    {
        var emails = await context.EmailNotifications.Where(x => x.TimeOfSending.Date == DateTime.Now.Date).ToListAsync();
        var patients = await context.Pacijenti.ToListAsync();

        foreach (var email in emails)
        {
            foreach (var patient in patients)
            {
                var e = new Email
                {
                    To = patient.Email,
                    Subject = email.Subject,
                    Body = email.Message,
                    From = configuration.Email,
                    Sent = null
                };
                context.Emails.Add(e);
            }
        }

        await context.SaveChangesAsync();
    }
}
