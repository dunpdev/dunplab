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
            var unprocessedZahtevi = await context.Uzorci
                .ProjectTo<UzorakFileDTO>(mapper.ConfigurationProvider)
                .Where(u => u.JeLiObradjen == false)
                .ToListAsync();

            foreach (var uzorak in unprocessedZahtevi)
            {
                var zahtev = context.Zahtevi.Find(uzorak.ZahtevId);
                if (zahtev == null) continue;
                var filePath = Path.Combine("wwwroot", "rezultati", $"{uzorak.KodEpruvete}.txt");
                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"{uzorak.ImePacijenta}{uzorak.PrezimePacijenta},{uzorak.KodEpruvete},{DateTime.Now:yyyyMMddHHmmss}");

                    foreach (var supstanca in uzorak.Supstance)
                    {
                        var random = new Random();
                        var vrednost = random.NextDouble() * supstanca.GornjaGranica * 1.1;
                        var jeLiBiloGreske = vrednost > supstanca.GornjaGranica ? "true" : "false";
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
