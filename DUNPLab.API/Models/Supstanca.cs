namespace DUNPLab.API.Models
{
    public class Supstanca
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Oznaka { get; set; }
        public string Opis { get; set; }
        public string Tip { get; set; }
        public double? DonjaGranica { get; set; }
        public double? GornjaGranica { get; set; }
        public string MetodTestiranja { get; set; }
        public double Cena { get; set; }
        public List<ZahtevSubstanca> ZahtevSupstance { get; set; }
    }
}
