namespace DUNPLab.API.Dto
{
    public class EmailNotificationDto
    {
        public string Message { get; set; }
        public DateTime TimeOfSending { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
    }
}
