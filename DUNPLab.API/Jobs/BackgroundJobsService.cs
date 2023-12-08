
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

    public async Task SendEmail()
    {
        var emails = await context.EmailNotifications.Where(x => x.TimeOfSending.Date == DateTime.Now.Date).ToListAsync();
        var patients = await context.Pacijenti.ToListAsync();

        foreach (var email in emails)
        {
            foreach (var patient in patients)
            {
                var e = new MailMessage
                {
                    From = new MailAddress(email.From),
                    Subject = email.Subject,
                    Body = email.Message,
                    To = { patient.Email }
                };
                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(
                    configuration?.Email,
                    configuration?.Password)
                };
                smtp.Send(e);
            }
        }

        context.EmailNotifications.RemoveRange(emails);
        await context.SaveChangesAsync();
    }
}
