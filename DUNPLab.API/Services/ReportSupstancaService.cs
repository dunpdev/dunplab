using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.EntityFrameworkCore;
using System;
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
            var pacijenti = _context.Pacijenti.ToList();

            foreach (var pacijent in pacijenti)
            {
                var uzorci = _context.Uzorci
                    .Include(u => u.Rezultati).ThenInclude(r => r.Supstanca)
                    .Where(u => u.PacijentId == pacijent.Id)
                    .ToList();

                
                if (uzorci.Any(uzorak => uzorak.Rezultati.Any(rezultat => rezultat.JeLiUGranicama == true)))
                {
                    var putanjaDoPreuzimanja = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    var putanjaDoPdf = Path.Combine(putanjaDoPreuzimanja, "Downloads", $"Izvestaj_{pacijent.Ime}_{pacijent.Prezime}.pdf");

                    using (var fs = new FileStream(putanjaDoPdf, FileMode.Create))
                    {
                        var dokument = new Document();
                        var pisac = PdfWriter.GetInstance(dokument, fs);

                        dokument.Open();

                        dokument.Add(new Paragraph($"Izveštaj za pacijenta: {pacijent.Ime} {pacijent.Prezime}")
                        {
                            SpacingAfter = 15f
                        });

                        var supstanceCount = new Dictionary<Supstanca, int>();

                        foreach (var uzorak in uzorci)
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
                            table.AddCell($"{(double)positiveResults / uzorci.Count:P}");
                        }

                        dokument.Add(table);

                        dokument.Close();
                        pisac.Close();
                        fs.Close();

                        byte[] pdfBytes;
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var fileStream = new FileStream(putanjaDoPdf, FileMode.Open))
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
}
