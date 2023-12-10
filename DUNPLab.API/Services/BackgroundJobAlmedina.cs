using DUNPLab.API.Infrastructure;
using System.Net.Mail;
using System.Net;

namespace DUNPLab.API.Services
{
	public class BackgroundJobAlmedina
	{
		private readonly DunpContext dbContext;

		public BackgroundJobAlmedina(DunpContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void SendEmailJob()
		{

			var danas = DateTime.Today;
			var datumIstekaDokumenta = danas.AddDays(7);

			var pacijenti = dbContext.Pacijenti
				.Where(p => p.DatumIstekaDokumenta == datumIstekaDokumenta)
				.ToList();

			foreach (var pacijent in pacijenti)
			{
				SendEmail(pacijent.Email, "Upozorenje: Isticanje dokumenata", "Za 7 dana vam ističu dokumenti. Molimo vas da ih produžite.");
			}
		}

		public void SendEmail(string to, string subject, string body)
		{
			string fromMail = "almedinasi00@gmail.com";
			string fromPassword = "jbma jocp rsxu fwcp";

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

			Console.WriteLine($"Email sent to: {to}");

		}

	}
}
