using DUNPLab.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Jobs
{
    public class ProcessedRequestRemover
    {
        private readonly ILogger<ProcessedRequestRemover> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly DunpContext _context;

        public ProcessedRequestRemover(ILogger<ProcessedRequestRemover> logger, IWebHostEnvironment env, DunpContext context)
        {
            _logger = logger;
            _env = env;
            _context = context;
        }

        public void RemoveProcessedRequests()
        {
            try
            {
                var zahtevi = _context.Zahtevi
                    .Where(z => z.JeLiObradjen == true && z.JeLiObradjen.HasValue);

                var zahteviIds = zahtevi.Select(z => z.Id);
                string wwwrootPath = _env.WebRootPath;
                string resultsPath = Path.Combine(wwwrootPath, "zahtevi");
                var files = Directory.GetFiles(resultsPath, "*.txt");
                var filesToDelete = new List<string>();

                foreach (var file in files)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);

                    if (int.TryParse(fileName, out int id))
                    {
                        if (zahteviIds.Contains(id))
                        {
                            filesToDelete.Add(file);
                        }
                    }
                }

                foreach (var file in filesToDelete)
                {
                    File.Delete(file);
                }

                _logger.LogInformation($"Deleted {filesToDelete.Count} files");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while removing processed requests: {ex.Message}");
            }
        }
    }
}
