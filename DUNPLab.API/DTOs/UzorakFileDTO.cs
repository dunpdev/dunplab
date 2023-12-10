using DUNPLab.API.Models;

namespace DUNPLab.API.DTOs
{
    public class UzorakFileDTO
    {
        public int ZahtevId { get; set; }
        public string ImePacijenta { get; set; }
        public string PrezimePacijenta { get; set; }
        public string KodEpruvete { get; set; }
        public DateTime DatumKreiranjaFajla { get; set; } = DateTime.UtcNow;
        public bool? JeLiObradjen { get; set; }
        public ICollection<SupstancaFileDTO> Supstance { get; set; }
    }
}
