namespace DUNPLab.API.Models
{
    public class Pacijent
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string Telefon { get; set; }
        public string Pol { get; set; }
        public string JMBG { get; set; }
        public string BrojDokumenta { get; set; }
        public DateTime? DatumIstekaDokumenta { get; set; }
        public bool DaLiJeArhiviran { get; set; } = false;
        public ICollection<Zahtev> Zahtevi { get; set; }
    }
}
