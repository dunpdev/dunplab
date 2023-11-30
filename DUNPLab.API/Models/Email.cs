using System.ComponentModel.DataAnnotations.Schema;

namespace DUNPLab.API.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public DateTime? Sent { get; set; }
        public int IdFile { get; set; }
        [ForeignKey("IdFile")]
        public File File { get; set; }
    }
}
