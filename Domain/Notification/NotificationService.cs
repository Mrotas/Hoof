using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using Domain.Notification.Models;

namespace Domain.Notification
{
    public class NotificationService : INotificationService
    {
        public void SendCreateAccountNotification(CreateAccountNotificationMessage message)
        {
            string subject = "Aktywacja konta w systemie Koła Łowieckiego Literatów Polskich nr 39";

            var body = new StringBuilder();
            body.Append("Wiadomość o utworzenia konta w systemie Koła Łowieckiego Literatów Polskich nr 39.").AppendLine().AppendLine();
            body.Append("Aby aktywować konto w systemie proszę otworzyć poniższy link: ").AppendLine();
            body.Append(message.ActivationLink);

            string fromMail = ConfigurationManager.AppSettings["MailUser"];
            string fromPassword = ConfigurationManager.AppSettings["MailPassword"];
            SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            var mailmessage = new MailMessage
            {
                From = new MailAddress(fromMail, "Koło Literatów Polskich nr 39"),
                Subject = subject,
                Body = body.ToString()
            };
            mailmessage.To.Add(message.EmailTo);

            client.Send(mailmessage);
        }
    }
}
