using AutoMapper;
using AutoMapper.QueryableExtensions;
using DUNPLab.API.DTOs;
using DUNPLab.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace DUNPLab.API.Services
{
    public class BackgroundJobsService : IBackgroundJobsService
    {
        private readonly DunpContext context;
        private readonly IMapper mapper;

        public BackgroundJobsService(DunpContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Rezultati()
        {
            //Console.WriteLine("Rezultati");
            //var unprocessedZahtevs = context.Zahtevi.Where(z => z.JeLiObradjen == false).Include(p => p.Pacijent).Include(z => z.ZahtevSupstance).ThenInclude(zs => zs.Supstanca).ToList();
            var unprocessedZahtevi = await context.Uzorci
                .ProjectTo<UzorakFileDTO>(mapper.ConfigurationProvider)
                .Where(u => u.JeLiObradjen == false)
                .ToListAsync();
            //var unprocessedZahtevi = context.Zahtevi
            //    .Include(z => z.Testiranje)
            //    .ThenInclude(t => t.Uzorci)
            //    .Include(z => z.ZahtevSupstance)
            //    .ThenInclude(zs => zs.Supstanca)
            //    .Include(z => z.Pacijent)
            //    .Where(z => z.JeLiObradjen == false)
            //    .ToList();

            foreach (var uzorak in unprocessedZahtevi)
            {
                var zahtev = context.Zahtevi.Find(uzorak.ZahtevId);
                if (zahtev == null) continue;
                var filePath = Path.Combine("wwwroot", "rezultati", $"{uzorak.ImePacijenta}{uzorak.PrezimePacijenta}{uzorak.KodEpruvete}.txt");
                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"{uzorak.ImePacijenta}{uzorak.PrezimePacijenta},{uzorak.KodEpruvete},{DateTime.Now:yyyyMMddHHmmss}");

                    foreach (var supstanca in uzorak.Supstance)
                    {
                        var random = new Random();
                        var vrednost = random.NextDouble() * supstanca.GornjaGranica * 1.1;
                        var jeLiBiloGreske = vrednost > supstanca.GornjaGranica ? "true" : "false";
                        //round the random num on 2 decimals
                        var pom = vrednost.ToString().Substring(0, 4);
                        writer.WriteLine($"{supstanca.Oznaka},{pom},{jeLiBiloGreske}");
                    }
                }

                zahtev.JeLiObradjen = true;
                context.Update(zahtev);
            }
            await context.SaveChangesAsync();
        }
    }

}
