using DUNPLab.API.Services.ZahtevZaTestiranje;

namespace DUNPLab.API.Jobs.ZahtevZaTestiranjeJobs
{
    public class BackgroundJobsService_ZahtevZaTestiranje : IBackgroundJobsService_ZahtevZaTestiranje
    {
        private readonly IZahtevZaTestiranje _zahtevZaTestiranje;

        public BackgroundJobsService_ZahtevZaTestiranje(IZahtevZaTestiranje zahtevZaTestiranje)
        {
            _zahtevZaTestiranje = zahtevZaTestiranje;
        }

        public async Task CreateTestRequestsForTomorrow()
        {
            await _zahtevZaTestiranje.ScheduleTestsAsync();
        }
    }
}
