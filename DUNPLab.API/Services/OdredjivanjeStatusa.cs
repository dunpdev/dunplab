using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Services
{
    public class OdredjivanjeStatusa : IOdredjivanjeStatusa
    {
        private readonly DunpContext context;
        private readonly ILogger<OdredjivanjeStatusa> logger;

        public OdredjivanjeStatusa(DunpContext context, ILogger<OdredjivanjeStatusa> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void Odredi()
        {
            List<Rezultat> rezultati = context.Rezultati
                    .Where(r => r.Obradjen == false)
                    .Include(r => r.Supstanca)
                    .Include(r => r.Uzorak)
                    .ToList();

            foreach (Rezultat rezultat in rezultati)
            {
                try
                {
                    if (rezultat.Supstanca == null)
                    {
                        throw new Exception($"Nedostaje supstanca! Dalja obrada rezultata {rezultat.Id} je otkazana.");
                    }

                    if (rezultat.Uzorak == null)
                    {
                        throw new Exception($"Nedostaje uzorak! Dalja obrada rezultata {rezultat.Id} je otkazana.");
                    }

                    if (rezultat.Vrednost > rezultat.Supstanca.GornjaGranica || rezultat.Vrednost < rezultat.Supstanca.DonjaGranica)
                    {
                        rezultat.Uzorak.KonacanRezultat = "pozitivan";
                    }
                    else if (rezultat.Vrednost < rezultat.Supstanca.GornjaGranica && rezultat.Vrednost > rezultat.Supstanca.DonjaGranica)
                    {
                        rezultat.Uzorak.KonacanRezultat = "negativan";
                    }
                    else if (rezultat.Vrednost == rezultat.Supstanca.GornjaGranica || rezultat.Vrednost == rezultat.Supstanca.DonjaGranica)
                    {
                        rezultat.Uzorak.KonacanRezultat = "sumnjiv";
                    }

                    logger.LogInformation("Rezultat je obradjen. Rezultat: " + rezultat.Uzorak.KonacanRezultat);

                    rezultat.Obradjen = true;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex.Message);
                }
            }
        }
    }
}
