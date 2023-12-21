using DUNPLab.API.Models;

namespace DUNPLab.API.Services.ZahtevZaTestiranje
{
    public interface IZahtevZaTestiranje
    {
        Task<List<Pacijent>> GetRandom100PatientsAsync();
        Task ScheduleTestsAsync();
    }
}
