using System.Net.Mail;
using System.Net;
using System;
using DUNPLab.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly DunpContext context;
        private readonly ILogger<EmailService> logger;

        public EmailService(DunpContext context, ILogger<EmailService> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task SendEmail()
        {
            Console.WriteLine("Job for sending emails fired!");
            var emails = await context.Emails.Include(e => e.File).Where(e => e.Sent == null).ToListAsync();
            foreach (var email in emails)
            {
                var e = new MailMessage
                {
                    From = new MailAddress(email.From),
                    To = { email.To },
                    Subject = email.Subject,
                    Body = email.Body,
                    IsBodyHtml = false //Set to true if body contains HTML
                };

                if(email.File != null && email.File.Sadrzaj != null )
                {
                    var fileContentStream = new MemoryStream(email.File.Sadrzaj);
                    var attachment = new Attachment(fileContentStream, email.File.Ime);
                    e.Attachments.Add(attachment);
                }


                var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("th091605@gmail.com","password123"),
                    EnableSsl = true,
                    UseDefaultCredentials = true
                };
                try
                {

                    await smtp.SendMailAsync(e);
                    email.Sent = DateTime.Now;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}