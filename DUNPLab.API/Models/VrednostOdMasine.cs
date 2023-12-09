using System.ComponentModel.DataAnnotations.Schema;

namespace DUNPLab.API.Models
{
    public class VrednostOdMasine
    {
        public int Id { get; set; }
        public int RezultatOdMasineId { get; set; }
        [ForeignKey("RezultatOdMasineId")]
        public RezultatOdMasine RezultatOdMasine { get; set; }
        public string OznakaSubstance { get; set; }
        public double Vrednost { get; set; }
        public bool JeLiBiloGreske { get; set; }
    }
}
