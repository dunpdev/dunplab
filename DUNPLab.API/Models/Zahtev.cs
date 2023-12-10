using System.ComponentModel.DataAnnotations.Schema;

namespace DUNPLab.API.Models
{
    public class Zahtev
    {
        public Zahtev()
        {
            Supstance = new HashSet<Supstanca>();
            Metode = new List<string>();
        }
        public int Id { get; set; }
        public DateTime? DatumTestiranja { get; set; } // samo datum bez vremena. Vreme se kasnije odredjuje na osnovu zauzetosti sala
        public IList<string> Metode { get; set; } // metode na koje bi trebao da se testira pacijent npr urin i krv
        public int? TestiranjeId { get; set; } // zahtev u sebi u pocetku nema nformaciju o testiranju, ali kada se testiranje kreira onda se ovde upisuje id testiranja
        [ForeignKey("TestiranjeId")]
        public Testiranje Testiranje { get; set; }
        public int? PacijentId { get; set; }
        [ForeignKey("PacijentId")]
        public Pacijent Pacijent { get; set; }
        public ICollection<Supstanca> Supstance { get; set; }
        public bool? JeLiObradjen { get; set; }
        public List<ZahtevSubstanca> ZahtevSupstance { get; set; }
    }
}
