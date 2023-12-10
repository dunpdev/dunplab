
using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using DUNPLab.API.Services.Pacijenti;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace DUNPLab.API.Services.Mail
{
    public class MailService : IMailService
    {
        public IPacijentiService _pacijentService { get; set; }
        public  DunpContext _context { get; set; }
        public MailService(IPacijentiService pacijentService,DunpContext context)
        {
            this._pacijentService = pacijentService;

            this._context = context;
        }
        public async Task Sendmail(string emailPacijenta)
        {
            string senderEmail = "dzexi3@gmail.com";
            string senderPassword = "fdgkuycbocejqqkw";
            // Create and configure the SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;
            // Recipient's email address

            // Create a new MailMessage
            MailMessage mail = new MailMessage(senderEmail, emailPacijenta);
            // Set the subject and body of the email
            mail.Subject = "Termin pregleda!";
            mail.Body = "Za pet minuta vam je termin koji ste zakazali.";

            try
            {
                // Send the email
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task GetZahteveZaObavestenja()
        {
            //await Sendmail("vakson12@gmail.com");  //Test funkcije za slanje mejlova
            var result = await _context.Zahtevi.Where(zaht => zaht.JeLiObradjen == true).Include(zaht => zaht.Testiranje).Include(zaht=>zaht.Pacijent).ToListAsync();

            DateTime inFiveMinutes = DateTime.UtcNow.AddMinutes(5);
            if (result.Count > 0)
            {
                foreach (var zaht in result)
                {
                    if (zaht.Testiranje.DatumVremeTestiranja == inFiveMinutes) //Provera da li je pregled za 5 minuta
                    {
                        await Sendmail(zaht.Pacijent.Email);  //AKo jeste saljemo mejl
                    }
                }
            }

        }
    }
}
