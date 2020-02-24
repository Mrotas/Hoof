namespace Domain.Notification.Models
{
    public class CreateAccountNotificationMessage
    {
        public string EmailTo { get; set; }
        public string ActivationLink { get; set; }
    }
}
