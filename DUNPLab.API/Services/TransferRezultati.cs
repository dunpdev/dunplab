using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Globalization;

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
            logger.LogInformation("Beginning transfer. " + DateTime.UtcNow);
            List<RezultatOdMasine> rezultatiOdMasine = context.RezultatiOdMasine
                .Include(rm => rm.VrednostiOdMasine)
                .Where(rm => rm.JesuLiPrebaceni == false)
                .ToList();

            foreach (RezultatOdMasine rm in rezultatiOdMasine)
            {
                foreach (VrednostOdMasine sr in rm.VrednostiOdMasine)
                {
                    Rezultat rezultat = new Rezultat();

                    rezultat.JeLiUGranicama = sr.JeLiBiloGreske;
                    rezultat.Vrednost = sr.Vrednost;
                    rezultat.Uzorak = context.Uzorci.Where(u => u.KodEpruvete == rm.KodEpruvete).FirstOrDefault();
                    if (rezultat.Uzorak == null)
                    {
                        throw new Exception("Ne postoji uzorak sa kodom epruvete: " +  rm.KodEpruvete);
                    }
                    rezultat.IdUzorka = rezultat.Uzorak.Id;
                    rezultat.Uzorak.IzmenioDatumVreme = rm.DatumVreme;
                    rezultat.Supstanca = context.Supstance
                        .Where(s => s.Oznaka == sr.OznakaSubstance)
                        .FirstOrDefault();
                    if (rezultat.Supstanca == null)
                    {
                        throw new Exception("Ne postoji supstanca sa oznakom: " + sr.OznakaSubstance);
                    }
                    rezultat.IdSupstance = sr.Id;
                    context.Rezultati.Add(rezultat);
                }

                rm.JesuLiPrebaceni = true;
            }

            context.SaveChanges();

            logger.LogInformation("Transfer ended. " + DateTime.UtcNow);
        }
    }
}
