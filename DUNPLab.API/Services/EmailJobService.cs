using DUNPLab.API.Infrastructure;
using Hangfire;
using System.Net.Mail;
using System.Net;

namespace DUNPLab.API.Services
{
    public class EmailJobService : IEmailJobService
    {
        private readonly IConfiguration config;
        private readonly DunpContext db;

        public EmailJobService(IConfiguration config, DunpContext db)
        {
            this.config = config;
            this.db = db;
        }

        public void EnqueueEmailJob()
        {
            RecurringJob.AddOrUpdate(() => SendResultsAllPacijents(), "00 12 * * *");
        }

        public void SendEmail(string to, string subject, string body)
        {

            string fromMail = config["Email:FromMail"];
            string fromPassword = config["Email:FromPassword"];

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(to));
            message.Body = body;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);

            Console.WriteLine($"Email sent to: {to}, Subject: {subject}");
        }

        public void SendResultsAllPacijents()
        {

            var rezultatiPacijenti = db.Rezultati
                .Join(
                    db.Uzorci,
                    rezultat => rezultat.IdUzorka,
                    uzorak => uzorak.Id,
                    (rezultat, uzorak) => new { Rezultat = rezultat, Uzorak = uzorak }
                )
                .Join(
                    db.Testiranja,
                    rezultatUzorak => rezultatUzorak.Uzorak.IdTestiranja,
                    testiranje => testiranje.Id,
                    (rezultatUzorak, testiranje) => new { RezultatUzorak = rezultatUzorak, Testiranje = testiranje }
                )
                .Join(
                    db.Pacijenti,
                    joined => joined.Testiranje.TestOdradio,
                    pacijent => pacijent.Ime,
                    (joined, pacijent) => new { RezultatUzorak = joined.RezultatUzorak, Testiranje = joined.Testiranje, Pacijent = pacijent }
                )
                .Join(
                    db.Supstance,
                    joined => joined.RezultatUzorak.Rezultat.IdSupstance,
                    supstanca => supstanca.Id,
                    (joined, supstanca) => new { RezultatUzorak = joined.RezultatUzorak, Testiranje = joined.Testiranje, Pacijent = joined.Pacijent, Supstanca = supstanca }
                )
                .GroupBy(joined => new { joined.Pacijent.Ime, joined.Pacijent.Email })
                .Select(group => new
                {
                    NazivPacijenta = group.Key.Ime,
                    EmailPacijenta = group.Key.Email,

                    Rezultati = group.Select(joined => new
                    {
                        RezultatUGranicama = joined.RezultatUzorak.Rezultat.JeLiUGranicama,
                        RezultatVrednost = joined.RezultatUzorak.Rezultat.Vrednost,

                        Metodtestiranja = joined.RezultatUzorak.Uzorak.MetodTestiranja,
                        Konacanrezultat = joined.RezultatUzorak.Uzorak.KonacanRezultat,
                        Komentar = joined.RezultatUzorak.Uzorak.Komentar,
                        Cena = joined.RezultatUzorak.Uzorak.Cena,

                        NazivSupstane = joined.Supstanca.Naziv,
                        OpisSupstance = joined.Supstanca.Opis,
                        TipSupstance = joined.Supstanca.Tip,
                        DonjaGranicaSupstance = joined.Supstanca.DonjaGranica,
                        GornjaGranicaSupstance = joined.Supstanca.GornjaGranica,
                        MetodTestiranjaSupstance = joined.Supstanca.MetodTestiranja,
                    }).ToList()
                })
                .ToList();

            foreach (var pacijent in rezultatiPacijenti)
            {
                var body = "";

                if (pacijent.Rezultati.All(r => r.RezultatUGranicama == true))
                {
                    body = $@"<p>Pozdrav, {pacijent.NazivPacijenta}. Svi rezultati su negativni. Ne morate ići kod lekara.</p>";
                }

                else
                {
                    body = $@"
                    <html>
                       <head>
                          <p>Pozdrav, {pacijent.NazivPacijenta}. Bar jedan rezultat je pozitivan. Potrebno je posetiti lekara.</p>
                       <style>
                    table {{
                       font-family: Arial, sans-serif;
                       border-collapse: collapse;
                       width: 100%;
                    }}
                       th, td {{
                       border: 1px solid #dddddd;
                       text-align: left;
                       padding: 8px;
                    }}
                    th {{
                       background-color: #f2f2f2;
                    }}
                    </style>
                    </head>
                    <body>
                    <table>
                       <tr>
                          <th>Metod testiranja</th>
                          <th>Vrednost</th>
                          <th>Komentar</th>
                          <th>Cena</th> 
                          <th>Naziv supstance</th>
                          <th>Opis supstance</th>
                          <th>Tip supstance</th>
                          <th>Donja granica supstance</th>
                          <th>Gornja granica supstance</th>
                          <th>Metod testiranja supstance</th>
                      </tr>";

                    foreach (var rezultat in pacijent.Rezultati)
                    {
                        body += $@"
                        <tr>
                          <td>{rezultat.Metodtestiranja}</td
                          <td>{rezultat.RezultatVrednost}</td>
                          <td>{rezultat.Komentar}</td>
                          <td>{rezultat.Cena}</td>
                          <td>{rezultat.NazivSupstane}</td>
                          <td>{rezultat.OpisSupstance}</td>
                          <td>{rezultat.TipSupstance}</td>
                          <td>{rezultat.DonjaGranicaSupstance}</td>
                          <td>{rezultat.GornjaGranicaSupstance}</td>
                          <td>{rezultat.MetodTestiranjaSupstance}</td>
                        </tr>";
                    }

                    body += @"
                    </table>
                    </body>
                    </html>";
                }

                SendEmail(pacijent.EmailPacijenta, "Rezultati testiranja", body);
            }
        }
    }
}