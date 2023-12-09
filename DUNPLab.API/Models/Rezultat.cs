using System.ComponentModel.DataAnnotations.Schema;

namespace DUNPLab.API.Models
{
    public class Rezultat
    {
        public int Id { get; set; }
        public bool? JeLiUGranicama { get; set; } // Da li je rezultat u granicama ili nije
        public double? Vrednost { get; set; } // Vrednost rezultata dobijena od masine
        public int IdUzorka { get; set; }
        [ForeignKey("IdUzorka")]
        public Uzorak Uzorak { get; set; } // Uzorak za koji je rezultat
        public int IdSupstance { get; set; }
        [ForeignKey("IdSupstance")]
        public Supstanca Supstanca { get; set; } // Supstanca za koju je rezultat

        public bool Obradjen { get; set; } = false; // Da li je rezultat obradjen    
    }
}
