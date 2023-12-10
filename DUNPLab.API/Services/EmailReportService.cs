using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Services
{
    public class EmailReportService : IEmailReportService
    {
        private readonly DunpContext _context;
        public EmailReportService(DunpContext context)
        {
            _context = context;
        }

        public async Task SavePatientEmail()
        {
            var reports = await _context.Reports.Where(x => x.Pacijent.Email != null).Include(x => x.Pacijent).Include(x => x.File).ToListAsync();
            foreach (var report in reports)
            {

                if (!_context.Emails.Any(e => e.To == report.Pacijent.Email))
                {
                    var email = new Email
                    {
                        To = report.Pacijent.Email,
                        Subject = "Rezultati testiranja",
                        Body = "Poštovani, u prilogu se nalaze rezultati testiranja.",
                        From = "edinakucevic26@gmail.com",
                        Sent = null,
                        IdFile = report.IdFile

                    };
                    _context.Emails.Add(email);
                    await _context.SaveChangesAsync();
                }

            }
        }

        public async Task SendEmailReport()
        {


            var emails = await _context.Emails.Where(x => x.Sent == null).Include(x => x.File).ToListAsync();
            foreach (var email in emails)
            {
                var e = new MailMessage
                {
                    From = new MailAddress(email.From),
                    To = { email.To },
                    Subject = email.Subject,
                    Body = email.Body
                };
                if (email.File != null && email.File.Sadrzaj != null)
                {
                    //MemoryStream je koristan kada želite raditi sa bajtovima u memoriji,
                    //kao što je slučaj kada imate niz bajtova koji predstavljaju podatke koje želite manipulisati ili koristiti u nekom drugom kontekstu
                    MemoryStream stream = new MemoryStream(email.File.Sadrzaj);
                    e.Attachments.Add(new Attachment(stream, email.File.Ime + ".pdf", "application/pdf"));
                }

                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("edinakucevic26@gmail.com", "hllseqzxdcakuxni"),
                    EnableSsl = true
                };
                await smtp.SendMailAsync(e);
                email.Sent = DateTime.Now;

            }
            await _context.SaveChangesAsync();
        }
    }
}
