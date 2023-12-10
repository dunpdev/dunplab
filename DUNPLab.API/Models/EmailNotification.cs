namespace DUNPLab.API.Models
{
    public class EmailNotification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime TimeOfSending { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }

    }
}
