using Domain.Notification.Models;

namespace Domain.Notification
{
    public interface INotificationService
    {
        void SendCreateAccountNotification(CreateAccountNotificationMessage message);
    }
}