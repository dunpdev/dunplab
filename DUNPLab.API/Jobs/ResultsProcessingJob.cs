using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;

namespace DUNPLab.API.Jobs
{
    public class ResultsProcessingJob
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly DunpContext _context;

        public ResultsProcessingJob(IWebHostEnvironment hostingEnvironment, DunpContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public void ProcessResults()
        {
            string wwwrootPath = _hostingEnvironment.WebRootPath;
            string resultsPath = Path.Combine(wwwrootPath, "rezultati");
            var files = Directory.GetFiles(resultsPath, "*.txt");

            foreach (var file in files)
            {
                var allData = System.IO.File.ReadAllLines(file);
                var resultMainData = allData[0].Split(',');
                var testerFullName = resultMainData[0];
                var kodEpruvete = resultMainData[1];
                var date = resultMainData[2]; // datum u formatu yyyyMMddhhmmss (tako pise u zadatku)
                var substancesResults = allData.Skip(1).ToArray();

                var rezultati = new RezultatOdMasine
                {
                    ImeIPrezime = testerFullName,
                    KodEpruvete = kodEpruvete,
                    DatumVreme = DateTime.ParseExact(date, "yyyyMMddHHmmss", null),
                };
                var vrednostiOdMasine = new List<VrednostOdMasine>();
                foreach (var line in substancesResults)
                {
                    var substanceData = line.Split(',');
                    var substanceLabel = substanceData[0];
                    var substanceValue = substanceData[1];
                    var substanceError = substanceData[2];
                    vrednostiOdMasine.Add(new VrednostOdMasine
                    {
                        OznakaSubstance = substanceLabel,
                        Vrednost = double.Parse(substanceValue),
                        JeLiBiloGreske = bool.Parse(substanceError)
                    });
                }
                rezultati.VrednostiOdMasine = vrednostiOdMasine;
                _context.RezultatiOdMasine.Add(rezultati);

                _context.SaveChanges();
            }
        }
    }
}
