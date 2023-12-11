using System.ComponentModel.DataAnnotations.Schema;

namespace DUNPLab.API.Models
{
    public class Report
    {
        public int Id { get; set; }
        public int idPacijent { get; set; }
        [ForeignKey("idPacijent")]
        public Pacijent Pacijent { get; set; }
        public int IdFile { get; set; }
        [ForeignKey("IdFile")]
        public File File { get; set; }
    }
}
