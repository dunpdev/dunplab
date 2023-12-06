using DUNPLab.API.Infrastructure;

namespace DUNPLab.API.Jobs
{
    public class ProcessedFilesRemoverJob
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly DunpContext _context;

        public ProcessedFilesRemoverJob(IWebHostEnvironment hostingEnvironment, DunpContext context)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._context = context;
        }

        public void DeleteProcessedResults()
        {
            var processedFilesIds = _context.RezultatiOdMasine.Select(rom => rom.KodEpruvete);

            string wwwrootPath = _hostingEnvironment.WebRootPath;
            string resultsPath = Path.Combine(wwwrootPath, "rezultati");
            var files = Directory.GetFiles(resultsPath, "*.txt");

            foreach (var file in files)
            {
                var allData = System.IO.File.ReadAllLines(file);
                var resultMainData = allData[0].Split(',');
                var kodEpruvete = resultMainData[1];

                if (processedFilesIds.Contains(kodEpruvete))
                    System.IO.File.Delete(file);
            }
        }
    }
}
