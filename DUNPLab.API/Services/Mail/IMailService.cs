using DUNPLab.API.Models;

namespace DUNPLab.API.Services.Mail
{
    public interface IMailService
    {
        Task Sendmail(string emailPacijenta);

        Task GetZahteveZaObavestenja();
    }
}
