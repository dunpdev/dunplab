using System.Runtime.ConstrainedExecution;

namespace DUNPLab.API.Models
{
    public class Testiranje
    {
        public Testiranje() {
            Uzorci = new HashSet<Uzorak>();
            //ako je bar jedan uzorak los onda je ceo rezultat los i treba alarmirati pacijenta da proveri stanje kod doktora
        }
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int ZahtevId { get; set; }
        public Zahtev Zahtev { get; set; }
        public double UkupnaCena { get; set; } // ukupna cena svih uzoraka
        public string NacinPlacanja { get; set; } // kartica, kes, cek, osiguranje
        public string TestOdradio { get; set; } // Osoba koja je uzela uzorak
        public bool? JesuLiPotvrdjeniSviUzorci { get; set; } // da li je zavrsena obrada svih uzorka i potvrdjeni rezultati
        public DateTime? DatumVremeTestiranja { get; set; } // datum i vreme kada je uzet uzorak
        public DateTime? DatumVremeRezultata { get; set; } // datum i vreme kada je rezultat gotov
        public string Status { get; set; } // u toku, zavrseno, otkazano zbog problema sa uzorkom
        public string BrojSobe { get; set; } // broj sobe u kojoj je uzet uzorak
        public string Izmenio { get; set; } // osoba koja je izmenila podatke
        public DateTime? IzmenioDatumVreme { get; set; } // datum i vreme kada su podaci izmenjeni
        public ICollection<Uzorak> Uzorci { get; set; } // uzorci koji su uzeti za testiranje
    }
}
