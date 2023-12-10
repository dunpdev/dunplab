using DUNPLab.API.Services.Pacijenti;
using Hangfire;

namespace DUNPLab.API.Jobs.PacijentiJobs
{
    public class PacijentiJobRegistrator : IPacijentiJobRegistrator
    {
        private readonly IRecurringJobManager _recurringJobManager;
        public IPacijentiService  _pacijentService { get; set; }
        public PacijentiJobRegistrator(IRecurringJobManager reccuringJobManager, IPacijentiService pacijentService)
        {
            _recurringJobManager = reccuringJobManager; 
           _pacijentService=pacijentService;
        }

        public void Register()
        {
            _recurringJobManager.AddOrUpdate("VahidovJob",()=>_pacijentService.Seed(), "0 0 * * 0");
        }
    }
}
