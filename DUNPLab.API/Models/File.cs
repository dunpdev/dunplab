namespace DUNPLab.API.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public byte[] Sadrzaj { get; set; }
        public bool? JeLiObrisan { get; set; }
    }
}
