using MailKit.Net.Smtp;
using MailSender.Configuration;
using MimeKit;
using System;

namespace MailSender.Services
{
    public class SmtpClientService
    {
        private SmtpClient _client;
        private MailSettings _mailSettings;
        public SmtpClientService(ConfigurationProvider configurationProvider)
        {
            _mailSettings = configurationProvider.GetMailSettings();
            _client = new SmtpClient();
            _client.Connect(_mailSettings.SmtpServer, _mailSettings.Port, false);
            Console.WriteLine("connected");
            _client.Authenticate(_mailSettings.SenderEmail, _mailSettings.Password);
            Console.WriteLine("Authenticated");
        }
        public void SendMessage(MimeMessage message)
        {
            Console.WriteLine($"Subject: { message.Subject}; To: {message.To}" );           
            _client.Send(message);

        }
        public void CloseSmtpConnection()
        {
            _client.Disconnect(true);
        }
    }
}
