using EmailExchanging;
using MailSender.Configuration;
using MimeKit;

namespace MailSender.Services
{
    public class MailSenderService
    {
        private MailSettings _mailSettings;
        private SmtpClientService _client;

        public MailSenderService()
        {
            ConfigurationProvider configurationProvider = new ConfigurationProvider();
            _mailSettings = configurationProvider.GetMailSettings();
            _client = DiContainer.GetService<SmtpClientService>();
        }
        public void SendEmail(EmailInputModel _emailInputModel)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress("", _emailInputModel.Email));
            emailMessage.Subject = _emailInputModel.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = _emailInputModel.Message
            };

            _client.SendMessage(emailMessage);
        }
    }
}