using System.ComponentModel.DataAnnotations.Schema;

namespace DUNPLab.API.Models
{
    public class ZahtevSubstanca
    {
        public int Id { get; set; }
        public int? ZahtevId { get; set; }
        [ForeignKey("ZahtevId")]
        public Zahtev Zahtev { get; set; }
        public int? SubstancaId { get; set; }
        [ForeignKey("SubstancaId")]
        public Supstanca Supstanca { get; set; }
    }
}
