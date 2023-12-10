using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using Hangfire;

namespace DUNPLab.API.Services
{
    public class ZahtevBackgroundService
    {
        private readonly DunpContext _dbContext;

        public ZahtevBackgroundService(DunpContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ScheduleZahtevCreation()
        {
            RecurringJob.AddOrUpdate(() => CreateTestRequests(1), Cron.Daily(12, 0)); // Postavljanje da se posao pokreće svaku noć u 12:00
        }

        public async Task CreateTestRequests(int roomNumber)
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);

            var newRequest = new Zahtev
            {
                DatumTestiranja = tomorrow,
                Metode = new List<string> { "Urin", "Krv" } // Primer metoda testiranja
            };

            _dbContext.Zahtevi.Add(newRequest); // Dodaj zahtev u DbContext
            await _dbContext.SaveChangesAsync(); // Čuvanje promena u bazi

            // Simulacija vremena trajanja testiranja (5 minuta)
            await Task.Delay(TimeSpan.FromMinutes(5));

            roomNumber++;
            if (roomNumber <= 4)
            {
                BackgroundJob.Schedule(() => CreateTestRequests(roomNumber), TimeSpan.Zero);
            }
        }
    }
}
