using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DUNPLab.API.Services
{
    public class ReportSupstancaService : IReportSupstancaService
    {
        private readonly DunpContext _context;

        public ReportSupstancaService(DunpContext context)
        {
            _context = context;
        }

        public void GeneratePdfReport()
        {
            var pacijentiSaRezultatima = _context.Pacijenti
                .Where(p => _context.Zahtevi
                    .Include(z => z.Testiranje.Uzorci)
                    .ThenInclude(u => u.Rezultati)
                    .Any(z => z.PacijentId == p.Id && z.Testiranje.Uzorci.Any(u => u.Rezultati.Any())))
                .ToList();

            foreach (var pacijent in pacijentiSaRezultatima)
            {
                var pdfPath = Path.Combine("C:\\Users\\Dzejlana\\Downloads", $"Izvestaj_{pacijent.Ime}_{pacijent.Prezime}.pdf");

                using (var fs = new FileStream(pdfPath, FileMode.Create))
                {
                    var document = new Document();
                    var writer = PdfWriter.GetInstance(document, fs);

                    document.Open();

                    document.Add(new Paragraph($"Izveštaj za pacijenta: {pacijent.Ime} {pacijent.Prezime}")
                    {
                        SpacingAfter = 15f
                    });

                    var zahtevi = _context.Zahtevi
                        .Include(z => z.Testiranje.Uzorci).ThenInclude(u => u.Rezultati).ThenInclude(r => r.Supstanca)
                        .Include(z => z.Pacijent)
                        .Where(z => z.PacijentId == pacijent.Id)
                        .ToList();

                    var supstanceCount = new Dictionary<Supstanca, int>();

                    foreach (var zahtev in zahtevi)
                    {
                        foreach (var uzorak in zahtev.Testiranje.Uzorci)
                        {
                            foreach (var rezultat in uzorak.Rezultati)
                            {
                                Console.WriteLine($"Supstanca: {rezultat.Supstanca.Naziv}, JeLiUGranicama: {rezultat.JeLiUGranicama}");

                                if (rezultat.JeLiUGranicama == true)
                                {
                                    if (!supstanceCount.ContainsKey(rezultat.Supstanca))
                                    {
                                        supstanceCount[rezultat.Supstanca] = 1;
                                    }
                                    else
                                    {
                                        supstanceCount[rezultat.Supstanca]++;
                                    }
                                }
                            }
                        }
                    }

                    var sortedSupstance = supstanceCount.OrderByDescending(kv => kv.Value);

                    var table = new PdfPTable(3);
                    table.AddCell("Naziv supstance");
                    table.AddCell("Broj pozitivnih rezultata");
                    table.AddCell("Procentualno");

                    foreach (var kvp in sortedSupstance)
                    {
                        var supstanca = kvp.Key;
                        var positiveResults = kvp.Value;

                        table.AddCell(supstanca.Naziv);
                        table.AddCell(positiveResults.ToString());
                        table.AddCell($"{(double)positiveResults / zahtevi.Count:P}");
                    }

                    document.Add(table);

                    document.Close();
                    writer.Close();
                    fs.Close();

                    byte[] pdfBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var fileStream = new FileStream(pdfPath, FileMode.Open))
                        {
                            fileStream.CopyTo(memoryStream);
                        }
                        pdfBytes = memoryStream.ToArray();
                    }

                    var file = new Models.File
                    {
                        Ime = $"Izvestaj_{pacijent.Ime}_{pacijent.Prezime}.pdf",
                        Sadrzaj = pdfBytes,
                        JeLiObrisan = false
                    };

                    _context.Files.Add(file);
                    _context.SaveChanges();

                    var report = new Models.Report
                    {
                        idPacijent = pacijent.Id,
                        IdFile = file.Id
                    };

                    _context.Reports.Add(report);
                    _context.SaveChanges();
                }
            }
        }
    }
}
