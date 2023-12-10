using System.ComponentModel.DataAnnotations.Schema;

namespace DUNPLab.API.Models
{
    public class Uzorak
    {
        public Uzorak()
        {
            Rezultati = new HashSet<Rezultat>();
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string KodEpruvete { get; set; } // Jedinstveni kod za Uzorak (ovo je potrebno zbog dobijanja rezultata od mascine
        public string MetodTestiranja { get; set; } // test iz krvi, urin, pljuvacka, stolica
        public string KonacanRezultat { get; set; } // pozitivan, negativan, sumnjiv. Ako je bar jedna supstanca van opsega onda je pozitivan, ako je na granici sumknjiv i ako nema ni jednih ni drugih onda je negativan tj pacijent je u redu za taj uzorak
        public string Komentar { get; set; } // Uzorak moze sadrzati neki dodatni komentar recimo "Danas nam masine nisu davale bas najbolje rezultate za CRP, moguce da je kvar"
        public double? Cena { get; set; } // Ukupna cena svih supstanci
        public string Kutija { get; set; } // Kutija u kojem se nalazi epruveta formira se na sledeci nacin BXggggmmddxxxxxx gggg - godina, mm - mesec, dd - dan, xxxxxx - redom brojevi za taj dan od 000001 do 999999
        public int IdTestiranja { get; set; }
        [ForeignKey("IdTestiranja")]
        public Testiranje Testiranje { get; set; }
        public ICollection<Rezultat> Rezultati { get; set; }
        public string Izmenio { get; set; } // osoba koja je izmenila podatke
        public DateTime? IzmenioDatumVreme { get; set; } // datum i vreme kada su podaci izmenjeni

        
        public int PacijentId { get; set; }
        [ForeignKey("PacijentId")]
        public Pacijent Pacijent { get; set; }
    }
}
