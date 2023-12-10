﻿using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using iTextSharp.text.pdf;
using System.Reflection.Metadata;

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
                var pdfPath = Path.Combine("C:\\Users\\edina\\Downloads", $"Izvestaj_{pacijent.Ime}_{pacijent.Prezime}.pdf");

                using (var fs = new FileStream(pdfPath, FileMode.Create))
                {
                    var document = new Document();
                    var writer = PdfWriter.GetInstance(document, fs);

                    document.Open();

                    document.Add(new Paragraph($"Izveštaj za pacijenta: {pacijent.Ime} {pacijent.Prezime}"));

                    var uzorci = _context.Uzorci
                        .Include(u => u.Rezultati).ThenInclude(r => r.Supstanca)
                        .Where(u => u.PacijentId == pacijent.Id)
                        .ToList();

                    var supstanceCount = new Dictionary<Supstanca, int>();

                    foreach (var uzorak in uzorci)
                    {
                        document.Add(new Paragraph($"Uzorak: {uzorak.Naziv}"));

                        var resultTable = new PdfPTable(4);
                        resultTable.AddCell("Supstanca");
                        resultTable.AddCell("Rezultat");
                        resultTable.AddCell("Da li je u granicama");
                        resultTable.AddCell("Komentar");

                        foreach (var rezultat in uzorak.Rezultati)
                        {
                            Console.WriteLine($"Supstanca: {rezultat.Supstanca.Naziv}, JeLiUGranicama: {rezultat.JeLiUGranicama}");

                            resultTable.AddCell(rezultat.Supstanca.Naziv);

                            resultTable.AddCell(rezultat.JeLiUGranicama.HasValue ? (rezultat.JeLiUGranicama.Value ? "Da" : "Ne") : "N/A");

                            // Dodaj ovo za prikupljanje broja pozitivnih rezultata
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

                        document.Add(resultTable);
                    }

                    var sortedSupstance = supstanceCount.OrderByDescending(kv => kv.Value);

                    // Dodajte tabelu sa podacima o supstancama
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
                        PacijentId = pacijent.Id,
                        FileId = file.Id
                    };
                    _context.Reports.Add(report);
                    _context.SaveChanges();
                }
            }
        }
    }
    
}
