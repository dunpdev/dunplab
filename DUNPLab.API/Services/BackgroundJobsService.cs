using DUNPLab.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace DUNPLab.API.Services
{
    public class BackgroundJobsService : IBackgroundJobsService
    {
        private readonly DunpContext context;
        public BackgroundJobsService(DunpContext context)
        {
            this.context = context;
        }

        public async Task Rezultati()
        {
            //Console.WriteLine("Rezultati");
            //var unprocessedZahtevs = context.Zahtevi.Where(z => z.JeLiObradjen == false).Include(p => p.Pacijent).Include(z => z.ZahtevSupstance).ThenInclude(zs => zs.Supstanca).ToList();
            var unprocessedZahtevi = context.Uzorci
                .Include(u => u.Testiranje)
                .ThenInclude(t => t.Zahtev)
                .Where(z => z.Testiranje.Zahtev.JeLiObradjen == false)
                .ToList();
            //var unprocessedZahtevi = context.Zahtevi
            //    .Include(z => z.Testiranje)
            //    .ThenInclude(t => t.Uzorci)
            //    .Include(z => z.ZahtevSupstance)
            //    .ThenInclude(zs => zs.Supstanca)
            //    .Include(z => z.Pacijent)
            //    .Where(z => z.JeLiObradjen == false)
            //    .ToList();
            if (unprocessedZahtevi.Count != 0)
            {
                var zahtevi = unprocessedZahtevi.AsQueryable().Include(z => z.Testiranje.Zahtev.ZahtevSupstance).ThenInclude(zs => zs.Supstanca).ToList();

                foreach (var uzorak in unprocessedZahtevi)
                {
                    var filePath = Path.Combine("wwwroot", "rezultati", $"{uzorak.Testiranje.Zahtev.Pacijent.Ime}{uzorak.Testiranje.Zahtev.Pacijent.Ime}{uzorak.KodEpruvete}.txt");
                    using (var writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine($"{uzorak.Testiranje.Zahtev.Pacijent.Ime}{uzorak.Testiranje.Zahtev.Pacijent.Prezime},{uzorak.Testiranje.Zahtev.Id},{DateTime.Now:yyyyMMddHHmmss}");

                        foreach (var supstanca in uzorak.Testiranje.Zahtev.ZahtevSupstance)
                        {
                            var random = new Random();
                            var vrednost = random.NextDouble() * supstanca.Supstanca.GornjaGranica * 1.1;
                            var jeLiBiloGreske = vrednost > supstanca.Supstanca.GornjaGranica ? "true" : "false";
                            writer.WriteLine($"{supstanca.Supstanca.Oznaka},{vrednost},{jeLiBiloGreske}");
                        }
                    }

                    uzorak.Testiranje.Zahtev.JeLiObradjen = true;
                    context.Update(uzorak.Testiranje.Zahtev);
                    await context.SaveChangesAsync();
                }
            }
        }
    }

}
