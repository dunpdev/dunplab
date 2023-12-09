using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Services
{
    public class ArhivirajPacijenteService : IArhivirajPacijenteService
    {
        private readonly DunpContext _context;
        public ArhivirajPacijenteService(DunpContext dunpContext)
        {
            _context = dunpContext;
        }
        public async Task ArhivirajPacijente()
        {
            Console.WriteLine("Arhiviranje...");
            var godinuDanaUnazad = DateTime.Now.AddYears(-1);

            var pacijentiZaArhiviranje = _context.Pacijenti.Where(p => !p.DaLiJeArhiviran &&
                    (!p.Zahtevi.Any() || p.Zahtevi.All(z => z.DatumTestiranja <= godinuDanaUnazad && z.JeLiObradjen == true))).ToList();

            foreach (var pacijent in pacijentiZaArhiviranje)
            {
                pacijent.DaLiJeArhiviran = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
