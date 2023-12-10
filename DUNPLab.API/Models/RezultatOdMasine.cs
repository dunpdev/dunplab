using System.ComponentModel.DataAnnotations.Schema;

﻿namespace DUNPLab.API.Models
{
    public class RezultatOdMasine
    {
        public int Id { get; set; }
        public string ImeIPrezime { get; set; }
        public string KodEpruvete { get; set; }
        public DateTime DatumVreme { get; set; }
        public bool JesuLiPrebaceni { get; set; } = false;
        public ICollection<VrednostOdMasine> VrednostiOdMasine { get; set; }
    }
}
