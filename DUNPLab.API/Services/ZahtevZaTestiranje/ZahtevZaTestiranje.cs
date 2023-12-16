using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Services.ZahtevZaTestiranje
{
    public class ZahtevZaTestiranje : IZahtevZaTestiranje
    {
        private readonly DunpContext _context;

        public ZahtevZaTestiranje(DunpContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<Pacijent>> GetRandom100PatientsAsync()
        {
            var allPatientIds = await _context.Pacijenti.Select(p => p.Id).ToListAsync();
            var shuffledIds = allPatientIds.OrderBy(x => Guid.NewGuid()).Take(100).ToList();

            var randomPatients = await _context.Pacijenti
                                        .Where(p => shuffledIds.Contains(p.Id))
                                        .ToListAsync();

            return randomPatients;
        }

        public async Task ScheduleTestsAsync()
        {
            try
            {
                var randomPatients = await GetRandom100PatientsAsync();
                var supstanceIzBaze = await _context.Supstance.ToListAsync();

                DateTime startDateTime = DateTime.Today.AddDays(1).Date.AddHours(9);
                TimeSpan timeSlot = TimeSpan.FromMinutes(5);
                int brojProstorija = 4;
                int pacijentCount = randomPatients.Count;
                int index = 0;

                while (index < pacijentCount)
                {
                    for (int i = 0; i < brojProstorija && index < pacijentCount; i++)
                    {
                        Zahtev zahtev = new Zahtev
                        {
                            DatumTestiranja = startDateTime,
                            PacijentId = randomPatients[index].Id,
                            Metode = new List<string> { "Urin", "Krv" },
                            JeLiObradjen = false,
                            ZahtevSupstance = new List<ZahtevSubstanca>()
                        };

                        foreach (var supstanca in supstanceIzBaze)
                        {
                            var novaVeza = new ZahtevSubstanca
                            {
                                Zahtev = zahtev,
                                Supstanca = supstanca
                            };
                            zahtev.ZahtevSupstance.Add(novaVeza);
                        }

                        _context.Zahtevi.Add(zahtev);
                        index++;
                        startDateTime = startDateTime.Add(timeSlot);
                    }
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška pri kreiranju zahteva: {ex.Message}");
            }
        }
    }
}
