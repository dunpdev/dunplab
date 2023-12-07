using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Services
{
    public class TransferRezultati : ITransferRezultati
    {
        private readonly DunpContext context;
        private readonly ILogger<TransferRezultati> logger;

        public TransferRezultati(DunpContext context,
            ILogger<TransferRezultati> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public void Transfer()
        {
            logger.LogInformation("Pocinjanje transfera. " + DateTime.UtcNow);
            List<RezultatOdMasine> rezultatiOdMasine = context.RezultatiOdMasine
                .Include(rm => rm.VrednostiOdMasine)
                .Where(rm => rm.JesuLiPrebaceni == false)
                .ToList();

            bool error = false;

            List<Rezultat> rezultatiZaDodavanje = new List<Rezultat>();

            foreach (RezultatOdMasine rm in rezultatiOdMasine)
            {
                error = false;
                foreach (VrednostOdMasine sr in rm.VrednostiOdMasine)
                {
                    try
                    {
                        Rezultat rezultat = new Rezultat();

                        rezultat.JeLiUGranicama = sr.JeLiBiloGreske;
                        rezultat.Vrednost = sr.Vrednost;

                        rezultat.Uzorak = context.Uzorci
                            .Where(u => u.KodEpruvete == rm.KodEpruvete)
                            .FirstOrDefault();

                        if (rezultat.Uzorak == null)
                        {
                            throw new Exception("Ne postoji uzorak sa datim kodom epruvete: " + rm.KodEpruvete);
                        }

                        rezultat.Supstanca = context.Supstance
                            .Where(s => s.Oznaka == sr.OznakaSubstance)
                            .FirstOrDefault();

                        if (rezultat.Supstanca == null)
                        {
                            throw new Exception("Ne postoji supstanca sa oznakom: " + sr.OznakaSubstance);
                        }

                        rezultat.IdUzorka = rezultat.Uzorak.Id;
                        rezultat.Uzorak.IzmenioDatumVreme = rm.DatumVreme;

                        rezultat.IdSupstance = sr.Id;

                        //context.Rezultati.Add(rezultat);
                        rezultatiZaDodavanje.Add(rezultat);
                    }
                    catch (Exception ex)
                    {
                        logger.LogWarning(ex.Message);
                        logger.LogWarning($"Zbog nedostatka podataka transfer rezulatata {rm.Id} je odlozen.");
                        error = true;
                        continue;
                    }
                }

                if (!error)
                {
                    rm.JesuLiPrebaceni = true;
                    context.Rezultati.AddRange(rezultatiZaDodavanje);
                }

                rezultatiZaDodavanje.Clear();
            }

            context.SaveChanges();

            logger.LogInformation("Transfer zavrsen. " + DateTime.UtcNow);
        }
    }
}
